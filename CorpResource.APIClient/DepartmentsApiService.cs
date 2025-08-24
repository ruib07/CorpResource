using CorpResource.APIClient.Contracts;
using CorpResource.Domain.Models;
using System.Net.Http.Json;

namespace CorpResource.APIClient;

public class DepartmentsApiService : IDepartmentsApiService
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "departments";

    public DepartmentsApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CorpResourceApi");
    }

    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Department>>(_baseUrl);
    }

    public async Task<Department> GetDepartment(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Department>($"{_baseUrl}/{id}");
    }

    public async Task<Department> SaveDepartment(Department department)
    {
        HttpResponseMessage response;

        if (department.Id == Guid.Empty)
            response = await _httpClient.PostAsJsonAsync(_baseUrl, department);
        else
            response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{department.Id}", department);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Department>();
    }

    public async Task DeleteDepartment(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
