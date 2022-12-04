using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Generator.Model
{
    public class ResumeManager
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Dictionary<string,string> Contact { get; set; }
        public Dictionary<string,string> Address { get; set; }
        public List<Dictionary<string,string>> Education { get; set; }
        public List<Dictionary<string, string>> WorkExperience { get; set; }
        public List<Dictionary<string, string>> Skills { get; set; }
        public List<Dictionary<string, string>> Awards { get; set; }
        
    }
}
