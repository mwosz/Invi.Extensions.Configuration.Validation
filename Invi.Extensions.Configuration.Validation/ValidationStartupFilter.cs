using System.Collections.Generic;
using System.Linq;

namespace Invi.Extensions.Configuration.Validation
{
    public class ValidationStartupFilter
    {
        public ValidationStartupFilter(IEnumerable<IValidation> validationList)
        {
            validationList.ToList().ForEach(v => v.Validate());
        }
    }
}