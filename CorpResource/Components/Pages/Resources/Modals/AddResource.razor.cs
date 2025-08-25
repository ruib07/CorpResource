using CorpResource.APIClient.Contracts;
using CorpResource.Components.Shared;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CorpResource.Components.Pages.Resources.Modals;

public partial class AddResource : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [Inject] IAppSnackbar Snackbar { get; set; }
    [Inject] IResourcesApiService ResourcesApiService { get; set; }
    [Inject] IDepartmentsApiService DepartmentsApiService { get; set; }

    private MudForm form;
    private readonly IEnumerable<ResourceType> resourceTypes = Enum.GetValues<ResourceType>();
    private readonly IEnumerable<ResourceStatus> resourceStatuses = Enum.GetValues<ResourceStatus>();
    private IEnumerable<Department> Departments = Enumerable.Empty<Department>();
    private readonly Resource Resource = new();
    private string SelectedDepartmentId
    {
        get => Resource.DepartmentId == Guid.Empty ? string.Empty : Resource.DepartmentId.ToString();
        set
        {
            if (Guid.TryParse(value, out var guid))
                Resource.DepartmentId = guid;
            else
                Resource.DepartmentId = Guid.Empty;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Departments = await DepartmentsApiService.GetDepartments();
    }

    protected async Task Save()
    {
        await form.Validate();

        if (!form.IsValid)
        {
            Snackbar.Error("Something went wrong.");
            return;
        }

        await ResourcesApiService.SaveResource(Resource);

        Snackbar.Success("Resource created successfully.");
        MudDialog.Close(DialogResult.Ok(Resource));
    }

    protected void Cancel() => MudDialog.Cancel();
}
