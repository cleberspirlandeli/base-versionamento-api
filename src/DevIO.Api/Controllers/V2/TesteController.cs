using DevIO.Api.Controllers.Common;
using DevIO.Business.Intefaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class TesteController : MainController
    {

        private readonly ILogger _logger;

        public TesteController(INotificador notificador, IUser user, ILogger<TesteController> logger) : base(notificador, user)
        {
            _logger = logger;
        }


        [HttpGet]
        public string Valor()
        {
            //throw new Exception("Error");

            try
            {
                var i = 0;
                var result = 42 / i;
            }
            catch (DivideByZeroException e)
            {
                e.Ship(HttpContext);
            }


            _logger.LogDebug("Log de Debug");
            _logger.LogInformation("Log de Informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de problema Critico");


            return "Sou a V2";
        }
    }
}
