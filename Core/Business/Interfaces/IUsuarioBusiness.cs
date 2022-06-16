using Core.Models.DTOs;
using Core.Models.DTOs.UsuarioDTO;
using Core.Models.DTOs.UsuarioDTO.Comercio;
using Core.Models.DTOs.UsuarioDTO.Consumidor;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IUsuarioBusiness
    {
        Task<UsuarioDto> RegistrarComercio (RegistroComercioDto user);
        Task<UsuarioDto> RegistrarConsumidor (RegistroConsumidorDto user);
        Task<string> LoginComercio(UsuarioLoginDto userDto);
        Task<string> LoginConsumidor(UsuarioLoginDto userDto);
        bool ComercioExistente(string email, string nombreUsuario, string cuit);
        bool ConsumidorExistente(string email, string nombreUsuario);
        Task<UsuarioComercioDto> GetPerfilComercio(int idUsuario, int idComercio);
        Task<UsuarioConsumidorDto> GetPerfilConsumidor(int idUsuario, int idConsumidor);
        Task<UsuarioComercioDto> EditarPerfilComercio(UsuarioComercioDto comercio, int idUsuario, int idComercio);
        Task<UsuarioConsumidorDto> EditarPerfilConsumidor(UsuarioConsumidorDto consumidor, int idUsuario, int idConsumidor);

        Task<bool?> DeleteComercio(int idUsuario, int idComercio);
        Task<bool?> DeleteConsumidor(int idUsuario, int idConsumidor);
    }
}
