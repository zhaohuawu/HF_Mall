using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Linq;

namespace Bryan.MicroService
{
    public class GlobalRoutes : IApplicationModelConvention
    {
        private readonly AttributeRouteModel prefix;
        public GlobalRoutes(IRouteTemplateProvider routeTemplateProvider)
        {
            this.prefix = new AttributeRouteModel(routeTemplateProvider);
            AttributeRouteModel attributeRouteModel = this.prefix;
            attributeRouteModel.Template += "/[controller]/[action]";
        }
        
        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel controllerModel in application.Controllers)
            {
                List<SelectorModel> list = controllerModel.Selectors.ToList<SelectorModel>();
                if (list.Any<SelectorModel>())
                {
                    foreach (SelectorModel selectorModel in list)
                    {
                        selectorModel.AttributeRouteModel = this.prefix;
                    }
                }
            }
        }
    }
}
