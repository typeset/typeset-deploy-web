using System;

namespace Typeset.Deploy.Domain.GitHub
{
    public interface IGitHubRepository
    {
        string GetLatestCommitDownloadUrl();
        string GetLatestCommitSha();
    }
}
