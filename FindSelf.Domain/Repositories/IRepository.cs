using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
