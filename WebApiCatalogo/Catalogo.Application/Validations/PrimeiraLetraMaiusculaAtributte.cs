using System.ComponentModel.DataAnnotations;

namespace WebApiCatalogo.Catalogo.Application.Validations
{
    public class PrimeiraLetraMaiusculaAtributte : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome do produto deve ser maiuscula");
            }
            
            return ValidationResult.Success;   

        }
    }
}
