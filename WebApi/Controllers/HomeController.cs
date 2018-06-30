using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HomeController : ApiController
    {
        RegistrationContext context;

        public HomeController()
        {
            context = new RegistrationContext();

        }

        public List<Registration> Get()
        {
            return context.Registrations.ToList();
        }

        public Registration Get(int id)
        {
            return context.Registrations.SingleOrDefault(x => x.ID == id);
        }

        public HttpResponseMessage Post(Registration reg)
        {
            context.Registrations.Add(reg);
            context.SaveChanges();
            var message = Request.CreateResponse(HttpStatusCode.Created, reg);
            message.Headers.Location = new Uri(Request.RequestUri + reg.ID.ToString());
            return message;
        }
        public void Put(Registration reg)
        {
            var preObj = context.Registrations.SingleOrDefault(x => x.ID == reg.ID);
            context.Registrations.Remove(preObj);
            context.Registrations.Add(reg);
            context.SaveChanges();
        }

        [Route("api/Registration/Get")]
        public void Delete([FromUri]int id)
        {
            var preObj = context.Registrations.SingleOrDefault(x => x.ID == id);
            context.Registrations.Remove(preObj);
            context.SaveChanges();
        }

    }


}
