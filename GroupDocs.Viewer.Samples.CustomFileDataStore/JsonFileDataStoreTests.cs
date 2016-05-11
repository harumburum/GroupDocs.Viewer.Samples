using System;
using System.IO;
using GroupDocs.Viewer.Domain;
using NUnit.Framework;

namespace GroupDocs.Viewer.Samples.CustomFileDataStore
{
    [TestFixture]
    public class JsonFileDataStoreTest
    {
        [Test]
        public void ShouldSaveGetFileData()
        {
            const string guid = "document.doc";
            DirectoryInfo workingDir = new DirectoryInfo("c:\\storage\\");

            JsonFileDataStore jsonFileDataStore = new JsonFileDataStore(workingDir);

            DateTime today = DateTime.Now.Date;

            FileData source = new FileData();
            source.DateCreated = today;
            source.DateModified = today;
            source.PageCount = 1;

            FileDescription fileDescription = new FileDescription(guid);

            jsonFileDataStore.SaveFileData(fileDescription, source);

            FileData extractedFileData = jsonFileDataStore.GetFileData(fileDescription);

            Assert.AreEqual(today, extractedFileData.DateModified);
            Assert.AreEqual(today, extractedFileData.DateCreated);
            Assert.AreEqual(1, extractedFileData.PageCount);
        }
    }
}
