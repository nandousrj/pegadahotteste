using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class GrupoService : BaseService, IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _grupoRepository = grupoRepository;
        }


        public async Task<bool> Incluir(Grupo valor) 
        {
            if (!ExecutarValidacao(new GrupoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;

            var dados = await _grupoRepository.ObterPorNomePOC(valor.desc_grupo.ToUpper().Trim());

            if (dados != null)
            {
                if (dados.desc_grupo.ToUpper().Trim() == valor.desc_grupo.ToUpper().Trim())
                {
                    Notificar("Já existe um Grupo com esta descrição informada.");
                    return false;
                }
            }

            await _grupoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Grupo valor)
        {
            if (!ExecutarValidacao(new GrupoValidation(), valor)) return false;

            var dados = await _grupoRepository.ObterPorNomePOC(valor.desc_grupo.ToUpper().Trim());
            if (dados.desc_grupo.ToUpper().Trim() == valor.desc_grupo.ToUpper().Trim()
                && dados.id_grupo != valor.id_grupo)
            {
                Notificar("Já existe um Grupo com esta descrição informada.");
                return false;
            }

            await _grupoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task Excluir(int id)
        {
            await _grupoRepository.ExlcuirPOC(id);
        }

        public void Dispose()
        {
            _grupoRepository?.Dispose();
        }
    }
}