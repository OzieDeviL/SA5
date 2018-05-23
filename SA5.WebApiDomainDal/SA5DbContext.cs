using Microsoft.EntityFrameworkCore;
using SA5.WebApiDomainDal.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA5.WebApiDomainDal
{
    public class SA5DbContext : DbContext
    {
        public SA5DbContext(DbContextOptions<SA5DbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
