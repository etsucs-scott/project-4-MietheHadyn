using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;

namespace _1260FinalProj.Logic
{
    public class Searching
    {
        //most methods will retrun either filepath or file contents
        /* Pseudo for looking though the file/Entities folder: (total guess work)
         * using streamreader to read each file in the folder, then split on '|' and check for matching criteria
         * for each file in Entities folder
         *   [search for criteria] split on '|' and check key value parts for match
         *   return file path(s) or file contents
         */

        public void SearchByID(int ID, string Entitypath) //returns file path or file contents
        {
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
                            Console.WriteLine($"Match found: {line}"); //temp. return line or file path
                            return;
                        }
                    }
                }
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

        public void SearchByCategory(string Category, string Entitypath) //returns a list of file paths
        {
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
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"No match found for Name: {Category} in line: {line}");
                            return; //maybe, once actual return is decided, this returns something to trigger a "we found nothing" screen in UI
                        }
                    }
                }
            }
        }

        public Dictionary<string, int> CategoryQTY(string Entitypath, List<string> Categories)
        {
            var categoryCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // initialize counts for the requested categories
            foreach (var cat in Categories)
            {
                categoryCounts[cat] = 0;
            }

            if (!File.Exists(Entitypath))
            {
                // return initialized counts (or throw if you prefer)
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