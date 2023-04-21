using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class TipoContaValidation : AbstractValidator<TipoConta>
    {
        public TipoContaValidation()
        {
            RuleFor(f => f.desc_tipo_conta)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.sigla)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(3, 5)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}