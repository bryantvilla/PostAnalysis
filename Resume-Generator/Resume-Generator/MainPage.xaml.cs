namespace Resume_Generator;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private void LogoutBtn_Clicked(object sender, EventArgs e)
	{
        Navigation.PushAsync(new Startup());
    }

	private void CanvasBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Canvas());
	}

	private void PostAnalysisBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PostAnalysis());
	}
}

