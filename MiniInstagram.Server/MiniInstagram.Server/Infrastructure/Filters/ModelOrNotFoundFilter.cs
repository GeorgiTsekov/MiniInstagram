using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiniInstagram.Server.Infrastructure.Filters
{
    public class ModelOrNotFoundFilter : ActionFilterAttribute
    {
        // TODO use this functionality later
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (context.Result is ObjectResult result)
        //    {
        //        var model = result.Value;

        //        if (model != null)
        //        {
        //            context.Result = new NotFoundResult();
        //        }
        //    }
        //}
    }
}
