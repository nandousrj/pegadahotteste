using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class GarotaValidation : AbstractValidator<Garota>
    {
        public GarotaValidation()
        {
            RuleFor(f => f.nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.cpf.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(f => CpfValidacao.Validar(f.cpf)).Equal(true)
                     .WithMessage("O cpf fornecido é inválido.");

            RuleFor(f => f.dt_nascimento)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(s => s.email).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                     .EmailAddress().WithMessage("{PropertyName} inválido");

            RuleFor(p => p.senha).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                   // .MinimumLength(8).WithMessage("Sua senha deve possuir no mínimo 8 caracteres.")
                   // .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Length(6, 10).WithMessage("Sua senha precisa ter entre {MinLength} e {MaxLength} caracteres.")
                    .Matches(@"[A-Z]+").WithMessage("Sua senha deve possuir no mínimo uma letra maiúscula.")
                    .Matches(@"[a-z]+").WithMessage("Sua senha deve possuir no mínimo uma letra minúscula.")
                    .Matches(@"[0-9]+").WithMessage("Sua senha deve possuir no mínimo um número.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Sua senha deve possuir no mínimo um caracter especial(!? *.).");
        }
    }
}