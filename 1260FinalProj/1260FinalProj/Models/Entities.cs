using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics;

namespace _1260FinalProj.Models
{
    public class Entities
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Identifier too long (64 character limit).")]
        public string? Category { get; set; }
        public List<string> Categories = new List<string>() { "Person", "Place", "Thing", "Idea" }; //default list of categories; can be added to by user
        public string? Description { get; set; }
        public DateTime LastUpdateDT { get; set; }
        public int LastUpdate { get; set; }

        public object Entity; 

        public int UpdateINT()
        {
            LastUpdate = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return LastUpdate;
        }


        public Entities(int v1, string v2, string v3, string v4)
        {
            this.ID = new Random().Next(1, 1000000); //simple way to generate random ID. figure a better way to ensure uniqueness later
            this.Name = v2;
            this.Category = v3;
            this.Description = v4;
            this.LastUpdateDT = DateTime.Now;
            this.LastUpdate = UpdateINT();
        }

        public Entities() { } //allows empty entity created, attributed added later
        public string FormatItem()
        {
            return $" ID: {ID} | Name: {Name} | Category: {Category} | Description: {Description} | Last Updated: {LastUpdateDT}";

        }
    }
}
