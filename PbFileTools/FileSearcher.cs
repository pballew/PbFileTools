using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PB.FileTools
{
    public class FileSearcher
    {
        /// <summary>
        /// Recursively searches all files in the passed in directory for files that match the set of extensions passed in. Search is case-insensitive.
        /// </summary>
        /// <param name="searchPath">Path to directory to search.</param>
        /// <param name="extensions">List of extensions to search for.  Leading '.' character is allowed but not required.</param>
        /// <returns></returns>
        public List<FileInfo> SearchAllFilesByExtensions(string searchPath, List<string> extensions)
        {
            var loweredExtensions = new List<string>();
            foreach (var extension in extensions)
            {
                var extensionWithDot = extension;
                if (!extension.StartsWith("."))
                {
                    extensionWithDot = "." + extension;
                }
                loweredExtensions.Add(extensionWithDot.ToLower());
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

        /// <summary>
        /// Recursively searches filenames of all files in the passed in directory for files that match the passed in search text. Search is case-insensitive.
        /// </summary>
        /// <param name="searchPath">Path to directory to search.</param>
        /// <param name="searchText">Text to use when searching by filename.</param>
        /// <returns></returns>
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
