using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Glovali.Common.Rest
{
    /// <summary>
    /// Represents the <see cref="ModelStateExtensions"/> class.
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Gets the error messages from the model state dictionary.
        /// </summary>
        /// <param name="dictionary">The model state dictionary.</param>
        /// <returns>Returns the error messages.</returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
        }
    }
}
