using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class PermissoesInstituicaoService : BaseService, IPermissoesInstituicaoService
    {
        private readonly IPermissoesInstituicaoRepository _permissoesInstituicaoRepository;

        public PermissoesInstituicaoService(IPermissoesInstituicaoRepository permissoesInstituicaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _permissoesInstituicaoRepository = permissoesInstituicaoRepository;
        }


        public async Task<bool> Incluir(PermissoesInstituicao valor) 
        {
            if (!ExecutarValidacao(new PermissoesInstituicaoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _permissoesInstituicaoRepository.ObterPorNomePOC(valor.nome.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.nome.ToUpper().Trim() == valor.nome.ToUpper().Trim())
                {
                    Notificar("Já existe um Nome com esta descrição informada.");
                    return false;
                }
            }

            await _permissoesInstituicaoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(PermissoesInstituicao valor)
        {
            if (!ExecutarValidacao(new PermissoesInstituicaoValidation(), valor)) return false;

            var dados = await _permissoesInstituicaoRepository.ObterPorNomePOC(valor.nome.ToUpper().Trim());
            if (dados.nome.ToUpper().Trim() == valor.nome.ToUpper().Trim()
                && dados.id_instituicao != valor.id_instituicao)
            {
                Notificar("Já existe um Nome com esta descrição informada.");
                return false;
            }

            await _permissoesInstituicaoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _permissoesInstituicaoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _permissoesInstituicaoRepository?.Dispose();
        }
    }
}