using Microsoft.VisualStudio.TestTools.UnitTesting;
using PB.FileTools;
using System.Collections.Generic;
using System.Linq;

namespace PbFileToolsTests
{
    [TestClass]
    public class FileSearcherTests
    {
        string _testDataDirectory = @"..\..\..\TestData\";
        FileSearcher _fileSearcher = new FileSearcher();
        List<string> _extensions = new List<string>();

        [TestMethod]
        public void SearchAllFilesByExtensions_TxtExtension_FileFound()
        {
            //Arrange
            string extension = ".txt";
            _extensions.Add(extension);

            //Act
            var files = _fileSearcher.SearchAllFilesByExtensions(_testDataDirectory, _extensions);

            //Assert
            Assert.IsTrue(files.Count > 0);
            foreach (var file in files)
            {
                Assert.AreEqual(extension, file.Extension);
            }
        }

        [TestMethod]
        public void SearchAllFilesByExtensions_MixedCase_FileFound()
        {
            //Arrange
            string extension = ".tXt";
            _extensions.Add(extension);

            //Act
            var files = _fileSearcher.SearchAllFilesByExtensions(_testDataDirectory, _extensions);

            //Assert
            Assert.IsTrue(files.Count > 0);
            foreach (var file in files)
            {
                Assert.AreEqual(extension.ToLower(), file.Extension);
            }
        }

        [TestMethod]
        public void SearchAllFilesByExtensions_InSubdir_FileFound()
        {
            //Arrange
            string extension = ".dir1";
            _extensions.Add(extension);

            //Act
            var files = _fileSearcher.SearchAllFilesByExtensions(_testDataDirectory, _extensions);

            //Assert
            Assert.IsTrue(files.Count > 0);
            foreach (var file in files)
            {
                Assert.AreEqual(extension, file.Extension);
            }
        }

        [TestMethod]
        public void SearchAllFilesByExtensions_ExtensionNotExist_NoFilesFound()
        {
            //Arrange
            string extension = ".notexist";
            _extensions.Add(extension);

            //Act
            var files = _fileSearcher.SearchAllFilesByExtensions(_testDataDirectory, _extensions);

            //Assert
            Assert.AreEqual(files.Count, 0);
        }

        [TestMethod]
        public void SearchAllFilesByText_TextMatchesFile_FileFound()
        {
            //Arrange
            string searchText = "orem";

            //Act
            var files = _fileSearcher.SearchAllFilesByText(_testDataDirectory, searchText);

            //Assert
            Assert.AreEqual(1, files.Count);
            var file = files.First();
            Assert.IsTrue(file.Name.Contains(searchText));
        }

        [TestMethod]
        public void SearchAllFilesByText_InSubdir_FileFound()
        {
            //Arrange
            string searchText = "ile";

            //Act
            var files = _fileSearcher.SearchAllFilesByText(_testDataDirectory, searchText);

            //Assert
            Assert.AreEqual(1, files.Count);
            var file = files.First();
            Assert.IsTrue(file.Name.Contains(searchText));
        }

        [TestMethod]
        public void SearchAllFilesByText_NonMatchingText_NoFilesFound()
        {
            //Arrange
            string searchText = "nonMatching";

            //Act
            var files = _fileSearcher.SearchAllFilesByText(_testDataDirectory, searchText);

            //Assert
            Assert.AreEqual(0, files.Count);
        }
    }
}
