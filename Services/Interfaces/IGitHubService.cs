using GitHubClient.Core.Models;

namespace GitHubClient.Ui.Services.Interfaces
{
    public interface IGitHubService
    {
        Task<ResponseModel<List<CommitModel>>> GetCommitsAsync(string userNameAndRepoName, string token);
        Task<ResponseModel<List<CommitDetailModel>>> GetCommitByShaAsync(string userNameAndRepoName, string sha, string token);
    }
}
