using System;
using Helsingborg.Models;
using Microsoft.EntityFrameworkCore;

namespace Helsingborg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<DataCustomer> DataCust { get; set; }
    }
}

