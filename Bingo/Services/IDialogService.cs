namespace Bingo.Services;

public interface IDialogService
{
    /// <summary>
    /// Shows a confirmation dialog with Yes/No buttons.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <returns>True if the user clicked Yes, false otherwise.</returns>
    bool ShowConfirmation(string message, string title);

    /// <summary>
    /// Shows an information dialog with an OK button.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="title">The title of the dialog.</param>
    void ShowInformation(string message, string title);
}
