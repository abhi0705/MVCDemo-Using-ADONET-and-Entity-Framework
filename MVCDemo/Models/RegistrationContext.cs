using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVCDemo.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace MVCDemo.Models
{
    public class RegistrationContext : DbContext // entity frame work
    {

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Country> Countries { get; set; }


    }
}
