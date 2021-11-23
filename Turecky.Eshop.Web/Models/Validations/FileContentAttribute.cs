using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Turecky.Eshop.Web.Models.Validation
{
    public class FileContentAttribute : IClientModelValidator
    {
        public FileContentAttribute()
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttributes(context.Attributes, "data-val", "true");
            MergeAttributes(context.Attributes, "data-val-filecontent", $"File does not contain {ContentType}");
            MergeAttributes(context.Attributes, "data-val-filecontent-type", $"{ContentType}");
        }

        public bool MergeAttributes(IDictionary<string, string> attributes, string key, string value) 
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }

    
}
