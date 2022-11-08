namespace Resume_Generator;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var navpage = new NavigationPage(new Canvas());
        MainPage = navpage;

	}
}