#nullable enable
using OBSWebsocketDotNet;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace OBSWebsocketDotNet
{
    public static class JsonObjectExtensions
    {
          /// <summary>
        /// Merges the properties of the specified source <see cref="JsonObject"/> into the target <see
        /// cref="JsonObject"/>.
        /// </summary>
        /// <remarks>If a property in the source <see cref="JsonObject"/> has the same key as a property
        /// in the target <see cref="JsonObject"/>, the value from the source will overwrite the value in the target.
        /// Properties in the target that do not exist in the source will remain unchanged.</remarks>
        /// <param name="dist">The target <see cref="JsonObject"/> to which properties will be added or updated.</param>
        /// <param name="source">The source <see cref="JsonObject"/> containing the properties to merge into the target.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dist"/> or <paramref name="source"/> is null.</exception>
        public static void Merge(this JsonObject dist, JsonObject source)
        {
            ArgumentNullException.ThrowIfNull(dist);
            ArgumentNullException.ThrowIfNull(source);

            // 遍歷來源 JsonObject 的所有屬性
            foreach (var kvp in source)
            {
                string propertyName = kvp.Key;
                JsonNode? sourceValue = kvp.Value;

                // 檢查目標 JsonObject 是否已經包含此屬性
                if (dist.ContainsKey(propertyName))
                {
                    JsonNode? targetValue = dist[propertyName];
                    
                    // 如果來源和目標的值都是 JsonObject，則遞迴合併
                    if (sourceValue is JsonObject sourceObj && targetValue is JsonObject targetObj)
                    {
                        targetObj.Merge(sourceObj);
                    }
                    else
                    {
                        // 否則直接覆蓋目標值
                        dist[propertyName] = sourceValue?.DeepClone();
                    }
                }
                else
                {
                    // 如果目標中不存在此屬性，則直接添加（深拷貝以避免引用共享）
                    dist[propertyName] = sourceValue?.DeepClone();
                }
            }
        }               /// <summary>
        /// 將 JsonObject 轉換為指定類型的新物件實例，使用 JsonSerializerOptions
        /// 此方法提供了與 Newtonsoft.Json JObject.ToObject&lt;T&gt;() 類似的功能
        /// </summary>
        /// <typeparam name="T">要轉換的目標類型</typeparam>
        /// <param name="obj">要轉換的 JsonObject</param>
        /// <param name="options">JSON 序列化選項，如果為 null 則使用預設值</param>
        /// <returns>轉換後的新物件實例</returns>
        /// <exception cref="ArgumentNullException">當 obj 為 null 時拋出</exception>
        /// <exception cref="JsonException">當轉換失敗時拋出</exception>
        [RequiresUnreferencedCode("Use ToObject<T>(this JsonObject obj, JsonSerializerContext context) instead")]
        [RequiresDynamicCode("Use ToObject<T>(this JsonObject obj, JsonSerializerContext context) instead")]
        public static T ToObject<T>(this JsonObject obj, JsonSerializerOptions? options = null) where T : class
        {
            return obj.Deserialize<T>(options) ?? throw new JsonException($"Failed to deserialize JsonObject to type {typeof(T).Name}: result is null");
        }        /// <summary>
        /// 將 JsonObject 轉換為指定類型的新物件實例，使用 JsonSerializerContext
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">要轉換的目標類型</typeparam>
        /// <param name="obj">要轉換的 JsonObject</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <returns>轉換後的新物件實例</returns>
        /// <exception cref="ArgumentNullException">當 obj 或 context 為 null 時拋出</exception>
        /// <exception cref="JsonException">當轉換失敗時拋出</exception>
        public static T ToObject<T>(this JsonObject obj, JsonSerializerContext context) where T : class
        {
            ArgumentNullException.ThrowIfNull(obj);
            ArgumentNullException.ThrowIfNull(context);
            
            var typeInfo = context.GetTypeInfo(typeof(T));
            if (typeInfo == null)
                throw new JsonException($"No JsonTypeInfo found for type {typeof(T).Name} in the provided context");
                
            return obj.Deserialize(typeInfo) as T ?? throw new JsonException($"Failed to deserialize JsonObject to type {typeof(T).Name}: result is null");
        }

        public static bool HasValue(this JsonObject obj, string key)
        {
            ArgumentNullException.ThrowIfNull(obj);
            ArgumentNullException.ThrowIfNull(key);
            return obj.ContainsKey(key) && obj[key] is not null;
        }

        public static bool HasValues(this JsonObject obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            return obj.Count > 0;
        }

        public static bool ContainsKey(this JsonNode node, string key)
        {
            try
            {
                var _node = node.AsObject();

                return _node.ContainsKey(key);
            }
            catch (InvalidCastException)
            {
                // 如果 node 不是 JsonObject，則返回 false
                return false;
            }
            catch (NullReferenceException)
            {
                // 如果 node 為 null，則返回 false
                return false;
            }
            catch (Exception ex)
            {
                // 捕獲其他異常，這裡可以根據需要進行處理或記錄
                throw new JsonException($"Error checking key '{key}' in JsonNode: {ex.Message}", ex);
            }

        }

        /// <summary>
        /// 將物件序列化為 JsonNode，使用 JsonSerializerOptions
        /// 此方法提供了與 Newtonsoft.Json JToken.FromObject() 類似的功能
        /// </summary>
        /// <param name="obj">要序列化的物件</param>
        /// <param name="options">JSON 序列化選項，如果為 null 則使用預設值</param>
        /// <returns>序列化後的 JsonNode</returns>
        /// <exception cref="JsonException">當序列化失敗時拋出</exception>
        [RequiresUnreferencedCode("Use FromObject(object obj, JsonSerializerContext context) instead")]
        [RequiresDynamicCode("Use FromObject(object obj, JsonSerializerContext context) instead")]
        public static JsonNode? FromObject(object? obj, JsonSerializerOptions? options = null)
        {
            if (obj == null)
                return null;

            try
            {
                return JsonSerializer.SerializeToNode(obj, options);
            }
            catch (Exception ex)
            {
                throw new JsonException($"Failed to serialize object of type {obj.GetType().Name} to JsonNode", ex);
            }
        }        /// <summary>
        /// 將物件序列化為 JsonNode，使用 JsonSerializerContext
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">物件的類型</typeparam>
        /// <param name="obj">要序列化的物件</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <returns>序列化後的 JsonNode</returns>
        /// <exception cref="ArgumentNullException">當 context 為 null 時拋出</exception>
        /// <exception cref="JsonException">當序列化失敗時拋出</exception>
        public static JsonNode? FromObject<T>(T obj, JsonSerializerContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            
            if (obj == null)
                return null;

            try
            {
                var typeInfo = context.GetTypeInfo(typeof(T));
                if (typeInfo == null)
                    throw new JsonException($"No JsonTypeInfo found for type {typeof(T).Name} in the provided context");
                    
                return JsonSerializer.SerializeToNode(obj, typeInfo);
            }
            catch (Exception ex)
            {
                throw new JsonException($"Failed to serialize object of type {typeof(T).Name} to JsonNode", ex);
            }
        }
    }
}