using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using UmbracoDemoSite.Core.Features.Shared;
using UmbracoDemoSite.Core.Services;
using GM = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Core.Features.ContactFormPage;

public class ContactFormPageController(
    ILogger<ContactFormPageController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor,
    IViewModelService viewModelService,
    IPublishedValueFallback publishedValueFallback) : PageBaseController(logger, compositeViewEngine, umbracoContextAccessor, viewModelService)
{
    public IActionResult ContactFormPage(ContentModel model)
    {
        var mbModel = new GM.Page(model.Content, publishedValueFallback);
        if (mbModel == null)
        { return NotFound(); }

        var viewModel = new ContactFormPageViewModel()
        {
            Heading = mbModel.Heading,
            BodyText = mbModel.BodyText == null 
                ? null 
                : new HtmlString(mbModel.BodyText.ToString()),
            Blocks = mbModel.Blocks
        };

        MapSitePageBase(viewModel, mbModel);

        return View(viewModel);
    }
}