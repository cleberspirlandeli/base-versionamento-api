using DevIO.Api.Controllers.Common;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Controllers.V1
{
    [Obsolete()]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class TesteController : MainController
    {
        public TesteController(INotificador notificador, IUser user) : base(notificador, user)
        {
        }


        [HttpGet]
        public string Valor()
        {
            return "Sou a V1";
        }
    }
}
