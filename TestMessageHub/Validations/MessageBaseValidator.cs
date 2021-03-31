using FluentValidation;
using TestMessageHub.Models;

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
				.WithMessage("Unknown FromCompany name.");

			RuleFor(x => x.To.ToUpper()).Must(
				companyName => companyName == Companies.Adidas
				|| companyName == Companies.Nike
				|| companyName == Companies.Puma)
				.WithMessage("Unknown ToCompany name.");
		}
	}
}
