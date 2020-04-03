using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        private const double TOPERETIRO = 20000;
        private bool ConsignacionInicial = true;
        private const double MINIMOCONSIGNACION = 50000;
        private int contadorRetiro=0;
        private const double VALORRETITO = 5000;
        private const double VALORCONSIGNACION = 10000;

        public override void Consignar(double valor,string ciudad)
        {
            if(valor != 0)
            {
                if (this.ConsignacionInicial == true)
                {
                    if (valor >= MINIMOCONSIGNACION)
                    {
                        if (ciudad == this.Ciudad)
                        {
                            GenerarMovimiento(valor, "Consignacion");
                        }
                        else
                        {
                            valor -= VALORCONSIGNACION;//se cobra el valor de la consignacion
                            GenerarMovimiento(valor, "Consignacion");
                        }
                    }
                    else
                    {
                        throw new CuentaCorrienteConsignarException("No es posible realizar la consignacion," +
                            " la primera consignacion debe ser mayor a 50000");
                    }
                }
                else
                {
                    if (ciudad == this.Ciudad)
                    {
                        GenerarMovimiento(valor, "Consignacion");
                    }
                    else
                    {
                        valor -= VALORCONSIGNACION;
                        GenerarMovimiento(valor, "Consignacion");
                    }
                }
            }
            else
            {
                throw new CuentaCorrienteConsignarException("No es posible realizar la consignacion, debe ser mayor que 0");
            }



        }

        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            if(valor != 0)
            {
                if (nuevoSaldo >= TOPERETIRO)
                {
                    if (contadorRetiro < 3)
                    {
                        GenerarMovimiento(valor, "Retiro");
                    }
                    else
                    {
                        valor -= VALORRETITO;
                        GenerarMovimiento(valor, "Retiro");
                    }
                }
                else
                {
                    throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
                }
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro de 0");

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
            if(tipo == "Retiro")
            {
                MovimientoFinanciero consignacion = new MovimientoFinanciero();
                consignacion.ValorConsignacion = valor;
                consignacion.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(consignacion);
            }
        }
        }
    }

    public class CuentaCorrienteConsignarException : Exception
    {
        public CuentaCorrienteConsignarException() { }
        public CuentaCorrienteConsignarException(string message) : base(message) { }
        public CuentaCorrienteConsignarException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteConsignarException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    [Serializable]
    public class CuentaAhorroTopeDeRetiroException : Exception
    {
        public CuentaAhorroTopeDeRetiroException() { }
        public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
        public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaAhorroTopeDeRetiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
