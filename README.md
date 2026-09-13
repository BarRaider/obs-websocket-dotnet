# obs-websocket-dotnet
![Build status](https://github.com/Palakis/obs-websocket-dotnet/workflows/obs-websocket-dotnet%20Tests/badge.svg)  Releases: [![NuGet](https://img.shields.io/nuget/v/obs-websocket-dotnet.svg?style=flat)](https://www.nuget.org/packages/obs-websocket-dotnet)  

Official .NET library (written in C#) to communicate with an obs-websocket server.

This library is available on the [NuGet gallery](https://www.nuget.org/packages/obs-websocket-dotnet)  
See the `TestClient` project for a working example.

Supported target frameworks: **netstandard2.1** and **net10.0**. Tests target **net10.0**. `TestClient` targets **net10.0-windows**. Both were moved from net9.

## New in v5.7.0
Brings the library up to date with the obs-websocket 5.7.0 protocol, and adds first-class .NET 10 support.

* **.NET 10 support** — the package now multi-targets `netstandard2.1` and `net10.0`; existing consumers on
  older TFMs are unaffected, .NET 10 apps get a first-party asset. Tests and `TestClient` were retargeted
  from net9 to net10
* **Event subscriptions** — control which events you receive via `EventSubscriptions`, including the four
  high-volume events (`InputVolumeMeters`, `InputActiveStateChanged`, `InputShowStateChanged`,
  `SceneItemTransformChanged`) which now require explicit opt-in
* **8 new events**: `CustomEvent`, `InputSettingsChanged`, `SourceFilterSettingsChanged`, `RecordFileChanged`,
  `ScreenshotSaved`, `CanvasCreated`, `CanvasRemoved`, `CanvasNameChanged`
* **17 new requests**:
  * Outputs (generic, works on any output by name): `GetOutputList`, `GetOutputStatus`, `ToggleOutput`,
    `StartOutput`, `StopOutput`, `GetOutputSettings`, `SetOutputSettings`
  * Recording: `SetRecordDirectory`, `SplitRecordFile`, `CreateRecordChapter`
  * Inputs: `GetInputDeinterlaceMode`, `SetInputDeinterlaceMode`, `GetInputDeinterlaceFieldOrder`,
    `SetInputDeinterlaceFieldOrder` (each `Set*` also has a convenience overload taking a typed enum)
  * Other: `GetSourceFilterKindList`, `GetSceneItemSource`, `GetCanvasList`
* **Fixed:** `GetInputAudioTracks` always returned `false` for every track (the response's
  `inputAudioTracks` field wasn't being unwrapped before deserializing)
* `OpenSourceProjector` and `OpenVideoMixProjector` are now declared on `IOBSWebsocket` (they were missing)
* Greatly expanded test suite: request/response round-trip coverage plus conformance gates that fail the
  build if the library drifts from the protocol spec again

> **Upgrading:** if you *implement* `IOBSWebsocket` yourself (e.g. a hand-written mock), this release adds
> members to the interface and you will need to implement them. Simply *using* `OBSWebsocket` or
> `IOBSWebsocket` requires no changes, and no existing method signature or behavior has changed.

> **Not yet covered:** the optional `canvasUuid` parameter added in 5.7.0 to several pre-existing requests,
> and `*Uuid`-based addressing generally (the library only supports name-based addressing for inputs today).

## New in v5.0.1
* Fixes for deserialization issues in MediaInputStatus
* Allow OBSVideoSettings to be updated via the API
* New ILogger support instead of writing to console
* New UnsupportedEvent event
* Updated to netstandard 2.1

Please report issues/bugs via the [Issues Tracker](https://github.com/BarRaider/obs-websocket-dotnet/issues) or discuss in our [Discord](http://discord.barraider.com)

## Dev Discussions
**Discord:** Discuss in #developers-chat in [Bar Raiders](http://discord.barraider.com)

## EOL for v4.x branch
NOTE: We will no longer be updating the v4.x branch as we move towards v5.0 (which is NOT backwards compatible).
