using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
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

        // Token: 0x060000BC RID: 188 RVA: 0x0000481C File Offset: 0x00002A1C
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
