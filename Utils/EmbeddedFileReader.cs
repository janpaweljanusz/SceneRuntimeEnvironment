using System;
using System.Reflection;

namespace Utils
{
    public class EmbeddedFileReader
    {
        public string ReadFromEmbeddedFile(string embeddedPath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream(embeddedPath) ?? throw new Exception("no stream from assembly.GetManifestResourceStream");
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
