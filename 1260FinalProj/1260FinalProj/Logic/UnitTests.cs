using _1260FinalProj.Models;
using NUnit.Framework;
using Xunit;


namespace _1260FinalProj.Logic
{
    public class LogicUnitTests
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
            string correctResult = "ID: 879|Name: John Doe|Category: Person|Description: This is the most regular guy to exist. He also exists to diversify the Category results.|LastUpdate: 4/27/2026 1:24:00 PM";

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


    }
    public class PageUnitTests
    {
        //page tests


    }
}
