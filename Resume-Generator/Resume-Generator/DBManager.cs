using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Generator
{
    public class DBManager {
        private string DBPath;

        public DBManager()
        {
            DBPath = getDBDirectory();
        }

        private string generateGUID() { 
            return Guid.NewGuid().ToString();
        }

        public BasicUser createNewUser(string FrstName, string mdlName, string LstName) {
            string filename = FrstName + "+" + LstName + "=" + generateGUID();
            if (mdlName != "") { 
                filename = FrstName + "+" + mdlName + "+" + LstName + "=" + generateGUID(); 
            }

            string newfilePath = DBPath + "\\" + filename + ".json";
            System.IO.File.Copy(DBPath+"\\Default.json", newfilePath);
            Dictionary<string, Dictionary<string, string>> UserDict = LoadJson(newfilePath);
            UserDict["Profile"]["firstname"] = FrstName;
            UserDict["Profile"]["lastname"] = LstName;
            UserDict["Profile"]["middlename"] = mdlName;
            updateJson(UserDict, newfilePath);
            return new BasicUser(newfilePath, DBPath);

        }

        private void updateJson(Dictionary<string, Dictionary<string, string>> myDictionary, string filename) {
            var updatedDict = JsonConvert.SerializeObject(myDictionary);
            File.WriteAllText(filename, updatedDict);
        }

        public Dictionary<string, Dictionary<string, string>> LoadJson(string myFileName)
        {
            Dictionary<string, Dictionary<string, string>> mydictionary = new Dictionary<string, Dictionary<string, string>>();
            var text = File.ReadAllText(myFileName);
            mydictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
            return mydictionary;
        }

        public List<BasicUser> getAllUsers()
        {
            BasicUser[] Users;
            List<BasicUser> myCollection = new List<BasicUser>();
            var files = Directory.GetFiles(DBPath, "*.*", SearchOption.AllDirectories).Where(name => !name.Contains("Default")); ;
            foreach (string file in files)
            {
                myCollection.Add(new BasicUser(file, DBPath));   
            };
            return myCollection;
        }

        public string getDBDirectory()
        {
            string DBPath = System.AppDomain.CurrentDomain.BaseDirectory;
            int index = DBPath.LastIndexOf("bin");
            if (index >= 0)
                DBPath = DBPath.Substring(0, index); // or index + 1 to keep slash
            DBPath = DBPath + "Resources\\DB";
            return DBPath;
        }


    };

    public class BasicUser {

        public BasicUser(string filename, string dbpath) {
            FileName = filename;
            DBPath = dbpath;
            GUID = GetGUIDFromName(filename);
            FirstName = GetFirstNameFromName(filename);
            LastName = GetLastNameFromName(filename);
            MiddleName = GetMiddleNameFromName(filename);

        }

        public string FirstName = "John";
        public string LastName = "Jingleheimerschmit";
        public string MiddleName = "Jacob";
        public string GUID;
        public string FileName;
        string DBPath;

        private string GetGUIDFromName(string filename)
        {
            filename = filename.Substring(filename.Length - 41);
            filename = filename.Substring(0, filename.Length - 5);
            return filename;
        }

        private string GetFirstNameFromName(string filename)
        {
            filename = filename.Remove(0, DBPath.Length + 1);
            filename = filename.Replace("=", " ");
            if (filename.Length >= 48) {
                filename = filename.Substring(0, filename.Length - 5);
                filename = filename.Substring(0, filename.Length - 41);
            }
            int index = filename.IndexOf("+");
            if (index >= 0)
                filename = filename.Substring(0, index);
            return filename;
        }

        private string GetLastNameFromName(string filename)
        {
            if (filename.Length >= 43)
            {
                filename = filename.Remove(0, DBPath.Length + 1);
                filename = filename.Replace("=", " ");
                filename = filename.Substring(0, filename.Length - 41);
            }
            int index = filename.LastIndexOf("+");
            if (index >= 0)
            {
                filename = filename.Substring(index + 1);
            }
            return filename;
        }

        private string GetMiddleNameFromName(string filename)
        {
            if (filename.Length >= 43)
            {
                filename = filename.Remove(0, DBPath.Length + 1);
                filename = filename.Replace("=", " ");
                filename = filename.Substring(0, filename.Length - 41);
            }
            int frstindex = filename.IndexOf("+");
            int lstindex = filename.LastIndexOf("+");
            if (frstindex == lstindex)
            {
                filename = "";
            }
            else {
                if (frstindex >= 0 && lstindex >= 0) {
                    filename.Substring(frstindex, lstindex);
                }
            }
            return filename;
        }
    }

    public class ResumeManager
    {
        string FirstName;
        string MiddleName;
        string LastName;
        List<Dictionary<string, string>> Education;
        List<Dictionary<string, string>> WorkExperience;
        List<Dictionary<string, string>> Skills;
        List<Dictionary<string, string>> Awards;

        public ResumeManager()
        {
            FirstName = "John";
            MiddleName = "James";
            LastName = "Doe";

            //Education Dummy Data
            Education = new List<Dictionary<string, string>>();
            Dictionary<string, string> tempEd = new Dictionary<string, string>();
            tempEd["School Name"] = "Florida International University";
            tempEd["Degree"] = "Bachelors of Science";
            tempEd["Field of Study"] = "Computer Science";
            tempEd["City"] = "Miami";
            tempEd["Province"] = "Florida";
            tempEd["FromMonth"] = "08";
            tempEd["FromYear"] = "2017";
            tempEd["ToMonth"] = "05";
            tempEd["ToYear"] = "2020";
            Education.Add(tempEd);

            //Work Experience Dummy Data
            WorkExperience = new List<Dictionary<string, string>>();
            Dictionary<string, string> tempWork = new Dictionary<string, string>();
            tempWork["Company"] = "Google";
            tempWork["Position"] = "Software Engineer";
            tempWork["Country"] = "USA";
            tempWork["City"] = "Mountain View";
            tempWork["Province"] = "California";
            tempWork["FromMonth"] = "08";
            tempWork["FromYear"] = "2020";
            tempWork["ToMonth"] = "05";
            tempWork["ToYear"] = "2022";
            tempWork["Description"] = "Building dynamic and scalable apps. Worked on the google suite in developing many security features for the cloud.";
            WorkExperience.Add(tempWork);

            Dictionary<string, string> tempWork2 = new Dictionary<string, string>();
            tempWork2["Company"] = "Google";
            tempWork2["Position"] = "Software Engineer";
            tempWork2["Country"] = "USA";
            tempWork2["City"] = "Mountain View";
            tempWork2["Province"] = "California";
            tempWork2["FromMonth"] = "05";
            tempWork2["FromYear"] = "2022";
            tempWork2["ToMonth"] = "12";
            tempWork2["ToYear"] = "2022";
            tempWork2["Description"] = "Building dynamic and scalable apps. Worked on the google suite in developing many security features for the cloud.";
            WorkExperience.Add(tempWork2);

            

        }
    }
}
