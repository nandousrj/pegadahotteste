using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaCategoriaRepository : IRepository<GarotaCategoria>
    {

        Task<List<GarotaCategoria>> ConsultarPOC(string nome, string apelido, string cpf);
        Task<GarotaCategoria> ObterPorIdPOC(int id);
        Task<GarotaCategoria> RetornarDadosGarotaCategoriaUsuario(int id_categoria, string desczona, int destaque);
        Task<List<GarotaCategoria>> RetornarDadosGarotaCategoriaUsuarioPerfil(int id_usuario);
        Task<List<GarotaCategoria>> RetornarDadosCategoriaUsuarioPerfil(int id_usuario);
        Task<List<GarotaCategoria>> RetornarDadosGarotaCategoriaLogada(int id_garota);
        Task<List<GarotaCategoria>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo);
        Task<List<GarotaCategoria>> RetornarDadosGrupo(int id_grupo);
        Task<List<GarotaCategoria>> RetornarDadosVisualizadas(int id_categoria);
        Task<List<GarotaCategoria>> RetornarDadosVisualizadasTodos(int id_categoria);
        Task<List<GarotaCategoria>> RetornarDadosCurtidas(int id_categoria);
        Task<List<GarotaCategoria>> RetornarDadosCurtidasTodos(int id_categoria);
        Task<List<GarotaCategoria>> RetornarDadosCurtidasVisualizadasTotal(int id_categoria);
        Task<List<GarotaCategoria>> RetornarDadosCurtidasVisualizadasTotalTodos(int id_categoria);
        Task<GarotaCategoria> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria);
        Task<List<GarotaCategoria>> RelatorioAluguel();
        Task<List<GarotaCategoria>> ConsultarGarotaPorId(int id_garota);
        Task<List<GarotaCategoria>> RetornarTodosPOC(int status);
        Task<int> AtualizarPromocaoMesGratis();
        Task<List<GarotaCategoria>> RetornaCupomAtivo();
        //Task<List<GarotaCategoriaAtende>> RetornarGarotaCategoriaAtende(int id_garota_categoria);
        //Task<List<GarotaCategoriaIdioma>> RetornarGarotaCategoriaIdioma(int id_garota_categoria);
        Task IncluirPOC(GarotaCategoria valor);
        Task AlterarPOC(GarotaCategoria valor);
        Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria);

        //Task ExlcuirPOC(int Id);

    }
}

