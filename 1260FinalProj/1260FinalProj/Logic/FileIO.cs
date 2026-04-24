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
            Entitypath = $@"wwwroot/Entities/{FileName}.{FileType}";
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
                Console.WriteLine("File created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving entry: {ex.Message}");
            }
        }


        public void EditFile(int ID, string Name, string Category, string Description, DateTime LastUpdate) //do I use ItemPath as a parameter?
        {
            //i need a way to tell it to look at a pre-existing file, not create a new one
            try
            {
                //update file stuff, depending on what field is changed, but also update the LastUpdate field to current time

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

        public void LoadFile()
        {
            //pulls info from files, into the appropriate fields for the entities

            try
            {

                var FileItems = File.ReadAllLines(Entitypath);
                foreach (var line in FileItems)
                {
                    string[] parts = line.Split('|');  //split on '|'
                    var entity = new Entities();    //create ONE entity per line
                    //I don't think these two are gonna work properly

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
                            case "category":
                                entity.Category = value;
                                break;
                            case "Description":
                                entity.Description = value;
                                break;
                            case "LastUpdated":
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


    }

}

