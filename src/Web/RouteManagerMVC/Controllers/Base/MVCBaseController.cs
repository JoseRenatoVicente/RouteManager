using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers.Base;

//[SecurityHeaders]
public class MvcBaseController : Controller
{
    private readonly ICollection<string> Errors = new List<string>();
    private readonly INotifier Notifier;

    public MvcBaseController(INotifier notifier)
    {
        Notifier = notifier;
    }

    protected async Task<ActionResult> CustomResponseAsync(object result = null, string actionName = "Index")
    {
        if (await IsOperationValid())
        {
            return RedirectToAction(actionName);
        }
        TempData["Errors"] = await GetErrors();

        return View(result);
    }

    protected async Task<ActionResult> CustomResponseAsync(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            await AddError(erro.ErrorMessage);
        }

        return await CustomResponseAsync();
    }

    protected async Task<ActionResult> CustomResponseAsync(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            await AddError(error.ErrorMessage);
        }

        return await CustomResponseAsync();
    }

    protected Task<bool> IsOperationValid()
    {
        return Task.Run(() => !Notifier.IsNotified());
    }

    protected async Task<IEnumerable<string>> GetErrors()
    {
        foreach (var item in Notifier.GetNotifications())
        {
            await AddError(item);
        }
        Notifier.Clear();
        return Errors;
    }

    protected Task Notification(string error)
    {
        return Task.Run(() => Notifier.Handle(error));
    }
    private Task AddError(string error)
    {
        return Task.Run(() => Errors.Add(error));
    }
    protected Task ClearErrors()
    {
        return Task.Run(() => Notifier.Clear());
    }

}