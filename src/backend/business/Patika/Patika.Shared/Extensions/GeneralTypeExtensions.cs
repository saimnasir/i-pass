using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace Patika.Shared.Extensions
{
    public static class GeneralTypeExtensions
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
        public static Guid NewSequentalGuid(this Guid obj)
        {
            var randomBytes = new byte[10];
            RandomNumberGenerator.GetBytes(randomBytes);
            long timestamp = DateTime.UtcNow.Ticks / 10000L;

            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

            return new Guid(guidBytes);
        }

        public static string ToJson(this object obj) => JsonSerializer.Serialize(obj);
        public static T JsonTo<T>(this string json) => JsonSerializer.Deserialize<T>(json);

        public static T JsonToOrNull<T>(this string json) where T: class
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return null;
            }
        }
        public static Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ExecuteAsync();

            async Task<List<T>> ExecuteAsync()
            {
                var list = new List<T>();

                await foreach (var element in source)
                {
                    list.Add(element);
                }

                return list;
            }
        }


        public static T NewId<T>(T id)
        {
            if (typeof(T) != typeof(Guid) && id != null)
                return id;
            if (typeof(T) == typeof(Guid) && id.ToString() != Guid.Empty.ToString())
                return id;
            if (typeof(T) == typeof(Guid))
                return (T)Convert.ChangeType(new Guid().NewSequentalGuid(), typeof(T));
            return default;
        }
    }
}