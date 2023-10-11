using CoreLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using System.Text;

namespace TalabatApi.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int lifetime;

        public CachedAttribute(int Lifetime)
        {
           lifetime = Lifetime;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashingservice = context.HttpContext.RequestServices.GetRequiredService<ICachingService>();

            var cashedkey =  GetCashedKeyFromRequest(context.HttpContext.Request);

            var cashedResponse = await cashingservice.GetCachedResponseAsync(cashedkey);

            if (!string.IsNullOrEmpty(cashedResponse))
            {
                var result = new ContentResult()
                {
                    Content = cashedResponse,
                    ContentType = "Application/json",
                    StatusCode = 200
                };
                return;
            }

           var executedEndPoint = await next.Invoke();

            if (executedEndPoint.Result is OkObjectResult okObjectResult)
            {
                await cashingservice.CreateCachedResponceAsync(cashedkey, okObjectResult, TimeSpan.FromSeconds(lifetime));
            }
        }

        private string GetCashedKeyFromRequest(HttpRequest request)
        {
            var cashedkey = new StringBuilder();
            cashedkey.Append(request.Path);

            foreach (var (key , value) in request.Query.OrderBy(x=>x.Key))
            {
                cashedkey.Append($"|{key}-{value}");
            }

            return  cashedkey.ToString();

        }
    }
}
