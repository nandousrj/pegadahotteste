﻿using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class TipoContatoValidation : AbstractValidator<TipoContato>
    {
        public TipoContatoValidation()
        {
            RuleFor(f => f.desc_tipo_contato)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}