using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class PermissoesUsuarioValidation : AbstractValidator<PermissoesUsuario>
    {
        public PermissoesUsuarioValidation()
        {
            RuleFor(f => f.nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.login)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 20)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => LoginValidacao.Validar(f.login)).Equal(true)
                    .WithMessage("O Login está fora do padrão (nome.sobrenome).");

            RuleFor(s => s.email).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .EmailAddress().WithMessage("{PropertyName} inválido");

            RuleFor(f => f.telefone)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.celular)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.id_setor)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.responsavel)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.senha_api).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
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