namespace MauiApp2;

public partial class MainPage : ContentPage
{
	int profilecount = 0;
    int experiencecount = 0;
    int educationcount = 0;
    int postanalysiscount = 0;
    int generatecount = 0;


    public MainPage()
	{
		InitializeComponent();
	}
    private void ProfileBtnClicked(object sender, EventArgs e)
    {
        profilecount = testButton(profilecount, ProfileBtn, "Profile");
    }
    private void EducationBtnClicked(object sender, EventArgs e) 
    {
        educationcount = testButton(educationcount, EducationBtn, "Education");
    }
    private void ExperienceBtnClicked(object sender, EventArgs e)
    {
        experiencecount = testButton(experiencecount, ExperienceBtn, "Experience");
    }
    private void PostAnalysisBtnClicked(object sender, EventArgs e)
    {
        postanalysiscount = testButton(postanalysiscount, PostAnalysisBtn, "Post");
    }
    private void GenerateBtnClicked(object sender, EventArgs e)
    {
        generatecount = testButton(generatecount,GenerateBtn,"Generate");
    }
    private int testButton(int tempcount, Button buttonobj, string name) {
        tempcount++;

        if (tempcount == 1)
            buttonobj.Text = $"{name} Button Clicked {tempcount} time";
        else
            buttonobj.Text = $"{name} Button Clicked {tempcount} times";

        SemanticScreenReader.Announce(buttonobj.Text);
        return tempcount;
    }
}

