using CorpResource.APIClient.Contracts;
using CorpResource.Components.Shared;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Users.Modals;

public partial class AddUser : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [Inject] IAppSnackbar Snackbar { get; set; }
    [Inject] IUsersApiService UsersApiService { get; set; }
    [Inject] IDepartmentsApiService DepartmentsApiService { get; set; }

    private MudForm form;
    private Department? SelectedDepartment;
    private IEnumerable<Department> Departments;
    private readonly User User = new()
    {
        Role = Roles.User
    };

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var allDepartments = await DepartmentsApiService.GetDepartments();

            Departments = allDepartments.Where(d => d.ManagerId == null);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching departments: {ex.Message}");
        }
    }

    protected async Task Save()
    {
        await form.Validate();

        if (!form.IsValid)
        {
            Snackbar.Error("Something went wrong.");
            return;
        }

        if (SelectedDepartment is not null)
            User.DepartmentId = SelectedDepartment.Id;
        else
            User.DepartmentId = null;

            await UsersApiService.SaveUser(User);

        Snackbar.Success("User created successfully.");
        MudDialog.Close(DialogResult.Ok(User));
    }

    protected void Cancel() => MudDialog.Cancel();
}
