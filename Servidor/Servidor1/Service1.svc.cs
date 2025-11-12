using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel; // Necesario para FaultException
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks; // Necesario para Task.FromResult

namespace Servidor1
{
    // ESTA ES LA CLASE QUE IMPLEMENTA LA INTERFAZ
    public class Service1 : IService1
    {
        // Implementación Síncrona
        public double Calcular(string operacion, double n1, double n2)
        {
            double resultado = 0;

            switch (operacion.ToLower())
            {
                case "sumar":
                case "+":
                    resultado = n1 + n2;
                    break;
                case "restar":
                case "-":
                    resultado = n1 - n2;
                    break;
                case "multiplicar":
                case "*":
                    resultado = n1 * n2;
                    break;
                case "dividir":
                case "/":
                    if (n2 == 0)
                    {
                        throw new FaultException("Error: No se puede dividir por cero.");
                    }
                    resultado = n1 / n2;
                    break;
                default:
                    throw new FaultException("Error: Operación no reconocida...");
            }

            return resultado;
        }

        // Implementación Asíncrona
        public Task<double> CalcularAsync(string operacion, double n1, double n2)
        {
            double resultado = 0;

            switch (operacion.ToLower())
            {
                case "sumar":
                case "+":
                    resultado = n1 + n2;
                    break;
                case "restar":
                case "-":
                    resultado = n1 - n2;
                    break;
                case "multiplicar":
                case "*":
                    resultado = n1 * n2;
                    break;
                case "dividir":
                case "/":
                    if (n2 == 0)
                    {
                        throw new FaultException("Error: No se puede dividir por cero.");
                    }
                    resultado = n1 / n2;
                    break;
                default:
                    throw new FaultException("Error: Operación no reconocida...");
            }

            // Devolvemos el resultado envuelto en una Tarea completada
            return Task.FromResult(resultado);
        }
    }
}