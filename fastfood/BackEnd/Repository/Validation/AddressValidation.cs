using System.ComponentModel.DataAnnotations;

namespace BackEnd.Repository.Validation
{

    public class AddressValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string address = value as string;

            if (string.IsNullOrEmpty(address))
            {
                return ValidationResult.Success;
            }

            if (address.Length < 5)
            {
                return new ValidationResult("Độ dài địa chỉ phải từ 5 đến 250 ký tự");
            }

            return ValidationResult.Success;
        }
    }
}