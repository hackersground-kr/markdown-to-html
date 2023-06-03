using System.IO;
using System.Net;
using System.Threading.Tasks;

using Markdig;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MD2Html.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity.
    /// </summary>
    public static class ConvertHttpTrigger
    {
        /// <summary>
        /// Invokes the conversion method from markdown to HTML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance that contains the markdown string.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the HTML string converted from markdown.</returns>
        [FunctionName(nameof(ConvertMDtoHtmlAsync))]
        [OpenApiOperation(operationId: "ConvertMDtoHTML", tags: new[] { "md2html" }, Summary = "Convert Markdown to HTML", Description = "Convert Markdown to HTML", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(schemeName: "function_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header, Description = "API Key through the request header")]
        [OpenApiRequestBody(contentType: "text/plain", bodyType: typeof(string), Required = true, Description = "Markdown contents")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "HTML contents converted from Markdown", Description = "HTML contents")]
        public static async Task<IActionResult> ConvertMDtoHtmlAsync(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "convert/md/to/html")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var md = default(string);
            using (var reader = new StreamReader(req.Body))
            {
                md = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var pipeline = new MarkdownPipelineBuilder()
                               .UseAdvancedExtensions()
                               .UseEmojiAndSmiley()
                               .UseYamlFrontMatter()
                               .Build();
            var html = Markdown.ToHtml(md, pipeline);

            var result = new ContentResult()
            {
                Content = html,
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;
        }
    }
}