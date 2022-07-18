﻿using IPass.Domain.PasswordDomain.Entities;
using Patika.Shared.Interfaces;
using System;

namespace IPass.Domain.PasswordDomain.Repositories
{
    public interface IMemoryTypeRepository : IGenericRepository<MemoryType, Guid> 
    {

    }
}