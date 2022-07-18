using IPass.Domain.CommonDomain.Entities;
using Patika.Shared.Interfaces;
using System;

namespace IPass.Domain.PasswordDomain.Repositories
{
    public interface IPinCodeRepository : IGenericRepository<PinCode, Guid> 
    {

    }
}
