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
    Dictionary<string, HorizontalStackLayout> rows = new Dictionary<string, HorizontalStackLayout>();
    Dictionary<string, bool> selected = new Dictionary<string, bool>();
    Dictionary<string, CheckBox> checkbox = new Dictionary<string, CheckBox>();
    Dictionary<string, Button> buttons = new Dictionary<string, Button>();

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
        Button btn = new Button();
        btn.Clicked += new EventHandler(ConfirmBtn_Clicked);
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
        btn.Style = App.Current.Resources["LoadBtn"] as Style;


        row.Children.Add(chckbx);
        row.Children.Add(lbl1);
        row.Children.Add(lbl2);
        row.Children.Add(lbl3);
        row.Children.Add(btn);
        UserList.Children.Add(row);
        addToDictionaries(user, chckbx, row, btn);

        return row;
    }

    private void addToDictionaries(BasicUser user, CheckBox chck, HorizontalStackLayout row, Button btn) {
        selected[user.FileName] = false;
        checkbox[user.FileName] = chck;
        rows[user.FileName] = row;
        buttons[user.FileName] = btn;
    }

    private async void ConfirmBtn_Clicked(object sender, EventArgs e)
    {
        string action = "Retry";
        string filepath = "";
        string result;
        foreach(var btn in buttons)
        {
            if (btn.Value == sender) {
                filepath = btn.Key;
            }
        }
        do
        {
            result = await DisplayPromptAsync("Decryption", "Password:");
            if (result == "password")
            {
                await DisplayAlert("Success!", "file successfully decrypted", "OK");
                Navigation.PushAsync(new MainPage(db.createNewResume(filepath)));
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
        string text = System.Environment.NewLine;
        List<string> filePaths = new List<string>();
        foreach (var val in selected)
        {
            if (val.Value) {
                BasicUser currUser = new BasicUser(val.Key, db.getDBDirectory());
                filePaths.Add(currUser.FileName);
                text = text + currUser.FirstName + " " + currUser.MiddleName + " "+ currUser.LastName+ " " + System.Environment.NewLine;
            }
        }
        string Header = "WARNING: Are you sure? ";
        string paragraph = "The following users are set to be deleted."+System.Environment.NewLine+" This deletion is permenant and cannot be undone.";
        bool action = await DisplayAlert(Header, paragraph + text, "Yes", "No");
        if (action)
        {
            foreach (var file in filePaths)
            {
                db.deleteUser(file);
                UserList.Children.Remove(rows[file]);
                rows.Remove(file);
                checkbox.Remove(file);
                selected.Remove(file);
            }
            await DisplayAlert("STATUS", "deletion successful", "OK");

        }

    }

    private void CheckBox_Changed(object sender, EventArgs e)
    {
        foreach (var val in selected) {
            if (checkbox[val.Key] == sender) { 
                if(val.Value == false)
                {
                    selected[val.Key] = true;
                }
                else
                {
                    selected[val.Key] = false;
                }
            }
        }

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