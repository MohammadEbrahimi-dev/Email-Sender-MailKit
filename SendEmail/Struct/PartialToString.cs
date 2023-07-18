using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace SendEmail.Struct
{
    //Partial Convertor To String
    public class PartialToString
    {
        public static string RenderViewToString(Controller controller, string viewName, object model = null)
        {
            
            controller.ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                //Generator of view
                IViewEngine? viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewresult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewresult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                    );
                viewresult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}