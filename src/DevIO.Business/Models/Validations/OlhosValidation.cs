using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class OlhosValidation : AbstractValidator<Olhos>
    {
        public OlhosValidation()
        {
            RuleFor(f => f.desc_olhos)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

        }
    }
}