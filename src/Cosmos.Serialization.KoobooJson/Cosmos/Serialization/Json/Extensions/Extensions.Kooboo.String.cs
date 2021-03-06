﻿using System;
using System.Threading.Tasks;
using Kooboo.Json;
using Cosmos.Serialization.Json.Kooboo;

// ReSharper disable once CheckNamespace
namespace Cosmos.Serialization.Json
{
    using K = KoobooJsonHelper;

    /// <summary>
    /// KoobooJson extensions
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// From KoobooJson
        /// </summary>
        /// <param name="json"></param>
        /// <param name="option"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromKoobooJson<T>(this string json, JsonDeserializeOption option = null)
        {
            return K.Deserialize<T>(json, option);
        }

        /// <summary>
        /// From KoobooJson
        /// </summary>
        /// <param name="json"></param>
        /// <param name="optionAct"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromKoobooJson<T>(this string json, Action<JsonDeserializeOption> optionAct)
        {
            return K.Deserialize<T>(json, optionAct);
        }

        /// <summary>
        /// From KoobooJson
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static object FromKoobooJson(this string json, Type type, JsonDeserializeOption option = null)
        {
            return K.Deserialize(json, type, option);
        }

        /// <summary>
        /// From KoobooJson
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="optionAct"></param>
        /// <returns></returns>
        public static object FromKoobooJson(this string json, Type type, Action<JsonDeserializeOption> optionAct)
        {
            return K.Deserialize(json, type, optionAct);
        }

        /// <summary>
        /// From KoobooJson async
        /// </summary>
        /// <param name="json"></param>
        /// <param name="option"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> FromKoobooJsonAsync<T>(this string json, JsonDeserializeOption option = null)
        {
            return K.DeserializeAsync<T>(json, option);
        }

        /// <summary>
        /// From KoobooJson async
        /// </summary>
        /// <param name="json"></param>
        /// <param name="optionAct"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> FromKoobooJsonAsync<T>(this string json, Action<JsonDeserializeOption> optionAct)
        {
            return K.DeserializeAsync<T>(json, optionAct);
        }

        /// <summary>
        /// From KoobooJson async
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<object> FromKoobooJsonAsync(this string json, Type type, JsonDeserializeOption option = null)
        {
            return K.DeserializeAsync(json, type, option);
        }

        /// <summary>
        /// From KoobooJson async
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="optionAct"></param>
        /// <returns></returns>
        public static Task<object> FromKoobooJsonAsync(this string json, Type type, Action<JsonDeserializeOption> optionAct)
        {
            return K.DeserializeAsync(json, type, optionAct);
        }
    }
}