using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ApiPerfil.Attributes
{
    public class VerificacaoModelVazio : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var type = value.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var propertyValue = property.GetValue(value);
                if (propertyValue != null)
                {
                    return ValidationResult.Success;
                }
            }
            
            return new ValidationResult(ErrorMessage ?? "Pelo menos uma propriedade deve ser fornecida.");
        }
    }
}