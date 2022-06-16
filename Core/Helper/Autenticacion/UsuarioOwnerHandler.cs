using Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;


namespace Core.Helper.Autenticacion
{
    public class UsuarioOwnerHandler
    {

        public static ClaimsUsuario GetClaimsUsuario(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var stringSplit = token.Split(' ');

            var Token = handler.ReadJwtToken(stringSplit[0]);

            var claimsUserId = Token.Claims.Where(x => x.Type == "nameid").FirstOrDefault();

            var rol = Token.Claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            var email = Token.Claims.Where(x => x.Type == "email").FirstOrDefault().Value;

            var nombre = Token.Claims.Where(x => x.Type == "unique_name").FirstOrDefault().Value;

            var CID = Token.Claims.Where(x => x.Type == "CID").FirstOrDefault().Value;

            var id = int.Parse(claimsUserId.Value);

            return new ClaimsUsuario
            {
                    UsuarioId = id,
                    Rol = rol,
                    Email = email,
                    NombreUsuario = nombre,
                    CID = int.Parse(CID)
            };
        }

        public static bool EsComercioOwner(string token, Comercio comercio)
        {
            var handler = new JwtSecurityTokenHandler();

            var stringSplit = token.Split(' ');

            var Token = handler.ReadJwtToken(stringSplit[0]);

            var claimsUserId = Token.Claims.Where(x => x.Type == "nameid").FirstOrDefault();

            var userRole = Token.Claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            var id = int.Parse(claimsUserId.Value);

            return (id.Equals(comercio.UsuarioId));
        }


        public static bool EsOwnerProducto(string token, Producto producto) 
        {
            var handler = new JwtSecurityTokenHandler();

            var stringSplit = token.Split(' ');

            var Token = handler.ReadJwtToken(stringSplit[0]);

            var claimsUserId = Token.Claims.Where(x => x.Type == "nameid").FirstOrDefault();

            var userRole = Token.Claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            var id = int.Parse(claimsUserId.Value);

            return (userRole == Enum.GetName(ERoles.Comerciante) &&
                id == producto.Comercio.Usuario.Id);
        }

        public static bool EsComerciante(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var stringSplit = token.Split(' ');

            var Token = handler.ReadJwtToken(stringSplit[0]);

            var userRole = Token.Claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            return (userRole == Enum.GetName(ERoles.Comerciante));
        }

        public static bool EsAdmin(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var stringSplit = token.Split(' ');

            var Token = handler.ReadJwtToken(stringSplit[0]);

            var userRole = Token.Claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            return (userRole == Enum.GetName(ERoles.Administrador));
        }




    }
}
