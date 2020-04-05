using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories;
using Domain.Entities;
using Domain.Contracts;


namespace Application
{
    public class RetirarDepositoAterminoService
    {
        readonly IUnitOfWork _unitOfWork;
        public RetirarDepositoAterminoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RetirarDepositoResponse Ejecutar(RetirarDepositoRequest request)
        {
            var cuenta = _unitOfWork.DepositoATerminoRepository.FindFirstOrDefault(t => t.Numero == request.NumeroCuenta);
            if (cuenta != null)
            {
                cuenta.Retirar(request.Valor);
                _unitOfWork.Commit();
                return new RetirarDepositoResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new RetirarDepositoResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class RetirarDepositoRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }

    }
    public class RetirarDepositoResponse
    {
        public string Mensaje { get; set; }
    }
}

