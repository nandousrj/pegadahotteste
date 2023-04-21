using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class NovidadeValidation : AbstractValidator<Novidade>
    {
        public NovidadeValidation()
        {
            RuleFor(c => c.desc_novidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 150).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(c => c.ordem)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(c => c.GarotaCategoria.Categoria.id_categoria)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

        }
    }
}