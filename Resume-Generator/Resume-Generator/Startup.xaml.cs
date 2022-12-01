namespace Resume_Generator;

public partial class Startup : ContentPage
{
	public Startup()
	{
		InitializeComponent();
	}

	private void ConfirmBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}