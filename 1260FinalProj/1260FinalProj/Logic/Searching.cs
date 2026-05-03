using _1260FinalProj.Models;

namespace _1260FinalProj.Logic
{
    public class Searching
    {

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

                            // populate all fields for this line
                            if (key.Equals("ID", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(value, out var parsedId))
                                {
                                    entity.ID = parsedId;
                                    if (parsedId == ID) isMatch = true; //compare ID
                                }
                            }
                            else if (key.Equals("Name", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Name = value;
                            }
                            else if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Category = value;
                            }
                            else if (key.Equals("Description", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Description = value;
                            }
                            else if (key.Equals("LastUpdate", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(value, out var lu))
                                {
                                    entity.LastUpdate = lu;
                                    entity.LastUpdateDT = new DateTime(1970, 1, 1).AddSeconds(lu);
                                }
                            }
                        }

                        // add the fully-populated entity only if it matched
                        if (isMatch)
                            FoundFiles.Add(entity);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip unreadable files (or log)
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }
            }

            return FoundFiles;
        }


        public static List<Entities> SearchByName(string Name, string Entitypath)
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

                            //populate all fields for this line
                            if (key.Equals("ID", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(value, out var parsedId))
                                {
                                    entity.ID = parsedId;

                                }
                            }
                            else if (key.Equals("Name", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Name = value;
                                if (value == Name) isMatch = true; //compare Name
                            }
                            else if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Category = value;
                            }
                            else if (key.Equals("Description", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Description = value;
                            }
                            else if (key.Equals("LastUpdate", StringComparison.OrdinalIgnoreCase))
                            {
                                DateTime TempDT = DateTime.Parse(value);
                                entity.LastUpdateDT = TempDT;
                            }
                        }

                        // add the fully-populated entity only if it matched
                        if (isMatch)
                            FoundFiles.Add(entity);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip unreadable files (or log)
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }
            }

            return FoundFiles;
        }



        public static List<Entities> SearchByCategory(string Category, string Entitypath)
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

                            //populate all fields for this line
                            if (key.Equals("ID", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(value, out var parsedId))
                                {
                                    entity.ID = parsedId;

                                }
                            }
                            else if (key.Equals("Name", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Name = value;
                            }
                            else if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Category = value;
                                if (value == Category) isMatch = true; //compare Name
                            }
                            else if (key.Equals("Description", StringComparison.OrdinalIgnoreCase))
                            {
                                entity.Description = value;
                            }
                            else if (key.Equals("LastUpdate", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(value, out var lu))
                                {
                                    entity.LastUpdate = lu;
                                    entity.LastUpdateDT = new DateTime(1970, 1, 1).AddSeconds(lu);
                                }
                            }
                        }

                        // add the fully-populated entity only if it matched
                        if (isMatch)
                            FoundFiles.Add(entity);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip unreadable files (or log)
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }
            }

            return FoundFiles;
        }


        public static List<string> GetCategoryNames(string entitiesDir)
        {
            List<string> FileCats = new List<string>();

            List<Entities> FoundFiles = new List<Entities>();
            IEnumerable<string> filesToSearch;

            if (Directory.Exists(entitiesDir))
            {
                filesToSearch = Directory.EnumerateFiles(entitiesDir, "*.txt", SearchOption.TopDirectoryOnly);
            }
            else if (File.Exists(entitiesDir))
            {
                filesToSearch = new[] { entitiesDir };
            }
            else
            {
                //nothing to search
                return FileCats;
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

                            if (key.Equals("Category", StringComparison.OrdinalIgnoreCase))
                            {
                                if (FileCats.Contains(value, StringComparer.OrdinalIgnoreCase))
                                    continue; //already have this category
                                else
                                {
                                    FileCats.Add(value);
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // skip unreadable files (or log)
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }
            }
            return FileCats.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        
        public static Dictionary<string, int> CategoryQTY(string Entitypath, List<string> FileCats)
        {
            var categoryCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            //initialize counts requested categories
            foreach (var cat in FileCats)
            {
                categoryCounts[cat] = 0;
            }

            //If path is directory, process each file inside
            while (Directory.Exists(Entitypath))
            {
                var files = Directory.GetFiles(Entitypath, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file))
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
                                    if (categoryCounts.ContainsKey(value))
                                        categoryCounts[value]++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return categoryCounts;

        }
    }
}