using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PB.FileTools
{
    public class FileSearcher
    {
        public List<FileInfo> SearchAllFilesByExtensions(string searchPath, List<string> extensions)
        {
            var loweredExtensions = new List<string>();
            foreach(var extension in extensions)
            {
                loweredExtensions.Add(extension.ToLower());
            }
            extensions = loweredExtensions;

            var filesInPath = Directory.GetFiles(searchPath);
            var matchingFiles = filesInPath
                .Where(x => extensions.Contains(Path.GetExtension(x).ToLower()))
                .Select(x => new FileInfo(x))
                .ToList();

            foreach (var path in Directory.EnumerateDirectories(searchPath))
            {
                matchingFiles.AddRange(SearchAllFilesByExtensions(path, extensions));
            }

            return matchingFiles;
        }

        public List<FileInfo> SearchAllFilesByText(string searchPath, string searchText)
        {
            searchText = searchText.ToLower();
            var filesInPath = Directory.GetFiles(searchPath);
            var matchingFiles = filesInPath
                .Where(x => x.ToLower().Contains(searchText))
                .Select(x => new FileInfo(x))
                .ToList();

            foreach (var path in Directory.EnumerateDirectories(searchPath))
            {
                matchingFiles.AddRange(SearchAllFilesByText(path, searchText));
            }

            return matchingFiles;
        }
    }
}
