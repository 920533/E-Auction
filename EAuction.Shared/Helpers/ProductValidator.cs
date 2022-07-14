using EAuction.Shared;
using EAuction.Shared.Seller;
using FluentValidation;

namespace EAuction.Shared.Helpers
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName)
               .Must((model, val) => ValidateProductName(val))
               .WithMessage(Constants.InvalidProduct);
        }

        public bool ValidateProductName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name.Length > 30 || name.Length < 5)
                return false;
            else
                return true;
        }
    }
}
