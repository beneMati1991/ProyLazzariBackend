using AutoMapper;
using Core.Models.DTOs;
using Core.Models.DTOs.UnidadDTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
    public class ApiMappings : Profile
    {
        public ApiMappings()
        {
            CreateMap<Producto, ProductoDtoGetAllResponse>().ReverseMap();
            CreateMap<Producto, ProductoDtoAlta>().ReverseMap();
            CreateMap<Producto, ProductoDtoEditar>().ReverseMap();
            CreateMap<Producto, ProductoDtoGetPorComercio>().ReverseMap();
            CreateMap<UnidadDeMedida, UnidadesDtoGetAllResponse>().ReverseMap();
        }
    }
}
