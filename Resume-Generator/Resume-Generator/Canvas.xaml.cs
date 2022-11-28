using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System.Security.Cryptography.X509Certificates;

namespace Resume_Generator;

public partial class Canvas : ContentPage
{
    ResumeManager user;
    Color MainColor = new Color(200,50,0);
    Color SecondaryColor = new Color(0,0,0);
    Color TertiaryColor = new Color(0,0,250);
    Color FontColor = new Color(0,250,0);
    public Canvas(ResumeManager db)
    {
        user = db;
		InitializeComponent();
        MainColorBtn.BackgroundColor = MainColor;
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
        FontColorBtn.BackgroundColor = FontColor;
        TestingLabel.TextColor = FontColor;
        MainColorTester.Fill = MainColor;
        SecondaryColorTester.Fill = SecondaryColor;
        MainColorTester.Stroke = TertiaryColor;
        SecondaryColorTester.Stroke = TertiaryColor;


    }
	private void createNewCarosel(string imagename) {
        CarouselView carouselView = new CarouselView();
        carouselView.ItemTemplate = new DataTemplate(() =>
        {

            Image image = new Image { Source = ImageSource.FromFile(imagename+".jpg") };
            image.SetBinding(Image.SourceProperty, "ImageUrl");

            Label styleNameLabel = new Label { Text = imagename};
            styleNameLabel.SetBinding(Label.TextProperty, "styleName");

            StackLayout stackLayout = new StackLayout();
            stackLayout.Add(image);
            stackLayout.Add(styleNameLabel);

            Frame frame = new Frame
            {
                HasShadow = true,
                BorderColor = Colors.DarkGray,
                CornerRadius = 5,
                Margin = 20,
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            StackLayout rootStackLayout = new StackLayout();
            rootStackLayout.Add(frame);

            return rootStackLayout;
        });

    }

	private void ProfileBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage(user));
	}

	private void PostAnalysisBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PostAnalysis(user));
	}

	private void LogoutBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Startup());
	}

    async private void MainColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an R value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        MainColor = new Color(Int32.Parse(result), 0, 0);
        MainColorBtn.BackgroundColor = MainColor;
        MainColorTester.Fill = MainColor;

    }

    async private void SecondaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an R value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        SecondaryColor = new Color(Int32.Parse(result), 0, 0);
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        SecondaryColorTester.Fill = SecondaryColor;
    }

    async private void TertiaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an B value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        TertiaryColor = new Color(0, 0, Int32.Parse(result));
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
        MainColorTester.Stroke = TertiaryColor;
        SecondaryColorTester.Stroke = TertiaryColor;
    }

    async private void FontColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an G value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        FontColor = new Color(0, Int32.Parse(result), 0);
        FontColorBtn.BackgroundColor = FontColor;
        TestingLabel.TextColor = FontColor;
    }
}

public class Resumes
{
    public string styleName { get; set; }
    public string ImageUrl { get; set; }
}