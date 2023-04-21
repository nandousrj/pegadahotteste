using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Estilo, EstiloViewModel>().ReverseMap();
            CreateMap<Zona, ZonaViewModel>().ReverseMap();            

            CreateMap<BairroViewModel, Bairro>();
            CreateMap<Bairro,BairroViewModel>()
                .ForMember(dest => dest.descricao_zona, opt => opt.MapFrom(src => src.Zona.desc_zona));

            CreateMap<Atende, AtendeViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<TipoContato, TipoContatoViewModel>().ReverseMap();
            CreateMap<Idioma, IdiomaViewModel>().ReverseMap();
            CreateMap<Olhos, OlhosViewModel>().ReverseMap();
            CreateMap<Sexo, SexoViewModel>().ReverseMap();
            CreateMap<TipoAnuncio, TipoAnuncioViewModel>().ReverseMap();
            CreateMap<TipoPagamento, TipoPagamentoViewModel>().ReverseMap();
            CreateMap<TipoLog, TipoLogViewModel>().ReverseMap();
            CreateMap<TipoConta, TipoContaViewModel>().ReverseMap();            
            CreateMap<Banco, BancoViewModel>().ReverseMap();

            CreateMap<GrupoViewModel, Grupo>();
            CreateMap<GrupoImagemViewModel, Grupo>();
            CreateMap<Grupo, GrupoViewModel>();

          //  CreateMap<CriticaViewModel, Critica>();
            CreateMap<TipoCritica, TipoCriticaViewModel>();
            CreateMap<TipoCriticaImagemViewModel, TipoCritica>();
            CreateMap<Garota, GarotaViewModel>();
            CreateMap<GarotaImagemViewModel, Garota>();
            CreateMap<GarotaCategoria, GarotaCategoriaViewModel>();
            CreateMap<GarotaCategoriaAtende, GarotaCategoriaAtendeViewModel>();
            CreateMap<GarotaCategoriaIdioma, GarotaCategoriaIdiomaViewModel>();
            CreateMap<GarotaCategoriaImagemViewModel, GarotaCategoria>();
            CreateMap<Trabalho, TrabalhoViewModel>();
            CreateMap<Visualizacao, VisualizacaoViewModel>();
            CreateMap<Parametros, ParametrosViewModel>();

            CreateMap<Novidade, NovidadeViewModel>()
            .ForMember(dest => dest.idCategoria, opt => opt.MapFrom(src => src.GarotaCategoria.Categoria.id_categoria))
            .ForMember(dest => dest.descCategoria, opt => opt.MapFrom(src => src.GarotaCategoria.Categoria.desc_categoria));
            CreateMap<NovidadeViewModel, Novidade>();
            CreateMap<NovidadeImagemViewModel, Novidade>();

            CreateMap<Anuncio, AnuncioViewModel>();
            CreateMap<AnuncioImagemViewModel, Anuncio>();

            CreateMap<TipoControleSistema, TipoControleSistemaViewModel>().ReverseMap();
            CreateMap<ControleSistema, ControleSistemaViewModel>().ReverseMap();

            CreateMap<PermissoesSistema, PermissoesSistemaViewModel>().ReverseMap();
            CreateMap<PermissoesInstituicao, PermissoesInstituicaoViewModel>().ReverseMap();
            CreateMap<PermissoesUsuario, PermissoesUsuarioViewModel>().ReverseMap();
            CreateMap<PermissoesPerfil, PermissoesPerfilViewModel>().ReverseMap();


            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            //CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();

            CreateMap<ProdutoImagemViewModel, Produto>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        }
    }
}
