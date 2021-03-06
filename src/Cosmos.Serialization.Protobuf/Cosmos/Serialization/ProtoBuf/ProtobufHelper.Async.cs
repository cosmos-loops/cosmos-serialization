using System;
using System.IO;
using System.Threading.Tasks;
using Cosmos.Conversions;

namespace Cosmos.Serialization.ProtoBuf
{
    /// <summary>
    /// Google protobuf helper
    /// </summary>
    public static partial class ProtobufHelper
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<byte[]> SerializeAsync(object obj)
        {
            if (obj is null)
                return new byte[0];
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var stream = await PackAsync(obj);
#else
            using var stream = await PackAsync(obj);
#endif
            return await stream.CastToBytesAsync();
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> DeserializeAsync<T>(byte[] data)
        {
            if (data is null || data.Length == 0)
                return default;

            return (T) await DeserializeAsync(data, typeof(T));
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<object> DeserializeAsync(byte[] data, Type type)
        {
            if (data is null || data.Length == 0)
                return default;
#if !NETFRAMEWORK && !NETSTANDARD2_0
            await using var ms = new MemoryStream(data);
#else
            using var ms = new MemoryStream(data);
#endif
            return await UnpackAsync(ms, type);
        }
    }
}