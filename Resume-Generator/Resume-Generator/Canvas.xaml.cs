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
            TextColor = FontColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 28,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 385, 0, 0)
        };
        if (user.MiddleName == "")
        {

            name.Text = user.FirstName + ' ' + user.LastName;
        } 
        else 
        {
            name.Text = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName;
                
        }
        
        One.Children.Add(name);

        //Contact Info
        VerticalStackLayout info = new VerticalStackLayout()
        {
            Margin = new Thickness(0, 435, 0, 0)
        };
        One.Children.Add(info);

        Thickness contact = new Thickness(0, 0, 0, 0);

        if (user.Profile["StreetAddress1"] != "")
        {
            info.Add(new Editor() { Text = user.Profile["StreetAddress1"], FontSize = 14, TextColor = FontColor, Margin = contact });
        }
        if (user.Profile["Email"] != "")
        {
            info.Add(new Editor() { Text = user.Profile["Email"], FontSize = 14, TextColor = FontColor, Margin = contact });
        }
        if (user.Profile["PhoneNo"] != "")
        {
            info.Add(new Editor() { Text = user.Profile["PhoneNo"], FontSize = 14, TextColor = FontColor, Margin = contact });
        }
        if (user.Profile["URL"] != "")
        {
            info.Add(new Editor() { Text = user.Profile["URL"], FontSize = 14, TextColor = FontColor, Margin = contact });
        }




        // First Column End

        // Second Column Start
        Grid Two = new Grid();
        Main.SetColumn(Two, 1);
        Main.Children.Add(Two);

        // Vertical Stack Layout to keep all content vertical here
        VerticalStackLayout vertstack = new VerticalStackLayout();
        Two.Children.Add(vertstack);


        // Begin Education with title
        
        Editor edtitle = new Editor()
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
            if (item["EduInclude"] == "True")
            {
                HorizontalStackLayout top_line_ed = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0,-5,0,0)
                };
                Editor school_name = new Editor()
                {
                    Text = item["SchoolName"],
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 320
                };
                Editor school_endyear = new Editor()
                {
                    Text = item["EduToYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 80
                };
                top_line_ed.Add(school_name);
                top_line_ed.Add(school_endyear);
                Editor school_location = new Editor()
                {
                    Text = item["DegreeCity"] + ", " + item["EduProvince"],
                    TextColor = FontColor,
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -12, 0, 10),
                    WidthRequest = 400
                };
                Editor school_degree = new Editor()
                {
                    Text = item["EducationalLevel"] + " in " + item["FieldOfStudy"],
                    TextColor = FontColor,
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -22, 0, 10),
                    WidthRequest = 400
                };
                vertstack.Add(top_line_ed);
                vertstack.Add(school_location);
                vertstack.Add(school_degree);
            }
        }
        // End Education



        // Begin Work Experience Details
        Editor worktitle = new Editor()
        {
            Text = "Work Experience",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Center
        };
        vertstack.Children.Add(worktitle);
        foreach (var item in user.Experience)
        {
            if (item["ExpInclude"] == "True")
            {
                HorizontalStackLayout top_line_exp = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, -5, 0, 0)
                };
                Editor work_name = new Editor()
                {
                    Text = item["Company"],
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 275
                };
                Editor work_years = new Editor()
                {
                    Text = item["ExpFromMM"] + "/" + item["ExpFromYYYY"] + " - " + item["ExpToMM"] + "/" + item["ExpToYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 125
                };
                top_line_exp.Add(work_name);
                top_line_exp.Add(work_years);
                vertstack.Add(top_line_exp);
                Editor work_pos = new Editor()
                {
                    Text = item["Position"],
                    FontSize = 14,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, -12, 0, 0),
                    WidthRequest = 400
                };
                vertstack.Add(work_pos);
                Editor work_location = new Editor()
                {
                    Text = item["ExpCity"] + ", " + item["ExpProvince"] + ", " + item["ExpCountry"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -12, 0, 10),
                    WidthRequest = 400
                };
                vertstack.Add(work_location);
                Editor work_desc = new Editor()
                {
                    Text = item["ExpDescription"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -22, 0, 10),
                    WidthRequest = 400
                };
                vertstack.Add(work_desc);
            }
        }
        // End Work Experience

        // Begin Certifications Details
        Editor certstitle = new Editor()
        {
            Text = "Certifications & Awards",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Center
        };
        vertstack.Children.Add(certstitle);
        foreach (var item in user.Certifications)
        {
            if (item["CertInclude"] == "True")
            {
                HorizontalStackLayout top_line_certs = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, -5, 0, 0)
                };
                Editor cert_name = new Editor()
                {
                    Text = item["Certification"],
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 275
                };
                Editor cert_date = new Editor()
                {
                    Text = "Achieved " + item["CertFromMM"] + "/" + item["CertFromYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 125
                };
                top_line_certs.Add(cert_name);
                top_line_certs.Add(cert_date);
                vertstack.Add(top_line_certs);
                Editor cert_org = new Editor()
                {
                    Text = item["Organization"],
                    FontSize = 16,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, -12, 0, 0),
                    WidthRequest = 400
                };
                vertstack.Add(cert_org);
            }
        }
        // End Certifications


        // End Second Column

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