using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Security.Cryptography.X509Certificates;

namespace Resume_Generator;

public partial class Canvas : ContentPage
{
    ResumeManager user;
    List<Rectangle> MainColorList;
    Color MainColor = new Color(200,50,0);
    Color SecondaryColor = new Color(0,0,0);
    Color TertiaryColor = new Color(0,0,250);
    Color FontColor = new Color(0,250,0);
    public Canvas(ResumeManager db)
    {
		InitializeComponent();
        user = db;
        MainColorList = new List<Rectangle>();
        //FirstName.Text = db.FirstName;
        MainColorBtn.BackgroundColor = MainColor;
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
        FontColorBtn.BackgroundColor = FontColor;

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
        foreach (var item in MainColorList)
        {
            item.Background = MainColor;
        }

    }

    async private void SecondaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an R value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        SecondaryColor = new Color(Int32.Parse(result), 0, 0);
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
    }

    async private void TertiaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an B value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        TertiaryColor = new Color(0, 0, Int32.Parse(result));
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
    }

    async private void FontColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an G value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        FontColor = new Color(0, Int32.Parse(result), 0);
        FontColorBtn.BackgroundColor = FontColor;
    }

    private void Icon1_Clicked(object sender, EventArgs e)
    {
        Grid One = new Grid();
        One.ColumnDefinitions.Add(new ColumnDefinition(275));
        One.ColumnDefinitions.Add(new ColumnDefinition());
        Grid Two = new Grid();
        One.Children.Add(Two);
        Two.SetValue(Grid.ColumnProperty, "0");
        Rectangle rect1 = new Rectangle();
        rect1.Background = MainColor;
        rect1.WidthRequest = 300;
        rect1.HeightRequest = 300;
        rect1.VerticalOptions = LayoutOptions.Start;
        MainColorList.Add(rect1);
        Two.Children.Add(rect1);
        VerticalStackLayout vertstack = new VerticalStackLayout();
        Two.Children.Add(vertstack);
        foreach (var item in user.Education)
        {
            foreach (var kvp in item)
            {
                Editor school_editor = new Editor();
                school_editor.HeightRequest = 25;
                school_editor.Text = kvp.Value;
                school_editor.TextColor = FontColor;
                vertstack.Add(school_editor);
            }
        }

        CanvasBoundary.Children.Add(One); // VERY IMPORTANT!!!!




    }
}

public class Resumes
{
    public string styleName { get; set; }
    public string ImageUrl { get; set; }
}