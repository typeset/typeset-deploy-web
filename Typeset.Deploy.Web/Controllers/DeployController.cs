using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Typeset.Deploy.Web.Models.Deploy;

namespace Typeset.Deploy.Web.Controllers
{
    public class DeployController : Controller
    {
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
                var success = false;

                using (var client = new HttpClient())
                {
                    var json = new
                    {
                        branches = new
                        {
                            @default = new
                            {
                                commit_id = "typeset-vLatest", //todo: Provide appropriate id
                                commit_message = "trigger via typeset-deploy",
                                download_url = "https://github.com/typeset/typeset/tarball/master",
                            }
                        }
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(json));
                    var response = client.PostAsync(model.BuildUrl, content).Result;
                    success = response.StatusCode.Equals(HttpStatusCode.OK);
                }

                return View("Success");
            }
            else
            {
                return View(model);
            }
        }
    }
}
