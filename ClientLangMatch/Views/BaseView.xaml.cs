using ClientLangMatch.Models;
using ClientLangMatch.Services;
using Microsoft.Maui.Controls;

namespace ClientLangMatch.Views;

public partial class BaseView : TabbedPage
{
    private readonly IUserDataService _userDataService;
    public BaseView()
	{
		InitializeComponent();

        _userDataService = new UserDataService();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        collectionView.ItemsSource = await _userDataService.GetAllUsersAsync();

    }

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("-----> Item buttom clicked!");

        var navigationParmeter = new Dictionary<string, object>
        {
            {   nameof(User), e.CurrentSelection.FirstOrDefault() as User }
        };

        await Shell.Current.GoToAsync(nameof(ManageUserView), navigationParmeter);
    }
}