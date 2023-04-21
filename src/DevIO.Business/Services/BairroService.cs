using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class BairroService : BaseService, IBairroService
    {
        private readonly IBairroRepository _bairroRepository;

        public BairroService(IBairroRepository bairroRepository,
                                 INotificador notificador) : base(notificador)
        {
            _bairroRepository = bairroRepository;
        }


        public async Task<bool> Incluir(Bairro valor) 
        {
            if (!ExecutarValidacao(new BairroValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _bairroRepository.ObterPorNomePOC(valor.desc_bairro.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_bairro.ToUpper().Trim() == valor.desc_bairro.ToUpper().Trim())
                {
                    Notificar("Já existe um Bairro com esta descrição informada.");
                    return false;
                }
            }

            await _bairroRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Bairro valor)
        {
            if (!ExecutarValidacao(new BairroValidation(), valor)) return false;

            var dados = await _bairroRepository.ObterPorNomePOC(valor.desc_bairro.ToUpper().Trim());
            if (dados.desc_bairro.ToUpper().Trim() == valor.desc_bairro.ToUpper().Trim()
                && dados.id_bairro != valor.id_bairro)
            {
                Notificar("Já existe um Bairro com esta descrição informada.");
                return false;
            }

            await _bairroRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _bairroRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _bairroRepository?.Dispose();
        }
    }
}