using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class ControleSistemaValidation : AbstractValidator<ControleSistema>
    {
        public ControleSistemaValidation()
        {
            RuleFor(f => f.cod_parametro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.val_parametro)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
               .Length(2, 200)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.id_tipo_controle_sistema)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(f => f.desc_controle)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
               .Length(2, 200)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            //RuleFor(f => f.status)
            //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

        }
    }
}