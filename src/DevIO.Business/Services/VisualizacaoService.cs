using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class VisualizacaoService : BaseService, IVisualizacaoService
    {
        private readonly IVisualizacaoRepository _visualizacaoRepository;

        public VisualizacaoService(IVisualizacaoRepository visualizacaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _visualizacaoRepository = visualizacaoRepository;
        }


        public async Task<bool> Incluir(Visualizacao valor)
        {
            if (!ExecutarValidacao(new VisualizacaoValidation(), valor)
                //  || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)
                ) return false;


            await _visualizacaoRepository.IncluirPOC(valor);
            return true;
        }

        public async Task<bool> Alterar(Visualizacao valor)
        {
            if (!ExecutarValidacao(new VisualizacaoValidation(), valor)) return false;
          
            await _visualizacaoRepository.AlterarPOC(valor);
            return true;
        }

        public async Task<bool> AlterarSiteVisualizacaoGrupo(int id_grupo)
        {
            if (id_grupo == null) return false;

            await _visualizacaoRepository.AlterarSiteVisualizacaoGrupoPOC(id_grupo);
            return true;
        }

        //public async Task Excluir(int id)
        //{
        //    await _visualizacaoRepository.ExlcuirPOC(id);
        //}

        public void Dispose()
        {
            _visualizacaoRepository?.Dispose();
        }
    }
}