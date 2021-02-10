/*
    The MIT License (MIT)

    Copyright (c) 2017 Stéphane Lepin

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Instance of a connection with an obs-websocket server
    /// </summary>
    public partial class OBSWebsocket
    {
        #region Private Members

        private const string SOURCE_TYPE_JSON_FIELD = "sourceType";
        private const string SOURCE_TYPE_BROWSER_SOURCE = "browser_source";

        #endregion

        /// <summary>
        /// Get basic OBS video information
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSVideoInfo> GetVideoInfo(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetVideoInfo", cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<OBSVideoInfo>(response.ToString());
        }
        /// <summary>
        /// List existing outputs
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSOutputInfo[]> ListOutputs(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("ListOutputs", cancellationToken).ConfigureAwait(false);
            JObject[]? jOutputs = response["outputs"]?.Children<JObject>().ToArray();
            if (jOutputs == null)
                return Array.Empty<OBSOutputInfo>();
            int outputCount = jOutputs.Length;
            if (outputCount == 0)
                return Array.Empty<OBSOutputInfo>();
            OBSOutputInfo[] outputs = new OBSOutputInfo[outputCount];

            for (int i = 0; i < outputCount; i++)
            {
                try
                {
                    outputs[i] = OBSOutputInfo.CreateOutput(jOutputs[i]);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
                {
                    OBSError?.Invoke(this, new OBSErrorEventArgs("Error parsing an Output.", ex, jOutputs[i]));
                }
#pragma warning restore CA1031 // Do not catch general exception types
            }
            return outputs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSOutputInfo> GetOutput(string outputName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "outputName", outputName }
            };
            JObject response = await SendRequest("GetOutputInfo", requestFields, cancellationToken).ConfigureAwait(false);

            return OBSOutputInfo.CreateOutput(response["outputInfo"] as JObject 
                ?? throw ErrorResponseException.FromMissingProperty("outputInfo", response));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task StartOutput(string outputName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "outputName", outputName }
            };

            return SendRequest("StartOutput", requestFields, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputName"></param>
        /// <param name="force"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task StopOutput(string outputName, bool force, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "outputName", outputName },
                { "force", force }
            };

            return SendRequest("StopOutput", requestFields, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task StopOutput(string outputName, CancellationToken cancellationToken = default) => StopOutput(outputName, false, cancellationToken);

        /// <summary>
        /// Request a screenshot from the specified source. An <paramref name="embedPictureFormat"/> or <paramref name="saveToFilePath"/> must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat">Format of the Data URI encoded picture. Can be "png", "jpg", "jpeg" or "bmp" (or any other value supported by Qt's Image module)</param>
        /// <param name="saveToFilePath">Full file path (file extension included) where the captured image is to be saved. Can be in a format different from pictureFormat. Can be a relative path.</param>
        /// <param name="width">Screenshot width. Defaults to the source's base width.</param>
        /// <param name="height">Screenshot height. Defaults to the source's base height.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName, string? embedPictureFormat,
            string? saveToFilePath = null, int width = -1, int height = -1, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            if (embedPictureFormat != null)
            {
                requestFields.Add("embedPictureFormat", embedPictureFormat);
            }
            if (saveToFilePath != null)
            {
                requestFields.Add("saveToFilePath", saveToFilePath);
            }
            if (width > -1)
            {
                requestFields.Add("width", width);
            }
            if (height > -1)
            {
                requestFields.Add("height", height);
            }

            JObject response = await SendRequest("TakeSourceScreenshot", requestFields, cancellationToken).ConfigureAwait(false);
            return ParseObject<SourceScreenshotResponse>(response);
        }

        /// <summary>
        /// Request a screenshot from the specified source. An <paramref name="embedPictureFormat"/> or <paramref name="saveToFilePath"/> must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat"></param>
        /// <param name="saveToFilePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName, string? embedPictureFormat, string? saveToFilePath = null, CancellationToken cancellationToken = default)
            => TakeSourceScreenshot(sourceName, embedPictureFormat, saveToFilePath, -1, -1, cancellationToken);

        /// <summary>
        /// Request a screenshot from the specified source embedded in the response.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat">Format of the Data URI encoded picture. Can be "png", "jpg", "jpeg" or "bmp" (or any other value supported by Qt's Image module)</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName, string embedPictureFormat, CancellationToken cancellationToken = default)
        {
            return TakeSourceScreenshot(sourceName, embedPictureFormat, null, -1, -1, cancellationToken);
        }

        /// <summary>
        /// Get the current scene info along with its items
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="OBSScene"/> object describing the current scene</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSScene> GetCurrentScene(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetCurrentScene", cancellationToken).ConfigureAwait(false);
            return ParseObject<OBSScene>(response);
        }

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException">Thrown if the requested scene does not exist.</exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetCurrentScene(string sceneName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "scene-name", sceneName }
            };

            await SendRequest("SetCurrentScene", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the filename formatting string
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Current filename formatting string</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<string?> GetFilenameFormatting(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetFilenameFormatting", cancellationToken).ConfigureAwait(false);
            return response["filename-formatting"]?.Value<string>() 
                ?? throw ErrorResponseException.FromMissingProperty("filename-formatting", response);
        }

        /// <summary>
        /// Get OBS stats (almost the same info as provided in OBS' stats window)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSStats> GetStats(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetStats", cancellationToken).ConfigureAwait(false);
            JObject prop = response["stats"] as JObject ?? throw ErrorResponseException.FromMissingProperty("stats", response);
            return ParseObject<OBSStats>(prop);
        }

        /// <summary>
        /// List every available scene
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="List{OBSScene}" /> of <see cref="OBSScene"/> objects describing each scene</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<OBSScene>> ListScenes(CancellationToken cancellationToken = default)
        {
            GetSceneListInfo? response = await GetSceneList(cancellationToken).ConfigureAwait(false);
            return response.Scenes;
        }

        /// <summary>
        /// Get a list of scenes in the currently active profile
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<GetSceneListInfo> GetSceneList(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetSceneList", cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetSceneListInfo>(response.ToString());
        }

        /// <summary>
        /// Changes the order of scene items in the requested scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to reorder (defaults to current)</param>
        /// <param name="sceneItems">List of items to reorder, only ID or Name required</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ReorderSceneItems(List<SceneItemStub> sceneItems, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject();
            if (sceneName != null)
            {
                requestFields.Add("scene", sceneName);
            }

            JObject? items = JObject.Parse(JsonConvert.SerializeObject(sceneItems));
            requestFields.Add("items", items);

            await SendRequest("ReorderSceneItems", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the specified scene's transition override info
        /// </summary>
        /// <param name="sceneName">Name of the scene to return the override info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>TransitionOverrideInfo</returns>
        public async Task<TransitionOverrideInfo> GetSceneTransitionOverride(string sceneName, CancellationToken cancellationToken)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName }
            };

            JObject response = await SendRequest("GetSceneTransitionOverride", requestFields, cancellationToken).ConfigureAwait(false);
            return response.ToObject<TransitionOverrideInfo>() ?? throw ErrorResponseException.FromNullResponseObject<TransitionOverrideInfo>(response);
        }

        /// <summary>
        /// Set specific transition override for a scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to set the transition override</param>
        /// <param name="transitionName">Name of the transition to use</param>
        /// <param name="transitionDuration">Duration in milliseconds of the transition if transition is not fixed. Defaults to the current duration specified in the UI if there is no current override and this value is not given</param>
        /// <param name="cancellationToken"></param>
        public async Task SetSceneTransitionOverride(string sceneName, string transitionName, int transitionDuration = -1,
            CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName },
                { "transitionName", transitionName }
            };

            if (transitionDuration >= 0)
            {
                requestFields.Add("transitionDuration", transitionDuration);
            }

            await SendRequest("SetSceneTransitionOverride", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set specific transition override for a scene with the existing duration.
        /// </summary>
        /// <param name="sceneName">Name of the scene to set the transition override</param>
        /// <param name="transitionName">Name of the transition to use</param>
        /// <param name="cancellationToken"></param>
        public Task SetSceneTransitionOverride(string sceneName, string transitionName, CancellationToken cancellationToken = default)
            => SetSceneTransitionOverride(sceneName, transitionName, -1, cancellationToken);

        /// <summary>
        /// Remove any transition override from a specific scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to remove the transition override</param>
        /// <param name="cancellationToken"></param>
        public async Task RemoveSceneTransitionOverride(string sceneName, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName }
            };

            await SendRequest("RemoveSceneTransitionOverride", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<SourceInfo>> GetSourcesList(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetSourcesList", cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<SourceInfo>>(response["sources"]?.ToString()
                ?? throw new ErrorResponseException("Response did not contain 'sources'.", response));
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<SourceType>> GetSourceTypesList(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetSourceTypesList", cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<SourceType>>(response["types"]?.ToString()
                ?? throw new ErrorResponseException("Response did not contain ''.", response));
        }

        /// <summary>
        /// Change the visibility of the specified scene item
        /// </summary>
        /// <param name="itemName">Scene item which visiblity will be changed</param>
        /// <param name="visible">Desired visiblity</param>
        /// <param name="sceneName">Scene name of the specified item</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSourceRender(string itemName, bool visible, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "item", itemName },
                { "visible", visible }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemProperties", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<SceneItemProperties> GetSceneItemProperties(string itemName, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject response = await GetSceneItemPropertiesJson(itemName, sceneName, cancellationToken).ConfigureAwait(false);
            return response.ToObject<SceneItemProperties>()
                ?? throw new ErrorResponseException("Response could not be parsed into SceneItemProperties.", response);
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// Response is a JObject
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<JObject> GetSceneItemPropertiesJson(string itemName, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "item", itemName }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            return await SendRequest("GetSceneItemProperties", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="sourceName">The name of the source</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<TextGDIPlusProperties> GetTextGDIPlusProperties(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName }
            };

            JObject response = await SendRequest("GetTextGDIPlusProperties", requestFields, cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TextGDIPlusProperties>(response.ToString());
        }

        /// <summary>
        /// Set the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="properties">properties for the source</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetTextGDIPlusProperties(TextGDIPlusProperties properties, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = JObject.Parse(JsonConvert.SerializeObject(properties));

            await SendRequest("SetTextGDIPlusProperties", requestFields, cancellationToken).ConfigureAwait(false);

        }

        /// <summary>
        /// Move a filter in the chain (relative positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="movement">Direction to move</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task MoveSourceFilter(string sourceName, string filterName, FilterMovementType movement, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "movementType", movement.ToString().ToLowerInvariant() }
            };

            await SendRequest("MoveSourceFilter", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Move a filter in the chain (absolute index positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="newIndex">Desired position of the filter in the chain</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ReorderSourceFilter(string sourceName, string filterName, int newIndex, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "newIndex", newIndex }
            };

            await SendRequest("ReorderSourceFilter", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">Filter settings</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSourceFilterSettings(string sourceName, string filterName, JObject filterSettings, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "filterSettings", filterSettings }
            };

            await SendRequest("SetSourceFilterSettings", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify the Source Filter's visibility
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Source filter name</param>
        /// <param name="filterEnabled">New filter state</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSourceFilterVisibility(string sourceName, string filterName, bool filterEnabled, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "filterEnabled", filterEnabled }
            };

            await SendRequest("SetSourceFilterVisibility", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<FilterSettings[]> GetSourceFilters(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            JObject response = await SendRequest("GetSourceFilters", requestFields, cancellationToken).ConfigureAwait(false);

            return response["filters"]?.ToObject<FilterSettings[]>()
                ?? throw new ErrorResponseException("Response did not contain 'filters'.", response);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="cancellationToken"></param>
        public async Task<FilterSettings> GetSourceFilterInfo(string sourceName, string filterName, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName }
            };

            JObject response = await SendRequest("GetSourceFilterInfo", requestFields, cancellationToken);

            return response.ToObject<FilterSettings>() ?? throw ErrorResponseException.FromNullResponseObject<FilterSettings>(response);
        }

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="filterName"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<bool> RemoveFilterFromSource(string sourceName, string filterName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName }
            };
            try
            {
                await SendRequest("RemoveFilterFromSource", requestFields, cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                //TODO: exception handling
                OBSLogger.Error(e.Message);
                OBSLogger.Debug(e);
            }
            return false;
        }

        /// <summary>
        /// Add a filter to a source
        /// </summary>
        /// <param name="sourceName">Name of the source for the filter</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterType">Type of filter</param>
        /// <param name="filterSettings">Filter settings object</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task AddFilterToSource(string sourceName, string filterName, string filterType, JObject filterSettings, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterType", filterType },
                { "filterName", filterName },
                { "filterSettings", filterSettings }
            };

            await SendRequest("AddFilterToSource", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start/Stop the streaming output
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ToggleStreaming(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopStreaming", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start/Stop the recording output
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ToggleRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the current status of the streaming and recording outputs
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="OutputStatus"/> object describing the current outputs states</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OutputStatus> GetStreamingStatus(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetStreamingStatus", cancellationToken).ConfigureAwait(false);
            OutputStatus? outputStatus = new OutputStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// List all transitions
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="List{T}"/> of all transition names</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<string>> ListTransitions(CancellationToken cancellationToken = default)
        {
            GetTransitionListInfo? transitions = await GetTransitionList(cancellationToken).ConfigureAwait(false);

            List<string> transitionNames = new List<string>();
            foreach (TransitionSettings? item in transitions.Transitions)
                transitionNames.Add(item.Name);


            return transitionNames;
        }

        /// <summary>
        /// Get the current transition name and duration
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="TransitionSettings"/> object with the current transition name and duration</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<TransitionSettings> GetCurrentTransition(CancellationToken cancellationToken = default)
        {
            JObject respBody = await SendRequest("GetCurrentTransition", cancellationToken).ConfigureAwait(false);
            try
            {
                TransitionSettings? settings = respBody.ToObject<TransitionSettings>();
                if (settings != null)
                    return settings;
                throw new ErrorResponseException("Response body could not be parsed into 'TransitionSettings'.", respBody);
            }
            catch (JsonException ex)
            {
                throw new ErrorResponseException($"Invalid response body: {ex.Message}.", respBody, ex);
            }
        }

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetCurrentTransition(string transitionName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "transition-name", transitionName }
            };

            await SendRequest("SetCurrentTransition", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="duration">Desired transition duration (in milliseconds)</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetTransitionDuration(int duration, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "duration", duration }
            };

            await SendRequest("SetTransitionDuration", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which volume will be changed</param>
        /// <param name="volume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="useDecibel">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetVolume(string sourceName, float volume, bool useDecibel = false, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName },
                { "volume", volume },
                { "useDecibel", useDecibel }
            };

            await SendRequest("SetVolume", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which volume will be changed</param>
        /// <param name="volume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetVolume(string sourceName, float volume, CancellationToken cancellationToken = default) => SetVolume(sourceName, volume, false, cancellationToken);

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` if using amplitude/mul (useDecibel is false), under `0.0` if using dB (useDecibel is true).
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="useDecibel">Output volume in decibels of attenuation instead of amplitude/mul.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<VolumeInfo> GetVolume(string sourceName, bool useDecibel = false, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName },
                { "useDecibel", useDecibel }
            };

            JObject? response = await SendRequest("GetVolume", requestFields, cancellationToken).ConfigureAwait(false);
            return new VolumeInfo(response);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` using amplitude/mul.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<VolumeInfo> GetVolume(string sourceName, CancellationToken cancellationToken = default) => GetVolume(sourceName, false, cancellationToken);

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be changed</param>
        /// <param name="mute">Desired mute state</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetMute(string sourceName, bool mute, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName },
                { "mute", mute }
            };

            await SendRequest("SetMute", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be toggled</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ToggleMute(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName }
            };

            await SendRequest("ToggleMute", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the position of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which position will be changed</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="cancellationToken"></param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemPosition(string itemName, float x, float y, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "item", itemName },
                { "x", x },
                { "y", y }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemPosition", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Set the position of the specified scene item in the current scene.
        /// </summary>
        /// <param name="itemName">Name of the scene item which position will be changed</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSceneItemPosition(string itemName, float x, float y, CancellationToken cancellationToken = default) => SetSceneItemPosition(itemName, x, y, null, cancellationToken);

        /// <summary>
        /// Set the scale and rotation of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which transform will be changed</param>
        /// <param name="rotation">Rotation in Degrees</param>
        /// <param name="xScale">Horizontal scale factor</param>
        /// <param name="yScale">Vertical scale factor</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemTransform(string itemName, float rotation = 0, float xScale = 1, float yScale = 1, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "item", itemName },
                { "x-scale", xScale },
                { "y-scale", yScale },
                { "rotation", rotation }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemTransform", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the scale and rotation of the specified scene item in the current scene.
        /// </summary>
        /// <param name="itemName">Name of the scene item which transform will be changed</param>
        /// <param name="rotation">Rotation in Degrees</param>
        /// <param name="xScale">Horizontal scale factor</param>
        /// <param name="yScale">Vertical scale factor</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSceneItemTransform(string itemName, float rotation = 0, float xScale = 1, float yScale = 1, CancellationToken cancellationToken = default)
            => SetSceneItemTransform(itemName, rotation, xScale, yScale, null, cancellationToken);

        /// <summary>
        /// Sets the scene specific properties of a source. Unspecified properties will remain unchanged. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="props">Object containing changes</param>
        /// <param name="sceneName">Option scene name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemProperties(SceneItemProperties props, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            var requestFields = JObject.FromObject(props);

            if (requestFields["item"] == null)
            {
                requestFields["item"] = props.ItemName;
            }

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemProperties", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Sets the scene specific properties of a source in the current scene.
        /// Unspecified properties will remain unchanged. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="props">Object containing changes</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSceneItemProperties(SceneItemProperties props, CancellationToken cancellationToken = default)
            => SetSceneItemProperties(props, null, cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sceneName"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemProperties(JObject obj, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            // Serialize object to SceneItemProperties (needed before proper deserialization)
            SceneItemProperties? props = JsonConvert.DeserializeObject<SceneItemProperties>(obj.ToString(), DefaultSerializerSettings);

            // Deserialize object
            JObject? requestFields = JObject.Parse(JsonConvert.SerializeObject(props, DefaultSerializerSettings));

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemProperties", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSceneItemProperties(JObject obj, CancellationToken cancellationToken = default)
            => SetSceneItemProperties(obj, null, cancellationToken);

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="scName">Desired scene collection name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetCurrentSceneCollection(string scName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sc-name", scName }
            };

            await SendRequest("SetCurrentSceneCollection", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the name of the current scene collection
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Name of the current scene collection</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<string> GetCurrentSceneCollection(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetCurrentSceneCollection", cancellationToken).ConfigureAwait(false);
            return response["sc-name"]?.ToString() ?? throw new ErrorResponseException("Response did not contain 'sc-name'", response);
        }

        /// <summary>
        /// List all scene collections
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="List{T}"/> of the names of all scene collections</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<string>> ListSceneCollections(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("ListSceneCollections", cancellationToken).ConfigureAwait(false);
            JArray? items = (JArray?)response["scene-collections"];
            List<string> sceneCollections = new List<string>();
            if (items == null)
                return sceneCollections;
            foreach (JObject item in items)
            {
                string? name = (string?)item["sc-name"];
                if (name != null)
                    sceneCollections.Add(name);
            }

            return sceneCollections;
        }

        /// <summary>
        /// Set the current profile to the specified one
        /// </summary>
        /// <param name="profileName">Name of the desired profile</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetCurrentProfile(string profileName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "profile-name", profileName }
            };

            await SendRequest("SetCurrentProfile", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the name of the current profile
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Name of the current profile</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<string> GetCurrentProfile(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetCurrentProfile", cancellationToken).ConfigureAwait(false);
            return (string?)response["profile-name"] ?? throw new ErrorResponseException("Response did not contain 'profile-name'.", response);
        }

        /// <summary>
        /// List all profiles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="List{T}"/> of the names of all profiles</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<List<string>> ListProfiles(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("ListProfiles", cancellationToken).ConfigureAwait(false);
            JArray? items = (JArray?)response["profiles"] ?? throw new ErrorResponseException("Response did not contain 'profiles'.", response);
            List<string> profiles = new List<string>();
            foreach (JObject item in items)
            {
                string? value = (string?)item["profile-name"];
                if (value != null)
                    profiles.Add(value);
            }

            return profiles;
        }

        // TODO: needs updating, could include stream settings (https://github.com/Palakis/obs-websocket/blob/4.x-current/docs/generated/protocol.md#startstreaming).
        /// <summary>
        /// Start streaming. Will trigger an error if streaming is already active
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartStreaming(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStreaming", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Stop streaming. Will trigger an error if streaming is not active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StopStreaming(CancellationToken cancellationToken = default)
        {
            await SendRequest("StopStreaming", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle Streaming
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartStopStreaming(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopStreaming", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start recording. Will trigger an error if recording is already active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Stop recording. Will trigger an error if recording is not active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StopRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("StopRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle recording
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartStopRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Pause the current recording. Returns an error if recording is not active or already paused.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task PauseRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("PauseRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Resume/unpause the current recording (if paused). Returns an error if recording is not active or not paused.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task ResumeRecording(CancellationToken cancellationToken = default)
        {
            await SendRequest("ResumeRecording", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the current recording folder
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="recFolder">Recording folder path</param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetRecordingFolder(string recFolder, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "rec-folder", recFolder }
            };
            await SendRequest("SetRecordingFolder", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the path of the current recording folder
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Current recording folder path</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<string> GetRecordingFolder(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetRecordingFolder", cancellationToken).ConfigureAwait(false);
            return response["rec-folder"]?.Value<string>() ?? throw ErrorResponseException.FromMissingProperty("rec-folder", response);
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Current transition duration (in milliseconds)</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<int> GetTransitionDuration(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetTransitionDuration", cancellationToken).ConfigureAwait(false);
            return response["transition-duration"]?.Value<int>() ?? throw ErrorResponseException.FromMissingProperty("transition-duration", response);
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Current transition duration (in milliseconds)</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<GetTransitionListInfo> GetTransitionList(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetTransitionList", cancellationToken).ConfigureAwait(false);

            return response.ToObject<GetTransitionListInfo>() ?? throw ErrorResponseException.FromNullResponseObject<GetTransitionListInfo>(response);
        }

        /// <summary>
        /// Get the position of the current transition. Value will be between 0.0 and 1.0.
        /// Note: Returns 1.0 when not active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<double> GetTransitionPosition(CancellationToken cancellationToken = default)
        {
            var response = await SendRequest("GetTransitionPosition", cancellationToken).ConfigureAwait(false);

            return response["position"]?.Value<double>() ?? throw ErrorResponseException.FromMissingProperty("position", response);
        }

        /// <summary>
        /// Get status of Studio Mode
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Studio Mode status (on/off)</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<bool> StudioModeEnabled(CancellationToken cancellationToken = default)
        {
            var response = await SendRequest("GetStudioModeStatus", cancellationToken).ConfigureAwait(false);
            return response["studio-mode"]?.Value<bool>() ?? throw ErrorResponseException.FromMissingProperty("studio-mode", response);
        }

        /// <summary>
        /// Disable Studio Mode
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DisableStudioMode(CancellationToken cancellationToken = default)
        {
            await SendRequest("DisableStudioMode", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Enable Studio Mode
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task EnableStudioMode(CancellationToken cancellationToken = default)
        {
            await SendRequest("EnableStudioMode", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns true if Studio Mode is enabled, false otherwise.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<bool> GetStudioModeStatus(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetStudioModeStatus", cancellationToken).ConfigureAwait(false);
            return response["studio-mode"]?.Value<bool>() ?? throw ErrorResponseException.FromMissingProperty("studio-mode", response);
        }

        /// <summary>
        /// Enable/disable Studio Mode
        /// </summary>
        /// <param name="enable">Desired Studio Mode status</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetStudioMode(bool enable, CancellationToken cancellationToken = default)
        {
            if (enable)
                await EnableStudioMode(cancellationToken).ConfigureAwait(false);
            else
                await DisableStudioMode(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle Studio Mode status (on to off or off to on)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ToggleStudioMode(CancellationToken cancellationToken = default)
        {
            await SendRequest("ToggleStudioMode", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the currently selected preview scene. Triggers an error
        /// if Studio Mode is disabled
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Preview scene object</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<OBSScene> GetPreviewScene(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetPreviewScene", cancellationToken).ConfigureAwait(false);
            OBSScene info = response?.ToObject<OBSScene>()
                ?? throw ErrorResponseException.FromNullResponseObject<OBSScene>(response);
            return info;
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <param name="previewScene">Preview scene name</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetPreviewScene(string previewScene, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "scene-name", previewScene }
            };
            await SendRequest("SetPreviewScene", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled.
        /// </summary>
        /// <param name="previewScene">Preview scene object</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetPreviewScene(OBSScene previewScene, CancellationToken cancellationToken = default)
        {
            await SetPreviewScene(previewScene.Name, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Triggers a Studio Mode transition (preview scene to program)
        /// </summary>
        /// <param name="transitionDuration">(optional) Transition duration</param>
        /// <param name="transitionName">(optional) Name of transition to use</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task TransitionToProgram(int transitionDuration = -1, string? transitionName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject();

            if (transitionDuration > -1 || transitionName != null)
            {
                JObject? withTransition = new JObject();

                if (transitionDuration > -1)
                    withTransition.Add("duration");

                if (transitionName != null)
                    withTransition.Add("name", transitionName);

                requestFields.Add("with-transition", withTransition);
            }

            await SendRequest("TransitionToProgram", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get if the specified source is muted
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Source mute status (on/off)</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<bool> GetMute(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName }
            };

            JObject? response = await SendRequest("GetMute", requestFields, cancellationToken).ConfigureAwait(false);
            return response["muted"]?.Value<bool>() ?? false;
        }

        /// <summary>
        /// Toggle the Replay Buffer on/off
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ToggleReplayBuffer(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopReplayBuffer", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start recording into the Replay Buffer. Triggers an error
        /// if the Replay Buffer is already active, or if the "Save Replay Buffer"
        /// hotkey is not set in OBS' settings
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartReplayBuffer(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartReplayBuffer", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Stop recording into the Replay Buffer. Triggers an error if the
        /// Replay Buffer is not active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StopReplayBuffer(CancellationToken cancellationToken = default)
        {
            await SendRequest("StopReplayBuffer", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle replay buffer
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task StartStopReplayBuffer(CancellationToken cancellationToken = default)
        {
            await SendRequest("StartStopReplayBuffer", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Save and flush the contents of the Replay Buffer to disk. Basically
        /// the same as triggering the "Save Replay Buffer" hotkey in OBS.
        /// Triggers an error if Replay Buffer is not active.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SaveReplayBuffer(CancellationToken cancellationToken = default)
        {
            await SendRequest("SaveReplayBuffer", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="syncOffset">Audio offset (in nanoseconds) for the specified source</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSyncOffset(string sourceName, int syncOffset, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName },
                { "offset", syncOffset }
            };
            await SendRequest("SetSyncOffset", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<int> GetSyncOffset(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "source", sourceName }
            };
            JObject? response = await SendRequest("GetSyncOffset", requestFields, cancellationToken).ConfigureAwait(false);
            return response["offset"]?.Value<int>() ?? throw ErrorResponseException.FromMissingProperty("offset", response);
        }

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItem">Scene item, requires name or id of item</param>
        /// /// <param name="sceneName">Scene name to delete item from (optional)</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DeleteSceneItem(SceneItemStub sceneItem, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject();

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            JObject minReqs = new JObject
            {
                { "id", sceneItem.ID }
            };

            if (sceneItem.SourceName != null)
            {
                minReqs.Add("name", sceneItem.SourceName);
            }
            requestFields.Add("item", minReqs);

            await SendRequest("DeleteSceneItem", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a scene item from the current scene.
        /// </summary>
        /// <param name="sceneItem">Scene item, requires name or id of item</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task DeleteSceneItem(SceneItemStub sceneItem, CancellationToken cancellationToken = default)
            => DeleteSceneItem(sceneItem, null, cancellationToken);

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// <param name="sceneName">Scene name to delete item from (optional)</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DeleteSceneItem(int sceneItemId, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject();

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            JObject minReqs = new JObject
            {
                { "id", sceneItemId }
            };
            requestFields.Add("item", minReqs);

            await SendRequest("DeleteSceneItem", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Deletes a scene item from the current scene.
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task DeleteSceneItem(int sceneItemId, CancellationToken cancellationToken = default)
            => DeleteSceneItem(sceneItemId, null, cancellationToken);

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItemName">Name of the scene item</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="sceneName">(optional) parent scene name of the specified source</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemCrop(string sceneItemName, SceneItemCropInfo cropInfo, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "item", sceneItemName },
                { "top", cropInfo.Top },
                { "bottom", cropInfo.Bottom },
                { "left", cropInfo.Left },
                { "right", cropInfo.Right }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("SetSceneItemCrop", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItemName">Name of the scene item</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSceneItemCrop(string sceneItemName, SceneItemCropInfo cropInfo, CancellationToken cancellationToken = default)
            => SetSceneItemCrop(sceneItemName, cropInfo, null, cancellationToken);

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItem">Scene item object</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="scene">Parent scene of scene item</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSceneItemCrop(SceneItem sceneItem, SceneItemCropInfo cropInfo, OBSScene scene, CancellationToken cancellationToken = default)
        {
            await SetSceneItemCrop(sceneItem.SourceName, cropInfo, scene.Name, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reset a scene item
        /// </summary>
        /// <param name="itemName">Name of the source item</param>
        /// <param name="sceneName">Name of the scene the source belongs to. Defaults to the current scene.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task ResetSceneItem(string itemName, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "item", itemName }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            await SendRequest("ResetSceneItem", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Reset a scene item in the current scene.
        /// </summary>
        /// <param name="itemName">Name of the source item</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task ResetSceneItem(string itemName, CancellationToken cancellationToken = default) => ResetSceneItem(itemName, null, cancellationToken);

        /// <summary>
        /// Send the provided text as embedded CEA-608 caption data. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="text">Captions text</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SendCaptions(string text, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "text", text }
            };

            await SendRequest("SendCaptions", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the filename formatting string
        /// </summary>
        /// <param name="filenameFormatting">Filename formatting string to set</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetFilenameFormatting(string filenameFormatting, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "filename-formatting", filenameFormatting }
            };

            await SendRequest("SetFilenameFormatting", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItem">Scene item, requires name or id</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DuplicateSceneItem(string fromSceneName, string toSceneName, SceneItem sceneItem, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "fromScene", fromSceneName },
                { "toScene", toSceneName }
            };

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
            {
                minReqs.Add("name", sceneItem.SourceName);
            }
            minReqs.Add("id", sceneItem.ID);
            requestFields.Add("item", minReqs);

            await SendRequest("DuplicateSceneItem", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItemID">Scene item id to duplicate</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DuplicateSceneItem(string fromSceneName, string toSceneName, int sceneItemID, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "fromScene", fromSceneName },
                { "toScene", toSceneName }
            };

            JObject minReqs = new JObject
            {
                { "id", sceneItemID }
            };
            requestFields.Add("item", minReqs);

            await SendRequest("DuplicateSceneItem", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get names of configured special sources (like Desktop Audio
        /// and Mic sources)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<Dictionary<string, string>> GetSpecialSources(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetSpecialSources", cancellationToken).ConfigureAwait(false);
            Dictionary<string, string>? sources = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JToken?> kvp in response)
            {
                string? key = kvp.Key;
                string? value = (string?)kvp.Value;
                if (key == null || value == null)
                    continue; // TODO: Is a null value ever valid?
                if (key != "request-type" && key != "message-id" && key != "status")
                {
                    sources.Add(key, value);
                }
            }
            return sources;
        }

        /// <summary>
        /// Set current streaming settings
        /// </summary>
        /// <param name="service">Service settings</param>
        /// <param name="save">Save to disk</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetStreamingSettings(StreamingService service, bool save, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "type", service.Type },
                { "settings", JToken.FromObject(service.Settings) },
                { "save", save }
            };
            await SendRequest("SetStreamSettings", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get current streaming settings
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<StreamingService> GetStreamSettings(CancellationToken cancellationToken = default)
        {
            JObject? response = await SendRequest("GetStreamSettings", cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<StreamingService>(response.ToString());
        }

        /// <summary>
        /// Save current Streaming settings to disk
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SaveStreamSettings(CancellationToken cancellationToken = default)
        {
            return SendRequest("SaveStreamSettings", cancellationToken);
        }

        /// <summary>
        /// Get settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        /// <param name="cancellationToken"></param>
        /// <returns>BrowserSource properties</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<BrowserSourceProperties> GetBrowserSourceProperties(string sourceName, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sourceName);
            }
            JObject? response = await SendRequest("GetSourceSettings", requestFields, cancellationToken).ConfigureAwait(false);
            if (response[SOURCE_TYPE_JSON_FIELD]?.ToString() != SOURCE_TYPE_BROWSER_SOURCE)
            {
                throw new Exception($"Invalid source_type. Expected: '{SOURCE_TYPE_BROWSER_SOURCE}' Received: '{response[SOURCE_TYPE_JSON_FIELD]}'");
            }

            return new BrowserSourceProperties(response);
        }
        /// <summary>
        /// Get settings of the specified BrowserSource in the current scene.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>BrowserSource properties</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<BrowserSourceProperties> GetBrowserSourceProperties(string sourceName, CancellationToken cancellationToken = default)
            => GetBrowserSourceProperties(sourceName, null, cancellationToken);

        /// <summary>
        /// Set settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="props">BrowserSource properties</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        [Obsolete("Deprecated in obs-websocket 4.8.0, use SetSourceSettings")]
        public async Task SetBrowserSourceProperties(string sourceName, BrowserSourceProperties props, string? sceneName = null, CancellationToken cancellationToken = default)
        {
            props.Source = sourceName;
            JObject? requestFields = JObject.FromObject(props);
            if (sceneName != null)
            {
                requestFields.Add("scene-name", sourceName);
            }

            await SetSourceSettings(sourceName, requestFields, SOURCE_TYPE_BROWSER_SOURCE).ConfigureAwait(false);
        }

        /// <summary>
        /// Set settings of the specified BrowserSource in the current scene.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="props">BrowserSource properties</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        [Obsolete("Deprecated in obs-websocket 4.8.0, use SetSourceSettings")]
        public Task SetBrowserSourceProperties(string sourceName, BrowserSourceProperties props, CancellationToken cancellationToken = default)
            => SetBrowserSourceProperties(sourceName, props, null, cancellationToken);

        /// <summary>
        /// Enable/disable the heartbeat event
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="cancellationToken"></param>
        [Obsolete("Will be deprecated in v4.9 and completely removed in v5.0")]
        public async Task SetHeartbeat(bool enable, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "enable", enable }
            };

            await SendRequest("SetHeartbeat", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the settings from a source item
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>settings</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<SourceSettings> GetSourceSettings(string sourceName, string? sourceType, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName }
            };
            if (sourceType != null)
            {
                requestFields.Add("sourceType", sourceType);
            }

            JObject result = await SendRequest("GetSourceSettings", requestFields, cancellationToken).ConfigureAwait(false);
            try
            {
                SourceSettings settings = result?.ToObject<SourceSettings>()
                    ?? throw ErrorResponseException.FromNullResponseObject<SourceSettings>(result);

                return settings;
            }
            catch (JsonException ex)
            {
                throw new ErrorResponseException($"Error deserializing response for 'GetSourceSettings': {ex.Message}.", result, ex);
            }
        }

        /// <summary>
        /// Get the settings from a source item in the current scene.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>settings</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<SourceSettings> GetSourceSettings(string sourceName, CancellationToken cancellationToken = default)
            => GetSourceSettings(sourceName, null, cancellationToken);
        /// <summary>
        /// Set settings of the specified source.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="settings">Settings for the source</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetSourceSettings(string sourceName, JObject settings, string? sourceType = null, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "sourceSettings", settings }
            };
            if (sourceType != null)
            {
                requestFields.Add("sourceType", sourceType);
            }

            await SendRequest("SetSourceSettings", requestFields, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Set settings of the specified source.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="settings">Settings for the source</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task SetSourceSettings(string sourceName, JObject settings, CancellationToken cancellationToken = default)
            => SetSourceSettings(sourceName, settings, null, cancellationToken);

        /// <summary>
        /// Gets settings for a media source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<MediaSourceSettings> GetMediaSourceSettings(string sourceName, CancellationToken cancellationToken = default)
        {
            JObject? requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "sourceType", "ffmpeg_source" }
            };

            JObject? response = await SendRequest("GetSourceSettings", requestFields, cancellationToken).ConfigureAwait(false);
            return response.ToObject<MediaSourceSettings>()
                ?? throw new ErrorResponseException("Response could not be parsed into MediaSourceSettings.", response);
        }

        /// <summary>
        /// Sets settings of a media source
        /// </summary>
        /// <param name="sourceSettings"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SetMediaSourceSettings(MediaSourceSettings sourceSettings, CancellationToken cancellationToken = default)
        {
            if (sourceSettings.SourceType != "ffmpeg_source")
            {
                throw new Exception("Invalid SourceType");
            }
            JObject? requestFields = JObject.FromObject(sourceSettings);
            await SendRequest("SetSourceSettings", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Open a projector window or create a projector on a monitor. Requires OBS v24.0.4 or newer.
        /// </summary>
        /// <param name="projectorType">Type of projector: "Preview" (default), "Source", "Scene", "StudioProgram", or "Multiview" (case insensitive)</param>
        /// <param name="monitor">Monitor to open the projector on. If -1 or omitted, opens a window</param>
        /// <param name="geometry">Size and position of the projector window (only if monitor is -1). Encoded in Base64 using Qt's geometry encoding. Corresponds to OBS's saved projectors</param>
        /// <param name="name">Name of the source or scene to be displayed (ignored for other projector types)</param>
        /// <param name="cancellationToken"></param>
        public async Task OpenProjector(string projectorType = "preview", int monitor = -1, string? geometry = null,
            string? name = null, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "type", projectorType },
                { "monitor", monitor }
            };

            if (geometry != null)
            {
                requestFields.Add("geometry", geometry);
            }

            if (name != null)
            {
                requestFields.Add("name", name);
            }

            await SendRequest("OpenProjector", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Renames a source.
        /// Note: If the new name already exists as a source, obs-websocket will return an error.
        /// </summary>
        /// <param name="currentName">Current source name</param>
        /// <param name="newName">New source name</param>
        /// <param name="cancellationToken"></param>
        public async Task SetSourceName(string currentName, string newName, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sourceName", currentName },
                { "newName", newName }
            };

            await SendRequest("SetSourceName", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the audio monitoring type of the specified source.
        /// Valid return values: none, monitorOnly, monitorAndOutput
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The monitor type in use</returns>
        public async Task<string> GetAudioMonitorType(string sourceName, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = await SendRequest("GetAudioMonitorType", requestFields, cancellationToken).ConfigureAwait(false);
            return response["monitorType"]?.Value<string>() ?? throw ErrorResponseException.FromMissingProperty("monitorType", response);
        }

        /// <summary>
        /// Set the audio monitoring type of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="monitorType">The monitor type to use. Options: none, monitorOnly, monitorAndOutput</param>
        /// <param name="cancellationToken"></param>
        public async Task SetAudioMonitorType(string sourceName, string monitorType, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "monitorType", monitorType }
            };

            await SendRequest("SetAudioMonitorType", requestFields, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Broadcast custom message to all connected WebSocket clients
        /// </summary>
        /// <param name="realm">Identifier to be choosen by the client</param>
        /// <param name="data">User-defined data</param>
        /// <param name="cancellationToken"></param>
        public async Task BroadcastCustomMessage(string realm, JObject data, CancellationToken cancellationToken = default)
        {
            var requestFields = new JObject
            {
                { "realm", realm },
                { "data", data }
            };

            await SendRequest("BroadcastCustomMessage", requestFields, cancellationToken).ConfigureAwait(false);
        }



        /// <summary>
        /// Deserializes a <see cref="JObject"/> into <typeparamref name="T"/>. Throws an appropriate <see cref="ErrorResponseException"/> for errors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jObject"></param>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        public static T ParseObject<T>(JObject jObject, [CallerMemberName] string? sourceName = null)
        {
            try
            {
                T obj = jObject.ToObject<T>()
                    ?? throw ErrorResponseException.FromNullResponseObject<T>(jObject);

                return obj;
            }
            catch (JsonException ex)
            {
                if (sourceName != null)
                    throw new ErrorResponseException($"Error deserializing JSON for '{sourceName}' into '{typeof(T).Name}': {ex.Message}.", jObject, ex);
                else
                    throw new ErrorResponseException($"Error deserializing JSON into '{typeof(T).Name}': {ex.Message}.", jObject, ex);
            }
        }
    }
}
