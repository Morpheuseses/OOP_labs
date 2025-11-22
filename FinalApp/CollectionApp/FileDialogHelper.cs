using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using FileLib;
using CollectionLib;
using JournalLib;

namespace CollectionApp.Helpers
{
    public static class FileDialogsHelper
    {
        public static async Task<NewAssessmentTree?> OpenFileCollectionAsync(Window parent)
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters =
                {
                    new FileDialogFilter { Name = "All Supported", Extensions = { "json", "xml", "bin" } }
                }
            };

            var result = await dialog.ShowAsync(parent);
            var path = result?.FirstOrDefault();
            if (path == null) return null;

            IFileSerializer<NewAssessmentTree> serializer = path switch
            {
                var p when p.EndsWith(".json") => new JsonSerializerWrapper<NewAssessmentTree>(),
                var p when p.EndsWith(".xml")  => new XmlSerializerWrapper<NewAssessmentTree>(),
                var p when p.EndsWith(".bin")  => new BinSerializer<NewAssessmentTree>(),
                _ => throw new Exception("Неизвестный формат файла")
            };

            return serializer.Load(path);
        }

        public static async Task SaveFileCollectionAsync(Window parent, NewAssessmentTree tree)
        {
            var dialog = new SaveFileDialog
            {
                Filters =
                {
                    new FileDialogFilter { Name = "JSON", Extensions = { "json" } },
                    new FileDialogFilter { Name = "XML", Extensions = { "xml" } },
                    new FileDialogFilter { Name = "Binary", Extensions = { "bin" } }
                }
            };

            var path = await dialog.ShowAsync(parent);
            if (path == null) return;

            IFileSerializer<NewAssessmentTree> serializer = path switch
            {
                var p when p.EndsWith(".json") => new JsonSerializerWrapper<NewAssessmentTree>(),
                var p when p.EndsWith(".xml")  => new XmlSerializerWrapper<NewAssessmentTree>(),
                var p when p.EndsWith(".bin")  => new BinSerializer<NewAssessmentTree>(),
                _ => throw new Exception("Неизвестный формат файла")
            };

            serializer.Save(path, tree);
        }
    
        public static async Task<JournalTxt?> OpenJournalAsync(Window parent)
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters =
                {
                    new FileDialogFilter { Name = "TXT", Extensions = { "txt" } }
                }
            };

            var result = await dialog.ShowAsync(parent);
            var path = result?.FirstOrDefault();
            if (path == null) return null;

            return new JournalTxt(path);
        }
        public static async Task SaveJournalAsync(Window parent, JournalTxt journal)
        {
            var dialog = new SaveFileDialog
            {
                Filters =
                {
                    new FileDialogFilter { Name = "TXT", Extensions = { "txt" } }
                }
            };

            var path = await dialog.ShowAsync(parent);
            if (path == null) return;

            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            await File.WriteAllTextAsync(path, journal.ToString());
        }
    }
}
