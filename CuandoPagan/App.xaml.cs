using Microsoft.Extensions.DependencyInjection;
using CuandoPagan.Views;

namespace CuandoPagan;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}