using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoContaService : BaseService, ITipoContaService
    {
        private readonly ITipoContaRepository _tipocontaRepository;

        public TipoContaService(ITipoContaRepository tipocontaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipocontaRepository = tipocontaRepository;
        }


        public async Task<bool> Incluir(TipoConta valor) 
        {
            if (!ExecutarValidacao(new TipoContaValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipocontaRepository.ObterPorNomePOC(valor.desc_tipo_conta.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_tipo_conta.ToUpper().Trim() == valor.desc_tipo_conta.ToUpper().Trim())
                {
                    Notificar("Já existe um Tipo de Conta com esta descrição informada.");
                    return false;
                }
            }

            await _tipocontaRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(TipoConta valor)
        {
            if (!ExecutarValidacao(new TipoContaValidation(), valor)) return false;

            var dados = await _tipocontaRepository.ObterPorNomePOC(valor.desc_tipo_conta.ToUpper().Trim());
            if (dados.desc_tipo_conta.ToUpper().Trim() == valor.desc_tipo_conta.ToUpper().Trim()
                && dados.id_tipo_conta != valor.id_tipo_conta)
            {
                Notificar("Já existe um Tipo de Conta com esta descrição informada.");
                return false;
            }

            await _tipocontaRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipocontaRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipocontaRepository?.Dispose();
        }
    }
}