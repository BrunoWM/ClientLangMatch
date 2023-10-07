using ClientLangMatch.Models;
using ClientLangMatch.Services;

namespace ClientLangMatch.Views;

[QueryProperty(nameof(User), "User")]
public partial class ManageUserView : ContentPage
{
    private readonly IUserDataService _dataService;
    User _user;
    public User User
    {
        get => _user;
        set
        {
            _isNew = IsNew(value);
            _user = value;

            OnPropertyChanged();
        }
    }

    bool _isNew;
    bool IsNew(User user)
    {
        if (user.Id == 0) return true;
        return false;
    }
    public ManageUserView(IUserDataService dataService)
	{
		InitializeComponent();
        _dataService = dataService;
        BindingContext = this;
    }

    async void OnSaveButtomClicked(object sender, EventArgs e)
    {
        if (_isNew)
        {
            await _dataService.AddUserAsync(User);
            System.Diagnostics.Debug.WriteLine("-----> New user added");
        }
        else
        {
            await _dataService.UpdateUserAsync(User);
            System.Diagnostics.Debug.WriteLine("-----> New user updated");
        }

        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtomClicked(object sender, EventArgs e)
    {
        await _dataService.DeleteUserAsync(User.Id);
        await Shell.Current.GoToAsync("..");
    }
    async void OnCancelButtomClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}