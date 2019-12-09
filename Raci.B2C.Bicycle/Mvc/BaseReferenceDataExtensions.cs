using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Mvc
{
    public static class BaseReferenceDataExtensions
    {
        public static void BindOptions<T>(this BaseReferenceData referenceData, Expression<Func<T, object>> property, IList<ReferenceData> items) where T : class
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (items != null)
            {
                foreach (ReferenceData currentItem in items)
                {
                    selectListItems.Add(new SelectListItem { Text = currentItem.Description, Value = currentItem.Id });
                }
            }

            referenceData.BindOptions<T>(property, selectListItems);
        }
    }
}