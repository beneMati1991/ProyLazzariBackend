using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validator
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.Nombre).NotEmpty()
                .Length(0, 255);
            RuleFor(p => p.Marca).NotEmpty()
                .Length(0, 255);
            RuleFor(p => p.Precio).NotEmpty();
            RuleFor(p => p.Detalle)
                .Length(0, 255);
            RuleFor(p => p.Cantidad).NotEmpty();
            RuleFor(p => p.Imagen)
                .Length(0, 255);
            RuleFor(p => p.UnidadMedidaId).NotEmpty();
            RuleFor(p => p.ComercioId).NotEmpty();
        }
    }
}
