namespace Resume_Generator;

public partial class PostAnalysis : ContentPage
{
	public PostAnalysis()
	{
		InitializeComponent();
	}

	private void ProfileBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}

	private void CanvasBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Canvas());
	}

	private void LogoutBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Startup());
	}
}