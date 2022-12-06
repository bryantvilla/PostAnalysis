using Microsoft.Maui.Controls.Shapes;
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
    string password = "";
    DBManager db;
    List<BasicUser> Users;
    Dictionary<string, Grid> rows = new Dictionary<string, Grid>();
    Dictionary<string, bool> selected = new Dictionary<string, bool>();
    Dictionary<string, CheckBox> checkbox = new Dictionary<string, CheckBox>();
    Dictionary<string, ImageButton> buttons = new Dictionary<string, ImageButton>();

    public Startup()
    {
        InitializeComponent();
        db = new DBManager();
        Users = db.getAllUsers();
        foreach (var user in Users.ToArray()) {
            createRow(user);
        }    
    }
    private void createRow(BasicUser user) {

        Grid gridRow = new Grid();
        gridRow.AddColumnDefinition(new ColumnDefinition(25));
        gridRow.AddColumnDefinition(new ColumnDefinition(50));
        gridRow.AddColumnDefinition(new ColumnDefinition(400));

        Label lbl1 = new Label();
        Label lbl2 = new Label();
        Label lbl3 = new Label();
        CheckBox chckbx = new CheckBox();
        ImageButton btn = new ImageButton();
        btn.Clicked += new EventHandler(ConfirmBtn_Clicked);
        chckbx.CheckedChanged += new EventHandler<CheckedChangedEventArgs>(CheckBox_Changed);

        gridRow.Children.Add(chckbx);
        HorizontalStackLayout fullname = new HorizontalStackLayout();
        fullname.Children.Add(lbl1);
        fullname.Children.Add(lbl2);
        fullname.Children.Add(lbl3);
        gridRow.Children.Add(fullname);
        gridRow.Children.Add(btn);
        chckbx.SetValue(Grid.ColumnProperty, 1);
        fullname.SetValue(Grid.ColumnProperty, 2);
        btn.SetValue(Grid.ColumnProperty, 0);

        btn.Source = "C:\\Users\\maspo\\source\\repos\\Resume-Generator\\Resume-Generator\\Resume-Generator\\Resources\\AppIcon\\loadIcon1.png";

        lbl1.Text = user.FirstName;
        lbl1.Margin += 2;
        lbl1.Margin += 2;
        lbl2.Text = user.MiddleName;
        lbl2.Margin += 2;
        lbl2.Margin += 2;
        lbl3.Text = user.LastName;
        lbl3.Margin += 2;
        lbl3.Margin += 2;
        btn.Style = App.Current.Resources["StartUpLoadBtn"] as Style;

        UserList.Children.Add(gridRow);
        addToDictionaries(user, chckbx, gridRow, btn);

    }

    private void addToDictionaries(BasicUser user, CheckBox chck, Grid row, ImageButton btn) {
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
        bool action = false;
        if (filePaths.Count != 0) {
            string Header = "WARNING: Are you sure? ";
            string paragraph = "The following users are set to be deleted." + System.Environment.NewLine + " This deletion is permenant and cannot be undone.";
            action = await DisplayAlert(Header, paragraph + text, "Yes", "No");
        }
        else
        {
            await DisplayAlert("STATUS", "No Users Selected","OK");
        }

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
                await requestPassword(sender, e);
                action = "completed";
            }

        } while (action == "Retry");
    }

    private async Task requestPassword(object sender, EventArgs e)
    {
        string action = "Retry";
        do
        {
            password = await DisplayPromptAsync("User Creation", "Password:");
            if (password == "")
            {
                await DisplayAlert("User Creation", "Password Required", "OK");
                action = "Retry";
            }
            else if (lastName == null)
            {
                action = "canceled";
            }
            else
            {
                BasicUser newUser = db.createNewUser(firstName, middleName, lastName);
                Users.Add(newUser);
                createRow(newUser);
                action = "completed";
            }

        } while (action == "Retry");
    }

}