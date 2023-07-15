using ToDo.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IToDoDbContext
    {
        DbSet<CustomerItem> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
