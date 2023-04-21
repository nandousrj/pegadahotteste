using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoCriticaService : BaseService, ITipoCriticaService
    {
        private readonly ITipoCriticaRepository _tipocriticaRepository;

        public TipoCriticaService(ITipoCriticaRepository tipocriticaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipocriticaRepository = tipocriticaRepository;
        }


        public async Task<bool> Incluir(TipoCritica valor) 
        {
            if (!ExecutarValidacao(new TipoCriticaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipocriticaRepository.ObterPorNomePOC(valor.desc_tipo_critica.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_tipo_critica.ToUpper().Trim() == valor.desc_tipo_critica.ToUpper().Trim())
                {
                    Notificar("Já existe um TipoCritica com esta descrição informada.");
                    return false;
                }
            }

            await _tipocriticaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(TipoCritica valor)
        {
            if (!ExecutarValidacao(new TipoCriticaValidation(), valor)) return false;

            var dados = await _tipocriticaRepository.ObterPorNomePOC(valor.desc_tipo_critica.ToUpper().Trim());
            if (dados.desc_tipo_critica.ToUpper().Trim() == valor.desc_tipo_critica.ToUpper().Trim()
                && dados.id_tipo_critica != valor.id_tipo_critica)
            {
                Notificar("Já existe um TipoCritica com esta descrição informada.");
                return false;
            }

            await _tipocriticaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipocriticaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipocriticaRepository?.Dispose();
        }
    }
}