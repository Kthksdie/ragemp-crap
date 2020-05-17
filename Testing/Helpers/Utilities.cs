using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Helpers {
    public static class Utilities {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            TextWriter writer = null;
            try {
                var jsonSerializerSettings = new JsonSerializerSettings {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Include
                };
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, jsonSerializerSettings);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally {
                if (writer != null) {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new() {
            TextReader reader = null;
            try {
                if (!File.Exists(filePath)) {
                    return default(T);
                }
                var jsonSerializerSettings = new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Include
                };
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents, jsonSerializerSettings);
            }
            finally {
                if (reader != null) {
                    reader.Close();
                }
            }
        }
    }
}
