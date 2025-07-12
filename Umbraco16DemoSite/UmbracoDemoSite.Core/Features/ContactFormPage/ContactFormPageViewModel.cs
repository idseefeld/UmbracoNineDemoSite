using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using UmbracoDemoSite.Core.Features.Shared.Components.ContactForm;
using UmbracoDemoSite.Core.Features.Shared.Content;

namespace UmbracoDemoSite.Core.Features.ContactFormPage;

public class ContactFormPageViewModel : SitePageBase, IHeadingPage
{
    public ContactFormPageViewModel() : base() { }

    public string? Heading { get; set; }
    public IHtmlContent? BodyText { get; set; }

    private BlockListModel? _blocks;
    public BlockListModel? Blocks
    {
        get
        {
            return _blocks;
        }
        set
        {
            if (value != null)
            {
                _blocks = value;
            }
            else
            {
                IList<BlockListItem> emptyList = new List<BlockListItem>();
                _blocks = new BlockListModel(emptyList);
            }
        }
    }

    public ContactFormModel FormModel { get; set; } = new();

}