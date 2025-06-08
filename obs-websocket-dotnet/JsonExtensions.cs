#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// 提供 JSON 序列化的擴展方法，類似於 Newtonsoft.Json 的 PopulateObject 功能
    /// </summary>
    public static partial class JsonExtensions
    {
        /// <summary>
        /// 使用指定的 JSON 內容和序列化上下文來填充現有物件的屬性
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 字串</param>
        /// <param name="value">要被填充的現有物件</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <exception cref="ArgumentNullException">當 context 為 null 時拋出</exception>
        public static void PopulateObject<T>(ReadOnlySpan<char> json, T value, JsonSerializerContext context) where T : class
        {
            ArgumentNullException.ThrowIfNull(context);
            PopulateObject(json, value, context, t => context.GetTypeInfo(t));
        }

        /// <summary>
        /// 使用指定的 JSON 字串和序列化上下文來填充現有物件的屬性
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 字串</param>
        /// <param name="value">要被填充的現有物件</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <exception cref="ArgumentNullException">當 context 為 null 時拋出</exception>
        public static void PopulateObject<T>(string json, T value, JsonSerializerContext context) where T : class
        {
            PopulateObject(json.AsSpan(), value, context);
        }        /// <summary>
        /// 使用指定的 JSON 內容和序列化選項來填充現有物件的屬性
        /// 注意：此方法需要反射功能，不適用於 AOT 編譯
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 字串</param>
        /// <param name="value">要被填充的現有物件</param>
        /// <param name="options">JSON 序列化選項，如果為 null 則使用預設值</param>
        /// <exception cref="JsonException">當反射被停用或無法找到型別資訊時拋出</exception>
        [RequiresUnreferencedCode("Use the method JsonExtensions.PopulateObject<T>(string json, T value, JsonSerializerContext context) instead")]
        [RequiresDynamicCode("Use the method JsonExtensions.PopulateObject<T>(string json, T value, JsonSerializerContext context) instead")]
        public static void PopulateObject<T>(ReadOnlySpan<char> json, T value, JsonSerializerOptions? options = default) where T : class
        {
            options = options ?? JsonSerializerOptions.Default;
            var originalResolver = options.TypeInfoResolver
                ?? (JsonSerializer.IsReflectionEnabledByDefault
                    ? new DefaultJsonTypeInfoResolver() : throw new JsonException("Reflection-based serialization is disabled, please use an explicit JsonSerializerContext"));
            PopulateObject(json, value, originalResolver, t => originalResolver.GetTypeInfo(t, options));
        }        /// <summary>
        /// 內部核心方法，執行實際的物件填充邏輯
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">JSON 內容</param>
        /// <param name="value">要被填充的物件</param>
        /// <param name="originalResolver">原始的型別資訊解析器</param>
        /// <param name="getOriginalTypeInfo">取得原始型別資訊的函數</param>
        /// <exception cref="ArgumentNullException">當 value 或 originalResolver 為 null 時拋出</exception>
        /// <exception cref="JsonException">當物件無法被填充時拋出</exception>
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode",
            Justification = "The required warning was emitted for public static void PopulateObject<T>(string json, T value, JsonSerializerOptions? options = default)")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode",
            Justification = "The required warning was emitted for public static void PopulateObject<T>(string json, T value, JsonSerializerOptions? options = default)")]
        static void PopulateObject<T>(ReadOnlySpan<char> json, T value, IJsonTypeInfoResolver originalResolver, Func<Type, JsonTypeInfo?> getOriginalTypeInfo) where T : class
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(originalResolver);

            if (value is IList list && list.IsFixedSize)
                throw new JsonException($"Fixed size lists of type {value.GetType()} cannot be populated with {nameof(JsonExtensions.PopulateObject)}<{typeof(T).Name}>()");
            var declaredType = typeof(T) == typeof(object) ? value.GetType() : typeof(T);
            var originalTypeInfo = getOriginalTypeInfo(declaredType);
            if (originalTypeInfo == null)
                throw new JsonException($"No JsonTypeInfo was generated for type {declaredType}");
            if (originalTypeInfo.Kind is JsonTypeInfoKind.None)
                throw new JsonException($"Object of type {value.GetType()} (kind {originalTypeInfo.Kind}) cannot be populated with {nameof(JsonExtensions.PopulateObject)}<{typeof(T).Name}>()");
            var originalOptions = originalTypeInfo.Options;

            JsonSerializerOptions populateOptions = new(originalOptions)
            {
                PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
                TypeInfoResolver = originalResolver
                    .WithAddedModifier(new RootObjectInjector<T>(value).WithRootObjectCreator)
                    .WithAddedModifier(PopulateProperties),
            };

            try
            {
                populateOptions.MakeReadOnly();
                if (populateOptions.GetTypeInfo(declaredType) == null)
                    throw new JsonException($"Object of type {declaredType} cannot be populated with {nameof(JsonExtensions.PopulateObject)}<{typeof(T).Name}>() using {originalResolver}");
                var returnedValue = JsonSerializer.Deserialize(json, declaredType, populateOptions);
                if (returnedValue is not null && !object.ReferenceEquals(returnedValue, value))
                    throw new JsonException($"A different object was returned for {returnedValue}");
            }
            catch (Exception ex)
            {
                throw new JsonException($"Object of type {declaredType} cannot be populated with {nameof(JsonExtensions.PopulateObject)}<{typeof(T).Name}>() using {originalResolver}", ex);
            }
        }        /// <summary>
        /// 根物件注入器，用於確保反序列化時使用現有的物件實例而非建立新實例
        /// </summary>
        /// <typeparam name="T">根物件的型別</typeparam>
        class RootObjectInjector<T> where T : class
        {
            readonly Type valueType;
            T? value;
            
            /// <summary>
            /// 初始化根物件注入器
            /// </summary>
            /// <param name="value">要注入的根物件實例</param>
            /// <exception cref="ArgumentNullException">當 value 為 null 時拋出</exception>
            public RootObjectInjector(T value) => (this.value, this.valueType) = (value ?? throw new ArgumentNullException(nameof(value)), value.GetType());            /// <summary>
            /// 當需要建立根物件時，提供現有的物件實例而非建立新實例
            /// </summary>
            /// <param name="typeInfo">JSON 型別資訊</param>
            internal void WithRootObjectCreator(JsonTypeInfo typeInfo)
            {
                if (valueType.IsAssignableTo(typeInfo.Type))
                {
                    var oldCreateObject = typeInfo.CreateObject;
                    typeInfo.CreateObject = () =>
                        Interlocked.Exchange(ref this.value, null) is { } rootValue
                            ? rootValue : (oldCreateObject is not null ? oldCreateObject() : throw new JsonException($"No default creator exists for {typeInfo.Type}."));
                }
            }
        }

        /// <summary>
        /// 設定屬性物件的建立處理方式為填充模式
        /// </summary>
        /// <param name="typeInfo">要修改的 JSON 型別資訊</param>
        static void PopulateProperties(JsonTypeInfo typeInfo)
        {
            // Set PreferredPropertyObjectCreationHandling unless explicitly set by attributes
            if (typeInfo.Kind == JsonTypeInfoKind.Object)
                typeInfo.PreferredPropertyObjectCreationHandling ??= JsonObjectCreationHandling.Populate;
        }    }

    
}
