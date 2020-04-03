using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class CuentaBancaria : Entity<int>, IServicioFinanciero
    {
        public CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public double Saldo { get; protected set; }
        public string Ciudad { get; set; }

        public abstract void Consignar(double valor,string ciudad);
        
        public abstract void Retirar(double valor);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, double valor, string ciudad)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor,ciudad);
        }


    }
}
