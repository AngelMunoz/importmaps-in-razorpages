using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Important;

[HtmlTargetElement("import-map")]
public class ImportMapTagHelper : TagHelper
{
    /// Local disk path for the import map
    [HtmlAttributeName("src")]
    public string? Src { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "script";
        output.Attributes.SetAttribute("type", "importmap");
        if (Src is  null) { return; }
        string src = Src;
        
        if(src.StartsWith('~'))
        {
            src = Path.GetFullPath(Src.Replace('~', '.'));
        }
        var content = await File.ReadAllTextAsync(src);
        output.Content.SetHtmlContent(content);
    }
}