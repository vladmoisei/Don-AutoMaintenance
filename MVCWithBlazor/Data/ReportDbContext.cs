using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Data
{
    public class ReportDbContext : IdentityDbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options)
            : base(options)
        { }

        public DbSet<PlcModel> Plcs { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<UtilajModel> UtilajModels { get; set; }
        public DbSet<ProblemaModel> ProblemaModels { get; set; }
        public DbSet<MVCWithBlazor.Models.ResponsabilModel> ResponsabilModel { get; set; }
    }
}
