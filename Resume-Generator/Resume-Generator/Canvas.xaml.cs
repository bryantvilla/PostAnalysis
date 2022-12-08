using Microsoft.Maui.Controls.Shapes;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Image = Microsoft.Maui.Controls.Image;
using Path = System.IO.Path;
using Microsoft.Maui.Graphics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Maui.Controls;

namespace Resume_Generator;

public partial class Canvas : ContentPage
{
    ResumeManager user;
    List<Rectangle> MainColorList;
    List<Rectangle> SecondaryColorList;
    List<Frame> MainColorListFrame;
    List<Label> MainColorListLabels;
    List<Editor> MainColorListEditors;
    List<Label> SecondaryColorListLabels;
    List<Editor> SecondaryColorListEditors;
    List<Label> TertiaryColorListLabels;
    List<Editor> TertiaryColorListEditors;
    List<Label> PrimaryFontColorListLabels;
    List<Editor> PrimaryFontColorListEditors;
    List<Label> SecondaryFontColorListLabels;
    List<Editor> SecondaryFontColorListEditors;
    Color BackGroundColor = Color.FromArgb("#ffffff");
    Color MainColor = Color.FromArgb("#B6862C");
    Color SecondaryColor = Color.FromArgb("#081E3F");
    Color TertiaryColor = Color.FromArgb("#CC0066");
    Color FontColor = Color.FromArgb("#000000");
    Color FontColorSecondary = Color.FromArgb("#ffffff");
    List<String> iconList;
    Image mainiconimage;
    FileImageSource iconimageselected;


