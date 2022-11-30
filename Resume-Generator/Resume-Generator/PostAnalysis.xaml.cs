using System;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.Maui.Controls;

namespace Resume_Generator;

public partial class PostAnalysis : ContentPage
{
    public PostAnalysis()
	{
		InitializeComponent();
		results.Text = "display results here";

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

    public static bool IsValidUrl(string webSiteUrl)
    {
        if (webSiteUrl.StartsWith("www."))
        {
            webSiteUrl = "http://" + webSiteUrl;
        }

        return Uri.TryCreate(webSiteUrl, UriKind.Absolute, out Uri uriResult)
                 && (uriResult.Scheme == Uri.UriSchemeHttp
                  || uriResult.Scheme == Uri.UriSchemeHttps) && uriResult.Host.Replace("www.", "").Split('.').Count() > 1 && uriResult.HostNameType == UriHostNameType.Dns && uriResult.Host.Length > uriResult.Host.LastIndexOf(".") + 1 && 100 >= webSiteUrl.Length;
    }
    

    public string[] scrapeJobListing(string url)
	{
		/*	
			[0] = Title
			[1] = Country
			[2] = State
			[3] = City
			[4] = Description

		*/
		string[] listing = new string[5];
		var web = new HtmlWeb();
		var doc = web.Load(url);
		var positionTitle = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]/div[2]/a");
		var location = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]/div[2]/div[2]/span");
		var desc = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]");
		


		foreach(var position in positionTitle)
		{
			listing[0] = position.InnerText;
		}

        foreach (var locations in location)
        {
            listing[1] = locations.InnerText;
        }
        
		foreach (var descript in desc)
        {
            listing[4] = descript.InnerText;
        }
        
		return listing;


    }


    private void ProcessBtn_Clicked(object sender, EventArgs e)
    {
		string placeholderstr = results.Text;
		string tempstr = URL.Text + " " + Position.Text + " " + Country.Text + " " + Province.Text + " " + City.Text;
        string[] jobInfo = scrapeJobListing(URL.Text);

        results.Text = jobInfo[4];
		Country.Text = jobInfo[1];
		Position.Text = jobInfo[0];

    }
}