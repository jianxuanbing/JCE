using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Validations
{
    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validation:IValidation
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">验证目标</param>
        /// <returns></returns>
        public ValidationResultCollection Validate(object target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var result=new ValidationResultCollection();
            var validationResults=new List<ValidationResult>();
            var context=new ValidationContext(target,null,null);
            var isValid = Validator.TryValidateObject(target, context, validationResults, true);
            if (!isValid)
            {
                result.AddList(validationResults);
            }
            return result;
        }
    }
}
