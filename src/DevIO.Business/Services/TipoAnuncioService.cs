using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoAnuncioService : BaseService, ITipoAnuncioService
    {
        private readonly ITipoAnuncioRepository _tipoanuncioRepository;

        public TipoAnuncioService(ITipoAnuncioRepository tipoanuncioRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipoanuncioRepository = tipoanuncioRepository;
        }


        public async Task<bool> Incluir(TipoAnuncio valor) 
        {
            if (!ExecutarValidacao(new TipoAnuncioValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipoanuncioRepository.ObterPorNomePOC(valor.desc_tipo_anuncio.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_tipo_anuncio.ToUpper().Trim() == valor.desc_tipo_anuncio.ToUpper().Trim())
                {
                    Notificar("Já existe um Tipo de Anúncio com esta descrição informada.");
                    return false;
                }
            }

            await _tipoanuncioRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(TipoAnuncio valor)
        {
            if (!ExecutarValidacao(new TipoAnuncioValidation(), valor)) return false;

            var dados = await _tipoanuncioRepository.ObterPorNomePOC(valor.desc_tipo_anuncio.ToUpper().Trim());
            if (dados.desc_tipo_anuncio.ToUpper().Trim() == valor.desc_tipo_anuncio.ToUpper().Trim()
                && dados.id_tipo_anuncio != valor.id_tipo_anuncio)
            {
                Notificar("Já existe um Tipo de Anúncio com esta descrição informada.");
                return false;
            }

            await _tipoanuncioRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipoanuncioRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipoanuncioRepository?.Dispose();
        }
    }
}