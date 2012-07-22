using System.Net.Http;
using Newtonsoft.Json;

namespace Typeset.Deploy.Domain.Repository
{
    public class GitHubRepository : IGitHubRepository
    {
        private const string GitHubApiUrl = "https://api.github.com";

        public string GetLatestCommitDownloadUrl(string username, string repositoryName)
        {
            var latestCommitId = GetLatestCommitSha(username, repositoryName);
            var downloadUrl = string.Format("https://github.com/{0}/{1}/tarball/{2}", username, repositoryName, latestCommitId);
            return downloadUrl;
        }

        public string GetLatestCommitSha(string username, string repositoryName)
        {
            var sha = string.Empty;

            using (var client = new HttpClient())
            {
                var requestUrl = string.Format("{0}/repos/{1}/{2}/commits", GitHubApiUrl, username, repositoryName);
                var response = client.GetAsync(requestUrl).Result;
                var jsonString = response.Content.ReadAsStringAsync().Result;
                dynamic json = JsonConvert.DeserializeObject(jsonString);
                dynamic latestCommit = json[0];
                sha = latestCommit.sha.Value.ToString();
            }

            return sha;
        }
    }
}
