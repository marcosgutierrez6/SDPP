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
    [ServiceContract]
    public interface IService1
    {
        // Le decimos explícitamente que esta operación se llama "Calcular"
        [OperationContract(Name = "Calcular")]
        double Calcular(string operacion, double n1, double n2);

        // Le decimos explícitamente que esta otra se llama "CalcularAsync"
        [OperationContract(Name = "CalcularAsync")]
        Task<double> CalcularAsync(string operacion, double n1, double n2);
    }
}