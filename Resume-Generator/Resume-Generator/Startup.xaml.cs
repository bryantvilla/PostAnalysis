using Newtonsoft.Json;
using SkiaSharp;
using System.Runtime.Versioning;
using System.Windows;

namespace Resume_Generator;

public partial class Startup : ContentPage
{
    string firstName = "";
    string middleName = "";
    string lastName = "";
    DBManager db;
    List<BasicUser> Users;
    public Startup()
    {
        InitializeComponent();
        db = new DBManager();
        Users = db.getAllUsers();
        foreach (var user in Users.ToArray()) {
            createRow(user);
        }
            
        
    }
    private HorizontalStackLayout createRow(BasicUser user) {
        HorizontalStackLayout row = new HorizontalStackLayout();
        
        Label lbl1 = new Label();
        Label lbl2 = new Label();
        Label lbl3 = new Label();
        CheckBox chckbx = new CheckBox();
        chckbx.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(CheckBox_Changed);
        
        lbl1.Text = user.FirstName;
        lbl1.Margin += 5;
        lbl1.Margin += 5;
        lbl2.Text = user.MiddleName;
        lbl2.Margin += 5;
        lbl2.Margin += 5;
        lbl3.Text = user.LastName;
        lbl3.Margin += 5;
        lbl3.Margin += 5;

        row.Children.Add(chckbx);
        row.Children.Add(lbl1);
        row.Children.Add(lbl2);
        row.Children.Add(lbl3);
        UserList.Children.Add(row);
        return row;
    }

    private async void ConfirmBtn_Clicked(object sender, EventArgs e)
    {
        string action = "Retry";
        string result;
        do
        {
            result = await DisplayPromptAsync("Decryption", "Password:");
            if (result == "password")
            {
                await DisplayAlert("Success!", "file successfully decrypted", "OK");
                Navigation.PushAsync(new MainPage());
                action = "Success";
            }
            else if (result == null)
            {
                action = "canceled";
            }
            else
            {
                action = await DisplayActionSheet("Incorrect password", "Retry", "Give Up");
            }

        } while (action == "Retry");

    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {


    }

    private void CheckBox_Changed(object sender, EventArgs e)
    {


    }

    private async void NewBtn_Clicked(object sender, EventArgs e)
    {
        await requestFirstName(sender, e);
        BasicUser newUser = db.createNewUser(firstName, middleName, lastName);
        Users.Add(newUser);
        createRow(newUser);

    }

    private async Task requestFirstName(object sender, EventArgs e) {
        string action = "Retry";
        do
        {
            firstName = await DisplayPromptAsync("User Creation", "First Name:");
            if (firstName == "")
            {
                await DisplayAlert("User Creation", "First Name Required", "OK");
                action = "Retry";
            }
            else if (firstName == null)
            {
                action = "canceled";
            }
            else
            {
                await requestMiddleName(sender, e);
                action = "completed";
            }

        } while (action == "Retry");
    }

    private async Task requestMiddleName(object sender, EventArgs e)
    {
        string action = "Resubmit";
        do
        {
            middleName = await DisplayPromptAsync("User Creation", "Middle Name:");
            if (middleName == "")
            {
                action = await DisplayActionSheet("No Middle Name Entered", "Resubmit", "Continue");
                if (action == "Continue") {
                    await requestLastName(sender, e);
                    action = "completed";
                }
            }
            else if (middleName == null)
            {
                action = "canceled";
            }
            else
            {
                await requestLastName(sender, e);
                action = "completed";
            }

        } while (action == "Resubmit");
    }

    private async Task requestLastName(object sender, EventArgs e)
    {
        string action = "Retry";
        do
        {
            lastName = await DisplayPromptAsync("User Creation", "Last Name:");
            if (lastName == "")
            {
                await DisplayAlert("User Creation", "Last Name Required", "OK");
                action = "Retry";
            }
            else if (lastName == null)
            {
                action = "canceled";
            }
            else
            {
                action = "completed";
            }

        } while (action == "Retry");
    }

}