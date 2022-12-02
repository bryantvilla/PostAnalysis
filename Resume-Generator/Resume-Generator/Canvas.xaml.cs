using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Security.Cryptography.X509Certificates;

namespace Resume_Generator;

public partial class Canvas : ContentPage
{
    ResumeManager user;
    List<Rectangle> MainColorList;
    List<Rectangle> SecondaryColorList;
    Color BackGroundColor = new Color(255, 255, 255);
    Color MainColor = new Color(200,50,0);
    Color SecondaryColor = new Color(0,0,0);
    Color TertiaryColor = new Color(0,0,250);
    Color FontColor = new Color(0,250,0);
    Color FontColorSecondary = new Color(0, 0, 0);
    public Canvas(ResumeManager db)
    {
		InitializeComponent();
        user = db;
        MainColorList = new List<Rectangle>();
        SecondaryColorList = new List<Rectangle>();
        //FirstName.Text = db.FirstName;
        BackgroundColorBtn.BackgroundColor = BackGroundColor;
        MainColorBtn.BackgroundColor = MainColor;
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
        FontColorBtn.BackgroundColor = FontColor;
        FontColorSecondaryBtn.BackgroundColor = FontColorSecondary;
        CanvasBoundary.BackgroundColor = BackGroundColor;

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
        if (result != null) {
            MainColor = new Color(Int32.Parse(result), 0, 0);
        }
        MainColorBtn.BackgroundColor = MainColor;
        foreach (var item in MainColorList)
        {
            item.Background = MainColor;
        }

    }

    async private void SecondaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an R value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        if (result != null)
        {
            SecondaryColor = new Color(Int32.Parse(result), 0, 0);
        }
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        foreach (var item in SecondaryColorList)
        {
            item.Background = SecondaryColor;
        }
    }

    async private void TertiaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an B value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        if (result != null)
        {
            TertiaryColor = new Color(Int32.Parse(result), 0, 0);
        }
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
    }

    async private void FontColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an G value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        if (result != null)
        {
            FontColor = new Color(Int32.Parse(result), 0, 0);
        }
        FontColorBtn.BackgroundColor = FontColor;
    }

    async private void FontColorSecondaryBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an G value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        if (result != null)
        {
            FontColorSecondary = new Color(Int32.Parse(result), 0, 0);
        }
        FontColorSecondaryBtn.BackgroundColor = FontColorSecondary;
    }

    async private void BackgroundColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 2", "give me an G value", initialValue: "250", maxLength: 3, keyboard: Keyboard.Numeric);
        if (result != null)
        {
            BackGroundColor = new Color(Int32.Parse(result), 0, 0);
        }
        BackgroundColorBtn.BackgroundColor = BackGroundColor;
        CanvasBoundary.BackgroundColor = BackGroundColor;
    }

    // Resume Template One Function
    private void Icon1_Clicked(object sender, EventArgs e)
    {
        // Clear the current template displayed, if any
        if (CanvasBoundary.Children.Count > 0)
        {
            CanvasBoundary.Children.Clear();
        };

        // Main grid housing all content
        Grid Main = new Grid();
        Main.ColumnDefinitions.Add(new ColumnDefinition(270));
        Main.ColumnDefinitions.Add(new ColumnDefinition(400));

        // First Column Start
        Grid One = new Grid();
        Main.Children.Add(One);
        Main.SetColumn(One, 0);

        Rectangle rect1 = new Rectangle() 
        {
            Background = MainColor,
            WidthRequest = 270,
            HeightRequest = 250,
            VerticalOptions = LayoutOptions.Start
        };
        MainColorList.Add(rect1);
        One.Children.Add(rect1);

        Rectangle rect2 = new Rectangle()
        {
            Background = SecondaryColor,
            WidthRequest = 270,
            HeightRequest = 670,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0,0,0,60)
        };
        SecondaryColorList.Add(rect2);
        One.Children.Add(rect2);

        // Icon
        Frame mainicon = new Frame()
        {
            VerticalOptions = LayoutOptions.Start,
            HeightRequest = 270,
            WidthRequest = 270,
            CornerRadius = 135,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 100, 0, 0),
            BorderColor = MainColor,
            Content = new Image()
            {
                WidthRequest = 270,
                HeightRequest = 270,
                Source = "person_image.jpg"
            }
        };
        One.Children.Add(mainicon);
        

        // First Name
        Label name = new Label()
        {
            Text = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName,
            TextColor = FontColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 28,
            //FontAutoScalingEnabled = true,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 410, 0, 0)
        };
        One.Children.Add(name);
        // First Column End

        // Second Column Start
        Grid Two = new Grid();
        Main.SetColumn(Two, 1);
        Main.Children.Add(Two);

        // Vertical Stack Layout to keep all content vertical here
        VerticalStackLayout vertstack = new VerticalStackLayout();
        Two.Children.Add(vertstack);

        // Begin Education with title
        Label edtitle = new Label()
        {
            Text = "Education",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Center
        };
        vertstack.Children.Add(edtitle);

        // Education Details
        foreach (var item in user.Education)
        {
            /* foreach (var kvp in item)
            {
                Editor school_editor = new Editor();
                school_editor.HeightRequest = 10;
                school_editor.Text = kvp.Value + "\nSome text";
                school_editor.TextColor = FontColor;
                school_editor.FontSize = 8;
                accessor.Add(school_editor);
            }*/
            Editor left_side = new Editor()
            {
                Text = item["SchoolName"] + "\n" + item["EducationalLevel"],
                TextColor = FontColor
            };
            Editor right_side = new Editor()
            {
                Text = item["EduToYYYY"],
                HorizontalTextAlignment = TextAlignment.End
            };
            
            HorizontalStackLayout education_horiz = new HorizontalStackLayout();
            education_horiz.Add(left_side);
            education_horiz.Add(right_side);
            vertstack.Add(education_horiz);
        }
        // Second Column End

        CanvasBoundary.Children.Add(Main); // VERY IMPORTANT!!!! Add the main grid to the CanvasBoundary Grid in the Xaml




    }

    private void Icon2_Clicked(object sender, EventArgs e)
    {
        CanvasBoundary.Children.Clear();
        Grid Main = new Grid();
        


        CanvasBoundary.Children.Add(Main);
    }

    private void Icon4_Clicked(object sender, EventArgs e)
    {

    }
}

public class Resumes
{
    public string styleName { get; set; }
    public string ImageUrl { get; set; }
}