using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class AnuncioValidation : AbstractValidator<Anuncio>
    {
        public AnuncioValidation()
        {
            RuleFor(c => c.desc_anuncio)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.ordem)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(f => f.status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");          
          
        }
    }
}