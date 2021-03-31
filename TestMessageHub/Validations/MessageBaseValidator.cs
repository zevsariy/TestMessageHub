using FluentValidation;
using TestMessageHub.Models;
using TestMessageHub.Models.Const;

namespace TestMessageHub.Validations
{
    public class MessageBaseValidator : AbstractValidator<MessageBase>
	{
		public MessageBaseValidator()
		{
			RuleFor(x => x.From).NotNull();
			RuleFor(x => x.To).NotNull();
			RuleFor(x => x.From.ToUpper()).Must(
				companyName => companyName == Companies.Adidas 
				|| companyName == Companies.Nike 
				|| companyName == Companies.Puma)
				.WithMessage(ErrorMessages.UnknownFromCompanyName);
			RuleFor(x => x.To.ToUpper()).Must(
				companyName => companyName == Companies.Adidas
				|| companyName == Companies.Nike
				|| companyName == Companies.Puma)
				.WithMessage(ErrorMessages.UnknownToCompanyName);
		}
	}
}
