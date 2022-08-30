using GitHubClient.Core.Models;
using GitHubClient.Ui.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GitHubClient.Ui.Pages.GitHub.Base
{
    public class GitHubBase : ComponentBase
    {
        [Inject]
        public IGitHubService _gitHubService { get; set; }
        public string SearchString = "";
        public bool _loading;
        public int FileCount = 0;
        public string PersonalAccessToken { get; set; }
        public string Username { get; set; }
        public string Repository { get; set; }
        public IEnumerable<CommitModel> CommitModels { get; set; } = Enumerable.Empty<CommitModel>();
        public IEnumerable<CommitDetailModel> CommitDetailModels { get; set; } = Enumerable.Empty<CommitDetailModel>();
        public bool ShowCommitsGrid { get; set; }
        private CommitModel _selectedItem;
        public CommitModel SelectedItem
        {
            get { return _selectedItem; } 
            set
            {
                if(value != _selectedItem)
                {
                    _selectedItem = value;
                    ToggleOpen();
                    GetCommitDetails();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ShowCommitsGrid = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

        }

        public bool FilterFunc1(CommitModel element) => FilterFunc(element, SearchString);

        public bool FilterFunc(CommitModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.CommitterName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CommitMessage.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public bool _isPopOverOpen;

        public void ToggleOpen()
        {
            if (_isPopOverOpen)
                _isPopOverOpen = false;
            else
                _isPopOverOpen = true;
        }

        private async void GetCommitDetails()
        {
            //Fetch commit details
            _loading = true;
            var data = await _gitHubService.GetCommitByShaAsync($"{Username}/{Repository}", _selectedItem.CommitSha, PersonalAccessToken);
            CommitDetailModels = data.Resonse;
            FileCount = CommitDetailModels.Count();
            _loading = false;
            if(FileCount > 0)
            {
                ShowCommitsGrid = false;
            }
            StateHasChanged();
        }

        private async Task GetCommites()
        {
            _loading = true;
            var data = await _gitHubService.GetCommitsAsync($"{Username}/{Repository}", PersonalAccessToken);
            CommitModels = data.Resonse;
            _loading = false;
        }

        public async void OnFetchClicked()
        {
            CommitDetailModels = Enumerable.Empty<CommitDetailModel>();
            SelectedItem = new CommitModel();
            await GetCommites();
            ShowCommitsGrid = true;
            StateHasChanged();
        }

        public void OnReadLaterClicked()
        {
            CommitDetailModels = Enumerable.Empty<CommitDetailModel>();
            SelectedItem = new CommitModel();
            ShowCommitsGrid = true;
        }

        public void OnMarkAsReadClicked()
        {
            CommitDetailModels = Enumerable.Empty<CommitDetailModel>();
            SelectedItem = new CommitModel();
            ShowCommitsGrid = true;
        }

    }
}
