using Core.Business.Interfaces;
using Core.Helper;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public class ConsumidorBusiness : IConsumidorBusiness
    {
        // TODO: Servicios
        private readonly IRepository _repository;

        public ConsumidorBusiness(IRepository repository)
        {

            _repository = repository;
        }

        public async Task<bool> EsOwner(int usuarioId, int consumidorId) 
        { 
            var consumidor = await _repository.GetById<Consumidor>(consumidorId);
            if (consumidor is null) throw new NullReferenceException(Constantes.ConsumidorInexistente);
            return consumidor.UsuarioId.Equals(usuarioId);
        }
    }
}
