using GitHubClient.Core.Models;
using GitHubClient.Ui.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace GitHubClient.Ui.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestTokenService _requestTokenService;

        public GitHubService(HttpClient httpClient, IRequestTokenService requestTokenService)
        {
            _httpClient = httpClient;
            _requestTokenService = requestTokenService;
        }

        public async Task<ResponseModel<List<CommitDetailModel>>> GetCommitByShaAsync(string userNameAndRepoName, string sha, string token)
        {
            var ResponseModel = new ResponseModel<List<CommitDetailModel>>();
            try
            {
                _requestTokenService.SetUpAuthProperties(token);
                string endPoint = "api/GitHub/GetCommitByShaAsync";
                var response = await _httpClient.GetAsync(endPoint + $"?userNameAndRepoName={userNameAndRepoName}&sha={sha}");
                string result = await response.Content.ReadAsStringAsync();
                ResponseModel = JsonConvert.DeserializeObject<ResponseModel<List<CommitDetailModel>>>(result);
                return ResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel.Message = ex.Message;
                ResponseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                return ResponseModel;
            }
        }

        public async Task<ResponseModel<List<CommitModel>>> GetCommitsAsync(string userNameAndRepoName, string token)
        {
            var ResponseModel = new ResponseModel<List<CommitModel>>();
            try
            {
                _requestTokenService.SetUpAuthProperties(token);
                string endPoint = "api/GitHub/GetCommitsAsync";
                var response = await _httpClient.GetAsync(endPoint+$"?userNameAndRepoName={userNameAndRepoName}");
                string result = await response.Content.ReadAsStringAsync();
                ResponseModel = JsonConvert.DeserializeObject<ResponseModel<List<CommitModel>>>(result);
                return ResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel.Message = ex.Message;
                ResponseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                return ResponseModel;
            }
        }
    }
}
