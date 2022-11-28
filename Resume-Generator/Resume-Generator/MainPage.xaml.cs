using static System.Net.Mime.MediaTypeNames;

namespace Resume_Generator;

public partial class MainPage : ContentPage
{
	ResumeManager db;
    Experience ExperienceRecords = new Experience();
    Certifications CertificationRecords = new Certifications();
    Skills SkillsRecords = new Skills();
    Education EducationRecords = new Education();


	public MainPage(ResumeManager db)
	{
        InitializeComponent();
        this.db = db;
		SetProfile();
		CreateEDUTable(db.Education);
	}
	private void CreateEDUTable(List<Dictionary<string,string>> Education)
	{
        var bckgrndclr = "Hrz1";
        foreach (var EducationItem in Education) {
            createEDURow(EducationItem);
		}

	}
	private HorizontalStackLayout createEDURow(Dictionary<string, string> EducationItem) {

        HorizontalStackLayout row = new HorizontalStackLayout();
        CheckBox EduInclude = new CheckBox();
        Button btn = new Button();
        Button btnDel = new Button();
        btnDel.Clicked += new EventHandler(EDUdelBtn_Clicked);
        //chckbx.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(CheckBox_Changed);
        
        Label SchoolName = new Label();
        Label EducationalLevel = new Label();
        Label FieldOfStudy = new Label();
        Label EduFromMM = new Label();
        Label EduFromYYYY = new Label();
        Label EduToMM = new Label();
        Label EduToYYYY = new Label();
        Label EduCurrent = new Label();
        Label DegreeCity = new Label();
        Label EduProvince = new Label();
        
        SchoolName.Text = EducationItem["SchoolName"];
        EducationalLevel.Text = EducationItem["EducationalLevel"];
        FieldOfStudy.Text = EducationItem["FieldOfStudy"];
        EduFromMM.Text = EducationItem["EduFromMM"];
        EduFromYYYY.Text = EducationItem["EduFromYYYY"];
        EduToMM.Text = EducationItem["EduToMM"];
        EduToYYYY.Text = EducationItem["EduToYYYY"];
        EduCurrent.Text = EducationItem["EduCurrent"];
        DegreeCity.Text = EducationItem["DegreeCity"];
        EduProvince.Text = EducationItem["EduProvince"];
        
        SchoolName.Style = App.Current.Resources["TableLabel"] as Style;
        EducationalLevel.Style = App.Current.Resources["TableLabel"] as Style;
        FieldOfStudy.Style = App.Current.Resources["TableLabel"] as Style;
        EduFromMM.Style = App.Current.Resources["TableLabel"] as Style;
        EduFromYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        EduToMM.Style = App.Current.Resources["TableLabel"] as Style;
        EduToYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        EduCurrent.Style = App.Current.Resources["TableLabel"] as Style;
        DegreeCity.Style = App.Current.Resources["TableLabel"] as Style;
        EduProvince.Style = App.Current.Resources["TableLabel"] as Style;
        
        btn.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        row.Children.Add(EduInclude);
        
        row.Children.Add(SchoolName);
        row.Children.Add(EducationalLevel);
        row.Children.Add(FieldOfStudy);
        row.Children.Add(EduFromMM);
        row.Children.Add(EduFromYYYY);
        row.Children.Add(EduToMM);
        row.Children.Add(EduToYYYY);
        row.Children.Add(EduCurrent);
        row.Children.Add(DegreeCity);
        row.Children.Add(EduProvince);

        row.Children.Add(btn);
        row.Children.Add(btnDel);
        //row.Style = App.Current.Resources[stylestr] as Style;
        EDU.Children.Add(row);
        EducationRecords.AddToRecords(
            EducationItem["ItemGUID"],
            row, 
            btn,
            btnDel,
            SchoolName,
            EducationalLevel,
            FieldOfStudy,
            EduFromMM,
            EduFromYYYY,
            EduToMM,
            EduToYYYY,
            EduCurrent,
            DegreeCity,
            EduProvince,
            EduInclude
            );
        return row;
	}
	private void SetProfile() {

		FirstName.Text = db.FirstName;
		LastName.Text = db.LastName;
		MiddleName.Text = db.MiddleName;
		Email.Text = db.Profile["Email"];
		URL.Text = db.Profile["URL"];
		PhoneNo.Text = db.Profile["PhoneNo"];
		StreetAddress1.Text = db.Profile["StreetAddress1"];
        StreetAddress2.Text = db.Profile["StreetAddress2"];
        City.Text = db.Profile["City"];
        Province.Text = db.Profile["Province"];
        Country.Text = db.Profile["Country"];
        PostalCode.Text = db.Profile["PostalCode"];
    }

