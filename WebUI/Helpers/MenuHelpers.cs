using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobControl.WebUI.Extensions
{
    public static class MenuHelpers
    {
        public static IHtmlContent BeginMenu(this IHtmlHelper helper, string menuId)
        {
            return new HtmlString($"<nav id={menuId}><ul>");
        }

        public static IHtmlContent EndMenu(this IHtmlHelper helper)
        {
            return new HtmlString("</nav>");
        }

        public static IHtmlContent BeginSubMenu(this IHtmlHelper helper, string menuText)
        {
            return new HtmlString($"<li><span>{menuText}</span><ul>");
        }

        public static IHtmlContent EndSubMenu(this IHtmlHelper helper)
        {
            return new HtmlString($"</ul></li>");
        }

        public static IHtmlContent MenuItem(this IHtmlHelper helper, string menuText, string controller, string action)
        {
            var href = $"{controller}/{action}";
            return new HtmlString($"<li><a href='{href}'>{menuText}</a></li>");
        }

    }
}
