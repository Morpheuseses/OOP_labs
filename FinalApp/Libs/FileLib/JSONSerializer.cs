using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using CollectionLib;
using Lib;

namespace FileLib;
public class JsonSerializerWrapper<T> : IFileSerializer<T>
{
    private JsonSerializerOptions Options => new()
    {
        WriteIndented = true,
        IncludeFields = true,
    };

    public void Save(string path, T data)
    {
        var json = JsonSerializer.Serialize(data, Options);
        File.WriteAllText(path, json);
    }

    public T Load(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, Options)!;
    }
}
