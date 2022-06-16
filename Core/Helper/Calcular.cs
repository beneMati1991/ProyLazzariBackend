using System;
using static Core.Models.NormalizacionDatosGeofApi.DireccionNormalizada;


namespace Core.Helper
{
    public static class Calcular
    {

        private const int radioTierra = 6371;

        /// <summary>
        /// Método para obtener la distancia en kilometros entre dos ubicaciones según su latitud y longitud
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="destino"></param>
        /// <returns>Distancia en metros</returns>
        public static double DistanciaEntreDosPuntosEnKm(Ubicacion origen, Ubicacion destino)
        {
            double distancia;

            try
            {
                double latitud = (destino.lat - origen.lat) * (Math.PI / 180);
                double longitud = (destino.lon - origen.lon) * (Math.PI / 180);
                double a = Math.Sin(latitud / 2) * Math.Sin(latitud / 2) + Math.Cos(origen.lat * (Math.PI / 180)) * Math.Cos(destino.lat * (Math.PI / 180)) * Math.Sin(longitud / 2) * Math.Sin(longitud / 2);
                double b = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                distancia = (radioTierra * b);
            }
            catch (Exception)
            {
                distancia = -1;
            }

            return Math.Round(distancia, 2);
        }

        /// <summary>
        /// Método para obtener la distancia en metros entre dos ubicaciones según su latitud y longitud
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="destino"></param>
        /// <returns>Distancia en metros</returns>
        public static double DistanciaEntreDosPuntosEnMts(Ubicacion origen, Ubicacion destino)
        {
            double distancia;

            try
            {
                double latitud = (destino.lat - origen.lat) * (Math.PI / 180);
                double longitud = (destino.lon - origen.lon) * (Math.PI / 180);
                double a = Math.Sin(latitud / 2) * Math.Sin(latitud / 2) + Math.Cos(origen.lat * (Math.PI / 180)) * Math.Cos(destino.lat * (Math.PI / 180)) * Math.Sin(longitud / 2) * Math.Sin(longitud / 2);
                double b = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                distancia = (radioTierra * b) * 1000;
            }
            catch (Exception)
            {
                distancia = -1;
            }

            return Math.Round(distancia, 2);
        }
    }

}
