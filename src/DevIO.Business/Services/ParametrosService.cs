using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ParametrosService : BaseService, IParametrosService
    {
        private readonly IParametrosRepository _parametrosRepository;

        public ParametrosService(IParametrosRepository parametrosRepository,
                                 INotificador notificador) : base(notificador)
        {
            parametrosRepository = _parametrosRepository;
        }


        public async Task<bool> Incluir(Parametros parametros) // estava sem o bool
        {
            //if (!ExecutarValidacao(new EstiloValidation(), parametros)                
            //    ) return false;

          //  var dados = await _estiloRepository.ObterPorNomePOC(parametros.desc_estilo.ToUpper().Trim());

            //if (dados != null)
            //{
            //    if (dados.desc_estilo.ToUpper().Trim() == parametros.desc_estilo.ToUpper().Trim())
            //    {
            //        Notificar("Já existe um parâmetro com esta descrição informada.");
            //        return false;
            //    }
            //}

            await _parametrosRepository.IncluirPOC(parametros);
            return true;
        }

        public async Task<bool> Alterar(Parametros parametros)
        {
            //if (!ExecutarValidacao(new EstiloValidation(), parametros)) return false;

            //var dados = await _parametrosRepository.ObterPorNomePOC(estilo.desc_estilo.ToUpper().Trim());
            //if (dados.desc_estilo.ToUpper().Trim() == estilo.desc_estilo.ToUpper().Trim()
            //    && dados.id_estilo != estilo.id_estilo)
            //{
            //    Notificar("Já existe um parâmetro com esta descrição informada.");
            //    return false;
            //}

            await _parametrosRepository.AlterarPOC(parametros);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _parametrosRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _parametrosRepository?.Dispose();
        }
    }
}