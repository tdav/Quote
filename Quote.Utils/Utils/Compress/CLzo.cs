using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Utils.Compress
{
    public class CLzo
    {
        public static byte[] Compress(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            return MiniLZO.Decompress(bytes);
        }

        public static byte[] Compress<T>(T obj)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Compress(str);
        }

        public static string Decompress(byte[] bytes)
        {
            var ba = MiniLZO.Decompress(bytes);
            return Encoding.UTF8.GetString(ba);
        }

        public static T Decompress<T>(Stream msi)
        {
            using (var mso = new MemoryStream())
            {
                msi.CopyTo(mso);
                var ba = mso.ToArray();

                var s = Encoding.UTF8.GetString(MiniLZO.Decompress(ba));
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
            }
        }
        public static async Task<T> DecompressAsync<T>(Stream msi)
        {
            using (var mso = new MemoryStream())
            {
                await msi.CopyToAsync(mso);
                var ba = mso.ToArray();

                var s = Encoding.UTF8.GetString(MiniLZO.Decompress(ba));
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
            }
        }

        public static T Decompress<T>(byte[] bytes)
        {
            var ba = MiniLZO.Decompress(bytes);
            var s = Encoding.UTF8.GetString(ba);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
        }
    }
}
