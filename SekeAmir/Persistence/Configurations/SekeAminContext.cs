using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class SekeAminContext(DbContextOptions<SekeAminContext> options):DbContext(options)
    {
    }
}
