# obs-websocket-dotnet
![Build status](https://github.com/Palakis/obs-websocket-dotnet/workflows/obs-websocket-dotnet%20Tests/badge.svg)  Releases: [![NuGet](https://img.shields.io/nuget/v/obs-websocket-dotnet.svg?style=flat)](https://www.nuget.org/packages/obs-websocket-dotnet)  

Official .NET library (written in C#) to communicate with an obs-websocket server.

This library is available on the [NuGet gallery](https://www.nuget.org/packages/obs-websocket-dotnet)  
See the `TestClient` project for a working example.  

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
