using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LoginRegister.Models
{
    public class OurDbContext:DbContext
    {
        public OurDbContext()
            : base("DESKTOP-DIQEHL2")
        {

        }
        public DbSet<UserAccount> userAccount { get; set; }

    }
}