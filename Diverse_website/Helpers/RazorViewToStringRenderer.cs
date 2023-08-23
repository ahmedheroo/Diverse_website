using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Helpers
{
    public class RazorViewToStringRenderer
    {

       
            private readonly IRazorViewEngine _viewEngine;
            private readonly ITempDataProvider _tempDataProvider;

            public RazorViewToStringRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
            {
                _viewEngine = viewEngine;
                _tempDataProvider = tempDataProvider;
            }

            public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
            {
                var httpContext = new DefaultHttpContext { RequestServices = new ServiceCollection().BuildServiceProvider() };
                var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

                using var sw = new StringWriter();
                var viewResult = _viewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            
        }
        
    }
}
