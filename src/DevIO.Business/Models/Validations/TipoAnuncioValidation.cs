using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class TipoAnuncioValidation : AbstractValidator<TipoAnuncio>
    {
        public TipoAnuncioValidation()
        {
            RuleFor(f => f.desc_tipo_anuncio)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

        }
    }
}