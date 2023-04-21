using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class TipoLogService : BaseService, ITipoLogService
    {
        private readonly ITipoLogRepository _tipologRepository;

        public TipoLogService(ITipoLogRepository tipologRepository,
                                 INotificador notificador) : base(notificador)
        {
            _tipologRepository = tipologRepository;
        }


        public async Task<bool> Incluir(TipoLog estilo) // estava sem o bool
        {
            if (!ExecutarValidacao(new TipoLogValidation(), estilo)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _tipologRepository.ObterPorNomePOC(estilo.descricao.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.descricao.ToUpper().Trim() == estilo.descricao.ToUpper().Trim())
                {
                    Notificar("Já existe um Tipo Log com esta descrição informada.");
                    return false;
                }
            }

            await _tipologRepository.IncluirPOC(estilo);
            return true;
        }

        public async Task<bool> Alterar(TipoLog tipolog)
        {
            if (!ExecutarValidacao(new TipoLogValidation(), tipolog)) return false;

            var dados = await _tipologRepository.ObterPorNomePOC(tipolog.descricao.ToUpper().Trim());
            if (dados.descricao.ToUpper().Trim() == tipolog.descricao.ToUpper().Trim()
                && dados.id_tipo_log != tipolog.id_tipo_log)
            {
                Notificar("Já existe um Tipo Log com esta descrição informada.");
                return false;
            }

            await _tipologRepository.AlterarPOC(tipolog);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _tipologRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _tipologRepository?.Dispose();
        }
    }
}