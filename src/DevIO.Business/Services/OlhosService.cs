using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class OlhosService : BaseService, IOlhosService
    {
        private readonly IOlhosRepository _olhosRepository;

        public OlhosService(IOlhosRepository olhosRepository,
                                 INotificador notificador) : base(notificador)
        {
            _olhosRepository = olhosRepository;
        }


        public async Task<bool> Incluir(Olhos valor) 
        {
            if (!ExecutarValidacao(new OlhosValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _olhosRepository.ObterPorNomePOC(valor.desc_olhos.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_olhos.ToUpper().Trim() == valor.desc_olhos.ToUpper().Trim())
                {
                    Notificar("Já existe um Olho com esta descrição informada.");
                    return false;
                }
            }

            await _olhosRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Olhos valor)
        {
            if (!ExecutarValidacao(new OlhosValidation(), valor)) return false;

            var dados = await _olhosRepository.ObterPorNomePOC(valor.desc_olhos.ToUpper().Trim());
            if (dados.desc_olhos.ToUpper().Trim() == valor.desc_olhos.ToUpper().Trim()
                && dados.id_olhos != valor.id_olhos)
            {
                Notificar("Já existe um Olho com esta descrição informada.");
                return false;
            }

            await _olhosRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _olhosRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _olhosRepository?.Dispose();
        }
    }
}