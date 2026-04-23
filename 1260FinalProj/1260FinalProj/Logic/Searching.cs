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

        public void SearchByName(string Name, string Entitypath) //mayble returns a list of file paths
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
                        if (key.Equals("Name", StringComparison.OrdinalIgnoreCase) && value.Equals(Name, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Match found: {line}"); //temp. return file path or contents
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"No match found for Name: {Name} in line: {line}");
                            return; //maybe, once actual return is decided, this returns something to trigger a "we found nothing" screen in UI
                        }
                    }
                }
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

        public static Dictionary<string, int> CategoryQTY(string Entitypath, List<string> Categories) //christ, let this work
        {
            //admittedly, this seems to work on only one file for now.
            
            //gets number of entries for each category
            Dictionary<string, int> categoryCounts = new Dictionary<string, int>();
            foreach (var cat in Categories)
            {
                categoryCounts[cat] = 0; //initialize counts to 0
            }

            using (var reader = new StreamReader(Entitypath)) //file.FileName)); via StackOverflow https://stackoverflow.com/questions/8821410/why-is-access-to-the-path-denied??
            {
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;


                    string[] parts = line.Split('|');  //split on '|'
                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length < 2) continue; //skip malformed entries
                        string key = keyValue[0].Trim(); //label
                        string value = keyValue[1].Trim(); //value
                        if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                        {
                            if (categoryCounts.ContainsKey(value))
                                categoryCounts[value]++;
                            break; //stop checking other parts of the line once a match is found
                        }
                        else
                        {
                            categoryCounts[value] = categoryCounts.GetValueOrDefault(value, 0) + 1;
                        }
                        break; //stop checking other parts of the line once a match is found
                    }
                    return categoryCounts;

                }
            }
            return categoryCounts;

        }
    }
}