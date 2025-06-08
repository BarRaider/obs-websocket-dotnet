#nullable enable
using OBSWebsocketDotNet;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// JsonSerializer 的靜態輔助類，提供 PopulateObject 方法
    /// 使用方式：JsonSerializer.PopulateObject(json, obj, options)
    /// </summary>
    public static partial class JsonSerializer2
    {
        /// <summary>
        /// 使用指定的 JSON 字串來填充現有物件的屬性
        /// 此方法提供了與 Newtonsoft.Json JsonConvert.PopulateObject 類似的功能
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 字串</param>
        /// <param name="obj">要被填充的現有物件</param>
        /// <param name="options">JSON 序列化選項，如果為 null 則使用預設值</param>
        /// <exception cref="JsonException">當反射被停用或無法找到型別資訊時拋出</exception>
        [RequiresUnreferencedCode("Use PopulateObject<T>(string json, T obj, JsonSerializerContext context) instead")]
        [RequiresDynamicCode("Use PopulateObject<T>(string json, T obj, JsonSerializerContext context) instead")]
        public static void PopulateObject<T>(string json, T obj, JsonSerializerOptions? options = null) where T : class
        {
            JsonExtensions.PopulateObject(json.AsSpan(), obj, options);
        }

        /// <summary>
        /// 使用指定的 JSON 內容來填充現有物件的屬性
        /// 此方法提供了與 Newtonsoft.Json JsonConvert.PopulateObject 類似的功能
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 內容</param>
        /// <param name="obj">要被填充的現有物件</param>
        /// <param name="options">JSON 序列化選項，如果為 null 則使用預設值</param>
        /// <exception cref="JsonException">當反射被停用或無法找到型別資訊時拋出</exception>
        [RequiresUnreferencedCode("Use PopulateObject<T>(ReadOnlySpan<char> json, T obj, JsonSerializerContext context) instead")]
        [RequiresDynamicCode("Use PopulateObject<T>(ReadOnlySpan<char> json, T obj, JsonSerializerContext context) instead")]
        public static void PopulateObject<T>(ReadOnlySpan<char> json, T obj, JsonSerializerOptions? options = null) where T : class
        {
            JsonExtensions.PopulateObject(json, obj, options);
        }

        /// <summary>
        /// 使用指定的 JSON 字串和序列化上下文來填充現有物件的屬性
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 字串</param>
        /// <param name="obj">要被填充的現有物件</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <exception cref="ArgumentNullException">當 context 為 null 時拋出</exception>
        public static void PopulateObject<T>(string json, T obj, JsonSerializerContext context) where T : class
        {
            JsonExtensions.PopulateObject(json, obj, context);
        }

        /// <summary>
        /// 使用指定的 JSON 內容和序列化上下文來填充現有物件的屬性
        /// 這是 AOT 編譯友好的版本，不需要反射
        /// </summary>
        /// <typeparam name="T">要填充的物件型別</typeparam>
        /// <param name="json">包含要填充資料的 JSON 內容</param>
        /// <param name="obj">要被填充的現有物件</param>
        /// <param name="context">JSON 序列化上下文，包含型別資訊</param>
        /// <exception cref="ArgumentNullException">當 context 為 null 時拋出</exception>
        public static void PopulateObject<T>(ReadOnlySpan<char> json, T obj, JsonSerializerContext context) where T : class
        {
            JsonExtensions.PopulateObject(json, obj, context);
        }
    }
}
