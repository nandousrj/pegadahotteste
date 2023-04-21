using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class EstiloService : BaseService, IEstiloService
    {
        private readonly IEstiloRepository _estiloRepository;

        public EstiloService(IEstiloRepository estiloRepository,
                                 INotificador notificador) : base(notificador)
        {
            _estiloRepository = estiloRepository;
        }


        public async Task<bool> Incluir(Estilo estilo) // estava sem o bool
        {
            if (!ExecutarValidacao(new EstiloValidation(), estilo)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _estiloRepository.ObterPorNomePOC(estilo.desc_estilo.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_estilo.ToUpper().Trim() == estilo.desc_estilo.ToUpper().Trim())
                {
                    Notificar("Já existe um estilo com esta descrição informada.");
                    return false;
                }
            }

            await _estiloRepository.IncluirPOC(estilo);
            return true;
        }

        public async Task<bool> Alterar(Estilo estilo)
        {
            if (!ExecutarValidacao(new EstiloValidation(), estilo)) return false;

            var dados = await _estiloRepository.ObterPorNomePOC(estilo.desc_estilo.ToUpper().Trim());
            if (dados.desc_estilo.ToUpper().Trim() == estilo.desc_estilo.ToUpper().Trim()
                && dados.id_estilo != estilo.id_estilo)
            {
                Notificar("Já existe um estilo com esta descrição informada.");
                return false;
            }

            await _estiloRepository.AlterarPOC(estilo);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _estiloRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _estiloRepository?.Dispose();
        }
    }
}