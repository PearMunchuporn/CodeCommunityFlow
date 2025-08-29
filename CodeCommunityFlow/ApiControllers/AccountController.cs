using CodeCommunityFlow.ServiceLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeCommunityFlow.ApiControllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
       
           IUserService userService;
           IContactService contactService;

            public AccountController(IUserService ius, IContactService iconser)
            {
                this.userService = ius;
                this.contactService = iconser;
            }

            [HttpGet]
            [Route("check-email")]//change name
            public IHttpActionResult CheckEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is required.");

                var user = userService.GetUserByEmail(email);
                return Ok(new { exists = user != null });
             }


            [HttpGet]
            [Route("checkWorkemail")]//change name
            public IHttpActionResult checkWorkemail(string workEmail)
            {
                if (string.IsNullOrWhiteSpace(workEmail))
                    return BadRequest("Work email is required.");

                var user = contactService.GetContactorByEmail(workEmail);
                return Ok(new { exists = user != null });
            }

    }
}
