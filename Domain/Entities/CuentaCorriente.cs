using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : CuentaBancaria
    {
        public const double SOBREGIRO = -1000;
        public bool ConsignacionInicial = true;
        private const double MINIMOCONSIGNACION = 100000;
        private const double COSTORETIRO = 4000;

        public override void Consignar(double valor,string ciudad)
        {
            if (valor != 0)
            {
                if (this.ConsignacionInicial == true)
                {
                    if (valor >= MINIMOCONSIGNACION)
                    {
                        GenerarMovimiento(valor, "Consignacion");
                    }
                    else
                    {
                        throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
                    }
                }
                else
                {
                    GenerarMovimiento(valor, "Consignacion");

                }
            }
            else
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar la consignacion, la consignacion debe ser mayor a 0");
            }
            


        }

        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            if (nuevoSaldo >= SOBREGIRO)
            {
                GenerarMovimiento(valor,"Retiro");
            }
            else
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar el Retiro, supera el valor de sobregiro permitido");
            }
        }
        public void GenerarMovimiento(double valor, string tipo)
        {
            if (tipo == "Consignacion")
            {
                MovimientoFinanciero consignacion = new MovimientoFinanciero();
                consignacion.ValorConsignacion = valor;
                consignacion.FechaMovimiento = DateTime.Now;
                Saldo += valor;
                this.Movimientos.Add(consignacion);
            }
            else
            if (tipo == "Retiro")
            {
                valor -= COSTORETIRO;
                MovimientoFinanciero movimiento = new MovimientoFinanciero();
                movimiento.ValorRetiro = valor;
                movimiento.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(movimiento);
            }
        }
    }

  



    [Serializable]
    public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
    {
        public CuentaCorrienteRetirarMaximoSobregiroException() { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteRetirarMaximoSobregiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

   
}
