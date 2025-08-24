using CorpResource.APIClient.Contracts;
using CorpResource.Components.Pages.Users.Modals;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Users;

public partial class Users : ComponentBase
{
    [Inject] IDialogService DialogService { get; set; }
    [Inject] IUsersApiService UsersApiService { get; set; }

    private IEnumerable<User> _users;

    private readonly string[] headings = 
    [
        "User Name", "Full Name", "Email", 
        "Role", "Created At", "Department ID", ""
    ];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var allUsers = await UsersApiService.GetUsers();

            _users = allUsers.Where(u => u.Role == Roles.User);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching users: {ex.Message}");
        }
    }

    protected async Task CreateUser()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<AddUser>("Create User", options);
        var result = await dialog.Result;

        if (!result.Canceled && result.Data is User newUser)
        {
            var list = _users?.ToList() ?? [];
            list.Add(newUser);
            _users = list;
            StateHasChanged();
        }
    }
}
