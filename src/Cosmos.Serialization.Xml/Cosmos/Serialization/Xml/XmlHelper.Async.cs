using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Conversions;

namespace Cosmos.Serialization.Xml
{
    /// <summary>
    /// Xml Helper
    /// </summary>
    public static partial class XmlHelper
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="o"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<string> SerializeAsync<T>(T o, Encoding encoding = null)
        {
            return await SerializeAsync(o, typeof(T), encoding);
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> SerializeAsync(object o, Type type, Encoding encoding = null)
        {
            if (o is null)
                return string.Empty;

            encoding ??= XmlManager.DefaultEncoding;
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var stream = await PackAsync(o, type);
#else
            using var stream = await PackAsync(o, type);
#endif
            return encoding.GetString(await stream.CastToBytesAsync());
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<byte[]> SerializeToBytesAsync<T>(T o)
        {
            return await SerializeToBytesAsync(o, typeof(T));
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<byte[]> SerializeToBytesAsync(object o, Type type)
        {
            if (o is null)
                return new byte[0];
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var stream = await PackAsync(o, type);
#else
            using var stream = await PackAsync(o, type);
#endif
            return await stream.CastToBytesAsync();
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> DeserializeAsync<T>(string xml, Encoding encoding = null)
        {
            return string.IsNullOrWhiteSpace(xml)
                ? default
                : (T) await DeserializeAsync(xml, typeof(T), encoding);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<object> DeserializeAsync(string xml, Type type, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return null;

            encoding ??= XmlManager.DefaultEncoding;
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var memoryStream = new MemoryStream(encoding.GetBytes(xml));
#else
            using var memoryStream = new MemoryStream(encoding.GetBytes(xml));
#endif
            return await UnpackAsync(memoryStream, type);
        }

        /// <summary>
        /// Deserialize from bytes
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> DeserializeFromBytesAsync<T>(byte[] data)
        {
            return data is null || data.Length is 0
                ? default
                : (T) await DeserializeFromBytesAsync(data, typeof(T));
        }

        /// <summary>
        /// Deserialize from bytes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<object> DeserializeFromBytesAsync(byte[] data, Type type)
        {
            if (data is null || data.Length == 0)
                return null;
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var memoryStream = new MemoryStream(data);
#else
            using var memoryStream = new MemoryStream(data);
#endif
            return await UnpackAsync(memoryStream, type);
        }
    }
}