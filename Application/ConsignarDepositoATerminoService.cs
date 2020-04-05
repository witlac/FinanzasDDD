using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;

namespace Application
{
    public class ConsignarDepositoATerminoService
    {
        readonly IUnitOfWork _unitOfWork;

        public ConsignarDepositoATerminoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConsignarDepositoResponse Ejecutar(ConsignarDepositoRequest request)
        {
            var cuenta = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
            if (cuenta != null)
            {
                cuenta.Consignar(request.Valor,request.Ciudad);
                _unitOfWork.Commit();
                return new ConsignarDepositoResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new ConsignarDepositoResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class ConsignarDepositoRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }

        public string Ciudad { get; set; }
    }
    public class ConsignarDepositoResponse
    {
        public string Mensaje { get; set; }
    }
}

