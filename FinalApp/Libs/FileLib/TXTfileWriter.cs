using System;
using System.IO;

namespace FileLib
{
    public static class TextFileWriter
    {
        public static void Write(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public static void Append(string path, string text)
        {
            File.AppendAllText(path, text);
        }

        public static string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл не найден: {path}");
            return File.ReadAllText(path);
        }
    }
}