	private void LogoutBtn_Clicked(object sender, EventArgs e)
	{
        Navigation.PushAsync(new Startup());
    }

	private void CanvasBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Canvas(db));
	}

	private void PostAnalysisBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PostAnalysis(db));
	}

	private async void ProfileBtn_Clicked(object sender, EventArgs e)
	{
		db.UpdateProfile(
            FirstName.Text,
			LastName.Text,
			MiddleName.Text,
			Email.Text,
			URL.Text,
			PhoneNo.Text,
			StreetAddress1.Text,
			StreetAddress2.Text,
			City.Text,
			Province.Text,
			Country.Text,
			PostalCode.Text
			);
        await DisplayAlert("STATUS", "update successful", "OK");
    }
    private async void EDUdelBtn_Clicked(object sender, EventArgs e) {
        string Header = "Warning";
        string paragraph = "Entry will be deleted. This cannot be undone. Continue?";
        bool action = await DisplayAlert(Header, paragraph, "Yes", "No");
        if (action)
        {
            foreach (var btn in EducationRecords.BtnDel) {
                if (btn.Value == sender) {
                    EDU.Children.Remove(EducationRecords.Row[btn.Key]);
                    EducationRecords.DeleteThisGUID(btn.Key);
                    db.RemoveFromEducation(btn.Key);
                }
            
            }
        }
    }
    private async void EDUupdateBtn_Clicked(object sender, EventArgs e)
    {
        bool action = true;
        Dictionary<string,string> Entries = new Dictionary<string, string>();
        Entries["SchoolName"] = SchoolName.Text;
        Entries["EducationalLevel"] = EducationalLevel.Text;
        Entries["FieldOfStudy"] = FieldOfStudy.Text;
        Entries["EduFromMM"] = EduFromMM.Text;
        Entries["EduFromYYYY"] = EduFromYYYY.Text;
        Entries["EduToMM"] = EduToMM.Text;
        Entries["EduToYYYY"] = EduToYYYY.Text;
        Entries["EduProvince"] = EduProvince.Text;
        Entries["DegreeCity"] = DegreeCity.Text;
        Entries["ItemGUID"] = Guid.NewGuid().ToString();

        if (EduCurrent.IsChecked)
            Entries["EduCurrent"] = "False";
        else
        {
            Entries["EduCurrent"] = "True";
        }
        string missingfields = "";
        foreach (var Entry in Entries) { 
            if (Entry.Value == "" || Entry.Value == null){
                missingfields = missingfields + Entry.Key + System.Environment.NewLine;
            }
        }
        if (missingfields != "") {
            string paragraph = "You are missing :" + System.Environment.NewLine + missingfields;
            string Header = "CONTINUE?";
            action = await DisplayAlert(Header, paragraph, "Yes", "No");
        }
        if (action)
        {
            createEDURow(Entries);
            db.AddToEducation(Entries);
        }
    }
}
public class Education
{
    Dictionary<string, Label> SchoolName;
    Dictionary<string, Label> EducationalLevel;
    Dictionary<string, Label> FieldOfStudy;
    Dictionary<string, Label> EduFromMM;
    Dictionary<string, Label> EduFromYYYY;
    Dictionary<string, Label> EduToMM;
    Dictionary<string, Label> EduToYYYY;
    Dictionary<string, Label> EduCurrent;
    Dictionary<string, Label> DegreeCity;
    Dictionary<string, Label> EduProvince;
    Dictionary<string, CheckBox> EduInclude;
    public Dictionary<string, HorizontalStackLayout> Row;
    Dictionary<string, Button> Btn;
    public Dictionary<string, Button> BtnDel;

