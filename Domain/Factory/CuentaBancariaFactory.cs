using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Factories
{
    public class CuentaBancariaFactory
    {
        public CuentaBancaria CrearCuentaBancaria(string tipoCuenta)
        {
            CuentaBancaria cuenta;
            if (tipoCuenta.Equals("corriente"))
            {
                cuenta = new CuentaCorriente();
                return cuenta;

            }
            else
            {
                cuenta = new CuentaAhorro();
                return cuenta;
            }
        }
    }
}
