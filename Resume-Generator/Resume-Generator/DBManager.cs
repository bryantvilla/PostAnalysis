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
}
