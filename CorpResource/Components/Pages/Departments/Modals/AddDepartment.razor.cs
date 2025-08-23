using CorpResource.APIClient.Contracts;
using CorpResource.Components.Shared;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Departments.Modals;

public partial class AddDepartment : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [Inject] IAppSnackbar Snackbar { get; set; }
    [Inject] IDepartmentsApiService DepartmentApiService { get; set; }

    private MudForm form; 
    private readonly Department Department = new(); 
    
    protected async Task Save() 
    { 
        await form.Validate(); 
        
        if (!form.IsValid) 
        { 
            Snackbar.Error("Something went wrong.");
            return; 
        }
        
        await DepartmentApiService.SaveDepartment(Department); 
        
        Snackbar.Success("Department created successfully."); 
        MudDialog.Close(DialogResult.Ok(Department)); 
    }

    protected void Cancel() => MudDialog.Cancel();
}
