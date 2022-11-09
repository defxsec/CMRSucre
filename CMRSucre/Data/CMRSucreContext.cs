using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMRSucre.Models;

namespace CMRSucre.Data
{
    public class CMRSucreContext : DbContext
    {
        public CMRSucreContext (DbContextOptions<CMRSucreContext> options)
            : base(options)
        {
        }

        public DbSet<CMRSucre.Models.Socio> Socios { get; set; } = default!;
        public DbSet<CMRSucre.Models.Certificado> Certificados { get; set; } = default!;
        public DbSet<CMRSucre.Models.Config> Configs { get; set; } = default!;
    }
}
