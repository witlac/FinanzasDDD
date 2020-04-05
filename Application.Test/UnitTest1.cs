using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Application.Test
{
    public class Tests
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

            _context = new BancoContext(optionsInMemory);
        }

        [Test]
        public void CrearCuentaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1111", Nombre = "aaaaa", TipoCuenta = "Corriente", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito la cuenta 1111.", response.Mensaje);
        }

        [Test]
        public void ConsignarCuentaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1112", Nombre = "aaaaa", TipoCuenta = "Corriente", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignacion = new ConsignarRequest { NumeroCuenta = "1112", Ciudad = "Valledupar", Valor = 50000 };
            ConsignarService _serviceConsignacion = new ConsignarService(new UnitOfWork(_context));
            var responseConsignacion = _serviceConsignacion.Ejecutar(requestConsignacion);
            Assert.AreEqual($"Su Nuevo saldo es 50000.", responseConsignacion.Mensaje);
        }

        [Test]
        public void RetirarCuentaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1113", Nombre = "aaaaa", TipoCuenta = "Corriente", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignacion = new ConsignarRequest { NumeroCuenta = "1113", Ciudad = "Valledupar", Valor = 100000 };
            ConsignarService _serviceConsignacion = new ConsignarService(new UnitOfWork(_context));
            var responseConsignacion = _serviceConsignacion.Ejecutar(requestConsignacion);

            var requestRetirar = new RetirarRequest { NumeroCuenta = "1113", Valor = 50000 };
            RetirarService _serviceRetirar = new RetirarService(new UnitOfWork(_context));
            var responseRetirar = _serviceRetirar.Ejecutar(requestRetirar);
            Assert.AreEqual($"Su Nuevo saldo es 50000.", responseRetirar.Mensaje);
        }

        [Test]
        public void CrearCuentaAhorroTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "2112", Nombre = "aaaaa", TipoCuenta = "Corriente", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito la cuenta 2112.", response.Mensaje);
        }

        [Test]
        public void ConsignarCuentaAhorroTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1222", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignacion = new ConsignarRequest { NumeroCuenta = "1222", Ciudad = "Valledupar", Valor = 50000 };
            ConsignarService _serviceConsignacion = new ConsignarService(new UnitOfWork(_context));
            var responseConsignacion = _serviceConsignacion.Ejecutar(requestConsignacion);
            Assert.AreEqual($"Su Nuevo saldo es 50000.", responseConsignacion.Mensaje);
        }

        [Test]
        public void ConsignarCuentaAhorroOtraCiudadTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1122", Nombre = "aaaaa", TipoCuenta = "Ahorro", Ciudad = "Valledupar" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignacion = new ConsignarRequest { NumeroCuenta = "1122", Ciudad = "Valleduapar", Valor = 50000 };
            ConsignarService _serviceConsignacion = new ConsignarService(new UnitOfWork(_context));
            var responseConsignacion = _serviceConsignacion.Ejecutar(requestConsignacion);
            Assert.AreEqual($"Su Nuevo saldo es 40000.", responseConsignacion.Mensaje);
        }

        [Test]
        public void CrearDepositoATerminoTest()
        {
            var request = new CrearDepositoATerminoRequest { Numero = "1111",Nombre = "Cristian Mejia", FechaDeInicio = DateTime.Now, FechaDeTermino = new DateTime(2020, 3, 16), TasaInteres = 0.02 };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se creó con exito el deposito 1111.", response.Mensaje);
        }

        [Test]
        public void ConsignarDepositoTest()
        {
            var request = new CrearDepositoATerminoRequest { Numero = "1112", Nombre = "Cristian Mejia", FechaDeInicio = DateTime.Now, FechaDeTermino = new DateTime(2020, 3, 16), TasaInteres = 0.02 };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignar = new ConsignarDepositoRequest { NumeroCuenta = "1112",Valor=2300000,Ciudad="valledupar" };
            ConsignarDepositoATerminoService _serviceConsignar = new ConsignarDepositoATerminoService(new UnitOfWork(_context));
            var responseConsignar = _serviceConsignar.Ejecutar(requestConsignar);
            Assert.AreEqual($"Su Nuevo saldo es 2300000.", responseConsignar.Mensaje);
        }

        [Test]
        public void ConsignarDepositoMenorTest()
        {
            var request = new CrearDepositoATerminoRequest { Numero = "2113", Nombre = "Cristian Mejia", FechaDeInicio = DateTime.Now, FechaDeTermino = new DateTime(2020, 3, 16), TasaInteres = 0.02 };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignar = new ConsignarDepositoRequest { NumeroCuenta = "2113", Valor = 900000, Ciudad = "valledupar" };
            ConsignarDepositoATerminoService _serviceConsignar = new ConsignarDepositoATerminoService(new UnitOfWork(_context));
            Exception responseConsignar = Assert.Throws<Exception>(() => _serviceConsignar.Ejecutar(requestConsignar));
            Assert.AreEqual($"No es posible realizar la consignacion, la primera consignacion debe ser mayor a 1 millon", responseConsignar.Message);
        }

        [Test]
        public void RetirarDepositoTest()
        {
            var request = new CrearDepositoATerminoRequest { Numero = "2222", Nombre = "Cristian Mejia", FechaDeInicio = DateTime.Now, FechaDeTermino = new DateTime(2020, 3, 16), TasaInteres = 0.02 };
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);

            var requestConsignar = new ConsignarDepositoRequest { NumeroCuenta = "2222", Valor = 2300000, Ciudad = "valledupar" };
            ConsignarDepositoATerminoService _serviceConsignar = new ConsignarDepositoATerminoService(new UnitOfWork(_context));
            var responseConsignar = _serviceConsignar.Ejecutar(requestConsignar);
            Assert.AreEqual($"Su Nuevo saldo es 2300000.", responseConsignar.Mensaje);

            var requestRetirar = new RetirarDepositoRequest { NumeroCuenta = "2222", Valor = 2300000};
            RetirarDepositoAterminoService _serviceRetirar = new RetirarDepositoAterminoService(new UnitOfWork(_context));
            var responseRetirar = _serviceRetirar.Ejecutar(requestRetirar);
            Assert.AreEqual($"Su Nuevo saldo es 0.", responseRetirar.Mensaje);


        }




    }
}