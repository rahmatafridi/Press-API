using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PressDot.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Base Controller default constructor
        /// </summary>
        public BaseController()
        {
            
        }

        /// <summary>
        /// Get inner exception message.
        /// </summary>
        /// <param name="ex">Object of Exception.</param>
        /// <returns>Message returned by exception.</returns>
        protected string GetExceptionMessage(Exception ex)
        {
            var message = ex.InnerException != null ? GetExceptionMessage(ex.InnerException) : ex.Message;
            return message;
        }
    }
}
