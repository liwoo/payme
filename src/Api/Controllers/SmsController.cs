using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
//using Api.Models;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        public SmsController()
        {
        }

        // GET api/sms
        [HttpGet("")]
        public ActionResult<IEnumerable<SMS>> GetSMSs()
        {
            return new List<SMS>
            {
                new SMS("08123321", "Hey there!"),
            };
        }

        // GET api/sms/5
        [HttpGet("{id}")]
        public ActionResult<SMS> GetSMSById(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/sms
        [HttpPost("")]
        public void PostSMS(SMS value)
        {
            throw new NotImplementedException();
        }

    }
}