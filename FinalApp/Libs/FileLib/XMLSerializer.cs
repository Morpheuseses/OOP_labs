using System.IO;
using System.Xml.Serialization;

namespace FileLib;
public class XmlSerializerWrapper<T> : IFileSerializer<T>
{
    public void Save(string path, T data)
    {
        var xs = new XmlSerializer(data.GetType());
        using var fs = new FileStream(path, FileMode.Create);
        xs.Serialize(fs, data);
    }

    public T Load(string path)
    {
        var xs = new XmlSerializer(typeof(T));
        using var fs = new FileStream(path, FileMode.Open);
        return (T)xs.Deserialize(fs)!;
    }
}
