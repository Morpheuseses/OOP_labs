
namespace FileLib;
public interface IFileSerializer<T>
{
    void Save(string path, T data);
    T Load(string path);
}
