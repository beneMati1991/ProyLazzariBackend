using Core.Models.DTOs.UnidadDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IUnidadDeMedidaBusiness
    {
        Task<IEnumerable<UnidadesDtoGetAllResponse>> GetAllUnidades();
    }
}
