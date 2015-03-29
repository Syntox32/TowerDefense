using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace ProjectGamma.Utilities
{
    public sealed class JsonConfig
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, dynamic> LoadFromFile(string path)
        {
            return Load(File.ReadAllText(path));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, dynamic> Load(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LoadFromFile<T>(string path)
        {
            return Load<T>(File.ReadAllText(path));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Load<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
