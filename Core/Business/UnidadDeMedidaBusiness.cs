using AutoMapper;
using Core.Business.Interfaces;
using Core.Models.DTOs.UnidadDTO;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public class UnidadDeMedidaBusiness : IUnidadDeMedidaBusiness
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UnidadDeMedidaBusiness(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnidadesDtoGetAllResponse>> GetAllUnidades()
        {
            var unidades = await _repository.GetAll<UnidadDeMedida>();
            var unidadesDTO = new List<UnidadesDtoGetAllResponse>();

            foreach (var unidad in unidades)
            {
                var unidadResponse = _mapper.Map<UnidadesDtoGetAllResponse>(unidad);
                unidadesDTO.Add(unidadResponse);
            }

            return unidadesDTO;
        }
    }
}