    public Canvas(ResumeManager db)
    {
		InitializeComponent();
        user = db;
        MainColorList = new List<Rectangle>();
        SecondaryColorList = new List<Rectangle>();
        MainColorListFrame = new List<Frame>();
        MainColorListLabels = new List<Label>();
        MainColorListEditors = new List<Editor>();
        SecondaryColorListLabels = new List<Label>();
        SecondaryColorListEditors = new List<Editor>();
        TertiaryColorListLabels = new List<Label>();
        TertiaryColorListEditors = new List<Editor>();
        PrimaryFontColorListLabels = new List<Label>();
        PrimaryFontColorListEditors = new List<Editor>();
        SecondaryFontColorListLabels = new List<Label>();
        SecondaryFontColorListEditors = new List<Editor>();
        BackgroundColorBtn.BackgroundColor = BackGroundColor;
        MainColorBtn.BackgroundColor = MainColor;
        SecondaryColorBtn.BackgroundColor = SecondaryColor;
        TertiaryColorBtn.BackgroundColor = TertiaryColor;
        FontColorBtn.BackgroundColor = FontColor;
        FontColorSecondaryBtn.BackgroundColor = FontColorSecondary;
        CanvasBoundary.BackgroundColor = BackGroundColor;
        var temp = ImageSource.FromFile("analysis_one.png");
        iconimageselected = (FileImageSource) temp;


        var iconList = new List<String>();
        iconList.Add("Analysis_one");
        iconList.Add("Analysis_two");
        iconList.Add("Analysis_three");
        iconList.Add("Dentist");
        iconList.Add("Gavel");
        iconList.Add("Hard_Hat_Worker");
        iconList.Add("Instructor");
        iconList.Add("IT");
        iconList.Add("Law");
        iconList.Add("Scientist_one");
        iconList.Add("Scientist_two");
        iconList.Add("Stethoscope");
        iconList.Add("Syringe");
        iconList.Add("Vet");

        IconPicker.ItemsSource = iconList;


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
        string result = null;

        result = await DisplayPromptAsync("Layout Main Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: MainColor.ToHex(), maxLength: 7, keyboard: Keyboard.Default);

        if (result == null || result.Length < 6)
        {
            result = MainColor.ToHex();
        }

        try
        {
            MainColor = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }

        MainColorBtn.BackgroundColor = MainColor;
        foreach (var item in MainColorList)
        {
            item.Background = MainColor;
        }
        foreach (var item in MainColorListFrame)
        {
            item.BorderColor = MainColor;
        }
        foreach (var item in MainColorListLabels)
        {
            item.TextColor = MainColor;
        }
        foreach (var item in MainColorListEditors)
        {
            item.TextColor = MainColor;
        }

    }

    async private void SecondaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = null;

        result = await DisplayPromptAsync("Layout Second Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: SecondaryColor.ToHex(), maxLength: 7, keyboard: Keyboard.Default);

        if (result == null)
        {
            result = SecondaryColor.ToHex();
        }

        try
        {
            SecondaryColor = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }

        SecondaryColorBtn.BackgroundColor = SecondaryColor;

        foreach (var item in SecondaryColorList)
        {
            item.Background = SecondaryColor;
        }
        foreach (var item in SecondaryColorListLabels)
        {
            item.TextColor = SecondaryColor;
        }
        foreach (var item in SecondaryColorListEditors)
        {
            item.TextColor = SecondaryColor;
        }
    }

    async private void TertiaryColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = null;

        result = await DisplayPromptAsync("Layout Tertiary Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: TertiaryColor.ToHex(), maxLength: 7, keyboard: Keyboard.Default);

        if (result == null)
        {
            result = TertiaryColor.ToHex();
        }

        try
        {
            TertiaryColor = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }

        TertiaryColorBtn.BackgroundColor = TertiaryColor;

        foreach (var item in TertiaryColorListLabels)
        {
            item.TextColor = TertiaryColor;
        }
        foreach (var item in TertiaryColorListEditors)
        {
            item.TextColor = TertiaryColor;
        }
    }

    async private void FontColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = null;

        result = await DisplayPromptAsync("Primary Font Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: FontColor.ToHex(), maxLength: 7, keyboard: Keyboard.Default);

        if (result == null)
        {
            result = FontColor.ToHex();
        }

        try
        {
            FontColor = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }

        FontColorBtn.BackgroundColor = FontColor;

        foreach (var item in PrimaryFontColorListLabels)
        {
            item.TextColor = FontColor;
        }
        foreach (var item in PrimaryFontColorListEditors)
        {
            item.TextColor = FontColor;
        }
    }

    async private void FontColorSecondaryBtn_Clicked(object sender, EventArgs e)
    {
        string result = null;

        result = await DisplayPromptAsync("Secondary Font Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: FontColorSecondary.ToHex(), maxLength: 7, keyboard: Keyboard.Default);

        if (result == null)
        {
            result = FontColorSecondary.ToHex();
        }

        try
        {
            FontColorSecondary = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }

        FontColorSecondaryBtn.BackgroundColor = FontColorSecondary;

        foreach (var item in SecondaryFontColorListLabels)
        {
            item.TextColor = FontColorSecondary;
        }
        foreach (var item in SecondaryFontColorListEditors)
        {
            item.TextColor = FontColorSecondary;
        }
    }

    async private void BackgroundColorBtn_Clicked(object sender, EventArgs e)
    {
        string result = null;

        result = await DisplayPromptAsync("Background Color", "Enter the hexadecimal value for the color you would like to use.", initialValue: BackGroundColor.ToHex(), maxLength: 7, keyboard: Keyboard.Default);
        
        if (result == null)
        {
            result = BackGroundColor.ToHex();
        }

        try
        {
            BackGroundColor = Color.FromArgb(result);
        }
        catch (System.FormatException)
        {
            await DisplayAlert("Cancelled", "Process Cancelled; No valid color given.", "Ok");
            return;
        }
        BackgroundColorBtn.BackgroundColor = BackGroundColor;
        CanvasBoundary.BackgroundColor = BackGroundColor;
    }

    // Resume Template One Function
    private void Icon1_Clicked(object sender, EventArgs e)
    {
        var myAssembly = typeof(Canvas).GetType().Assembly;
        string[] names = myAssembly.GetManifestResourceNames();

        // Clear the current template displayed, if any
        if (CanvasBoundary.Children.Count > 0)
        {
            CanvasBoundary.Children.Clear();
            MainColorList.Clear();
            SecondaryColorList.Clear();
            MainColorListFrame.Clear();
            MainColorListLabels.Clear();
            MainColorListEditors.Clear();
            SecondaryColorListLabels.Clear();
            SecondaryColorListEditors.Clear();
            TertiaryColorListLabels.Clear();
            TertiaryColorListEditors.Clear();
            PrimaryFontColorListLabels.Clear();
            PrimaryFontColorListEditors.Clear();
            SecondaryFontColorListLabels.Clear();
            SecondaryFontColorListEditors.Clear();
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
            HeightRequest = 200,
            VerticalOptions = LayoutOptions.Start
        };
        MainColorList.Add(rect1);
        One.Children.Add(rect1);

        Rectangle rect2 = new Rectangle()
        {
            Background = SecondaryColor,
            WidthRequest = 270,
            HeightRequest = 750,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0,0,0,30)
        };
        SecondaryColorList.Add(rect2);
        One.Children.Add(rect2);

        this.mainiconimage = new Image()
        {
            WidthRequest = 270,
            HeightRequest = 270,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Source = iconimageselected
        };
        
        // Icon
        Frame mainicon = new Frame()
        {
            VerticalOptions = LayoutOptions.Start,
            HeightRequest = 270,
            WidthRequest = 270,
            CornerRadius = 135,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 50, 0, 0),
            BorderColor = MainColor,
            BackgroundColor = MainColor,
            Content = mainiconimage
        };
        One.Children.Add(mainicon);
        MainColorListFrame.Add(mainicon);

        
        
        // First Name
        Label name = new Label()
        {
            TextColor = TertiaryColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 26,
            HorizontalTextAlignment = TextAlignment.Center,
            Padding = new Thickness(10, 0, 10, 0),
            Margin = new Thickness(0, 0, 0, 20)
        };
        if (user.MiddleName == "")
        {

            name.Text = user.FirstName + ' ' + user.LastName;
        } 
        else 
        {
            name.Text = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName;
                
        }
        TertiaryColorListLabels.Add(name);
        

        //Contact Info
        VerticalStackLayout info = new VerticalStackLayout()
        {
            Margin = new Thickness(0, 340, 0, 0)
        };
        One.Children.Add(info);
        info.Add(name);

        Thickness contact = new Thickness(0, 5, 0, 15);

        if (user.Profile["StreetAddress1"] != "")
        {
            HorizontalStackLayout address = new HorizontalStackLayout() { Margin = new Thickness(20,0,0,0) };
            Ellipse iconbkgd = new Ellipse() { Fill = MainColor, HeightRequest = 30, WidthRequest = 30, Margin = new Thickness(0, 0, -25, 10) };
            Image houseicon = new Image() { Source = ImageSource.FromFile("house_icon.png"), WidthRequest = 20, HeightRequest = 20, Margin = new Thickness(0, -12, 15, 0) };
            Label straddress = new Label() { Text = user.Profile["StreetAddress1"], FontSize = 14, TextColor = FontColorSecondary, Margin = contact };
            SecondaryFontColorListLabels.Add(straddress);
            address.Add(iconbkgd);
            address.Add(houseicon);
            address.Add(straddress);
            info.Add(address);
        }
        if (user.Profile["Email"] != "")
        {
            HorizontalStackLayout email = new HorizontalStackLayout() { Margin = new Thickness(20, 0, 0, 0) };
            Ellipse iconbkgd = new Ellipse() { Fill = MainColor, HeightRequest = 30, WidthRequest = 30, Margin = new Thickness(0, 0, -25, 10) };
            Image mailicon = new Image { Source = ImageSource.FromFile("mail_icon2.png"), WidthRequest = 20, HeightRequest = 20, Margin = new Thickness(0, -10, 15, 0) };
            Label emailaddr = new Label() { Text = user.Profile["Email"], FontSize = 14, TextColor = FontColorSecondary, Margin = contact };
            SecondaryFontColorListLabels.Add(emailaddr);
            email.Add(iconbkgd);
            email.Add(mailicon);
            email.Add(emailaddr);   
            info.Add(email);
        }
        if (user.Profile["PhoneNo"] != "")
        {
            HorizontalStackLayout phone = new HorizontalStackLayout() { Margin = new Thickness(20, 0, 0, 0) };
            Ellipse iconbkgd = new Ellipse() { Fill = MainColor, HeightRequest = 30, WidthRequest = 30, Margin = new Thickness(0, 0, -25, 10) };
            Image phoneicon = new Image() { Source = ImageSource.FromFile("phone_icon.png"), WidthRequest = 20, HeightRequest = 20, Margin = new Thickness(0, -10, 15, 0) };
            Label phonenum = new Label() { Text = user.Profile["PhoneNo"], FontSize = 14, TextColor = FontColorSecondary, Margin = contact };
            SecondaryFontColorListLabels.Add(phonenum);
            phone.Add(iconbkgd);
            phone.Add(phoneicon);
            phone.Add(phonenum);
            info.Add(phone);
        }
        if (user.Profile["URL"] != "")
        {
            HorizontalStackLayout urlline = new HorizontalStackLayout() { Margin = new Thickness(20, 0, 0, 0) };
            Ellipse iconbkgd = new Ellipse() { Fill = MainColor, HeightRequest = 30, WidthRequest = 30, Margin = new Thickness(0, 0, -25, 10) };
            Image webicon = new Image() { Source = ImageSource.FromFile("web_icon.png"), WidthRequest = 20, HeightRequest = 20, Margin = new Thickness(0, -10, 15, 0) };
            Label website = new Label() { Text = user.Profile["URL"], FontSize = 14, TextColor = FontColorSecondary, Margin = contact };
            SecondaryFontColorListLabels.Add(website);
            urlline.Add(iconbkgd);
            urlline.Add(webicon);
            urlline.Add(website);
            info.Add(urlline);
        }


        // Skills Section

        Label skillstitle = new Label()
        {
            Text = "Skills",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            TextColor = TertiaryColor,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0,10,0,10)
        };
        info.Add(skillstitle);
        TertiaryColorListLabels.Add(skillstitle);


