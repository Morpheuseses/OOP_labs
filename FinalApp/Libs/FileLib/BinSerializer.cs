using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable SYSLIB0011 // BinaryFormatter is obsolete
namespace FileLib
{
    public class BinSerializer<T> : IFileSerializer<T>
    {
        public void Save(string path, T data)
        {
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, data);
        }

        public T Load(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(fs);
        }
    }
}
#pragma warning restore SYSLIB0011
