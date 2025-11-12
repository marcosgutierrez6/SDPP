using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Servidor1
{
    public class Service1 : IService1
    {
        public RespuestaCalculo Calcular(PeticionCalculo peticion)
        {
            return RealizarCalculoLogica(peticion);
        }

        public Task<RespuestaCalculo> CalcularAsync(PeticionCalculo peticion)
        {
            RespuestaCalculo respuesta = RealizarCalculoLogica(peticion);
            return Task.FromResult(respuesta);
        }

        private RespuestaCalculo RealizarCalculoLogica(PeticionCalculo peticion)
        {
            var respuesta = new RespuestaCalculo();

            try
            {
                double resultado = 0;
                string simboloOperacion = "";

                if (peticion == null || string.IsNullOrWhiteSpace(peticion.Operacion))
                {
                    respuesta.Exito = false;
                    respuesta.MensajeError = "Petición inválida o sin operación.";
                    return respuesta;
                }

                switch (peticion.Operacion.ToLower())
                {
                    case "sumar":
                    case "+":
                        resultado = peticion.Numero1 + peticion.Numero2;
                        simboloOperacion = "+";
                        break;
                    case "restar":
                    case "-":
                        resultado = peticion.Numero1 - peticion.Numero2;
                        simboloOperacion = "-";
                        break;
                    case "multiplicar":
                    case "*":
                        resultado = peticion.Numero1 * peticion.Numero2;
                        simboloOperacion = "*";
                        break;
                    case "dividir":
                    case "/":
                        if (peticion.Numero2 == 0)
                        {
                            respuesta.Exito = false;
                            respuesta.MensajeError = "Error: No se puede dividir por cero.";
                            respuesta.OperacionRealizada = $"{peticion.Numero1} / {peticion.Numero2}";
                            return respuesta;
                        }
                        resultado = peticion.Numero1 / peticion.Numero2;
                        simboloOperacion = "/";
                        break;
                    default:
                        respuesta.Exito = false;
                        respuesta.MensajeError = "Error: Operación no reconocida.";
                        return respuesta;
                }

                respuesta.Exito = true;
                respuesta.Resultado = resultado;
                respuesta.OperacionRealizada = $"{peticion.Numero1} {simboloOperacion} {peticion.Numero2}";
                respuesta.MensajeError = null;
            }
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.MensajeError = $"Error inesperado en el servidor: {ex.Message}";
            }

            return respuesta;
        }
    }
}