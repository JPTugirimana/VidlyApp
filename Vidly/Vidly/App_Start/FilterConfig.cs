using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            //Add this line/filter in order to Authorize/filter on global level
            filters.Add(new AuthorizeAttribute());

            // Require secure connection
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
