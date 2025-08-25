using CorpResource.APIClient.Contracts;
using CorpResource.Components.Pages.Resources.Modals;
using CorpResource.Components.Shared;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace CorpResource.Components.Pages.Resources;

public partial class Resources : ComponentBase
{
    [Inject] IDialogService DialogService { get; set; }
    [Inject] IDepartmentsApiService DepartmentsApiService { get; set; }
    [Inject] IResourcesApiService ResourcesApiService { get; set; }

    private IEnumerable<Department> _departments;
    private IEnumerable<Resource> _resources;
    private string _searchString = "";
    private IEnumerable<Resource> filteredResources;

    private readonly string[] headings =
    [
        "Name", "Type", "Department", "Status",
        "Description", "Created At", ""
    ];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _resources = await ResourcesApiService.GetResources();
            _departments = await DepartmentsApiService.GetDepartments();

            filteredResources = _resources;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching resources: {ex.Message}");
        }
    }

    protected async Task CreateResource()
    {
        var options = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<AddResource>("Create Resource", options);
        var result = await dialog.Result;

        if (!result.Canceled && result.Data is Resource newResource)
        {
            var list = _resources?.ToList() ?? [];
            list.Add(newResource);
            _resources = list;
            StateHasChanged();
        }
    }

    #region Private Methods

    private string GetDepartmentName(Guid? departmentId)
    {
        if (departmentId == null || departmentId == Guid.Empty)
            return string.Empty;

        return _departments.FirstOrDefault(p => p.Id == departmentId)?.Name ?? departmentId.ToString();
    }

    private void OnSearchChanged(KeyboardEventArgs args)
    {
        filteredResources = TableFilter.ApplySearch(
            _resources,
            _searchString,
            x => x.Name,
            x => x.Type.ToString(),
            x => GetDepartmentName(x.DepartmentId),
            x => x.Status.ToString(),
            x => x.Description).ToList();

        StateHasChanged();
    }

    #endregion
}
