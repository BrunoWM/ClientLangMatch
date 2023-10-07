using ClientLangMatch.Views;

namespace ClientLangMatch;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(ManageUserView), typeof(ManageUserView));
	}
}
