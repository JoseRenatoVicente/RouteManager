using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RouteManagerMVC.Controllers.Base
{
    [Authorize]
    public class MVCBaseController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }

        protected void AddError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool IsOperationValid()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}