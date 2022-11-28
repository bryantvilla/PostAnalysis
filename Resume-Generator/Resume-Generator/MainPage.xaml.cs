namespace Resume_Generator;

public partial class MainPage : ContentPage
{
	DBManager db;

	public MainPage(DBManager db)
	{
		db = db;
		InitializeComponent();
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
}

