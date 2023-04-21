using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class BancoService : BaseService, IBancoService
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoService(IBancoRepository bancoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _bancoRepository = bancoRepository;
        }


        public async Task<bool> Incluir(Banco valor) 
        {
            if (!ExecutarValidacao(new BancoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _bancoRepository.ObterPorNomePOC(valor.desc_banco.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_banco.ToUpper().Trim() == valor.desc_banco.ToUpper().Trim())
                {
                    Notificar("Já existe um Banco com esta descrição informada.");
                    return false;
                }
            }

            await _bancoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Banco valor)
        {
            if (!ExecutarValidacao(new BancoValidation(), valor)) return false;

            var dados = await _bancoRepository.ObterPorNomePOC(valor.desc_banco.ToUpper().Trim());
            if (dados.desc_banco.ToUpper().Trim() == valor.desc_banco.ToUpper().Trim()
                && dados.id_banco != valor.id_banco)
            {
                Notificar("Já existe um Banco com esta descrição informada.");
                return false;
            }

            await _bancoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _bancoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _bancoRepository?.Dispose();
        }
    }
}