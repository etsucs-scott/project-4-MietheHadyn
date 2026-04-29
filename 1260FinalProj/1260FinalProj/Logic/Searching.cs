using _1260FinalProj.Models;

namespace _1260FinalProj.Logic
{
    public class Searching
    {
        public int SearchINT { get; set; }
        public string SearchTerm { get; set; }



        //         if (key.Equals("ID", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out int parsedID) && parsedID == ID)
        //                                {
        //                                    //match found
        //                                    isMatch = true;
        //                                    switch (key)  //this maps each value into its proper attribute
        //                                    {
        //                                        case "ID":
        //                                            entity.ID = int.Parse(value);
        //                                            break;
        //                                        case "Name":
        //                                            entity.Name = value;
        //                                            break;
        //                                        case "Category":
        //                                            entity.Category = value;
        //                                            break;
        //                                        case "Description":
        //                                            entity.Description = value;
        //                                            break;
        //                                        case "LastUpdate":
        //                                            entity.LastUpdate = int.Parse(value);
        //                                            break;
        //                                    }

        //}
        //if (isMatch)
        //{
        //    FoundFiles.Add(entity);
        //}

        public static List<Entities> SearchByID(int ID, string Entitypath)
        {
            List<Entities> FoundFiles = new List<Entities>();
            IEnumerable<string> filesToSearch;

            if (Directory.Exists(Entitypath))
            {
                filesToSearch = Directory.EnumerateFiles(Entitypath, "*.txt", SearchOption.TopDirectoryOnly);
            }
            else if (File.Exists(Entitypath))
            {
                filesToSearch = new[] { Entitypath };
            }
            else
            {
                //nothing to search
                return FoundFiles;
            }

            foreach (var file in filesToSearch)
            {
                try
                {
                    using var reader = new StreamReader(file);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var entity = new Entities();
                        var parts = line.Split('|');
                        bool isMatch = false;

                        foreach (var part in parts)
                        {
                            var kv = part.Split(new[] { ':' }, 2);
                            if (kv.Length < 2) continue;
                            var key = kv[0].Trim();
                            var value = kv[1].Trim();

                            if (key.Equals("ID", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out int parsedID) && parsedID == ID)
                            {
                                //match found
                                isMatch = true;
                                switch (key)  //this maps each value into its proper attribute
                                {
                                    case "ID":
                                        entity.ID = int.Parse(value);
                                        break;
                                    case "Name":
                                        entity.Name = value;
                                        break;
                                    case "Category":
                                        entity.Category = value;
                                        break;
                                    case "Description":
                                        entity.Description = value;
                                        break;
                                    case "LastUpdate":
                                        entity.LastUpdate = int.Parse(value);
                                        break;
                                }

                            }
                            if (isMatch)
                            {
                                FoundFiles.Add(entity);
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip files we cannot access; optionally log the path
                    continue;
                }
                catch (IOException)
                {
                    // skip problematic files; optionally log
                    continue;
                }
            }

            return FoundFiles;
        }


        public static List<Entities> SearchByName(string Name, string Entitypath) //idk if this works, nor can I really test it rn
        {
            bool isMatch = false;
            List<Entities> FoundFiles = new List<Entities>();
            IEnumerable<string> filesToSearch;

            if (Directory.Exists(Entitypath))
            {
                filesToSearch = Directory.EnumerateFiles(Entitypath, "*.txt", SearchOption.TopDirectoryOnly);
            }
            else if (File.Exists(Entitypath))
            {
                filesToSearch = new[] { Entitypath };
            }
            else
            {
                //nothing to search
                return FoundFiles;
            }

            foreach (var file in filesToSearch)
            {
                try
                {
                    using var reader = new StreamReader(file);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var entity = new Entities();
                        var parts = line.Split('|');


                        foreach (var part in parts)
                        {
                            var kv = part.Split(new[] { ':' }, 2);
                            if (kv.Length < 2) continue;
                            var key = kv[0].Trim();
                            var value = kv[1].Trim();

                            if (key.Equals("Name", StringComparison.OrdinalIgnoreCase) && value.Equals(Name, StringComparison.OrdinalIgnoreCase))
                            {
                                //match found
                                isMatch = true;
                                switch (key)  //this maps each value into its proper attribute
                                {
                                    case "ID":
                                        entity.ID = int.Parse(value);
                                        break;
                                    case "Name":
                                        entity.Name = value;
                                        break;
                                    case "Category":
                                        entity.Category = value;
                                        break;
                                    case "Description":
                                        entity.Description = value;
                                        break;
                                    case "LastUpdate":
                                        entity.LastUpdate = int.Parse(value);
                                        break;
                                }

                            }
                            if (isMatch)
                            {
                                FoundFiles.Add(entity);
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip files we cannot access; optionally log the path
                    continue;
                }
                catch (IOException)
                {
                    // skip problematic files; optionally log
                    continue;
                }
            }

            return FoundFiles;
        }



        public static List<Entities> SearchByCategory(string Category, string Entitypath) //returns a list of file paths
        {
            bool isMatch = false;
            List<Entities> FoundFiles = new List<Entities>();
            IEnumerable<string> filesToSearch;

            if (Directory.Exists(Entitypath))
            {
                filesToSearch = Directory.EnumerateFiles(Entitypath, "*.txt", SearchOption.TopDirectoryOnly);
            }
            else if (File.Exists(Entitypath))
            {
                filesToSearch = new[] { Entitypath };
            }
            else
            {
                //nothing to search
                return FoundFiles;
            }

            foreach (var file in filesToSearch)
            {
                try
                {
                    using var reader = new StreamReader(file);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var entity = new Entities();
                        var parts = line.Split('|');

                        foreach (var part in parts)
                        {
                            var kv = part.Split(new[] { ':' }, 2);
                            if (kv.Length < 2) continue;
                            var key = kv[0].Trim();
                            var value = kv[1].Trim();

                            if (key.Equals("Category", StringComparison.OrdinalIgnoreCase) && value.Equals(Category, StringComparison.OrdinalIgnoreCase))
                            {
                                //match found
                                switch (key)  //this maps each value into its proper attribute
                                {
                                    case "ID":
                                        entity.ID = int.Parse(value);
                                        break;
                                    case "Name":
                                        entity.Name = value;
                                        break;
                                    case "Category":
                                        entity.Category = value;
                                        break;
                                    case "Description":
                                        entity.Description = value;
                                        break;
                                    case "LastUpdate":
                                        entity.LastUpdate = int.Parse(value);
                                        break;
                                }

                            }
                            if (isMatch)
                            {
                                FoundFiles.Add(entity);
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip files we cannot access; optionally log the path
                    continue;
                }
                catch (IOException)
                {
                    // skip problematic files; optionally log
                    continue;
                }
            }

            return FoundFiles;
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

                            break; //stop checking other parts of this file
                        }
                    }
                }
            }

            return categoryCounts;
        }
    }
}