        foreach (var item in user.Skills)
        {
            if (item["SkillInclude"] == "True")
            {
                Label skill_line = new Label() { Text = " " + item["Skill"] + ": " + item["Proficiency"], FontSize = 14, TextColor = FontColorSecondary, Margin = new Thickness(20, 0, 0, 10) };
                info.Add(skill_line);
                SecondaryFontColorListLabels.Add(skill_line);

            }
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
        MainColorListEditors.Add(edtitle);

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
                MainColorListEditors.Add(school_name);
                PrimaryFontColorListEditors.Add(school_endyear);
                PrimaryFontColorListEditors.Add(school_location);
                PrimaryFontColorListEditors.Add(school_degree);
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
        MainColorListEditors.Add(worktitle);

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
                MainColorListEditors.Add(work_name);
                PrimaryFontColorListEditors.Add(work_years);
                MainColorListEditors.Add(work_pos);
                PrimaryFontColorListEditors.Add(work_location);
                PrimaryFontColorListEditors.Add(work_desc);
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
        MainColorListEditors.Add(certstitle);

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
                    FontSize = 14,
                    TextColor = FontColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, -15, 0, 0),
                    WidthRequest = 400
                };
                vertstack.Add(cert_org);
                MainColorListEditors.Add(cert_name);
                PrimaryFontColorListEditors.Add(cert_date);
                PrimaryFontColorListEditors.Add(cert_org);
            }
        }
        // End Certifications
        /*string path = null;

        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources);

        string homeiconpath = Path.Combine(path, "AppIcon\\house_icon.png");

        vertstack.Add(new Label() { Text = path, TextColor = FontColor });
        vertstack.Add(new Image() { Source = homeiconpath, WidthRequest = 50 });*/

        // End Second Column

        CanvasBoundary.Children.Add(Main); // VERY IMPORTANT!!!! Add the main grid to the CanvasBoundary Grid in the Xaml




    }

    private void Icon2_Clicked(object sender, EventArgs e)
    {
        // Clear the current template displayed, if any
        if (CanvasBoundary.Children.Count > 0)
        {
            CanvasBoundary.Children.Clear();
            MainColorList.Clear();
            SecondaryColorList.Clear();
            MainColorListFrame.Clear();
            MainColorListLabels.Clear();
            MainColorListEditors.Clear();
            SecondaryColorListLabels.Clear();
            SecondaryColorListEditors.Clear();
            TertiaryColorListLabels.Clear();
            TertiaryColorListEditors.Clear();
            PrimaryFontColorListLabels.Clear();
            PrimaryFontColorListEditors.Clear();
            SecondaryFontColorListLabels.Clear();
            SecondaryFontColorListEditors.Clear();
        };

        // Main grid housing all content
        CanvasBoundary.Children.Clear();

        Color white = new Color(255, 255, 255); // White color
        Color black = new Color(0, 0, 0); // black color
        Color red = new Color(255, 0, 0); // red color


        VerticalStackLayout vsl = new VerticalStackLayout();
        

        // Place Name in a Label
        Label name = new Label()
        {
            Text = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName,
            TextColor = TertiaryColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 28,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(10, 5, 5, 10) // space the name away from the name border
        };
        TertiaryColorListLabels.Add(name);

        // Frame to nest the name in
        Frame nameFrame = new Frame()
        {
            Background = white,
            BorderColor = black,
            Content = name,
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            Margin = new Thickness(30, 20, 30, 20)
        };
        vsl.Add(nameFrame);

        // Contact Info Displayed Horizontally
        Editor contactInfo = new Editor()
        {
            Margin = new Thickness(10, 10, 10, 10),
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = TertiaryColor
        };
        TertiaryColorListEditors.Add(contactInfo);

        String contactString = "";
        
        if (user.Profile["StreetAddress1"] != "")
        {
            contactString += user.Profile["StreetAddress1"];
        }

        if (user.Profile["Email"] != "")
        {
            contactString += " | " + user.Profile["Email"];
        }

        if (user.Profile["PhoneNo"] != "")
        {
            contactString += " | " + user.Profile["PhoneNo"];
        }

        if (user.Profile["URL"] != "")
        {
            contactString += " | " + user.Profile["URL"];
        }

        contactInfo.Text = contactString;

        vsl.Add(contactInfo);

        this.mainiconimage = new Image()
        {
            WidthRequest = 100,
            HeightRequest = 100,
            Source = iconimageselected
        };

        // Icon
        Frame mainicon = new Frame()
        {
            VerticalOptions = LayoutOptions.Start,
            HeightRequest = 100,
            WidthRequest = 100,
            CornerRadius = 50,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 25, 0, 0),
            BorderColor = black,
            BackgroundColor = new Color(255, 255, 255),
            Content = mainiconimage
        };
        vsl.Add(mainicon);

        // Make Grid for bottom 2 columns
        Grid Bottom = new Grid();
        Bottom.Margin = new Thickness(0, 20, 0, 0);
        vsl.Add(Bottom);

        Bottom.ColumnDefinitions.Add(new ColumnDefinition(335));
        Bottom.ColumnDefinitions.Add(new ColumnDefinition(335));

        VerticalStackLayout left = new VerticalStackLayout();
        left.Margin = new Thickness(25, 0, 0, 0);
        VerticalStackLayout right = new VerticalStackLayout();
        right.Margin = new Thickness(0, 0, 25, 0);
        Bottom.SetColumn(left, 0);
        Bottom.SetColumn(right, 1);
        Bottom.Children.Add(left);
        Bottom.Children.Add(right);

        // Left Column: Education
        Editor edtitle = new Editor()
        {
            Text = "Education",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Start
        };
        left.Add(edtitle);
        MainColorListEditors.Add(edtitle);

        // Education Details
        foreach (var item in user.Education)
        {
            if (item["EduInclude"] == "True")
            {
                HorizontalStackLayout top_line_ed = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, -5, 0, 0)
                };
                Editor school_name = new Editor()
                {
                    Text = item["SchoolName"],
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 150
                };
                MainColorListEditors.Add(school_name);
                Editor school_endyear = new Editor()
                {
                    Text = item["EduToYYYY"],
                    TextColor = FontColor,
                    FontSize = 10,
                    Margin = new Thickness(80, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 80
                };
                top_line_ed.Add(school_name);
                top_line_ed.Add(school_endyear);
                PrimaryFontColorListEditors.Add(school_endyear);
                Editor school_location = new Editor()
                {
                    Text = item["DegreeCity"] + ", " + item["EduProvince"],
                    TextColor = FontColor,
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -5, 0, 10),
                    WidthRequest = 200
                };
                PrimaryFontColorListEditors.Add(school_location);
                Editor school_degree = new Editor()
                {
                    Text = item["EducationalLevel"] + " in " + item["FieldOfStudy"],
                    TextColor = FontColor,
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -5, 0, 10),
                    WidthRequest = 200
                };
                PrimaryFontColorListEditors.Add(school_degree);
                left.Add(top_line_ed);
                left.Add(school_location);
                left.Add(school_degree);
            }
        }
        // End Education

        // Left Column: Work Experience
        Editor worktitle = new Editor()
        {
            Text = "Work Experience",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Start
        };
        left.Children.Add(worktitle);
        MainColorListEditors.Add(worktitle);
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
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalTextAlignment = TextAlignment.Start,
                    WidthRequest = 200
                };
                MainColorListEditors.Add(work_name);
                Editor work_years = new Editor()
                {
                    Text = item["ExpFromMM"] + "/" + item["ExpFromYYYY"] + " - " + item["ExpToMM"] + "/" + item["ExpToYYYY"],
                    TextColor = FontColor,
                    FontSize = 10,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 100
                };
                PrimaryFontColorListEditors.Add(work_years);
                top_line_exp.Add(work_name);
                top_line_exp.Add(work_years);
                left.Add(top_line_exp);
                Editor work_pos = new Editor()
                {
                    Text = item["Position"],
                    FontSize = 10,
                    TextColor = MainColor,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 200
                };
                left.Add(work_pos);
                MainColorListEditors.Add(work_pos);
                Editor work_location = new Editor()
                {
                    Text = item["ExpCity"] + ", " + item["ExpProvince"] + ", " + item["ExpCountry"],
                    TextColor = FontColor,
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 200
                };
                PrimaryFontColorListEditors.Add(work_location);
                left.Add(work_location);
                Editor work_desc = new Editor()
                {
                    Text = item["ExpDescription"],
                    TextColor = FontColor,
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 200,
                    AutoSize = EditorAutoSizeOption.TextChanges,
                };
                PrimaryFontColorListEditors.Add(work_location);
                left.Add(work_desc);
            }
        }
        // End Work Experience

        // Right Column: Certifications
        Editor certificationsTitle = new Editor()
        {
            Text = "Certifications & Awards",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Start
        };
        right.Add(certificationsTitle);
        MainColorListEditors.Add(certificationsTitle);

        foreach (var item in user.Certifications)
        {
            if (item["CertInclude"] == "True")
            {
                HorizontalStackLayout hsl_certs = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, -5, 0, 0)
                };
                Editor cert_name = new Editor()
                {
                    Text = item["Certification"],
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 200
                };
                MainColorListEditors.Add(cert_name);
                Editor cert_date = new Editor()
                {
                    Text = "Achieved " + item["CertFromMM"] + "/" + item["CertFromYYYY"],
                    TextColor = FontColor,
                    FontSize = 10,
                    VerticalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(35, 0, 0, 0),
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 75
                };
                MainColorListEditors.Add(cert_date);
                hsl_certs.Add(cert_name);
                hsl_certs.Add(cert_date);
                right.Add(hsl_certs);
                Editor cert_org = new Editor()
                {
                    Text = item["Organization"],
                    FontSize = 10,
                    TextColor = FontColor,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 200
                };
                PrimaryFontColorListEditors.Add(cert_org);
                right.Add(cert_org);
            }
        }
        // End Certifications

        // Right Column: Skills
        Editor skillsTitle = new Editor()
        {
            Text = "Skills",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            HorizontalTextAlignment = TextAlignment.Start,
            Margin = new Thickness(0, 10, 0, 0)
        };
        right.Add(skillsTitle);
        MainColorListEditors.Add(skillsTitle);

        foreach (var item in user.Skills)
        {
            if (item["SkillInclude"] == "True")
            {
                HorizontalStackLayout hsl_skills = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Editor skill_name = new Editor()
                {
                    Text = item["Skill"],
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = FontColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 200
                };
                PrimaryFontColorListEditors.Add(skill_name);
                Editor skill_proficiency = new Editor()
                {
                    Text = item["Proficiency"],
                    TextColor = FontColor,
                    FontSize = 10,
                    VerticalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(35, 0, 0, 0),
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 75
                };
                PrimaryFontColorListEditors.Add(skill_proficiency);
                if (item["Category"] != null)
                {
                    skill_name.Text += " (" + item["Category"] + ")";
                }
                hsl_skills.Add(skill_name);
                hsl_skills.Add(skill_proficiency);
                right.Add(hsl_skills);
            }
        }
        // End Skills

        // End Right Column
        vsl.Padding = 1;
        CanvasBoundary.Children.Add(vsl);
    }

    private void Icon4_Clicked(object sender, EventArgs e)
    {
        // Clear the current template displayed, if any
        if (CanvasBoundary.Children.Count > 0)
        {
            CanvasBoundary.Children.Clear();
            MainColorList.Clear();
            SecondaryColorList.Clear();
            MainColorListFrame.Clear();
            MainColorListLabels.Clear();
            MainColorListEditors.Clear();
            SecondaryColorListLabels.Clear();
            SecondaryColorListEditors.Clear();
            TertiaryColorListLabels.Clear();
            TertiaryColorListEditors.Clear();
            PrimaryFontColorListLabels.Clear();
            PrimaryFontColorListEditors.Clear();
            SecondaryFontColorListLabels.Clear();
            SecondaryFontColorListEditors.Clear();
        };

        Color black = new Color(0, 0, 0); // black color

        // Main grid housing all content
        Grid Main = new Grid();
        Main.ColumnDefinitions.Add(new ColumnDefinition(270));
        Main.ColumnDefinitions.Add(new ColumnDefinition(400));

        // Left and Right Columns
        Grid LeftColumn = new Grid();
        Grid RightColumn = new Grid();
        Main.Children.Add(LeftColumn);
        Main.SetColumn(LeftColumn, 0);
        Main.Children.Add(RightColumn);
        Main.SetColumn(RightColumn, 1);

        // Left Column Background
        Rectangle rect1 = new Rectangle()
        {
            Background = new Color(255, 255, 255), // always will be white
            WidthRequest = 270,
            HeightRequest = 85,
            VerticalOptions = LayoutOptions.Start
        };
        LeftColumn.Children.Add(rect1);

        Rectangle rect2 = new Rectangle()
        {
            Background = MainColor,
            WidthRequest = 270,
            HeightRequest = 850,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 0, 30)
        };
        SecondaryColorList.Add(rect2);
        LeftColumn.Children.Add(rect2);

        // Left Column Vertical Stack Layout
        VerticalStackLayout vsl_Left = new VerticalStackLayout();
        LeftColumn.Add(vsl_Left);

        this.mainiconimage = new Image()
        {
            WidthRequest = 150,
            HeightRequest = 150,
            Source = iconimageselected
        };

        // Icon
        Frame mainicon = new Frame()
        {
            VerticalOptions = LayoutOptions.Start,
            HeightRequest = 150,
            WidthRequest = 150,
            CornerRadius = 75,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 20, 0, 0),
            BorderColor = MainColor,
            Background = new Color(255, 255, 255),
            Content = mainiconimage
        };
        vsl_Left.Add(mainicon);

        // Name
        Label name = new Label()
        {
            TextColor = TertiaryColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 26,
            HorizontalTextAlignment = TextAlignment.Center,
            Padding = new Thickness(10, 0, 10, 0),
            Margin = new Thickness(0, 40, 0, 20)
        };
        if (user.MiddleName == "")
        {

            name.Text = user.FirstName + ' ' + user.LastName;
        }
        else
        {
            name.Text = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName;

        }
        vsl_Left.Add(name);
        TertiaryColorListLabels.Add(name);

        // Left Column: Contact Information
        // Contact Label
        Thickness contact = new Thickness(0, 5, 0, 15); // for contact margin

        Label contactLabel = new Label()
        {
            Text = "Contact",
            TextColor = TertiaryColor,
            FontAttributes = FontAttributes.Bold,
            FontSize = 20,
            HorizontalTextAlignment = TextAlignment.Center,
            Padding = new Thickness(10, 0, 10, 0),
            Margin = new Thickness(0, 40, 0, 20)
        };
        vsl_Left.Add(contactLabel);
        TertiaryColorListLabels.Add(contactLabel);

        // Contact Information
        if (user.Profile["StreetAddress1"] != "")
        {
            HorizontalStackLayout address = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Center };
            Label addressLabel = new Label() { Text = "Address:", FontSize = 14, TextColor = FontColorSecondary, HorizontalOptions = LayoutOptions.Center };
            Label straddress = new Label() { Text = user.Profile["StreetAddress1"], FontSize = 14, TextColor = FontColorSecondary,
                HorizontalOptions = LayoutOptions.CenterAndExpand, Margin = contact };
            vsl_Left.Add(addressLabel);
            address.Add(straddress);
            vsl_Left.Add(address);
            SecondaryFontColorListLabels.Add(addressLabel);
            SecondaryFontColorListLabels.Add(straddress);
        }
        if (user.Profile["Email"] != "")
        {
            HorizontalStackLayout email = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Center };
            Label emailLabel = new Label() { Text = "Email:", FontSize = 14, TextColor = FontColorSecondary, HorizontalOptions = LayoutOptions.Center };
            Label emailaddr = new Label() { Text = user.Profile["Email"], FontSize = 14, TextColor = FontColorSecondary, 
                HorizontalOptions = LayoutOptions.Center, Margin = contact };
            vsl_Left.Add(emailLabel);
            email.Add(emailaddr);
            vsl_Left.Add(email);
            SecondaryFontColorListLabels.Add(emailLabel);
            SecondaryFontColorListLabels.Add(emailaddr);
        }
        if (user.Profile["PhoneNo"] != "")
        {
            HorizontalStackLayout phone = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Center };
            Label phoneLabel = new Label() { Text = "Phone:", FontSize = 14, TextColor = FontColorSecondary, HorizontalOptions = LayoutOptions.Center };
            Label phonenum = new Label() { Text = user.Profile["PhoneNo"], FontSize = 14, TextColor = FontColorSecondary,
                HorizontalOptions = LayoutOptions.Center, Margin = contact };
            vsl_Left.Add(phoneLabel);
            phone.Add(phonenum);
            vsl_Left.Add(phone);
            SecondaryFontColorListLabels.Add(phoneLabel);
            SecondaryFontColorListLabels.Add(phonenum);
        }
        if (user.Profile["URL"] != "")
        {
            HorizontalStackLayout urlline = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Center };
            Label websiteLabel = new Label() { Text = "Website:", FontSize = 14, TextColor = FontColorSecondary, HorizontalOptions = LayoutOptions.Center };
            Label website = new Label() { Text = user.Profile["URL"], FontSize = 14, TextColor = FontColorSecondary,
                HorizontalOptions = LayoutOptions.Center, Margin = contact };
            vsl_Left.Add(websiteLabel);
            urlline.Add(website);
            vsl_Left.Add(urlline);
            SecondaryFontColorListLabels.Add(websiteLabel);
            SecondaryFontColorListLabels.Add(website);
        }
        // End Contact Information

        // Left Column: Skills
        Editor skillsTitle = new Editor()
        {
            Text = "Skills",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = TertiaryColor,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(10, 10, 10, 0)
        };
        vsl_Left.Add(skillsTitle);
        TertiaryColorListEditors.Add(skillsTitle);

        foreach (var item in user.Skills)
        {
            if (item["SkillInclude"] == "True")
            {
                HorizontalStackLayout hsl_skills = new HorizontalStackLayout()
                {
                    Margin = new Thickness(10, 10, 0, 0)
                };
                Editor skill_name = new Editor()
                {
                    Text = item["Skill"],
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = FontColorSecondary,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 150
                };
                Editor skill_proficiency = new Editor()
                {
                    Text = item["Proficiency"],
                    TextColor = FontColorSecondary,
                    FontSize = 10,
                    VerticalTextAlignment = TextAlignment.End,
                    Margin = new Thickness(25, 0, 0, 0),
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 75
                };
                if (item["Category"] != null)
                {
                    skill_name.Text += " (" + item["Category"] + ")";
                }
                hsl_skills.Add(skill_name);
                hsl_skills.Add(skill_proficiency);
                vsl_Left.Add(hsl_skills);
                SecondaryFontColorListEditors.Add(skill_name);
                SecondaryFontColorListEditors.Add(skill_proficiency);
            }
        }
        // End Skills

        // End Left Column

        // Right Column Vertical Stack Layout
        VerticalStackLayout vsl_Right = new VerticalStackLayout();
        RightColumn.Add(vsl_Right);

        // Right Column: Education
        Editor edtitle = new Editor()
        {
            Text = "Education",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            Margin = new Thickness(0, 30, 0, 0),
            HorizontalTextAlignment = TextAlignment.Start
        };
        vsl_Right.Add(edtitle);
        MainColorListEditors.Add(edtitle);

        // Education Details
        foreach (var item in user.Education)
        {
            if (item["EduInclude"] == "True")
            {
                HorizontalStackLayout top_line_ed = new HorizontalStackLayout()
                {
                    Margin = new Thickness(0, -5, 0, 0)
                };
                Editor school_name = new Editor()
                {
                    Text = item["SchoolName"],
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = MainColor,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 250
                };
                MainColorListEditors.Add(school_name);
                Editor school_endyear = new Editor()
                {
                    Text = item["EduToYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    Margin = new Thickness(50, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 80
                };
                top_line_ed.Add(school_name);
                top_line_ed.Add(school_endyear);
                PrimaryFontColorListEditors.Add(school_endyear);
                Editor school_location = new Editor()
                {
                    Text = item["DegreeCity"] + ", " + item["EduProvince"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -5, 0, 10),
                    WidthRequest = 250
                };
                PrimaryFontColorListEditors.Add(school_location);
                Editor school_degree = new Editor()
                {
                    Text = item["EducationalLevel"] + " in " + item["FieldOfStudy"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(0, -5, 0, 10),
                    WidthRequest = 300
                };
                PrimaryFontColorListEditors.Add(school_degree);
                vsl_Right.Add(top_line_ed);
                vsl_Right.Add(school_location);
                vsl_Right.Add(school_degree);
            }
        }
        // End Education

        // Right Column: Work Experience
        Editor worktitle = new Editor()
        {
            Text = "Work Experience",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            Margin = new Thickness(0, 20, 0, 0),
            HorizontalTextAlignment = TextAlignment.Start
        };
        vsl_Right.Children.Add(worktitle);
        MainColorListEditors.Add(worktitle);

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
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalTextAlignment = TextAlignment.Start,
                    WidthRequest = 250
                };
                MainColorListEditors.Add(work_name);
                Editor work_years = new Editor()
                {
                    Text = item["ExpFromMM"] + "/" + item["ExpFromYYYY"] + " - " + item["ExpToMM"] + "/" + item["ExpToYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    Margin = new Thickness(50, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 80
                };
                PrimaryFontColorListEditors.Add(work_years);
                top_line_exp.Add(work_name);
                top_line_exp.Add(work_years);
                vsl_Right.Add(top_line_exp);
                Editor work_pos = new Editor()
                {
                    Text = item["Position"],
                    FontSize = 12,
                    TextColor = MainColor,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 250
                };
                vsl_Right.Add(work_pos);
                MainColorListEditors.Add(work_pos);
                Editor work_location = new Editor()
                {
                    Text = item["ExpCity"] + ", " + item["ExpProvince"] + ", " + item["ExpCountry"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 250
                };
                vsl_Right.Add(work_location);
                PrimaryFontColorListEditors.Add(work_location);
                Editor work_desc = new Editor()
                {
                    Text = item["ExpDescription"],
                    TextColor = FontColor,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, -5, 0, 0),
                    WidthRequest = 300,
                    AutoSize = EditorAutoSizeOption.TextChanges,
                };
                vsl_Right.Add(work_desc);
                PrimaryFontColorListEditors.Add(work_desc);
            }
        }
        // End Work Experience

        // Right Column: Certifications
        Editor certificationsTitle = new Editor()
        {
            Text = "Certifications & Awards",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = MainColor,
            Margin = new Thickness(0, 20, 0, 0),
            HorizontalTextAlignment = TextAlignment.Start
        };
        vsl_Right.Add(certificationsTitle);
        MainColorListEditors.Add(certificationsTitle);

        foreach (var item in user.Certifications)
        {
            if (item["CertInclude"] == "True")
            {
                HorizontalStackLayout hsl_certs = new HorizontalStackLayout()
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
                    WidthRequest = 250
                };
                MainColorListEditors.Add(cert_name);
                Editor cert_date = new Editor()
                {
                    Text = "Achieved " + item["CertFromMM"] + "/" + item["CertFromYYYY"],
                    TextColor = FontColor,
                    FontSize = 12,
                    Margin = new Thickness(50, 0, 0, 0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End,
                    AutoSize = EditorAutoSizeOption.Disabled,
                    WidthRequest = 80
                };
                PrimaryFontColorListEditors.Add(cert_date);
                hsl_certs.Add(cert_name);
                hsl_certs.Add(cert_date);
                vsl_Right.Add(hsl_certs);
                Editor cert_org = new Editor()
                {
                    Text = item["Organization"],
                    FontSize = 12,
                    TextColor = FontColor,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 300
                };
                vsl_Right.Add(cert_org);
                PrimaryFontColorListEditors.Add(cert_org);
            }
        }
        // End Certifications
        CanvasBoundary.Children.Add(Main);
    }

    public async void Generate_Clicked(object sender, EventArgs e)
    {
        string filename = await DisplayPromptAsync("Saving File", "What would you like to name your Resume file? A .pdf and .png file will be created.");
        if (filename == null)
        {
            await DisplayAlert("Cancelled", "Process Cancelled", "Ok");
            return;
        }

        string path = null;

        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string filepath = Path.Combine(path, filename + ".png");
        string pdffilepath = Path.Combine(path, filename + ".pdf");


        var result = await CanvasBoundary.CaptureAsync();
        var stream = await result.OpenReadAsync();

        using MemoryStream memoryStream = new();
        await stream.CopyToAsync(memoryStream);


        File.WriteAllBytes(filepath, memoryStream.ToArray());

        ImageData imageData = ImageDataFactory.Create(filepath);
        PdfDocument pdfDocument = new PdfDocument(new PdfWriter(pdffilepath));
        Document document = new Document(pdfDocument);

        iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData);
        image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth());
        image.SetAutoScaleHeight(true);

        document.SetMargins(0, 0, 0, 0);
        document.Add(image);
        pdfDocument.Close();
        document.Close();

        await DisplayAlert("Success","File created successfully in your 'Documents' folder.","Ok");


    }

    private async void IconPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        string selectedicon = (string)picker.ItemsSource[selectedIndex];
        selectedicon = selectedicon.ToLower();
        iconimageselected = (FileImageSource) ImageSource.FromFile(selectedicon + ".png");

        if (selectedIndex != -1)
        {
            mainiconimage.Source = iconimageselected;
        }
    }
}

public class Resumes
{
    public string styleName { get; set; }
    public string ImageUrl { get; set; }
}