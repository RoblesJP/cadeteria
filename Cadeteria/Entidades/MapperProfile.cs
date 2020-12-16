using AutoMapper;
using Cadeteria.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Entidades
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            CreateMap<Pedido, RegistrarPedidoViewModel>().ReverseMap();
            CreateMap<Pedido, ModificarPedidoViewModel>()
                .ForMember
                (
                    dest => dest.IdCliente, origen => origen.MapFrom(src => src.Cliente.Id)
                )
                .ForMember
                (
                    dest => dest.IdCadete, origen => origen.MapFrom(src => src.Cadete.Id)
                )
                .ReverseMap();
            CreateMap<Pedido, PedidoViewModel>()
                .ForMember
                (
                    dest => dest.NombreDeCliente, origen => origen.MapFrom(src => src.Cliente.Nombre)
                )
                .ForMember
                (
                    dest => dest.NombreDeCadete, origen => origen.MapFrom(src => src.Cadete.Nombre)
                )
                .ForMember
                (
                    dest => dest.Precio, origen => origen.MapFrom(src => src.GetPrecio())
                )
                .ReverseMap();

            CreateMap<Cliente, SelectListItem>()
                .ForMember
                (
                    dest => dest.Text, origen => origen.MapFrom(src => src.Nombre)
                )
                .ForMember
                (
                    dest => dest.Value, origen => origen.MapFrom(src => src.Id)
                );

            CreateMap<Cadete, SelectListItem>()
                .ForMember
                (
                    dest => dest.Text, origen => origen.MapFrom(src => src.Nombre + " | " + src.Vehiculo)
                )
                .ForMember
                (
                    dest => dest.Value, origen => origen.MapFrom(src => src.Id)
                );
        }
    }
}
