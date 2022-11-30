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

        public ResumeManager createNewResume(string filename)
        {

            ResumeManager resume = new ResumeManager(LoadJson(filename), filename, DBPath);
            return resume;
        }

        public void deleteUser(string file) {
            File.Delete(file);
        }

        public BasicUser createNewUser(string FrstName, string mdlName, string LstName) {
            string filename = FrstName + "+" + LstName + "=" + generateGUID();
            if (mdlName != "") { 
                filename = FrstName + "+" + mdlName + "+" + LstName + "=" + generateGUID(); 
            }

            string newfilePath = DBPath + "\\" + filename + ".json";
            System.IO.File.Copy(DBPath+"\\Default.json", newfilePath);
            Dictionary<string, List<Dictionary<string,string>>> UserDict = LoadJson(newfilePath);
            UserDict["Profile"][0]["FirstName"] = FrstName;
            UserDict["Profile"][0]["LastName"] = LstName;
            UserDict["Profile"][0]["MiddleName"] = mdlName;
            updateJson(UserDict, newfilePath);
            return new BasicUser(newfilePath, DBPath);

        }

        private void updateJson(Dictionary<string, List<Dictionary<string, string>>> myDictionary, string filename) {
            var updatedDict = JsonConvert.SerializeObject(myDictionary);
            File.WriteAllText(filename, updatedDict);
        }

        public Dictionary<string, List<Dictionary<string, string>>> LoadJson(string myFileName)
        {
            Dictionary<string, List<Dictionary<string, string>>> mydictionary = new Dictionary<string, List<Dictionary<string, string>>>();
            var text = File.ReadAllText(myFileName);
            mydictionary = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(text);
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
                    string lastname = filename.Substring(lstindex);
                    string firstname = filename.Substring(0,frstindex+1);
                    filename = filename.Replace(lastname, "");
                    filename = filename.Replace(firstname, "");
                }
            }
            return filename;
        }
    }

    public class ResumeManager
    {
        public String FirstName;
        public String MiddleName;
        public String LastName;
        private String Password;
        Dictionary<string, List<Dictionary<string, string>>> User;
        String filename;
        String DBPath;
        public Dictionary<string, string> Profile;
        public List<Dictionary<string, string>> Education;
        public List<Dictionary<string, string>> Experience;
        public List<Dictionary<string, string>> Skills;
        public List<Dictionary<string, string>> Certifications;

        public ResumeManager(Dictionary<string, List<Dictionary<string, string>>> User, string filename, string DBPath)
        {
            this.DBPath = DBPath;
            this.filename = filename;
            this.User = User;
            Profile = User["Profile"][0];
            Education = User["Education"];
            Experience = User["Experience"];
            Skills = User["Skills"];
            Certifications = User["Certifications"];
            this.FirstName = Profile["FirstName"];
            this.MiddleName = Profile["MiddleName"];
            this.LastName = Profile["LastName"];
        }

        public bool UpdateProfile(String FirstName,
            String LastName,
            String MiddleName,
            String Email,
            String URL,
            String PhoneNo,
            String StreetAddress1,
            String StreetAddress2,
            String City,
            String Province,
            String Country,
            String PostalCode) {

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.MiddleName = MiddleName;
            Profile["FirstName"] = FirstName;
            Profile["MiddleName"] = MiddleName;
            Profile["LastName"] = LastName;
            Profile["Email"] = Email;
            Profile["URL"] = URL;
            Profile["PhoneNo"] = PhoneNo;
            Profile["StreetAddress1"] = StreetAddress1;
            Profile["StreetAddress2"] = StreetAddress2;
            Profile["City"] = City;
            Profile["Province"] = Province;
            Profile["Country"] = Country;
            Profile["PostalCode"] = PostalCode;

            User["Profile"][0] = this.Profile;
            updateJson(this.User, filename);
            string newfilename = createFileName(filename);
            if (newfilename != filename) {
                System.IO.File.Copy(filename, newfilename);
                File.Delete(filename);
                filename = newfilename;
            }

            return true;
        }
        public bool UpdateDB(String Dictname) {
            List<Dictionary<string, string>> DictToUpdate = new List<Dictionary<string,string>>();
            if (Dictname == "Education")
            {
                DictToUpdate = Education;
            }
            else if (Dictname == "Experience")
            {
                DictToUpdate = Experience;
            }
            else if (Dictname == "Certifications") 
            {
                DictToUpdate = Certifications;
            }
            else if (Dictname == "Skills")
            {
                DictToUpdate = Skills;
            }
            User[Dictname] = DictToUpdate;
            updateJson(this.User, filename);
            return true;
        }
        public void AddToEducation(Dictionary<string, string> item) {
            Education.Add(item);
            User["Education"] = this.Education;
            updateJson(User, filename);
        }
        public void AddToExperience(Dictionary<string, string> item)
        {
            Experience.Add(item);
            User["Experience"] = this.Experience;
            updateJson(User, filename);
        }
        public void AddToCertifications(Dictionary<string, string> item)
        {
            Certifications.Add(item);
            User["Certifications"] = this.Certifications;
            updateJson(User, filename);
        }
        public void AddToSkills(Dictionary<string, string> item)
        {
            Skills.Add(item);
            User["Skills"] = this.Skills;
            updateJson(User, filename);
        }


        public void RemoveFromExperience(string itemGUID)
        {
            foreach (var entry in this.Experience) {
                if (entry["ItemGUID"] == itemGUID) {
                    Experience.Remove(entry);
                    break;
                }
            }
            User["Experience"] = this.Experience;
            updateJson(User, filename);
        }

        public void RemoveFromEducation(string itemGUID)
        {
            foreach (var entry in this.Education)
            {
                if (entry["ItemGUID"] == itemGUID)
                {
                    Education.Remove(entry);
                    break;
                }
            }
            User["Education"] = this.Education;
            updateJson(User, filename);
        }

        public void RemoveFromCertifications(string itemGUID)
        {
            foreach (var entry in this.Certifications)
            {
                if (entry["ItemGUID"] == itemGUID)
                {
                    Certifications.Remove(entry);
                    break;
                }
            }
            User["Certifications"] = this.Certifications;
            updateJson(User, filename);
        }

        public void RemoveFromSkills(string itemGUID)
        {
            foreach (var entry in this.Skills)
            {
                if (entry["ItemGUID"] == itemGUID)
                {
                    Skills.Remove(entry);
                    break;
                }
            }
            User["Skills"] = this.Skills;
            updateJson(User, filename);
        }
        private void updateJson(Dictionary<string, List<Dictionary<string, string>>> myDictionary, string filename)
        {
            var updatedDict = JsonConvert.SerializeObject(myDictionary);
            File.WriteAllText(filename, updatedDict);
        }
        private string GetGUIDFromName(string filename)
        {
            filename = filename.Substring(filename.Length - 41);
            filename = filename.Substring(0, filename.Length - 5);
            return filename;
        }

        private string createFileName(string currentfilename){
            string GUID = GetGUIDFromName(currentfilename);
            string filename = FirstName + "+" + LastName + "=" + GUID;
            if (MiddleName != "")
            {
                filename = FirstName + "+" + MiddleName + "+" + LastName + "=" + GUID;
            }

            string newfilePath = DBPath + "\\" + filename + ".json";
            return newfilePath;
        }
        
}
}
