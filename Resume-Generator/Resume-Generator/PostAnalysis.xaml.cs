using System;
using System.Linq;
using System.Web;
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
			[1] = State
			[2] = Country
			[3] = City
			[4] = Description

		*/

        string[] listing = new string[6];
        var web = new HtmlWeb();
        var doc = web.Load(url);
        var positionTitle = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]/div[2]/a");
        var location = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]/div[2]/div[2]/span");
        var desc = doc.DocumentNode.SelectNodes("//*[@id=\"MainCol\"]/div[1]/ul/li[1]");
        
        listing[2] = "US";
        Uri myUri = new Uri(url);
        String locName = HttpUtility.ParseQueryString(myUri.Query).Get("locName");


        foreach (var position in positionTitle)
        {
            listing[0] = position.InnerText;
        }
        if (locName != null)
        {
            listing[2] = locName;

        }
        else
        {
            foreach (var locations in location)
            {
                listing[1] = locations.InnerText;
            }
            String[] stateCity = listing[1].Split(",");
            listing[1] = stateCity[0];
            listing[3] = stateCity[1];
        }
        foreach (var descript in desc)
        {
            listing[4] += descript.InnerText;
        }

        listing[4] = listing[4].Replace("&hellip;", string.Empty);
        return listing;


    }


    private void ProcessBtn_Clicked(object sender, EventArgs e)
    {
        string placeholderstr = results.Text;
        string[] jobInfo = scrapeJobListing(URL.Text);

        /*	
        [0] = Title
        [1] = State
        [2] = Country
        [3] = City
        [4] = Description
        */


        Position.Text = jobInfo[0];
        Province.Text = jobInfo[1];
        Country.Text = jobInfo[2];
        City.Text = jobInfo[3];
        results.Text = jobInfo[4];

    }
}