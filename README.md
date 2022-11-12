# obs-websocket-dotnet
![Build status](https://github.com/Palakis/obs-websocket-dotnet/workflows/obs-websocket-dotnet%20Tests/badge.svg)  Releases: [![NuGet](https://img.shields.io/nuget/v/obs-websocket-dotnet.svg?style=flat)](https://www.nuget.org/packages/obs-websocket-dotnet)  

Official .NET library (written in C#) to communicate with an obs-websocket server.

This library is available on the [NuGet gallery](https://www.nuget.org/packages/obs-websocket-dotnet)  
See the `TestClient` project for a working example.  
  
# v5 Updates
NOTE: As OBS Websocket v5.0 is not backward compatible with 4.9.x, neither is the .Net version.  
**What's new in v5.0.0.3:**
* Fixed issue with integer overflow for OutputStatus objects  
(Older updates):
* Each event now has a dedicated EventArgs class. This will break the previous event signature
* Finished adding all v5 methods
* `Connect()` function is now obsolete, use `ConnectAsync()` instead.
* Additional bugfixes and stability fixes

Please report issues/bugs via the [Issues Tracker](https://github.com/BarRaider/obs-websocket-dotnet/issues) or discuss in our [Discord](http://discord.barraider.com)

## Dev Discussions
**Discord:** Discuss in #developers-chat in [Bar Raiders](http://discord.barraider.com)

## EOL for v4.x branch
NOTE: We will no longer be updating the v4.x branch as we move towards v5.0 (which is NOT backwards compatible).
