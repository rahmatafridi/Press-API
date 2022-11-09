using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Framework.Attribute;

namespace PressDot.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthenticatedController : BaseController
    {
    }
}
