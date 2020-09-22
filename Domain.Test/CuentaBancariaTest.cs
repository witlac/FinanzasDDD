using Domain.Entities;

using NUnit.Framework;
using System;

namespace Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignacionTest()
        {
            var cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(100000,"Bogota");
            Assert.AreEqual(90000, cuenta.Saldo);
        }

        [Test]
        public void RetirarTest()
        {
            var cuenta = new CuentaAhorro();
            cuenta.Numero = "11";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Valledupar";
            cuenta.Consignar(100000, "Bogota");
            cuenta.Retirar(50000);
            Assert.AreEqual(40000, cuenta.Saldo);
        }

        [Test]
        public void ConsignarCuentaCorrienteTest()
        { 
            var cuenta = new CuentaCorriente();
            cuenta.Numero = "11";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Ciudad = "Bogota";
            cuenta.Consignar(100000, "valledupar");
            Assert.AreEqual(100000, cuenta.Saldo);
        }

        [Test]
        public void ConsignarCertificadoDepositoTerminoTest()
        {
            var cuenta = new CertificadoDeDepositoATermino();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.FechaDeInicio = DateTime.Now; 
            cuenta.FechaDeTermino = new DateTime(2020, 3, 4);
            cuenta.Consignar(1000000,"Valledupar");
            Assert.AreEqual(1000000, cuenta.Saldo);
        }

        [Test]
        public void RetirarCertificadoDepositoTerminoTest()
        {
            var cuenta = new CertificadoDeDepositoATermino();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.FechaDeInicio = new DateTime(2020, 1, 3);
            cuenta.FechaDeTermino = new DateTime(2020, 1, 4);
            cuenta.Consignar(1000000,"Valledupar");
            cuenta.Retirar(1000000);
            Assert.AreEqual(0, cuenta.Saldo);
        }


    }
}