using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
   public  class GarotaCategoria : Entity
    {
        public int id_garota_categoria { get; set; }
        public string apelido { get; set; }
        public int idade { get; set; }

        public int id_garota { get; set; }
        public Garota Garota { get; set; }
        public int id_sexo { get; set; }
        public Sexo Sexo { get; set; }

        public int id_categoria { get; set; }
        public Categoria Categoria { get; set; }
        public int id_trabalho { get; set; }
        public Trabalho Trabalho { get; set; }
        public bool status { get; set; }
        public bool ind_destaque { get; set; }
        public string altura { get; set; }
        public string altura2 { get; set; }
        public string manequim { get; set; }
        public string manequim2 { get; set; }

        public int id_zona { get; set; }
        public Zona Zona { get; set; }
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
        public Estilo Estilo { get; set; }
        public decimal valor_aluguel { get; set; }
        public decimal valor_destaque { get; set; }
        public int desconto { get; set; }
        public PermissoesUsuario PermissoesUsuario { get; set; }
        public decimal total { get; set; }
        public int id_bairro { get; set; }
        public Bairro Bairro { get; set; }
        public string atendimento { get; set; }
        public string desc_bairro { get; set; }
        public int id_grupo { get; set; }
        public Grupo Grupo { get; set; }

        public string qtd_visualizacao2 { get; set; }
        public string qtd_curticao2 { get; set; }
        public string quantidade2 { get; set; }
        public int id_tipo_critica { get; set; }
        public TipoCritica TipoCritica { get; set; }

     //   public Critica Critica { get; set; }

        public string valora_luguel2 { get; set; }
        public string valor_destaque2 { get; set; }
        public string desconto2 { get; set; }
        public string total2 { get; set; }
        public bool ind_promocao_mes_gratis { get; set; }

        public int id_olhos { get; set; }
        public Olhos Olhos { get; set; }
        public int id_atende { get; set; }
        public Atende Atende { get; set; }
        public Idioma Idioma { get; set; }
        public int id_tipo_anuncio { get; set; }
        public TipoAnuncio TipoAnuncio { get; set; }
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
    }
}
