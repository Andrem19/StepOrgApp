using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using StepOrgApp.Services;
using StepOrgApp.Services.ApiRequests;
using Tewr.Blazor.FileReader;

namespace StepOrgApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
        builder.Services.AddScoped<HttpClient>();
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(SD.BaseAPIUrl) });
        builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddAuthorizationCore();
        //builder.Services.AddScoped<AuthStateProvider>();
        builder.Services.AddScoped<AuthenticationService>();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        builder.Services.AddBlazorWebView();
        builder.Services.AddScoped<GroupServiceRequest>();
        builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

        return builder.Build();
	}
}
