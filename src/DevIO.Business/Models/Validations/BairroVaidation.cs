using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class BairroValidation : AbstractValidator<Bairro>
    {
        public BairroValidation()
        {
            RuleFor(c => c.desc_bairro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Zona.id_zona)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser selecionado");
        }
    }
}