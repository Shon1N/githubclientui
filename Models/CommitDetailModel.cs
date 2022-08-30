using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubClient.Core.Models
{
    public class CommitDetailModel
    {
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Patch { get; set; }
        public int ChangeCount { get; set; }
        public int AdditionCount { get; set; }
        public int DeletionCount { get; set; }

    }
}
