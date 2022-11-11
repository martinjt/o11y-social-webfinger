using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace webfinger;
public static class O11ySocialWebfinger
{
    [FunctionName("O11ySocialWebfinger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = ".well-known/webfinger")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string name = req.Query["resource"];

        if (name != "acct:martindotnet@o11y.social")
            return new NotFoundResult();

        return new OkObjectResult(new ActivityPubAccount {
            Subject = "acct:martindotnet@o11y.social",
            Aliases = new List<string> {
                "https://hachyderm.io/@martindotnet",
                "https://hachyderm.io/users/MartinDotNet"
            },
            Links = new List<Link> {
                new Link {
                    Relation = "self",
                    Type = "application/activity+json",
                    Href = "https://hachyderm.io/users/MartinDotNet"
                }
            }
        });
    }
}
