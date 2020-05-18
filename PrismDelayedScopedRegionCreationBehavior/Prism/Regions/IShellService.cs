using System.Windows;

namespace Prism.Regions
{
    public interface IShellService
    {
        void ShowShell<T>() where T : Window;
    }
}