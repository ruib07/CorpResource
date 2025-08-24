using CorpResource.APIClient.Contracts;
using CorpResource.Domain.Enums;
using CorpResource.Domain.Models;
using System.Net.Http.Json;

namespace CorpResource.APIClient;

public class UsersApiService : IUsersApiService
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "users";

    public UsersApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CorpResourceApi");
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>(_baseUrl);
    }

    public async Task<User> GetUser(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<User>($"{_baseUrl}/by-id/{id}");
    }

    public async Task<IEnumerable<User>> GetUsersByRole(Roles role)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"{_baseUrl}/by-role/{role}");
    }

    public async Task<User> SaveUser(User user)
    {
        HttpResponseMessage response;

        if (user.Id == Guid.Empty)
            response = await _httpClient.PostAsJsonAsync(_baseUrl, user);
        else
            response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{user.Id}", user);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task DeleteUser(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
