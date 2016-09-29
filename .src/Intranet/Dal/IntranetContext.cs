using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Model;

namespace Intranet.Dal
{
    public class IntranetContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
    }

}
