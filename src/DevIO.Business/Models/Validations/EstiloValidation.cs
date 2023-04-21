using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class EstiloValidation : AbstractValidator<Estilo>
    {
        public EstiloValidation()
        {
            RuleFor(f => f.desc_estilo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
          
        }
    }
}