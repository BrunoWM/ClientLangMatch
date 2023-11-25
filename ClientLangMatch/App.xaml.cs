using ClientLangMatch.Views;

namespace ClientLangMatch;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new BaseView();
	}
}
