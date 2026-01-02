namespace Bingo.Services;

public interface IWindowService
{
    /// <summary>
    /// Closes the window of the specified type.
    /// </summary>
    /// <typeparam name="TWindow">The type of the window to close.</typeparam>
    void CloseWindow<TWindow>();
}
