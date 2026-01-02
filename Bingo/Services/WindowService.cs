using System.Windows;

namespace Bingo.Services;

public class WindowService : IWindowService
{
    public void CloseWindow<TWindow>()
    {
        Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(w => w.GetType() == typeof(TWindow))?.Close();
    }
}
