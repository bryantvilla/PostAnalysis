namespace Resume_Generator;

public partial class PostAnalysis : ContentPage
{
    ResumeManager db;
    public PostAnalysis(ResumeManager db)
	{
		this.db = db;
		InitializeComponent();
		results.Text = "display results here";

    }

	private void ProfileBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage(db));
	}

	private void CanvasBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Canvas(db));
	}

	private void LogoutBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Startup());
	}

    private void ProcessBtn_Clicked(object sender, EventArgs e)
    {
		string placeholderstr = results.Text;
		string tempstr = URL.Text + " " + Position.Text + " " + Country.Text + " " + Province.Text + " " + City.Text;
		if (tempstr != "    ")
		{
			results.Text = tempstr;
		}else
		{
			results.Text = "PLEASE ENTER VALID INFORMATION";
		}
    }
}