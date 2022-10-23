using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Resume_Generator;

public partial class MainPage : ContentPage
{
	ResumeManager db;
    Experience ExperienceRecords = new Experience();
    Certifications CertificationRecords = new Certifications();
    Skills SkillsRecords = new Skills();
    Education EducationRecords = new Education();


	public MainPage(ResumeManager db){
        InitializeComponent();
        this.db = db;
		SetProfile();
		CreateEDUTable(db.Education);
        CreateEXPTable(db.Experience);
        CreateSKILLSTable(db.Skills);
        CreateCERTTable(db.Certifications);

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
        EduInclude.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(EduInclude_Changed);
        
        Label SchoolName = new Label();
        Label EducationalLevel = new Label();
        Label FieldOfStudy = new Label();
        Label EduFromMM = new Label();
        Label EduFromYYYY = new Label();
        Label EduToMM = new Label();
        Label EduToYYYY = new Label();
        Label EduCurrent = new Label();//this is a label cause this ISNT the check box its the table value
        Label DegreeCity = new Label();
        Label EduProvince = new Label();
        
        if (EducationItem["EduInclude"] == "True")
        {
            EduInclude.IsChecked = true;
        }

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

    private void CreateEXPTable(List<Dictionary<string, string>> Experience)
    {
        var bckgrndclr = "Hrz1";
        foreach (var ExperienceItem in Experience)
        {
            createEXPRow(ExperienceItem);
        }

    }

    private HorizontalStackLayout createEXPRow(Dictionary<string, string> ExperienceItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();
        CheckBox ExpInclude = new CheckBox();
        Button btn = new Button();
        Button btnDel = new Button();
        btnDel.Clicked += new EventHandler(EXPdelBtn_Clicked);
        ExpInclude.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(ExpInclude_Changed);

        Label Company = new Label();
        Label Position = new Label();
        Label ExpCountry = new Label();
        Label ExpCity = new Label();
        Label ExpProvince = new Label();
        Label ExpFromMM = new Label();
        Label ExpFromYYYY = new Label();
        Label ExpToMM = new Label();
        Label ExpToYYYY = new Label();
        Label ExpCurrent = new Label();
        Label ExpDescription = new Label();

        if (ExperienceItem["ExpInclude"] == "True")
        {
            ExpInclude.IsChecked = true;
        }

        Company.Text = ExperienceItem["Company"];
        Position.Text = ExperienceItem["Position"];
        ExpCountry.Text = ExperienceItem["ExpCountry"];
        ExpCity.Text = ExperienceItem["ExpCity"];
        ExpProvince.Text = ExperienceItem["ExpProvince"];
        ExpFromMM.Text = ExperienceItem["ExpFromMM"];
        ExpFromYYYY.Text = ExperienceItem["ExpFromYYYY"];
        ExpToMM.Text = ExperienceItem["ExpToMM"];
        ExpToYYYY.Text = ExperienceItem["ExpToYYYY"];
        ExpCurrent.Text = ExperienceItem["ExpCurrent"];
        ExpDescription.Text = ExperienceItem["ExpDescription"];

        Company.Style = App.Current.Resources["TableLabel"] as Style;
        Position.Style = App.Current.Resources["TableLabel"] as Style;
        ExpCountry.Style = App.Current.Resources["TableLabel"] as Style;
        ExpCity.Style = App.Current.Resources["TableLabel"] as Style;
        ExpProvince.Style = App.Current.Resources["TableLabel"] as Style;
        ExpFromMM.Style = App.Current.Resources["TableLabel"] as Style;
        ExpFromYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        ExpToMM.Style = App.Current.Resources["TableLabel"] as Style;
        ExpToYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        ExpCurrent.Style = App.Current.Resources["TableLabel"] as Style;
        ExpDescription.Style = App.Current.Resources["TableLabel"] as Style;

        btn.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        row.Children.Add(ExpInclude);

        row.Children.Add(Company);
        row.Children.Add(Position);
        row.Children.Add(ExpCountry);
        row.Children.Add(ExpCity);
        row.Children.Add(ExpProvince);
        row.Children.Add(ExpFromMM);
        row.Children.Add(ExpFromYYYY);
        row.Children.Add(ExpToMM);
        row.Children.Add(ExpToYYYY);
        row.Children.Add(ExpCurrent);
        //row.Children.Add(ExpDescription);

        row.Children.Add(btn);
        row.Children.Add(btnDel);
        //row.Style = App.Current.Resources[stylestr] as Style;
        EXP.Children.Add(row);
        ExperienceRecords.AddToRecords(
            ExperienceItem["ItemGUID"],
            row,
            btn,
            btnDel,
            Company,
            Position,
            ExpCountry,
            ExpCity,
            ExpProvince,
            ExpFromMM,
            ExpFromYYYY,
            ExpToMM,
            ExpToYYYY,
            ExpCurrent,
            ExpDescription,
            ExpInclude
            );
        return row;
    }

    private void CreateSKILLSTable(List<Dictionary<string, string>> Skills)
    {
        var bckgrndclr = "Hrz1";
        foreach (var SkillsItem in Skills)
        {
            createSKILLSRow(SkillsItem);
        }

    }
    private HorizontalStackLayout createSKILLSRow(Dictionary<string, string> SkillsItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();
        CheckBox SkillsInclude = new CheckBox();
        Button btn = new Button();
        Button btnDel = new Button();
        btnDel.Clicked += new EventHandler(SKILLSdelBtn_Clicked);
        SkillsInclude.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(SkillsInclude_Changed);

        Label Category = new Label();
        Label Skill = new Label();
        Label Proficiency = new Label();

        if (SkillsItem["SkillInclude"] == "True")
        {
            SkillsInclude.IsChecked = true;
        }

        Category.Text = SkillsItem["Category"];
        Skill.Text = SkillsItem["Skill"];
        Proficiency.Text = SkillsItem["Proficiency"];

        Category.Style = App.Current.Resources["TableLabel"] as Style;
        Skill.Style = App.Current.Resources["TableLabel"] as Style;
        Proficiency.Style = App.Current.Resources["TableLabel"] as Style;

        btn.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        row.Children.Add(SkillsInclude);

        row.Children.Add(Category);
        row.Children.Add(Skill);
        row.Children.Add(Proficiency);

        row.Children.Add(btn);
        row.Children.Add(btnDel);
        //row.Style = App.Current.Resources[stylestr] as Style;
        SKILLS.Children.Add(row);
        SkillsRecords.AddToRecords(
            SkillsItem["ItemGUID"],
            row,
            btn,
            btnDel,
            Category,
            Skill,
            Proficiency,
            SkillsInclude
            );
        return row;
    }

    private void CreateCERTTable(List<Dictionary<string, string>> Certifications)
    {
        var bckgrndclr = "Hrz1";
        foreach (var CertificationItem in Certifications)
        {
            createCERTRow(CertificationItem);
        }

    }
    private HorizontalStackLayout createCERTRow(Dictionary<string, string> CertificationItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();
        CheckBox CertInclude = new CheckBox();
        Button btn = new Button();
        Button btnDel = new Button();
        btnDel.Clicked += new EventHandler(CERTdelBtn_Clicked);
        CertInclude.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(CertInclude_Changed);

        Label Certification = new Label();
        Label Organization = new Label();
        Label CertFromMM = new Label();
        Label CertFromYYYY = new Label();
        Label CertToMM = new Label();
        Label CertToYYYY = new Label();
        Label CertNotApplicable = new Label();

        if (CertificationItem["CertInclude"] == "True")
        {
            CertInclude.IsChecked = true;
        }

        Certification.Text = CertificationItem["Certification"];
        Organization.Text = CertificationItem["Organization"];
        CertFromMM.Text = CertificationItem["CertFromMM"];
        CertFromYYYY.Text = CertificationItem["CertFromYYYY"];
        CertToMM.Text = CertificationItem["CertToMM"];
        CertToYYYY.Text = CertificationItem["CertToYYYY"];
        CertNotApplicable.Text = CertificationItem["CertNotApplicable"];

        Certification.Style = App.Current.Resources["TableLabel"] as Style;
        Organization.Style = App.Current.Resources["TableLabel"] as Style;
        CertFromMM.Style = App.Current.Resources["TableLabel"] as Style;
        CertFromYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        CertToMM.Style = App.Current.Resources["TableLabel"] as Style;
        CertToYYYY.Style = App.Current.Resources["TableLabel"] as Style;
        CertNotApplicable.Style = App.Current.Resources["TableLabel"] as Style;

        btn.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.Style = App.Current.Resources["LoadBtn"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        row.Children.Add(CertInclude);

        row.Children.Add(Certification);
        row.Children.Add(Organization);
        row.Children.Add(CertFromMM);
        row.Children.Add(CertFromYYYY);
        row.Children.Add(CertToMM);
        row.Children.Add(CertToYYYY);
        row.Children.Add(CertNotApplicable);

        row.Children.Add(btn);
        row.Children.Add(btnDel);
        //row.Style = App.Current.Resources[stylestr] as Style;
        CERT.Children.Add(row);
        CertificationRecords.AddToRecords(
            CertificationItem["ItemGUID"],
            row,
            btn,
            btnDel,
            Certification,
            Organization,
            CertFromMM,
            CertFromYYYY,
            CertToMM,
            CertToYYYY,
            CertNotApplicable,
            CertInclude
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
    private void EduInclude_Changed(object sender, EventArgs e)
    {
        foreach (var item in EducationRecords.EduInclude)
        {
            if (sender == item.Value)
            {
                foreach (var eduItem in db.Education)
                {
                    if (item.Key == eduItem["ItemGUID"])
                    {
                        if (item.Value.IsChecked)
                        {
                            eduItem["EduInclude"] = "True";
                        }
                        else
                        {
                            eduItem["EduInclude"] = "False";
                        }
                    }
                }
            }
        }
        db.UpdateDB("Education");
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
        Entries["EduInclude"] = "True";
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

    private async void EXPdelBtn_Clicked(object sender, EventArgs e)
    {
        string Header = "Warning";
        string paragraph = "Entry will be deleted. This cannot be undone. Continue?";
        bool action = await DisplayAlert(Header, paragraph, "Yes", "No");
        if (action)
        {
            foreach (var btn in ExperienceRecords.BtnDel)
            {
                if (btn.Value == sender)
                {
                    EXP.Children.Remove(ExperienceRecords.Row[btn.Key]);
                    ExperienceRecords.DeleteThisGUID(btn.Key);
                    db.RemoveFromExperience(btn.Key);
                }

            }
        }
    }
    private void ExpInclude_Changed(object sender, EventArgs e)
    {
        foreach (var item in ExperienceRecords.ExpInclude)
        {
            if (sender == item.Value)
            {
                foreach (var expItem in db.Experience)
                {
                    if (item.Key == expItem["ItemGUID"])
                    {
                        if (item.Value.IsChecked)
                        {
                            expItem["ExpInclude"] = "True";
                        }
                        else
                        {
                            expItem["ExpInclude"] = "False";
                        }
                    }
                }
            }
        }
        db.UpdateDB("Experience");
    }
    private async void EXPupdateBtn_Clicked(object sender, EventArgs e)
    {
        bool action = true;
        Dictionary<string, string> Entries = new Dictionary<string, string>();
        Entries["Company"] = Company.Text;
        Entries["Position"] = Position.Text;
        Entries["ExpCountry"] = ExpCountry.Text;
        Entries["ExpCity"] = ExpCity.Text;
        Entries["ExpProvince"] = ExpProvince.Text;
        Entries["ExpFromMM"] = ExpFromMM.Text;
        Entries["ExpFromYYYY"] = ExpFromYYYY.Text;
        Entries["ExpToMM"] = ExpToMM.Text;
        Entries["ExpToYYYY"] = ExpToYYYY.Text;
        Entries["ExpDescription"] = ExpDescription.Text;
        Entries["ExpInclude"] = "True";
        Entries["ItemGUID"] = Guid.NewGuid().ToString();

        if (ExpCurrent.IsChecked)
            Entries["ExpCurrent"] = "False";
        else
        {
            Entries["ExpCurrent"] = "True";
        }
        string missingfields = "";
        foreach (var Entry in Entries)
        {
            if (Entry.Value == "" || Entry.Value == null)
            {
                missingfields = missingfields + Entry.Key + System.Environment.NewLine;
            }
        }
        if (missingfields != "")
        {
            string paragraph = "You are missing :" + System.Environment.NewLine + missingfields;
            string Header = "CONTINUE?";
            action = await DisplayAlert(Header, paragraph, "Yes", "No");
        }
        if (action)
        {
            createEXPRow(Entries);
            db.AddToExperience(Entries);
        }
    }

    private async void SKILLSdelBtn_Clicked(object sender, EventArgs e)
    {
        string Header = "Warning";
        string paragraph = "Entry will be deleted. This cannot be undone. Continue?";
        bool action = await DisplayAlert(Header, paragraph, "Yes", "No");
        if (action)
        {
            foreach (var btn in SkillsRecords.BtnDel)
            {
                if (btn.Value == sender)
                {
                    SKILLS.Children.Remove(SkillsRecords.Row[btn.Key]);
                    SkillsRecords.DeleteThisGUID(btn.Key);
                    db.RemoveFromSkills(btn.Key);
                }

            }
        }
    }
    private void SkillsInclude_Changed(object sender, EventArgs e)
    {
        foreach (var item in SkillsRecords.SkillsInclude)
        {
            if (sender == item.Value)
            {
                foreach (var skillItem in db.Skills)
                {
                    if (item.Key == skillItem["ItemGUID"])
                    {
                        if (item.Value.IsChecked)
                        {
                            skillItem["SkillInclude"] = "True";
                        }
                        else
                        {
                            skillItem["SkillInclude"] = "False";
                        }
                    }
                }
            }
        }
        db.UpdateDB("Skills");
    }
    private async void SKILLSupdateBtn_Clicked(object sender, EventArgs e)
    {
        bool action = true;
        Dictionary<string, string> Entries = new Dictionary<string, string>();
        Entries["Skill"] = Skill.Text;
        Entries["Proficiency"] = Proficiency.Text;
        Entries["Category"] = Category.Text;
        Entries["SkillInclude"] = "True";
        Entries["ItemGUID"] = Guid.NewGuid().ToString();

        string missingfields = "";
        foreach (var Entry in Entries)
        {
            if (Entry.Value == "" || Entry.Value == null)
            {
                missingfields = missingfields + Entry.Key + System.Environment.NewLine;
            }
        }
        if (missingfields != "")
        {
            string paragraph = "You are missing :" + System.Environment.NewLine + missingfields;
            string Header = "CONTINUE?";
            action = await DisplayAlert(Header, paragraph, "Yes", "No");
        }
        if (action)
        {
            createSKILLSRow(Entries);
            db.AddToSkills(Entries);
        }
    }

    private async void CERTdelBtn_Clicked(object sender, EventArgs e)
    {
        string Header = "Warning";
        string paragraph = "Entry will be deleted. This cannot be undone. Continue?";
        bool action = await DisplayAlert(Header, paragraph, "Yes", "No");
        if (action)
        {
            foreach (var btn in CertificationRecords.BtnDel)
            {
                if (btn.Value == sender)
                {
                    CERT.Children.Remove(CertificationRecords.Row[btn.Key]);
                    CertificationRecords.DeleteThisGUID(btn.Key);
                    db.RemoveFromCertifications(btn.Key);
                }

            }
        }
    }
    private void CertInclude_Changed(object sender, EventArgs e)
    {
        foreach (var item in CertificationRecords.CertInclude)
        {
            if (sender == item.Value)
            {
                foreach (var certItem in db.Certifications)
                {
                    if (item.Key == certItem["ItemGUID"])
                    {
                        if (item.Value.IsChecked)
                        {
                            certItem["EduInclude"] = "True";
                        }
                        else
                        {
                            certItem["EduInclude"] = "False";
                        }
                    }
                }
            }
        }
        db.UpdateDB("Certifications");
    }
    private async void CERTupdateBtn_Clicked(object sender, EventArgs e)
    {
        bool action = true;
        Dictionary<string, string> Entries = new Dictionary<string, string>();
        Entries["Certification"] = Certification.Text;
        Entries["Organization"] = Organization.Text;
        Entries["CertFromMM"] = CertFromMM.Text;
        Entries["CertFromYYYY"] = CertFromYYYY.Text;
        Entries["CertToMM"] = CertToMM.Text;
        Entries["CertToYYYY"] = CertToYYYY.Text;
        Entries["CertInclude"] = "True";
        Entries["ItemGUID"] = Guid.NewGuid().ToString();

        if (CertNotApplicable.IsChecked)
            Entries["CertNotApplicable"] = "False";
        else
        {
            Entries["CertNotApplicable"] = "True";
        }
        string missingfields = "";
        foreach (var Entry in Entries)
        {
            if (Entry.Value == "" || Entry.Value == null)
            {
                missingfields = missingfields + Entry.Key + System.Environment.NewLine;
            }
        }
        if (missingfields != "")
        {
            string paragraph = "You are missing :" + System.Environment.NewLine + missingfields;
            string Header = "CONTINUE?";
            action = await DisplayAlert(Header, paragraph, "Yes", "No");
        }
        if (action)
        {
            createCERTRow(Entries);
            db.AddToCertifications(Entries);
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
    public Dictionary<string, CheckBox> EduInclude;
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
        this.EduInclude[ItemGUID] = EduInclude;
        this.BtnDel[ItemGUID] = btnDel;
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
    Dictionary<string ,Label> Company;
    Dictionary<string ,Label> Position;
    Dictionary<string ,Label> ExpCountry;
    Dictionary<string ,Label> ExpCity;
    Dictionary<string ,Label> ExpProvince;
    Dictionary<string ,Label> ExpFromMM;
    Dictionary<string ,Label> ExpFromYYYY;
    Dictionary<string ,Label> ExpToMM;
    Dictionary<string ,Label> ExpToYYYY;
    Dictionary<string ,Label> ExpCurrent;
    Dictionary<string, Label> ExpDescription;
    public Dictionary<string, CheckBox> ExpInclude;
    public Dictionary<string, HorizontalStackLayout> Row;
    Dictionary<string, Button> Btn;
    public Dictionary<string, Button> BtnDel;

    public Experience()
    {
        Company = new Dictionary<string ,Label>();
        Position = new Dictionary<string ,Label>();
        ExpCountry = new Dictionary<string ,Label>();
        ExpCity = new Dictionary<string ,Label>();
        ExpProvince = new Dictionary<string ,Label>();
        ExpFromMM = new Dictionary<string ,Label>();
        ExpFromYYYY = new Dictionary<string ,Label>();
        ExpToMM = new Dictionary<string ,Label>();
        ExpToYYYY = new Dictionary<string ,Label>();
        ExpDescription = new Dictionary<string, Label>();
        ExpCurrent = new Dictionary<string, Label>();

        ExpInclude = new Dictionary<string, CheckBox>();
        Row = new Dictionary<string, HorizontalStackLayout>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }
    public void AddToRecords(string ItemGUID,
                            HorizontalStackLayout row,
                            Button btn,
                            Button btnDel,
                            Label Company,
                            Label Position,
                            Label ExpCountry,
                            Label ExpCity,
                            Label ExpProvince,
                            Label ExpFromMM,
                            Label ExpFromYYYY,
                            Label ExpToMM,
                            Label ExpToYYYY,
                            Label ExpCurrent,
                            Label ExpDescription,
                            CheckBox ExpInclude)
    {
        this.Row[ItemGUID] = row;
        this.Btn[ItemGUID] = btn;
        this.ExpInclude[ItemGUID] = ExpInclude;
        this.BtnDel[ItemGUID] = btnDel;
        this.Company[ItemGUID] = Company;
        this.Position[ItemGUID] = Position;
        this.ExpCountry[ItemGUID] = ExpCountry;
        this.ExpCity[ItemGUID] = ExpCity;
        this.ExpProvince[ItemGUID] = ExpProvince;
        this.ExpFromMM[ItemGUID] = ExpFromMM;
        this.ExpFromYYYY[ItemGUID] = ExpFromYYYY;
        this.ExpToMM[ItemGUID] = ExpToMM;
        this.ExpToYYYY[ItemGUID] = ExpToYYYY;
        this.ExpCurrent[ItemGUID] = ExpCurrent;
        this.ExpDescription[ItemGUID] = ExpDescription;
    }

    public void DeleteThisGUID(string ItemGUID)
    {
        this.Row.Remove(ItemGUID);
        this.Btn.Remove(ItemGUID);
        this.ExpInclude.Remove(ItemGUID);
        this.BtnDel.Remove(ItemGUID);
        this.Company.Remove(ItemGUID);
        this.Position.Remove(ItemGUID);
        this.ExpCountry.Remove(ItemGUID);
        this.ExpCity.Remove(ItemGUID);
        this.ExpProvince.Remove(ItemGUID);
        this.ExpFromMM.Remove(ItemGUID);
        this.ExpFromYYYY.Remove(ItemGUID);
        this.ExpToMM.Remove(ItemGUID);
        this.ExpToYYYY.Remove(ItemGUID);
        this.ExpCurrent.Remove(ItemGUID);
        this.ExpDescription.Remove(ItemGUID);
    }
}

public class Skills
{
    Dictionary<string, Label> Category;
    Dictionary<string, Label> Skill;
    Dictionary<string, Label> Proficiency;
    public Dictionary<string, CheckBox> SkillsInclude;
    public Dictionary<string, HorizontalStackLayout> Row;
    Dictionary<string, Button> Btn;
    public Dictionary<string, Button> BtnDel;

    public Skills()
    {
        Category = new Dictionary<string, Label>();
        Skill = new Dictionary<string, Label>();
        Proficiency = new Dictionary<string, Label>();
        SkillsInclude = new Dictionary<string, CheckBox>();
        Row = new Dictionary<string, HorizontalStackLayout>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(string ItemGUID,
                                HorizontalStackLayout row,
                                Button btn,
                                Button btnDel,
                                Label Category,
                                Label Proficiency,
                                Label Skill,
                                CheckBox SkillInclude)
    {

        this.Row[ItemGUID] = row;
        this.Btn[ItemGUID] = btn;
        this.SkillsInclude[ItemGUID] = SkillInclude;
        this.BtnDel[ItemGUID] = btnDel;
        this.Category[ItemGUID] = Category;
        this.Skill[ItemGUID] = Skill;
        this.Proficiency[ItemGUID] = Proficiency;
    }
    public void DeleteThisGUID(string ItemGUID)
    {
        this.Row.Remove(ItemGUID);
        this.Btn.Remove(ItemGUID);
        this.Category.Remove(ItemGUID);
        this.Skill.Remove(ItemGUID);
        this.SkillsInclude.Remove(ItemGUID);
        this.Proficiency.Remove(ItemGUID);
        this.BtnDel.Remove(ItemGUID);
    }
}

public class Certifications
{
    List<string> ItemGUID;
    Dictionary<string ,Label> Certification;
    Dictionary<string ,Label> Organization;
    Dictionary<string ,Label> CertFromMM;
    Dictionary<string ,Label> CertFromYYYY;
    Dictionary<string ,Label> CertToMM;
    Dictionary<string ,Label> CertToYYYY;
    Dictionary<string ,Label> CertNotApplicable;
    public Dictionary<string, CheckBox> CertInclude;
    public Dictionary<string, HorizontalStackLayout> Row;
    Dictionary<string, Button> Btn;
    public Dictionary<string, Button> BtnDel;

    public Certifications()
    {
        Certification = new Dictionary<string, Label>();
        Organization = new Dictionary<string, Label>();
        CertFromMM = new Dictionary<string, Label>();
        CertFromYYYY = new Dictionary<string, Label>();
        CertToMM = new Dictionary<string, Label>();
        CertNotApplicable = new Dictionary<string, Label>();
        CertToYYYY = new Dictionary<string, Label>();
        CertInclude = new Dictionary<string, CheckBox>();
        Row = new Dictionary<string, HorizontalStackLayout>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(string ItemGUID,
                                HorizontalStackLayout row,
                                Button btn,
                                Button btnDel,
                                Label Certification,
                                Label Organization,
                                Label CertFromMM,
                                Label CertFromYYYY,
                                Label CertToMM,
                                Label CertToYYYY,
                                Label CertNotApplicable,
                                CheckBox CertInclude)
    {

        this.Row[ItemGUID] = row;
        this.Btn[ItemGUID] = btn;
        this.CertInclude[ItemGUID] = CertInclude;
        this.BtnDel[ItemGUID] = btnDel;
        this.Certification[ItemGUID] = Certification;
        this.Organization[ItemGUID] = Organization;
        this.CertFromMM[ItemGUID] = CertFromMM;
        this.CertNotApplicable[ItemGUID] = CertNotApplicable;
        this.CertFromYYYY[ItemGUID] = CertFromYYYY;
        this.CertToMM[ItemGUID] = CertToMM;
        this.CertToYYYY[ItemGUID] = CertToYYYY;
    }
    public void DeleteThisGUID(string ItemGUID)
    {
        this.Row.Remove(ItemGUID);
        this.Btn.Remove(ItemGUID);
        this.Certification.Remove(ItemGUID);
        this.Organization.Remove(ItemGUID);
        this.CertInclude.Remove(ItemGUID);
        this.CertFromMM.Remove(ItemGUID);
        this.CertFromYYYY.Remove(ItemGUID);
        this.CertToYYYY.Remove(ItemGUID);
        this.CertToMM.Remove(ItemGUID);
        this.CertNotApplicable.Remove(ItemGUID);
        this.BtnDel.Remove(ItemGUID);
    }
}

