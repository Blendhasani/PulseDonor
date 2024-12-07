using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Core
{
    public class DonorDbContext : DbContext
    {
        public DonorDbContext(DbContextOptions<DonorDbContext> options)
           : base(options) { }
    }
}
