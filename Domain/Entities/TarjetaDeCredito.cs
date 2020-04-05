using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TarjetaDeCredito : Entity<int> , IServicioFinanciero
    {
        public double CupoPreaprobado { get; set; }
        public double Saldo { get; protected set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }

        public void Consignar(double valor, string ciudad)
        {
            throw new NotImplementedException();

        }

        public void Retirar(double valor)
        {
            throw new NotImplementedException();
        }
    }
}
