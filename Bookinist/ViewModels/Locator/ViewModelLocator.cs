using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.ViewModels.Locator
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindow => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
