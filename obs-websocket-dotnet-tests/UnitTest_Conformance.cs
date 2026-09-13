using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Reflection-based gates that fail the build if OBSWebsocket drifts from IOBSWebsocket or from
    /// the obs-websocket protocol, instead of relying on a human to notice during review.
    /// </summary>
    [TestClass]
    public class UnitTest_Conformance_InterfaceParity : OBSWebsocket
    {
        [TestMethod]
        public void AllPublicMethods_HaveMatchingInterfaceMembers()
        {
            var classMethods = typeof(OBSWebsocket)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName);

            var interfaceSignatures = typeof(IOBSWebsocket)
                .GetMethods()
                .Select(SignatureKey)
                .ToHashSet();

            var missing = classMethods
                .Where(m => !interfaceSignatures.Contains(SignatureKey(m)))
                .Select(Describe)
                .ToList();

            Assert.IsTrue(missing.Count == 0,
                "Methods on OBSWebsocket missing from IOBSWebsocket:\n" + string.Join("\n", missing));
        }

        [TestMethod]
        public void AllPublicEvents_HaveMatchingInterfaceMembers()
        {
            // UnsupportedEvent is intentionally internal-only escape hatch, not part of the public contract.
            var allowlist = new HashSet<string> { nameof(UnsupportedEvent) };

            var classEvents = typeof(OBSWebsocket)
                .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(e => !allowlist.Contains(e.Name));

            var interfaceEvents = typeof(IOBSWebsocket)
                .GetEvents()
                .ToDictionary(e => e.Name, e => e.EventHandlerType);

            var missing = classEvents
                .Where(e => !interfaceEvents.TryGetValue(e.Name, out var handlerType) || handlerType != e.EventHandlerType)
                .Select(e => e.Name)
                .ToList();

            Assert.IsTrue(missing.Count == 0,
                "Events on OBSWebsocket missing/mismatched on IOBSWebsocket:\n" + string.Join("\n", missing));
        }

        private static string SignatureKey(MethodInfo m) =>
            $"{m.Name}({string.Join(",", m.GetParameters().Select(p => p.ParameterType.FullName))})";

        private static string Describe(MethodInfo m) =>
            $"{m.ReturnType.Name} {m.Name}({string.Join(", ", m.GetParameters().Select(p => p.ParameterType.Name))})";
    }

    [TestClass]
    public class UnitTest_Conformance_EventDispatch : OBSWebsocket
    {
        [TestMethod]
        public void AllEvents_AreDispatchedByProcessEventType()
        {
            // Transport-level events, never routed through ProcessEventType/eventType switch.
            var excluded = new HashSet<string> { nameof(UnsupportedEvent), nameof(Connected), nameof(Disconnected) };

            var events = typeof(OBSWebsocket)
                .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(e => !excluded.Contains(e.Name));

            var fellThrough = new List<string>();
            foreach (var evt in events)
            {
                bool didFallThrough = false;
                EventHandler<Types.Events.UnsupportedEventArgs> handler = (s, e) => didFallThrough = true;
                UnsupportedEvent += handler;
                try
                {
                    // An empty synthetic body may fail to build a concrete EventArgs (NRE/InvalidCast) -
                    // that's expected and irrelevant here; we only care whether dispatch reached `default:`.
                    ProcessEventType(evt.Name, new JObject { ["eventData"] = new JObject() });
                }
                catch
                {
                }
                finally
                {
                    UnsupportedEvent -= handler;
                }

                if (didFallThrough)
                {
                    fellThrough.Add(evt.Name);
                }
            }

            Assert.IsTrue(fellThrough.Count == 0,
                "Events with no case in ProcessEventType (fell through to UnsupportedEvent):\n" + string.Join("\n", fellThrough));
        }

        [TestMethod]
        public void AllEventArgsTypes_HaveADeclaredEvent()
        {
            // The generic catch-all payload type, not tied to any single declared event.
            var allowlist = new HashSet<string> { nameof(Types.Events.UnsupportedEventArgs) };

            var eventArgsTypes = typeof(Types.Events.UnsupportedEventArgs).Assembly
                .GetTypes()
                .Where(t => t.Namespace == "OBSWebsocketDotNet.Types.Events" && t.Name.EndsWith("EventArgs"))
                .Where(t => !allowlist.Contains(t.Name));

            var declaredEventArgsTypes = typeof(OBSWebsocket)
                .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(e => e.EventHandlerType.IsGenericType)
                .Select(e => e.EventHandlerType.GetGenericArguments()[0])
                .ToHashSet();

            var orphans = eventArgsTypes
                .Where(t => !declaredEventArgsTypes.Contains(t))
                .Select(t => t.Name)
                .ToList();

            Assert.IsTrue(orphans.Count == 0,
                "EventArgs types with no declared event referencing them:\n" + string.Join("\n", orphans));
        }
    }

    [TestClass]
    public class UnitTest_Conformance_RequestNaming : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(60000)]
        public void AllRequestMethods_UseMethodNameAsRequestType()
        {
            Arrange();

            // Not obs-websocket requests (connection lifecycle, or the SendRequest primitive itself),
            // or convenience wrappers that intentionally reuse another request under a different name:
            // GetAuthInfo -> "GetAuthRequired" (pre-dates the nameof(...) convention), ListScenes ->
            // GetSceneList, GetCurrentSceneCollection -> GetSceneCollectionList, GetSceneItemTransformRaw
            // -> GetSceneItemTransform (same request, different response mapping).
            var skip = new HashSet<string>
            {
                nameof(Connect), nameof(ConnectAsync), nameof(Disconnect),
                nameof(SendRequest), nameof(GetAuthInfo),
                nameof(ListScenes), nameof(GetCurrentSceneCollection), nameof(GetSceneItemTransformRaw)
            };

            var methods = typeof(OBSWebsocket)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName)
                .Where(m => !skip.Contains(m.Name))
                .Where(m => m.GetParameters().All(p => !p.ParameterType.IsByRef));

            var failures = new List<string>();
            foreach (var method in methods)
            {
                int before = Server.Requests.Count;
                var args = method.GetParameters().Select(BuildDefaultArg).ToArray();

                try
                {
                    method.Invoke(this, args);
                }
                catch
                {
                    // Either pre-send validation/serialization threw, or post-send response mapping
                    // threw against our default empty response - both irrelevant to this gate.
                }

                var requests = Server.Requests;
                if (requests.Count > before)
                {
                    string actualType = (string)requests[requests.Count - 1]["requestType"];
                    if (actualType != method.Name)
                    {
                        failures.Add($"{method.Name} sent requestType '{actualType}'");
                    }
                }
            }

            Assert.IsTrue(failures.Count == 0,
                "Methods whose wire requestType does not match their C# method name:\n" + string.Join("\n", failures));
        }

        private static object BuildDefaultArg(ParameterInfo p)
        {
            if (p.HasDefaultValue && !(p.DefaultValue is DBNull))
            {
                return p.DefaultValue;
            }

            return p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null;
        }
    }
}