    public Education() {
        SchoolName = new Dictionary<string, Label>();
        EducationalLevel = new Dictionary<string, Label>();
        FieldOfStudy = new Dictionary<string, Label>();
        EduFromMM = new Dictionary<string, Label>();
        EduFromYYYY = new Dictionary<string, Label>();
        EduToMM = new Dictionary<string, Label>();
        EduToYYYY = new Dictionary<string, Label>();
        EduCurrent = new Dictionary<string, Label>();
        DegreeCity = new Dictionary<string, Label>();
        EduProvince = new Dictionary<string, Label>();
        EduInclude = new Dictionary<string, CheckBox>();
        Row = new Dictionary<string, HorizontalStackLayout>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(   string ItemGUID,
                                HorizontalStackLayout row,
                                Button btn,
                                Button btnDel,
                                Label SchoolName,
                                Label EducationalLevel,
                                Label FieldOfStudy,
                                Label EduFromMM,
                                Label EduFromYYYY,
                                Label EduToMM,
                                Label EduToYYYY,
                                Label EduCurrent,
                                Label DegreeCity,
                                Label EduProvince,
                                CheckBox EduInclude) {
        this.Row[ItemGUID] = row;
        this.Btn[ItemGUID] = btn;
        this.SchoolName[ItemGUID] = SchoolName;
        this.EducationalLevel[ItemGUID] = EducationalLevel;
        this.FieldOfStudy[ItemGUID] = FieldOfStudy;
        this.EduFromMM[ItemGUID] = EduFromMM;
        this.EduFromYYYY[ItemGUID] = EduFromYYYY;
        this.EduToMM[ItemGUID] = EduToMM;
        this.EduToYYYY[ItemGUID] = EduToYYYY;
        this.EduCurrent[ItemGUID] = EduCurrent;
        this.DegreeCity[ItemGUID] = DegreeCity;
        this.EduProvince[ItemGUID] = EduProvince;
        this.EduInclude[ItemGUID] = EduInclude;
        this.BtnDel[ItemGUID] = btnDel;
    }
    public void DeleteThisGUID(string ItemGUID) {
        this.Row.Remove(ItemGUID);
        this.Btn.Remove(ItemGUID);
        this.SchoolName.Remove(ItemGUID);
        this.EducationalLevel.Remove(ItemGUID);
        this.FieldOfStudy.Remove(ItemGUID);
        this.EduFromMM.Remove(ItemGUID);
        this.EduFromYYYY.Remove(ItemGUID);
        this.EduToMM.Remove(ItemGUID);
        this.EduToYYYY.Remove(ItemGUID);
        this.EduCurrent.Remove(ItemGUID);
        this.DegreeCity.Remove(ItemGUID);
        this.EduProvince.Remove(ItemGUID);
        this.EduInclude.Remove(ItemGUID);
        this.BtnDel.Remove(ItemGUID);
    }
}
public class Experience
{
    List<string> ItemGUID;
    Dictionary<string, string> Company;
    Dictionary<string, string> Position;
    Dictionary<string, string> ExpCountry;
    Dictionary<string, string> ExpCity;
    Dictionary<string, string> ExpProvince;
    Dictionary<string, string> ExpFromMM;
    Dictionary<string, string> ExpFromYYYY;
    Dictionary<string, string> ExpToMM;
    Dictionary<string, string> ExpToYYYY;
    Dictionary<string, string> ExpCurrent;
    Dictionary<string, string> ExpDescription;
    Dictionary<string, string> ExpInclude;
}


public class Skills
{
    List<string> ItemGUID;
    Dictionary<string, string> Category;
    Dictionary<string, string> Skill;
    Dictionary<string, string> Proficiency;
    Dictionary<string, string> SkillInclude;
}

public class Certifications
{
    List<string> ItemGUID;
    Dictionary<string, string> Certification;
    Dictionary<string, string> Organization;
    Dictionary<string, string> CertFromMM;
    Dictionary<string, string> CertFromYYYY;
    Dictionary<string, string> CertToMM;
    Dictionary<string, string> CertToYYYY;
    Dictionary<string, string> CertNotApplicable;
    Dictionary<string, string> CertInclude;
}

