using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;


namespace _1260FinalProj.Logic
{
    public class Searching
    {
        public int SearchINT { get; set; }
        public string SearchTerm { get; set; }
        
        public List<String> SearchByID(int ID, string Entitypath) //returns file path or file contents
        {
            List<string> FoundFiles = new List<string>();
            using (var reader = new StreamReader(Entitypath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue; //skip empty lines
                    string[] parts = line.Split('|');  //split on '|'
                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length < 2) continue; //skip malformed entries
                        string key = keyValue[0].Trim(); //label
                        string value = keyValue[1].Trim(); //value
                        if (key.Equals("ID", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out int parsedID) && parsedID == ID)
                        {
                            Console.WriteLine($"Match found: {line}"); //temp. return file path or contents
                            FoundFiles.Add(Entitypath);

                        }
                        else
                        {
                            Console.WriteLine($"No match found for Name: {ID} in line: {line}");
                            Entitypath = Path.Combine("wwwroot", "Entities", $"TextFile.txt"); //idek if this works tbh

                        }
                    }
                }
                return FoundFiles;
            }

        }

        public List<string> SearchByName(string Name, string Entitypath) //idk if this works, nor can I really test it rn
        {
            List<string> FoundFiles = new List<string>();
            using (var reader = new StreamReader(Entitypath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue; //skip empty lines
                    string[] parts = line.Split('|');  //split on '|'
                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length < 2) continue; //skip malformed entries
                        string key = keyValue[0].Trim(); //label
                        string value = keyValue[1].Trim(); //value
                        if (key.Equals("Name", StringComparison.OrdinalIgnoreCase) && value.Equals(Name, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Match found: {line}"); //temp. return file path or contents
                            FoundFiles.Add(Entitypath);
                            
                        }
                        else
                        {
                            Console.WriteLine($"No match found for Name: {Name} in line: {line}");
                            Entitypath = Path.Combine("wwwroot", "Entities", $"TextFile.txt"); //idek if this works tbh
                           
                        }
                    }
                }
                return FoundFiles;
            }
        }

        public List<string> SearchByCategory(string Category, string Entitypath) //returns a list of file paths
        {
            List<string> FoundFiles = new List<string>();
            using (var reader = new StreamReader(Entitypath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue; //skip empty lines
                    string[] parts = line.Split('|');  //split on '|'
                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length < 2) continue; //skip malformed entries
                        string key = keyValue[0].Trim(); //label
                        string value = keyValue[1].Trim(); //value
                        if (key.Equals("Category", StringComparison.OrdinalIgnoreCase) && value.Equals(Category, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Match found: {line}"); //temp. return file path or contents
                            FoundFiles.Add(Entitypath);

                        }
                        else
                        {
                            Console.WriteLine($"No match found for Name: {Category} in line: {line}");
                            Entitypath = Path.Combine("wwwroot", "Entities", $"TextFile.txt"); //Idek if this works

                        }
                    }
                }
                return FoundFiles;
            }
        }

        public Dictionary<string, int> CategoryQTY(string Entitypath, List<string> Categories)
        {
            var categoryCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            //initialize counts requested categories
            foreach (var cat in Categories)
            {
                categoryCounts[cat] = 0;
            }

            if (!File.Exists(Entitypath))
            {
                //return initial counts
                return categoryCounts;
            }

            using (var reader = new StreamReader(Entitypath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(new[] { ':' }, 2);
                        if (keyValue.Length < 2) continue;

                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                        {
                            //increment category in provided list
                            if (categoryCounts.ContainsKey(value))
                                categoryCounts[value]++;
                            //track new categories
                            else categoryCounts[value] = categoryCounts.GetValueOrDefault(value, 0) + 1;

                            break; // stop checking other parts of this line
                        }
                    }
                }
            }

            return categoryCounts;
        }
    }
}