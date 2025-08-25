using CorpResource.APIClient.Contracts;
using CorpResource.Domain.Models;
using System.Net.Http.Json;

namespace CorpResource.APIClient;

public class ResourceApiService : IResourcesApiService
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "resources";

    public ResourceApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CorpResourceApi");
    }

    public async Task<IEnumerable<Resource>> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Resource>>(_baseUrl);
    }

    public async Task<Resource> GetResource(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Resource>($"{_baseUrl}/{id}");
    }

    public async Task<Resource> SaveResource(Resource resource)
    {
        HttpResponseMessage response;

        if (resource.Id == Guid.Empty)
            response = await _httpClient.PostAsJsonAsync(_baseUrl, resource);
        else
            response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{resource.Id}", resource);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Resource>();
    }

    public async Task DeleteResource(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
