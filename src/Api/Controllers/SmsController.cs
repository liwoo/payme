using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.SMS.Commands;
using Application.SMS.DTOs;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Api.Models;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;
        public SmsController(ILogger<SmsController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        // GET api/sms
        [HttpGet("")]
        public ActionResult<IEnumerable<SMS>> GetSMSs()
        {
            return new List<SMS>
            {
                new SMS()
                    {
                        Id = 1,
                        Phone = "0888123456",
                        Message = "Some Message"
                    }
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
        async public Task<string> PostSMS(SMSBodyDto body)
        {
            return await this.mediator.Send(new ProcessSMS(body));
        }

    }
}