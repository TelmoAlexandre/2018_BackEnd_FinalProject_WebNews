using System.Web;
using System.Web.Mvc;

namespace WebNews_19089 {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
