using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WebLabsAsp.TagHelpers;

public class PagerTagHelper : TagHelper
{
    LinkGenerator _linkGenerator;

    public int PageCurrent { get; set; }

    public int PageTotal { get; set; }

    public string PagerClass { get; set; }

    public string Action { get; set; }

    public string Controller { get; set; }
    public Guid? GroupId { get; set; }

    public PagerTagHelper(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    public override void Process(TagHelperContext context,
        TagHelperOutput output)

    {
        output.TagName = "nav";
        var ulTag = new TagBuilder("ul");
        ulTag.AddCssClass("pagination");
        ulTag.AddCssClass(PagerClass);
        for (int i = 1; i <= PageTotal; i++)
        {
            var url = _linkGenerator.GetPathByAction(Action, Controller,
                new
                {
                    page = i,
                    group = GroupId == Guid.Empty
                        ? null
                        : GroupId
                });

            var item = GetPagerItem(
                url: url,
                text: i.ToString(),
                active: i == PageCurrent,
                disabled: i == PageCurrent
            );
            ulTag.InnerHtml.AppendHtml(item);
        }

        output.Content.AppendHtml(ulTag);
    }
    
    private TagBuilder GetPagerItem(string url, string text,
        bool active = false,
        bool disabled = false)

    {
        var liTag = new TagBuilder("li");
        liTag.AddCssClass("page-item");
        liTag.AddCssClass(active ? "active" : "");
        //liTag.AddCssClass(disabled ? "disabled" : "");
        var aTag = new TagBuilder("a");
        aTag.AddCssClass("page-link");
        aTag.Attributes.Add("href", url);
        aTag.InnerHtml.Append(text);
        liTag.InnerHtml.AppendHtml(aTag);
        return liTag;
    }
}