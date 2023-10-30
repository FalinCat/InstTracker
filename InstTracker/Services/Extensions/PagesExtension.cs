using Microsoft.AspNetCore.Components;

namespace InstTracker.Services.Extensions
{
    public static class PagesExtension
    {
        public static void ReloadPage(this NavigationManager manager)
        {
            manager.NavigateTo(manager.Uri, true);
        }
    }
}