using Microsoft.AspNetCore.Mvc.Formatters;

namespace _1260FinalProj.Models
{
    public class Entities
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateDT { get; set; }
        public int LastUpdate { get; set; }

        public int UpdateINT()
        {
            LastUpdate = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds; //auto-generated, idk if this works?
            return LastUpdate;
        }

        //something to generate a random, *UNIQUE* ID 


        public Entities(int v1, string v2, string v3, string v4, int v5)
        {
            this.ID = new Random().Next(1, 1000000); // This is a simple way to generate a random ID. In a real application, you would want to ensure uniqueness and handle potential collisions.
            this.Name = v2;
            this.Category = v3;
            this.Description = v4;
            this.LastUpdate = v5;
        }

        public Entities() { } //allows empty entity created, attributed added later
        public string FormatItem()
        {
            return $" ID: {ID.ToString()} \n Name:   {Name} \n Category:   {Category} \n Description:   {Description} \n Last Updated: {LastUpdate}";

        }
    }
}
