using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Newtonsoft.Json;
using Typeset.Deploy.Domain.GitHub;
using Typeset.Deploy.Web.Models.Deploy;

namespace Typeset.Deploy.Web.Controllers
{
    public class DeployController : Controller
    {
        private IGitHubRepository GitHubRepository { get; set; }

        public DeployController(IGitHubRepository gitHubRepository)
        {
            if (gitHubRepository == null)
            {
                throw new ArgumentNullException("gitHubRepository");
            }

            GitHubRepository = gitHubRepository;
        }

        [HttpGet]
        public ActionResult AppHarbor()
        {
            return View(new AppHarbor());
        }

        [HttpPost]
        public ActionResult AppHarbor(AppHarbor model)
        {
            if (ModelState.IsValid)
            {
                if (TryDeployToAppHarbor(model.BuildUrl))
                {
                    return View("Successful");
                }
                else
                {
                    return View("Unsuccessful");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Manually()
        {
            return Redirect("https://github.com/typeset/typeset/wiki/deploy");
        }

        protected string GetLatestCommitId()
        {
            var key = "typeset-latest-commit-id";
            var value = HttpRuntime.Cache.Get(key) as string;

            if (string.IsNullOrWhiteSpace(value))
            {
                value = GitHubRepository.GetLatestCommitSha();
                HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
            }

            return value;
        }

        protected string GetDownloadUrl()
        {
            var key = "typeset-download-url";
            var value = HttpRuntime.Cache.Get(key) as string;

            if (string.IsNullOrWhiteSpace(value))
            {
                value = GitHubRepository.GetLatestCommitDownloadUrl();
                HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
            }

            return value;
        }

        protected bool TryDeployToAppHarbor(string buildUrl)
        {
            var success = false;

            var downloadUrl = GetDownloadUrl();
            var latestCommitHash = GetLatestCommitId();

            using (var client = new HttpClient())
            {
                var json = new
                {
                    branches = new
                    {
                        @default = new
                        {
                            commit_id = latestCommitHash,
                            commit_message = "deployed via typeset-deploy",
                            download_url = downloadUrl,
                        }
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(json));
                var response = client.PostAsync(buildUrl, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;
                success = response.StatusCode.Equals(HttpStatusCode.OK) && string.IsNullOrWhiteSpace(responseBody);
            }

            return success;
        }
    }
}
