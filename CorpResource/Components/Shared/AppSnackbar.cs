using MudBlazor;

namespace CorpResource.Components.Shared;

public interface IAppSnackbar
{
    void Success(string message);
    void Error(string message);
}

public class AppSnackbar : IAppSnackbar
{
    private readonly ISnackbar _snackbar;

    public AppSnackbar(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public void Success(string message)
    {
        _snackbar.Add(message, Severity.Success);
    }

    public void Error(string message)
    {
        _snackbar.Add(message, Severity.Error);
    }
}
