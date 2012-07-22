using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Typeset.Deploy.Domain.GitHub
{
    public class GitHubRepository : IGitHubRepository
    {
        private const string ApiUrl = "https://api.github.com";
        private string Username { get; set; }
        private string RepositoryName { get; set; }

        public GitHubRepository(string username, string repositoryName)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                throw new ArgumentNullException("repositoryName");
            }

            Username = username;
            RepositoryName = repositoryName;
        }

        public virtual string GetLatestCommitDownloadUrl()
        {
            var latestCommitId = GetLatestCommitSha();
            var downloadUrl = string.Format("https://github.com/{0}/{1}/tarball/{2}", Username, RepositoryName, latestCommitId);
            return downloadUrl;
        }

        public virtual string GetLatestCommitSha()
        {
            var sha = string.Empty;

            using (var client = new HttpClient())
            {
                var requestUrl = string.Format("{0}/repos/{1}/{2}/commits", ApiUrl, Username, RepositoryName);
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
