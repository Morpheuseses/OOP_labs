using System;
using System.IO;
using System.Runtime.Serialization;

namespace FileLib
{
    public class BinSerializer<T> : IFileSerializer<T>
    {
        private readonly DataContractSerializer serializer = new(typeof(T));

        public void Save(string path, T data)
        {
            using var fs = new FileStream(path, FileMode.Create);
            serializer.WriteObject(fs, data);
        }

        public T Load(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            return (T)serializer.ReadObject(fs)!;
        }
    }
}
