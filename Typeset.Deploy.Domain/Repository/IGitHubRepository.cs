using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Deploy.Domain.Repository
{
    public interface IGitHubRepository
    {
        string GetLatestCommitDownloadUrl(string username, string repositoryName);
        string GetLatestCommitSha(string username, string repositoryName);
    }
}
