using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base;

namespace Domain.Contracts
{
    public interface IGenericFactory<T> where T : BaseEntity
    {
        T CreateEntity(String tipo);
      
    }
}
