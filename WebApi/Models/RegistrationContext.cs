using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Models
{
    public class RegistrationContext: DbContext 
    {
        public DbSet<Registration> Registrations { get; set; }
    }
}