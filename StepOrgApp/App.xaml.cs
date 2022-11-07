using StepOrgApp.Pages.Authentication;

namespace StepOrgApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		//SecureStorage.RemoveAll();
		MainPage = new MainPage();
	}
}
