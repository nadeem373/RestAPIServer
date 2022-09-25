using RestAPIServer.Models;
using RestAPIServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestAPIServer.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {

        // GET api/customer
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UtilityService.ReadFromDataStore());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST api/customer
        public HttpResponseMessage Post([FromBody]List<Customer> request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = new
                    {
                        message = "The request is invalid.",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, error);
                }
                UtilityService.WriteInDataStore(request);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, true);
        }


        [HttpGet]
        [Route("Generatedata")]
        public HttpResponseMessage GenerateData()
        {
            try
            {
                var result = UtilityService.GenerateCustomer(2);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
