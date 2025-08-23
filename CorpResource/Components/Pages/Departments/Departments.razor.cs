using CorpResource.APIClient.Contracts;
using CorpResource.Components.Pages.Departments.Modals;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Departments;

public partial class Departments : ComponentBase
{
    [Inject] IDialogService DialogService { get; set; }
    [Inject] IDepartmentsApiService DepartmentsApiService { get; set; }

    private IEnumerable<Department> _departments;

    private readonly string[] headings = ["Name", "Manager ID", "Created At", ""];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _departments = await DepartmentsApiService.GetDepartments();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching departments: {ex.Message}");
        }
    }

    protected async Task CreateDepartment()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<AddDepartment>("Create Department", options);
        var result = await dialog.Result;

        if (!result.Canceled && result.Data is Department newDepartment)
        {
            var list = _departments?.ToList() ?? []; 
            list.Add(newDepartment); 
            _departments = list; 
            StateHasChanged();
        }
    }

    //protected async Task EditDepartment(Guid Id)
    //{
    //    var departmentToEdit = _departments.FirstOrDefault(x => x.Id == Id);

    //    if (departmentToEdit == null) return;

    //    var parameters = new DialogParameters()
    //    {
    //        ["Department"] = new Department()
    //        {
    //            Id = departmentToEdit.Id,
    //            Name = departmentToEdit.Name,
    //            ManagerId = departmentToEdit.ManagerId
    //        }
    //    };

    //    var options = new DialogOptions()
    //    {
    //        CloseOnEscapeKey = true,
    //        MaxWidth = MaxWidth.Medium,
    //        FullWidth = true
    //    };

    //    var dialog = await DialogService.ShowAsync<EditDepartment>("Edit Department", parameters, options);
    //    var result = await dialog.Result;

    //    if (result.Canceled) return;

    //    var updatedItem = (Department)result.Data;

    //    await DepartmentsApiService.SaveDepartment(updatedItem);

    //    var index = _departments.ToList().FindIndex(x => x.Id == updatedItem.Id);

    //    if (index >= 0)
    //    {
    //        var list = _departments.ToList();
    //        list[index] = updatedItem;
    //        _departments = list;
    //    }

    //    StateHasChanged();
    //}

    //protected async Task DeleteDepartment(Guid Id) 
    //{
    //    var configurationToDelete = _departments.FirstOrDefault(x => x.Id == Id); 
        
    //    if (configurationToDelete == null) return; 
        
    //    var parameters = new DialogParameters() 
    //    { 
    //        ["Department"] = configurationToDelete 
    //    }; 
        
    //    var options = new DialogOptions() 
    //    { 
    //        CloseOnEscapeKey = true, 
    //        MaxWidth = MaxWidth.Small 
    //    }; 
        
    //    var dialog = await DialogService.ShowAsync<DeleteDepartment>("Confirm Delete", parameters, options); 
    //    var result = await dialog.Result; 
        
    //    if (result.Canceled || result.Data is not true) return; 
        
    //    await DepartmentsApiService.DeleteDepartment(Id); 
        
    //    _departments = _departments.Where(x => x.Id != Id).ToList(); 
    //    StateHasChanged(); 
    //}
}
