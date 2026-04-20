using _1260FinalProj.Models;

namespace _1260FinalProj.Logic
{
    public class FileIO
    {
        string ItmFileType = "txt";
        string ItmFileName = "Entity_File"; //or should this be { get; set; } to change for each entity?
        string Itempath;

        public string GetFileName(string Name, int ID) //or Entities.Name?
        {
            string ItmFileName = Name + "_" + ID;
            return ItmFileName;
        }
        public FileIO()
        {
            Itempath = $@"{ItmFileName}.{ItmFileType}";
        }

        public void CreateFile(int ID, string Name, string Category, string Description, DateTime LastUpdate)
        {
            GetFileName(Name, ID); //this will set ItmFileName to correct name for file
            Itempath = $@"{ItmFileName}.{ItmFileType}"; //make sure Itempath is defined w/ correct filename

            Console.WriteLine("Please give the name of the contributor?");
            string contributor = Console.ReadLine();
            string Entry = $"{ID}   {Name}  {Category}  {LastUpdate}    {contributor}"; //this might not be good, but works for now; maybe \n separated?
            try
            {
                using (StreamWriter sw = new StreamWriter(Itempath, true))
                {
                    sw.WriteLine(Entry); //Entry is Tab-separated, may change to newline-separated or something else

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
                //update file stuff
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File '{Itempath}' not found.");
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

                var FileItems = File.ReadAllLines(Itempath);
                foreach (var line in FileItems)
                {
                    string[] parts = line.Split('|');  // split line on '|'
                    var entity = new Entities();    // create ONE entity per line

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
                Console.WriteLine($"Error: File '{Itempath}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


    }

}

