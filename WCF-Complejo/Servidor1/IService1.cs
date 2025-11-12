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
        [OperationContract(Name = "Calcular")]
        RespuestaCalculo Calcular(PeticionCalculo peticion);

        [OperationContract(Name = "CalcularAsync")]
        Task<RespuestaCalculo> CalcularAsync(PeticionCalculo peticion);
    }

    [DataContract]
    public class PeticionCalculo
    {
        [DataMember]
        public string Operacion { get; set; }

        [DataMember]
        public double Numero1 { get; set; }

        [DataMember]
        public double Numero2 { get; set; }
    }

    [DataContract]
    public class RespuestaCalculo
    {
        [DataMember]
        public double Resultado { get; set; }

        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string MensajeError { get; set; }

        [DataMember]
        public string OperacionRealizada { get; set; }
    }
}