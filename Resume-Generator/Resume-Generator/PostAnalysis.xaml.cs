using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Maui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Resume_Generator;

public partial class PostAnalysis : ContentPage
{
    ResumeManager db;
	int currentIndx;
    string mainNode = "//*[@id=\"MainCol\"]/div[1]/ul/li[";
	string weburlStart = "https://www.glassdoor.com/Job/";
	string weburlEnd = "-jobs-SRCH_KO0,17.htm?context=Jobs&clickSource=searchBox";
    string currentPosition = "Jobs";
    public PostAnalysis(ResumeManager db)
	{
		this.db = db;
		InitializeComponent();
		results.Text = "display results here";

    }

	private void ProfileBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage(db));
	}

	private void CanvasBtn_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Canvas(db));
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
    

    public string[] scrapeJobListing()
	{
        string url = createURL(URL.Text);
		/*	
			[0] = Title
			[1] = State
			[2] = Country
			[3] = City
			[4] = Description

		*/

		string[] listing = new string[5];
		var web = new HtmlWeb();
		var doc = web.Load(url);
        var positionTitle = doc.DocumentNode.SelectNodes(mainNode + currentIndx.ToString()+"]/div[2]/a");
		var location = doc.DocumentNode.SelectNodes(mainNode + currentIndx.ToString() +"]/ div[2]/div[2]/span");
		var desc = doc.DocumentNode.SelectNodes(mainNode + currentIndx.ToString() +"]");

		listing[2] = "US";
        Uri myUri = new Uri(url);
        String locName = HttpUtility.ParseQueryString(myUri.Query).Get("locName");


		foreach(var position in positionTitle)
		{
			listing[0] = position.InnerText;
		}
		if ( locName != null)
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
        
		return listing;


    }


    private void ProcessBtn_Clicked(object sender, EventArgs e)
    {
        if (URL.Text != "" | URL.Text != null) {
            string placeholderstr = results.Text;
            this.currentIndx = 1;
            string[] jobInfo = scrapeJobListing();

            /*	
            [0] = Title
            [1] = State
            [2] = Country
            [3] = City
            [4] = Description
            */

            setText(jobInfo);
        }

    }

	private void setText(string[] jobInfo) {
        Position.Text = jobInfo[0];
        this.currentPosition = jobInfo[0];
        Province.Text = jobInfo[1];
        Country.Text = jobInfo[2];
        City.Text = jobInfo[3];
        jobInfo[4] = jobInfo[4].Replace(jobInfo[0], "" + System.Environment.NewLine);
        jobInfo[4] = jobInfo[4].Replace(jobInfo[1], "");
        jobInfo[4] = jobInfo[4].Replace(jobInfo[2], "");
        jobInfo[4] = jobInfo[4].Replace(jobInfo[3], "");
        jobInfo[4] = jobInfo[4].Replace("Easy Apply", "" + System.Environment.NewLine);

        if (jobInfo[4].Substring(0, 3).Contains("."))
        {
            jobInfo[4] = jobInfo[4].Replace(jobInfo[4].Substring(0, 3), "Rating: " + jobInfo[4].Substring(0, 3) + Environment.NewLine);
        }
        else if (jobInfo[4].Substring(0).All(char.IsDigit))
        {
            jobInfo[4] = jobInfo[4].Replace(jobInfo[4].Substring(0), "Rating: " + jobInfo[4].Substring(0) + Environment.NewLine);
        }

        int index = jobInfo[4].IndexOf(")") + 1;
        string leftover = jobInfo[4].Substring(index);
        int indexplus = leftover.IndexOf("+");
        int indexd = leftover.IndexOf("d");
        string daysresult;
        if (indexplus == indexd + 1)
        {
            daysresult = leftover.Substring(0, indexplus + 1);
        }
        else
        {
            daysresult = leftover.Substring(0, indexd + 1);
        }
        jobInfo[4] = jobInfo[4].Replace(daysresult, Environment.NewLine+"Days on Glassdoor: "+ daysresult + Environment.NewLine);
        jobInfo[4] = jobInfo[4].Substring(0, jobInfo[4].Length - 8);

        results.Text = jobInfo[4];
        
    }


	private void NextBtn_Clicked(object sender, EventArgs e)
	{
        string placeholderstr = results.Text;
        this.currentIndx = this.currentIndx + 1;
        if (URL.Text != "" | URL.Text != null)
        {
            string[] jobInfo = scrapeJobListing();
            setText(jobInfo);
        }

        /*	
        [0] = Title
        [1] = State
        [2] = Country
        [3] = City
        [4] = Description
        */


    }

    private void PrevBtn_Clicked(object sender, EventArgs e)
    {
        string placeholderstr = results.Text;
        if (this.currentIndx > 0)
        {
            this.currentIndx = this.currentIndx - 1;
            if (URL.Text != "" | URL.Text != null)
            {
                string[] jobInfo = scrapeJobListing();
                setText(jobInfo);
            }

            /*	
            [0] = Title
            [1] = State
            [2] = Country
            [3] = City
            [4] = Description
            */

        }
        else {
            DisplayAlert("STATUS", "No Previous Entries Available", "OK");
        }
    }
    private string createURL(string jobkeywords) {
        jobkeywords = jobkeywords.Trim();
        jobkeywords = jobkeywords.Replace(" ", "-");
        string url = weburlStart + jobkeywords + weburlEnd;
        return url;
    }
    private void UrlBtn_Clicked(object sender, EventArgs e) {
        if (URL.Text != "" | URL.Text != null)
        {
            string url = createURL(URL.Text);
            try
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch
            {
                
            }
        }   
    }
    private void Url2Btn_Clicked(object sender, EventArgs e)
    {
        if (this.currentPosition != "Jobs")
        {
            string url = createURL(this.currentPosition);
            try
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch
            {

            }
        }
    }
}