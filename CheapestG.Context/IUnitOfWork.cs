using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Context
{
    public interface IUnitOfWork
    {
        CheapestGContext dbContext { get; }
    }
}
