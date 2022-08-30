namespace GitHubClient.Core.Models
{
    public class CommitModel
    {
        public string CommitSha { get; set; }
        public string CommitterName { get; set; }
        public string Email { get; set; }
        public string CommitMessage { get; set; }
        public string Date { get; set; }
        public string TreeUrl { get; set; }
    }
}
