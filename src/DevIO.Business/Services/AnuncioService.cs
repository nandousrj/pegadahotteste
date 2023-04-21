using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class AnuncioService : BaseService, IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository,
                                 INotificador notificador) : base(notificador)
        {
            _anuncioRepository = anuncioRepository;
        }


        public async Task<bool> Incluir(Anuncio valor) 
        {
            if (!ExecutarValidacao(new AnuncioValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _anuncioRepository.ObterPorNomePOC(valor.desc_anuncio.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_anuncio.ToUpper().Trim() == valor.desc_anuncio.ToUpper().Trim())
                {
                    Notificar("Já existe um Anúncio com esta descrição informada.");
                    return false;
                }
            }

            await _anuncioRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Anuncio valor)
        {
            if (!ExecutarValidacao(new AnuncioValidation(), valor)) return false;

            var dados = await _anuncioRepository.ObterPorNomePOC(valor.desc_anuncio.ToUpper().Trim());
            if (dados.desc_anuncio.ToUpper().Trim() == valor.desc_anuncio.ToUpper().Trim()
                && dados.id_anuncio != valor.id_anuncio)
            {
                Notificar("Já existe um Anúncio com esta descrição informada.");
                return false;
            }

            await _anuncioRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _anuncioRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _anuncioRepository?.Dispose();
        }
    }
}