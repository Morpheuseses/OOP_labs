

public interface ILogger<T>
{
    void Append(string path, T data);
}