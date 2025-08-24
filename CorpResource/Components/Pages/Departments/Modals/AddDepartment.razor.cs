using CorpResource.APIClient.Contracts;
using CorpResource.Components.Shared;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Departments.Modals;

public partial class AddDepartment : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [Inject] IAppSnackbar Snackbar { get; set; }
    [Inject] IUsersApiService UsersApiService { get; set; }
    [Inject] IDepartmentsApiService DepartmentApiService { get; set; }

    private MudForm form;
    private User? SelectedUser;
    private IEnumerable<User> Users;
    private readonly Department Department = new(); 
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var allUsers = await UsersApiService.GetUsersByRole(Roles.User);
            
            Users = allUsers.Where(u => u.DepartmentId == null);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching users: {ex.Message}");
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

        if (SelectedUser is not null)
            Department.ManagerId = SelectedUser.Id;

        await DepartmentApiService.SaveDepartment(Department); 
        
        Snackbar.Success("Department created successfully."); 
        MudDialog.Close(DialogResult.Ok(Department)); 
    }

    protected void Cancel() => MudDialog.Cancel();
}
