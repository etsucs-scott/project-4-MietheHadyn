using _1260FinalProj.Logic;
using static _1260FinalProj.Logic.Searching;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _1260FinalProj.Models;
using System.Collections.Generic;
namespace _1260FinalProj.Services //Singleton service???
{
    public class SingletonSearchService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        //holder to avoid accessing properties on System.Object
        private class CategoryTotal
        {
            public string Category { get; set; } = string.Empty;
            public int Count { get; set; }
        }

        Dictionary<string, int> CategoryCounts = new Dictionary<string, int>();
        List<string> CategoryNames = new List<string>() /*{ "Person", "Place", "Thing", "Idea" }*/; //this, with the 
        List<CategoryTotal> CatTotals = new List<CategoryTotal>();

        public Dictionary<string, int>  FindTotals(string entitiesDir)
        {
            

            //Get category names 
            CategoryNames = Searching.GetCategoryNames(entitiesDir) ?? new List<string>();

            //Get category counts
            CategoryCounts = Searching.CategoryQTY(entitiesDir, CategoryNames);
                            


            //Build a typed list for binding (keep CategoryCounts as a Dictionary)
            CatTotals = CategoryNames
                .Select(cat => new CategoryTotal { Category = cat, Count = CategoryCounts.TryGetValue(cat, out var c) ? c : 0 })
                .ToList();

            return CategoryCounts;
        }


        


    }
}
