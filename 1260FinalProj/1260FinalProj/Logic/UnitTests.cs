using _1260FinalProj.Models;
using NUnit.Framework;
using System.Xml.Linq;
using Xunit;


namespace _1260FinalProj.Logic
{
    public class UnitTestService
    {
        //logic tests
        public Searching search = new Searching();
        [Fact]
        public static void TestNAMESearch(string entitiesDir)
        {
            //arrange
            string searchName = "John Doe";
            string NAMEselectedFileContentTEST = string.Empty;
            List<Entities> FoundFiles = Searching.SearchByName(searchName, entitiesDir);
            string correctResult = "ID: 879 | Name: John Doe| Category: Person | Description: This is the most regular guy to exist. He also exists to diversify the Category results.| LastUpdate: 4/27/2026 1:24:00 PM";

            //act
            foreach (var result in FoundFiles)
            {
                NAMEselectedFileContentTEST = result.FormatItem(result);

            }
            //assert
            Assert.True(NAMEselectedFileContentTEST == correctResult);

        }

        [Fact]
        public static void TestIDSearch(string entitiesDir)
        {
            //arrange
            int searchID = 879;
            string IDselectedFileContentTEST = string.Empty;
            List<Entities> FoundFiles = Searching.SearchByID(searchID, entitiesDir);
            string correctResult = "ID: 879|Name: John Doe|Category: Person|Description: This is the most regular guy to exist. He also exists to diversify the Category results.|LastUpdate: 4/27/2026 1:24:00 PM";
            //act
            foreach (var result in FoundFiles)
            {
                IDselectedFileContentTEST = result.FormatItem(result);
            }
            //assert
            Assert.True(IDselectedFileContentTEST == correctResult);
        }

        [Fact]
        public static void TestCATEGORYSearch(string entitiesDir)
        {
            //arrange
            string searchCategory = "Person";
            string CATEGORYselectedFileContentTEST = string.Empty;
            List<Entities> FoundFiles = Searching.SearchByCategory(searchCategory, entitiesDir);
            string correctResult = "ID: 879|Name: John Doe|Category: Person|Description: This is the most regular guy to exist. He also exists to diversify the Category results.|LastUpdate: 4/27/2026 1:24:00 PM";
            //act
            foreach (var result in FoundFiles)
            {
                CATEGORYselectedFileContentTEST = result.FormatItem(result) + "\n";
            }
            //assert
            Assert.True(CATEGORYselectedFileContentTEST == correctResult);
        }

        [Fact]
        public static void TestCreateFile(string entitiesDir)
        {
            //arrange
            int ID = 123;
            string Name = "Test Entity";
            string Category = "Test Category";
            string Description = "This is a test entity.";
            DateTime LastUpdate = DateTime.Now;
            FileIO fileIO = new FileIO();
            //act
            fileIO.CreateFile(ID, Name, Category, Description, LastUpdate);
            string expectedFilePath = Path.Combine("wwwroot", "Entities", $"Test_Entity_{ID}.txt");
            bool fileExists = File.Exists(expectedFilePath);
            //assert
            Assert.True(fileExists);
        }

        public static void TestEditFile(string entitiesDir)
        {
            //arrange
            int ID = 1232;
            string Name = "Test Edit Entity";
            string Category = "Test Category";
            string Description = "This is a test entity.";
            DateTime LastUpdate = DateTime.Now;
            FileIO fileIO = new FileIO();
            fileIO.CreateFile(ID, Name, Category, Description, LastUpdate);
            string expectedFilePath = Path.Combine("wwwroot", "Entities", $"Test_Entity_{ID}.txt");
            //act
            string newDescription = "This is an edited test entity.";
            fileIO.EditFile(ID, Name, Category, newDescription, LastUpdate, expectedFilePath);
            string[] fileLines = File.ReadAllLines(expectedFilePath);
            string lastLine = fileLines.Last();
            //assert
            Assert.True(lastLine.Contains(newDescription));

        }

        public static void TestDeleteFile(string entitiesDir)
        {
            //arrange
            int ID = 1233;
            string Name = "Test Delete Entity";
            string Category = "Test Category";
            string Description = "This is a test entity.";
            DateTime LastUpdate = DateTime.Now;
            FileIO fileIO = new FileIO();
            fileIO.CreateFile(ID, Name, Category, Description, LastUpdate);
            string expectedFilePath = Path.Combine("wwwroot", "Entities", $"Test_Entity_{ID}.txt");
            //act
            fileIO.DeleteFile(expectedFilePath, Name);
            bool fileExists = File.Exists(expectedFilePath);
            //assert
            Assert.False(fileExists);

        }
    }
    
}
