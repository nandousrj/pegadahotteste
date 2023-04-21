using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoContatoService : BaseService, ITipoContatoService
    {
        private readonly ITipoContatoRepository _tipocontatoRepository;

        public TipoContatoService(ITipoContatoRepository tipocontatoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipocontatoRepository = tipocontatoRepository;
        }


        public async Task<bool> Incluir(TipoContato tipocontato) // estava sem o bool
        {
            if (!ExecutarValidacao(new TipoContatoValidation(), tipocontato)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipocontatoRepository.ObterPorNomePOC(tipocontato.desc_tipo_contato.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_tipo_contato.ToUpper().Trim() == tipocontato.desc_tipo_contato.ToUpper().Trim())
                {
                    Notificar("Já existe um tipo contato com esta descrição informada.");
                    return false;
                }
            }

            await _tipocontatoRepository.IncluirPOC(tipocontato);
            return true;
        }

        public async Task<bool> Alterar(TipoContato tipocontato)
        {
            if (!ExecutarValidacao(new TipoContatoValidation(), tipocontato)) return false;

            var dados = await _tipocontatoRepository.ObterPorNomePOC(tipocontato.desc_tipo_contato.ToUpper().Trim());
            if (dados.desc_tipo_contato.ToUpper().Trim() == tipocontato.desc_tipo_contato.ToUpper().Trim()
                && dados.id_tipo_contato != tipocontato.id_tipo_contato)
            {
                Notificar("Já existe um tipo contato com esta descrição informada.");
                return false;
            }

            await _tipocontatoRepository.AlterarPOC(tipocontato);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipocontatoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipocontatoRepository?.Dispose();
        }
    }
}