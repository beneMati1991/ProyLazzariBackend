using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IConsumidorBusiness
    {
        Task<bool> EsOwner(int usuarioId, int consumidorId);
    }
}
