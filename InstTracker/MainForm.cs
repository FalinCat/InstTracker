using InstTracker.Data;
using InstTracker.Services;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

namespace InstTracker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var builder = Host.CreateApplicationBuilder();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=InstancesHistory.db");
            });
            builder.Services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(10);
            });

            builder.Services.AddWindowsFormsBlazorWebView();
            builder.Services.AddMudBlazorDialog();
            builder.Services.AddMudServices();

            builder.Services.AddApplicationServices();

            var app = builder.Build();
            app.Services.AddDbInitializer();

            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = builder.Services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<App>("#app");
            app.Start();
        }
    }
}