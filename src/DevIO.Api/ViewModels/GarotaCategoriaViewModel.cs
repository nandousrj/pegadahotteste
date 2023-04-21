using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class GarotaCategoriaViewModel
    {
        [Key]
        public int id_garota_categoria { get; set; }
     //   public string id_cripto { get; set; }
        public Guid Id { get; set; } // com entity tem que desabilitar
       
        public string apelido { get; set; }
        public int idade { get; set; }

        public int id_garota { get; set; }
        public GarotaViewModel Garota { get; set; }

        public int id_sexo { get; set; }
        public SexoViewModel Sexo { get; set; }

        public int id_categoria { get; set; }
        public CategoriaViewModel Categoria { get; set; }

        public int id_trabalho { get; set; }
        public TrabalhoViewModel Trabalho { get; set; }
        public bool status { get; set; }
        public bool ind_destaque { get; set; }
        public string altura { get; set; }
        public string altura2 { get; set; }
        public string manequim { get; set; }
        public string manequim2 { get; set; }

        public int id_zona { get; set; }
        public ZonaViewModel Zona { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string celular1 { get; set; }
        public string celular2 { get; set; }
        public string celular3 { get; set; }
        public string celular4 { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public string imagem_01 { get; set; }
        public string imagem_02 { get; set; }
        public string imagem_03 { get; set; }
        public string imagem_04 { get; set; }
        public string imagem_05 { get; set; }
        public string imagem_06 { get; set; }
        public string imagem_07 { get; set; }
        public string imagem_08 { get; set; }
        public string imagem_09 { get; set; }
        public string imagem_10 { get; set; }
        public int qtd_visualizacao { get; set; }
        public int qtd_curticao { get; set; }

        public int id_estilo { get; set; }
        public EstiloViewModel Estilo { get; set; }
        public decimal valor_aluguel { get; set; }
        public decimal valor_destaque { get; set; }
        public int desconto { get; set; }
        public PermissoesUsuarioViewModel PermissoesUsuario { get; set; }
        public decimal total { get; set; }
        public BairroViewModel Bairro { get; set; }
        public string atendimento { get; set; }
        public string desc_bairro { get; set; }

        public int id_grupo { get; set; }

        public GrupoViewModel Grupo { get; set; }

        public string qtd_visualizacao2 { get; set; }
        public string qtd_curticao2 { get; set; }
        public string quantidade2 { get; set; }

        public int id_tipo_critica { get; set; }
        public TipoCriticaViewModel TipoCritica { get; set; }
      //  public CriticaViewModel Critica { get; set; }

        public string valora_luguel2 { get; set; }
        public string valor_destaque2 { get; set; }
        public string desconto2 { get; set; }
        public string total2 { get; set; }
        public bool ind_promocao_mes_gratis { get; set; }

        public int id_olhos { get; set; }
        public OlhosViewModel Olhos { get; set; }

        public int id_atende { get; set; }
        public AtendeViewModel Atende { get; set; }
        public IdiomaViewModel Idioma { get; set; }

        public int id_tipo_anuncio { get; set; }
        public TipoAnuncioViewModel TipoAnuncio { get; set; }
        public string link_video_01 { get; set; }
        public string link_video_02 { get; set; }
        public string link_video_03 { get; set; }
        public string link_video_04 { get; set; }
        public string link_video_05 { get; set; }
        public string link_site_01 { get; set; }
        public string link_site_02 { get; set; }
        public string link_site_03 { get; set; }
        public string link_site_04 { get; set; }
        public string link_site_05 { get; set; }
        public bool ind_prive { get; set; }
        public string desc_anal { get; set; }
        public string desc_viagem { get; set; }
        public string desc_oral_sem_camisinha { get; set; }
        public string desc_dupla_penetracao { get; set; }
        public string imagem_banner { get; set; }
        public bool ind_cartao { get; set; }
        public bool ind_fim_de_semana { get; set; }

        public bool ind_cupom { get; set; }
        public int pct_cupom { get; set; }

        public IEnumerable<TipoCriticaViewModel> ListaTipoCritica { get; set; }
    }
}
