using System;
using System.Collections.Generic;

public class DBmanager
{
    string DBPath = getDBDirectory();

	public DBmanager()
	{
	}

	private string generateGUID():
		return Guid.NewGuid();

	public void createNewUser(string FrstName,string mdlName, string LstName):
        string FileName = FrstName + mdlName + LstName + generateGUID();

    private string getDBDirectory()
    {
        string DBPath = System.AppDomain.CurrentDomain.BaseDirectory;
        int index = DBPath.LastIndexOf("bin");
        if (index >= 0)
            DBPath = DBPath.Substring(0, index); // or index + 1 to keep slash
        DBPath = DBPath + "Resources\\DB";
        return DBPath;
    }

    public Dictionary<string, Dictionary<string, string>> LoadJson(string myFileName)
    {
        Dictionary<string, Dictionary<string, string>> mydictionary = new Dictionary<string, Dictionary<string, string>>();
        var text = File.ReadAllText(myFileName);
        mydictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
        return mydictionary;
    }
}
