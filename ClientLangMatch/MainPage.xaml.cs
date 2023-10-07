using ClientLangMatch.Models;
using ClientLangMatch.Services;
using ClientLangMatch.Views;

namespace ClientLangMatch;

public partial class MainPage : ContentPage
{
	private readonly IUserDataService _userDataService;

	public MainPage(IUserDataService userDataService)
	{
		InitializeComponent();

		_userDataService = userDataService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

        collectionView.ItemsSource = await _userDataService.GetAllUsersAsync();

    }

    async void OnAddUserClicked(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("-----> Add buttom clicked!");

        var navigationParameter = new Dictionary<string, object>
            {
                {   nameof(User), new User()}
            };

        await Shell.Current.GoToAsync(nameof(ManageUserView), navigationParameter);

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

