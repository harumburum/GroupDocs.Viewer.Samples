using System;
using System.IO;
using GroupDocs.Viewer.Domain;
using GroupDocs.Viewer.Helper;
using Newtonsoft.Json;

namespace GroupDocs.Viewer.Samples.CustomFileDataStore
{
    public class JsonFileDataStore : IFileDataStore
    {
        private readonly DirectoryInfo _workingDirectory;

        public JsonFileDataStore(DirectoryInfo workingDirectory)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException("workingDirectory");
            if(!workingDirectory.Exists)
                throw new DirectoryNotFoundException("Unable to find directory '" + workingDirectory.FullName + "'");

            _workingDirectory = workingDirectory;
        }

        public FileData GetFileData(FileDescription fileDescription)
        {
            if(fileDescription == null)
                throw new ArgumentNullException("fileDescription");

            var path = GetFilePath(fileDescription);
            var json = File.ReadAllText(path);

            if (fileDescription.DocumentType == "Words")
                return JsonConvert.DeserializeObject<WordsFileData>(json);

            return JsonConvert.DeserializeObject<FileData>(json);
        }

        public void SaveFileData(FileDescription fileDescription, FileData fileData)
        {
            if (fileDescription == null)
                throw new ArgumentNullException("fileDescription");
            if (fileData == null)
                throw new ArgumentNullException("fileData");

            var path = GetFilePath(fileDescription);
            var json = JsonConvert.SerializeObject(fileData);

            var dir = Path.GetDirectoryName(path);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(path, json);
        }

        private string GetFilePath(FileDescription fileDescription)
        {
            string directoryName = fileDescription.Guid;

            const char replacementCharacter = '_';
            const string fileName = "file-data.json";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
                directoryName = directoryName.Replace(invalidChar, replacementCharacter);

            string directoryPath = Path.Combine(_workingDirectory.FullName, directoryName);

            return Path.Combine(directoryPath, fileName);
        }
    }
}
