using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TestCreatorWebApp.Tests.Helpers
{
    public static class JsonResultHelper
    {
        public static T GetValueFromJsonResult<T>(this JsonResult jsonResult, string propertyName)
        {
            var property =
                jsonResult.Value.GetType()
                    .GetProperties()
                    .FirstOrDefault(p => String.CompareOrdinal(p.Name, propertyName) == 0);

            if (null == property)
                throw new ArgumentException("{propertyName} not found", "propertyName");
            return (T)property.GetValue(jsonResult.Value, null);
        }
    }
}
