using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_PRG271
{
    public static class FileHandler
    {
        //file to be saved in same folder as exe
        public static string FilePath = Path.Combine(AppContext.BaseDirectory, "animals.txt");

        //bad lines skipped last time we loaded
        public static int LinesSkipped = 0;

        //reads animals.txt and returns the list of animals
        public static List<Animal> Load()
        {
            List<Animal> animals = new List<Animal> ();
            LinesSkipped = 0;

            try
            {
                if (!File.Exists(FilePath))
                {
                    File.WriteAllText(FilePath, "");
                    return animals;
                }

                string[] lines = File.ReadAllLines(FilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 7)
                    {
                        LinesSkipped++;
                        continue;
                    }

                    //makes ID prefixes capitals
                    string id = parts[0].Trim().ToUpper();

                    if (!IsValidId(id))
                    {
                        LinesSkipped++;
                        continue;
                    }

                    //age/score must be whole nums
                    int age;
                    int score;
                        bool ageIsNumber = int.TryParse(parts[3].Trim(), out age);
                    bool scoreIsNumber = int.TryParse(parts[4].Trim(), out score);

                    if (!ageIsNumber || !scoreIsNumber)
                    {
                        LinesSkipped++;
                        continue;
                    }

                    //must be inbetween 0 and 100
                    if (age < 0 || age > 100 || score < 0 || score > 100)
                    {
                        LinesSkipped++;
                        continue;
                    }

                    //skip if we already got an animnal using this ID
                    bool alreadyThere = false;
                    foreach (Animal a in animals)
                    {
                        if (a.Id == id)
                        {
                            alreadyThere = true;
                        }
                    }

                    if (alreadyThere)
                    {
                        LinesSkipped++;
                        continue;
                    }

                    //status and housing unit worked out from score
                    Animal animal = new Animal(id, parts[1].Trim(), parts[2].Trim(), age, score);
                    animals.Add(animal);
                }
            }
            catch (IOException) {
                MessageBox.Show("animals.txt is being used by another program");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No permission to read animals.txt");
            }

            return animals;
        }

        //checks if id looks the way it should eg) WR-0001
        private static bool IsValidId(string id)
        {
            if(id.Length!=7)
            {
                return false;
            }

            if (!id.StartsWith("WR-"))
            {
                return false;
            }

            //last 4 chars must be digit
            for (int i=3; i<7; i++)
            {
                if (id[i] < '0' || id[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }
    } 
}
