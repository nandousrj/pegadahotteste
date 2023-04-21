using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class TrabalhoValidation : AbstractValidator<Trabalho>
    {
        public TrabalhoValidation()
        {
            RuleFor(f => f.desc_trabalho)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
          
        }
    }
}