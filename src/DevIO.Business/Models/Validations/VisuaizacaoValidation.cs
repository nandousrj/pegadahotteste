using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class VisualizacaoValidation : AbstractValidator<Visualizacao>
    {
        public VisualizacaoValidation()
        {
            RuleFor(f => f.GarotaCategoria.id_garota_categoria)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(f => f.GarotaCategoria.Categoria.id_categoria)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido.");

        }
    }
}