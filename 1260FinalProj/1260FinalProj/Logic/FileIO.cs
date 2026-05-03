using _1260FinalProj.Models;

namespace _1260FinalProj.Logic
{
    public class FileIO
    {
        string FileType = "txt";
        string FileName  { get; set; } 
        string Entitypath;
        


        public string GetFileName(string Name, int ID) 
        {
            string ItmFileName = Name + "_" + ID;
            return ItmFileName;
        }
        public FileIO()
        {
            Entitypath = Path.Combine("wwwroot", "Entities", $"{FileName}.{FileType}");
        }

        public void CreateFile(int ID, string Name, string Category, string Description, DateTime LastUpdate) 
        {
            FileName = GetFileName(Name, ID); //this will set ItmFileName to correct name for file
            Entitypath = Path.Combine("wwwroot", "Entities", $"{FileName}.{FileType}"); //writes into the Entities folder, in the wwwroot directory, with the correct name and file type


            string Entry = $"{ID} | {Name} | {Category} |{Description}| {LastUpdate}"; 
            try
            {
                using (StreamWriter sw = new StreamWriter(Entitypath, true))
                {
                    sw.WriteLine(Entry); //Entry should be '|'-separated

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving entry: {ex.Message}");
            }
        }


        public void EditFile(int ID, string Name, string Category, string Description, DateTime LastUpdate, string Entitypath) 
        {
            LoadFile(Entitypath);
            try
            {
                //Inputs to update file provided through GUI

                LastUpdate = DateTime.Now;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File '{Entitypath}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void LoadFile(string EntityPath)
        {
            
            try
            {

                var FileItems = File.ReadAllLines(Entitypath);
                foreach (var line in FileItems)
                {
                    string[] parts = line.Split('|');  //split on '|'
                    var entity = new Entities();    //create ONE entity per file
                    

                    foreach (var part in parts)
                    {
                        string[] keyValue = part.Split(':');
                        if (keyValue.Length < 2) continue; // skip malformed entries

                        string key = keyValue[0].Trim(); //label
                        string value = keyValue[1].Trim(); //value

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



                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File '{Entitypath}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void DeleteFile(string Entitypath, string FileName)
        {
            Entities FoundFile = new Entities();
            IEnumerable<string> filesToSearch;

            if (Directory.Exists(Entitypath))
            {
                filesToSearch = Directory.EnumerateFiles(Entitypath, "*.txt", SearchOption.TopDirectoryOnly);
            }
            else if (File.Exists(Entitypath))
            {
                filesToSearch = new[] { Entitypath };

            }

            File.Delete(Entitypath);

        }
    }

}

