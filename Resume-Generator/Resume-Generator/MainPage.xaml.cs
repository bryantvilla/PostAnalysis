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
        Grid Headers = new Grid();
        Headers.AddColumnDefinition(new ColumnDefinition(50));//0 selected
        Headers.AddColumnDefinition(new ColumnDefinition(125));//1 school name
        Headers.AddColumnDefinition(new ColumnDefinition(100));//2 level
        Headers.AddColumnDefinition(new ColumnDefinition(125));//3 fieldofstudy
        Headers.AddColumnDefinition(new ColumnDefinition(75));//4 from
        Headers.AddColumnDefinition(new ColumnDefinition(70));//5 to
        Headers.AddColumnDefinition(new ColumnDefinition(70));//6 current
        Headers.AddColumnDefinition(new ColumnDefinition(70));//7 city
        Headers.AddColumnDefinition(new ColumnDefinition(70));//8 province
        Headers.AddColumnDefinition(new ColumnDefinition(75));//9 btns

        Label SchoolName = new Label();
        Label EducationalLevel = new Label();
        Label FieldOfStudy = new Label();
        Label From = new Label();
        Label To = new Label();
        Label EduCurrent = new Label();//this is a label cause this ISNT the check box its the table value
        Label DegreeCity = new Label();
        Label EduProvince = new Label();

        SchoolName.Text = "Institute";
        EducationalLevel.Text = "Degree";
        FieldOfStudy.Text = "Field";
        From.Text = "Start Date";
        To.Text = "End Date";
        EduCurrent.Text = "Current";
        DegreeCity.Text = "City";
        EduProvince.Text = "Province";

        SchoolName.Style = App.Current.Resources["TableHeader"] as Style;
        EducationalLevel.Style = App.Current.Resources["TableHeader"] as Style;
        FieldOfStudy.Style = App.Current.Resources["TableHeader"] as Style;
        From.Style = App.Current.Resources["TableHeader"] as Style;
        To.Style = App.Current.Resources["TableHeader"] as Style;
        EduCurrent.Style = App.Current.Resources["TableHeader"] as Style;
        DegreeCity.Style = App.Current.Resources["TableHeader"] as Style;
        EduProvince.Style = App.Current.Resources["TableHeader"] as Style;

        Headers.Children.Add(SchoolName);
        Headers.Children.Add(EducationalLevel);
        Headers.Children.Add(FieldOfStudy);
        Headers.Children.Add(From);
        Headers.Children.Add(To);
        Headers.Children.Add(EduCurrent);
        Headers.Children.Add(DegreeCity);
        Headers.Children.Add(EduProvince);

        SchoolName.SetValue(Grid.ColumnProperty, 1);
        EducationalLevel.SetValue(Grid.ColumnProperty, 2);
        FieldOfStudy.SetValue(Grid.ColumnProperty, 3);
        From.SetValue(Grid.ColumnProperty, 4);
        To.SetValue(Grid.ColumnProperty, 5);
        EduCurrent.SetValue(Grid.ColumnProperty, 6);
        DegreeCity.SetValue(Grid.ColumnProperty, 7);
        EduProvince.SetValue(Grid.ColumnProperty, 8);

        EDU.Children.Add(Headers);

        foreach (var EducationItem in Education) {
            createEDURow(EducationItem);
		}

	}

	private void createEDURow(Dictionary<string, string> EducationItem) {

        HorizontalStackLayout row = new HorizontalStackLayout();

        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 selected
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 school name
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 level
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 fieldofstudy
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//4 from
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//5 to
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//6 current
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//7 city
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//8 province
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//9 btns

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
        
        btn.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        gridRow.Children.Add(EduInclude);
        
        
        gridRow.Children.Add(SchoolName);
        gridRow.Children.Add(EducationalLevel);
        gridRow.Children.Add(FieldOfStudy);

        HorizontalStackLayout From = new HorizontalStackLayout();
        Label fowardslash = new Label();
        fowardslash.Style = App.Current.Resources["TableLabel"] as Style;
        fowardslash.Text = "/";
        From.Children.Add(EduFromMM);
        From.Children.Add(fowardslash);
        From.Children.Add(EduFromYYYY);
        gridRow.Children.Add(From);

        HorizontalStackLayout To = new HorizontalStackLayout();
        Label fowardslash2 = new Label();
        fowardslash2.Text = "/";
        fowardslash2.Style = App.Current.Resources["TableLabel"] as Style;
        To.Children.Add(EduToMM);
        To.Children.Add(fowardslash2);
        To.Children.Add(EduToYYYY);
        gridRow.Children.Add(To);

        gridRow.Children.Add(EduCurrent);
        gridRow.Children.Add(DegreeCity);
        gridRow.Children.Add(EduProvince);

        HorizontalStackLayout temp = new HorizontalStackLayout();
        temp.Children.Add(btn);
        temp.Children.Add(btnDel);
        gridRow.Children.Add(temp);

        EduInclude.SetValue(Grid.ColumnProperty, 0);
        SchoolName.SetValue(Grid.ColumnProperty, 1);
        EducationalLevel.SetValue(Grid.ColumnProperty, 2);
        FieldOfStudy.SetValue(Grid.ColumnProperty, 3);
        From.SetValue(Grid.ColumnProperty, 4);
        To.SetValue(Grid.ColumnProperty, 5);
        EduCurrent.SetValue(Grid.ColumnProperty, 6);
        DegreeCity.SetValue(Grid.ColumnProperty, 7);
        EduProvince.SetValue(Grid.ColumnProperty, 8);
        temp.SetValue(Grid.ColumnProperty, 9);

        //row.Style = App.Current.Resources[stylestr] as Style;
        EDU.Children.Add(gridRow);
        EducationRecords.AddToRecords(
            EducationItem["ItemGUID"],
            gridRow, 

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
	}

    private void CreateEXPTable(List<Dictionary<string, string>> Experience)
    {
        var bckgrndclr = "Hrz1";
        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 Company
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 Position
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 Country
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//4 City
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//5 Province
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//6 From
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//7 To
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//8 Current
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//9 btns

        Label Company = new Label();
        Label Position = new Label();
        Label ExpCountry = new Label();
        Label ExpCity = new Label();
        Label ExpProvince = new Label();
        Label From = new Label();
        Label To = new Label();
        Label ExpCurrent = new Label();
        Label ExpDescription = new Label();

        Company.Text = "Company";
        Position.Text = "Position";
        ExpCountry.Text = "Country";
        ExpCity.Text = "City";
        ExpProvince.Text = "Province";
        From.Text = "Start Date";
        To.Text = "End Date";
        ExpCurrent.Text = "Current";

        Company.Style = App.Current.Resources["TableHeader"] as Style;
        Position.Style = App.Current.Resources["TableHeader"] as Style;
        ExpCountry.Style = App.Current.Resources["TableHeader"] as Style;
        ExpCity.Style = App.Current.Resources["TableHeader"] as Style;
        ExpProvince.Style = App.Current.Resources["TableHeader"] as Style;
        From.Style = App.Current.Resources["TableHeader"] as Style;
        To.Style = App.Current.Resources["TableHeader"] as Style;
        ExpCurrent.Style = App.Current.Resources["TableHeader"] as Style;

        gridRow.Children.Add(Company);
        gridRow.Children.Add(Position);
        gridRow.Children.Add(ExpCountry);
        gridRow.Children.Add(ExpCity);
        gridRow.Children.Add(ExpProvince);
        gridRow.Children.Add(From);
        gridRow.Children.Add(To);
        gridRow.Children.Add(ExpCurrent);

        Company.SetValue(Grid.ColumnProperty, 1);
        Position.SetValue(Grid.ColumnProperty, 2);
        ExpCountry.SetValue(Grid.ColumnProperty, 3);
        ExpCity.SetValue(Grid.ColumnProperty, 4);
        ExpProvince.SetValue(Grid.ColumnProperty, 5);
        From.SetValue(Grid.ColumnProperty, 6);
        To.SetValue(Grid.ColumnProperty, 7);
        ExpCurrent.SetValue(Grid.ColumnProperty, 8);

        EXP.Children.Add(gridRow);

        foreach (var ExperienceItem in Experience)
        {
            createEXPRow(ExperienceItem);
        }

    }

    private void createEXPRow(Dictionary<string, string> ExperienceItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();

        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 Company
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 Position
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 Country
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//4 City
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//5 Province
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//6 From
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//7 To
        gridRow.AddColumnDefinition(new ColumnDefinition(70));//8 Current
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//9 btns

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

        btn.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        gridRow.Children.Add(ExpInclude);

        gridRow.Children.Add(Company);
        gridRow.Children.Add(Position);
        gridRow.Children.Add(ExpCountry);
        gridRow.Children.Add(ExpCity);
        gridRow.Children.Add(ExpProvince);

        HorizontalStackLayout From = new HorizontalStackLayout();
        Label fowardslash = new Label();
        fowardslash.Style = App.Current.Resources["TableLabel"] as Style;
        fowardslash.Text = "/";
        From.Children.Add(ExpFromMM);
        From.Children.Add(fowardslash);
        From.Children.Add(ExpFromYYYY);
        gridRow.Children.Add(From);

        HorizontalStackLayout To = new HorizontalStackLayout();
        Label fowardslash2 = new Label();
        fowardslash2.Text = "/";
        fowardslash2.Style = App.Current.Resources["TableLabel"] as Style;
        To.Children.Add(ExpToMM);
        To.Children.Add(fowardslash2);
        To.Children.Add(ExpToYYYY);
        gridRow.Children.Add(To);
        gridRow.Children.Add(ExpCurrent);
        //row.Children.Add(ExpDescription);

        HorizontalStackLayout temp = new HorizontalStackLayout();
        temp.Children.Add(btn);
        temp.Children.Add(btnDel);
        gridRow.Children.Add(temp);

        ExpInclude.SetValue(Grid.ColumnProperty, 0);
        Company.SetValue(Grid.ColumnProperty, 1);
        Position.SetValue(Grid.ColumnProperty, 2);
        ExpCountry.SetValue(Grid.ColumnProperty, 3);
        ExpCity.SetValue(Grid.ColumnProperty, 4);
        ExpProvince.SetValue(Grid.ColumnProperty, 5);
        From.SetValue(Grid.ColumnProperty, 6);
        To.SetValue(Grid.ColumnProperty, 7);
        ExpCurrent.SetValue(Grid.ColumnProperty, 8);
        temp.SetValue(Grid.ColumnProperty, 9);

        //row.Style = App.Current.Resources[stylestr] as Style;
        EXP.Children.Add(gridRow);
        ExperienceRecords.AddToRecords(
            ExperienceItem["ItemGUID"],
            gridRow,
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
    }

    private void CreateSKILLSTable(List<Dictionary<string, string>> Skills)
    {
        var bckgrndclr = "Hrz1";
        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 Category
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 skill
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 Proficiency
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//4 btns

        Label Category = new Label();
        Label Skill = new Label();
        Label Proficiency = new Label();

        Category.Text = "Category";
        Skill.Text = "Skill";
        Proficiency.Text = "Proficiency";

        Category.Style = App.Current.Resources["TableHeader"] as Style;
        Skill.Style = App.Current.Resources["TableHeader"] as Style;
        Proficiency.Style = App.Current.Resources["TableHeader"] as Style;

        gridRow.Children.Add(Category);
        gridRow.Children.Add(Skill);
        gridRow.Children.Add(Proficiency);

        Category.SetValue(Grid.ColumnProperty, 1);
        Skill.SetValue(Grid.ColumnProperty, 2);
        Proficiency.SetValue(Grid.ColumnProperty, 3);

        SKILLS.Children.Add(gridRow);
        foreach (var SkillsItem in Skills)
        {
            createSKILLSRow(SkillsItem);
        }

    }
    private void createSKILLSRow(Dictionary<string, string> SkillsItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();

        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 Category
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 skill
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 Proficiency
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//4 btns

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
        btn.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);

        gridRow.Children.Add(SkillsInclude);

        gridRow.Children.Add(Category);
        gridRow.Children.Add(Skill);
        gridRow.Children.Add(Proficiency);

        HorizontalStackLayout temp = new HorizontalStackLayout();
        temp.Children.Add(btn);
        temp.Children.Add(btnDel);
        gridRow.Children.Add(temp);

        SkillsInclude.SetValue(Grid.ColumnProperty, 0);
        Category.SetValue(Grid.ColumnProperty, 1);
        Skill.SetValue(Grid.ColumnProperty, 2);
        Proficiency.SetValue(Grid.ColumnProperty, 3);
        temp.SetValue(Grid.ColumnProperty, 4);

        //row.Style = App.Current.Resources[stylestr] as Style;
        SKILLS.Children.Add(gridRow);
        SkillsRecords.AddToRecords(
            SkillsItem["ItemGUID"],
            gridRow,
            btn,
            btnDel,
            Category,
            Skill,
            Proficiency,
            SkillsInclude
            );
    }

    private void CreateCERTTable(List<Dictionary<string, string>> Certifications)
    {
        var bckgrndclr = "Hrz1";
        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 certification
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 organization
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 from
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 to
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 notapplicable
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//4 btns

        Label Certification = new Label();
        Label Organization = new Label();
        Label From = new Label();
        Label To = new Label();
        Label CertNotApplicable = new Label();

        Certification.Text = "Certification";
        Organization.Text = "Organization";
        From.Text = "ISS Date";
        To.Text = "Exp Date";
        CertNotApplicable.Text = "N/A";

        Certification.Style = App.Current.Resources["TableHeader"] as Style;
        Organization.Style = App.Current.Resources["TableHeader"] as Style;
        From.Style = App.Current.Resources["TableHeader"] as Style;
        To.Style = App.Current.Resources["TableHeader"] as Style;
        CertNotApplicable.Style = App.Current.Resources["TableHeader"] as Style;

        gridRow.Children.Add(Certification);
        gridRow.Children.Add(Organization);
        gridRow.Children.Add(From);
        gridRow.Children.Add(To);
        gridRow.Children.Add(CertNotApplicable);

        Certification.SetValue(Grid.ColumnProperty, 1);
        Organization.SetValue(Grid.ColumnProperty, 2);
        From.SetValue(Grid.ColumnProperty, 3);
        To.SetValue(Grid.ColumnProperty, 4);
        CertNotApplicable.SetValue(Grid.ColumnProperty, 5);

        //row.Style = App.Current.Resources[stylestr] as Style;
        CERT.Children.Add(gridRow);
        foreach (var CertificationItem in Certifications)
        {
            createCERTRow(CertificationItem);
        }

    }
    private void createCERTRow(Dictionary<string, string> CertificationItem)
    {

        HorizontalStackLayout row = new HorizontalStackLayout();

        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(50));//0 Include
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//1 certification
        gridRow.AddColumnDefinition(new ColumnDefinition(100));//2 organization
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 from
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 to
        gridRow.AddColumnDefinition(new ColumnDefinition(125));//3 notapplicable
        gridRow.AddColumnDefinition(new ColumnDefinition(75));//4 btns

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

        btn.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.Style = App.Current.Resources["TableButton"] as Style;
        btnDel.BackgroundColor = new Color(250, 0, 0);
        gridRow.Children.Add(CertInclude);

        gridRow.Children.Add(Certification);
        gridRow.Children.Add(Organization);
        HorizontalStackLayout From = new HorizontalStackLayout();
        Label fowardslash = new Label();
        fowardslash.Style = App.Current.Resources["TableLabel"] as Style;
        fowardslash.Text = "/";
        From.Children.Add(CertFromMM);
        From.Children.Add(fowardslash);
        From.Children.Add(CertFromYYYY);
        gridRow.Children.Add(From);

        HorizontalStackLayout To = new HorizontalStackLayout();
        Label fowardslash2 = new Label();
        fowardslash2.Text = "/";
        fowardslash2.Style = App.Current.Resources["TableLabel"] as Style;
        To.Children.Add(CertToMM);
        To.Children.Add(fowardslash2);
        To.Children.Add(CertToYYYY);
        gridRow.Children.Add(To);
        //row.Children.Add(ExpDescription);
        gridRow.Children.Add(CertNotApplicable);

        HorizontalStackLayout temp = new HorizontalStackLayout();
        temp.Children.Add(btn);
        temp.Children.Add(btnDel);
        gridRow.Children.Add(temp);

        CertInclude.SetValue(Grid.ColumnProperty, 0);
        Certification.SetValue(Grid.ColumnProperty, 1);
        Organization.SetValue(Grid.ColumnProperty, 2);
        From.SetValue(Grid.ColumnProperty, 3);
        To.SetValue(Grid.ColumnProperty, 4);
        CertNotApplicable.SetValue(Grid.ColumnProperty, 5);
        temp.SetValue(Grid.ColumnProperty, 6);

        //row.Style = App.Current.Resources[stylestr] as Style;
        CERT.Children.Add(gridRow);
        CertificationRecords.AddToRecords(
            CertificationItem["ItemGUID"],
            gridRow,
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
    public Dictionary<string, Grid> Row;
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
        Row = new Dictionary<string, Grid>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(   string ItemGUID,
                                Grid row,
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
    public Dictionary<string, Grid> Row;
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
        Row = new Dictionary<string, Grid>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }
    public void AddToRecords(string ItemGUID,
                            Grid row,
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
    public Dictionary<string, Grid> Row;
    Dictionary<string, Button> Btn;
    public Dictionary<string, Button> BtnDel;

    public Skills()
    {
        Category = new Dictionary<string, Label>();
        Skill = new Dictionary<string, Label>();
        Proficiency = new Dictionary<string, Label>();
        SkillsInclude = new Dictionary<string, CheckBox>();
        Row = new Dictionary<string, Grid>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(string ItemGUID,
                                Grid row,
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
    public Dictionary<string, Grid> Row;
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
        Row = new Dictionary<string, Grid>();
        Btn = new Dictionary<string, Button>();
        BtnDel = new Dictionary<string, Button>();

    }

    public void AddToRecords(string ItemGUID,
                                Grid row,
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

