using System;
using System.Net;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected IMapper Mapper { get; }

        protected ILogger Logger { get; }

        public AbstractController(IMapper mapper, ILoggerFactory loggerFactory, string name)
        {
            Mapper = mapper;
            Logger = loggerFactory.CreateLogger(name);
        }

        [NonAction]
        public BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(new ApiError((int)HttpStatusCode.BadRequest, message, Logger));
        }

        [NonAction]
        public NotFoundObjectResult NotFound(string message)
        {
            return NotFound(new ApiError((int)HttpStatusCode.NotFound, message, Logger));
        }

        internal async Task<ActionResult<T_RETURN>> ManageError<T_RETURN, T_OBJECT>(Func<Task<T_OBJECT>> action, bool withReturn = true)
        {
            try
            {
                T_OBJECT obj = await action.Invoke().ConfigureAwait(false);

                if (obj == null)
                {
                    Logger.LogError(typeof(T_OBJECT).Name + " is null");
                    return StatusCode(500, typeof(T_OBJECT).Name + " is null");
                }

                if (withReturn)
                {
                    if (typeof(T_OBJECT).IsPrimitive)
                    {
                        Logger.LogDebug("Success : Return primitive type value");
                        return Ok(obj);
                    }
                    else
                    {
                        Logger.LogDebug("Success : Return mapping object value");
                        return Ok(Mapper.Map<T_RETURN>(obj));
                    }
                }
                else
                {
                    Logger.LogDebug("Success : Return without value");
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        internal async Task<ActionResult<T_OBJECT>> ManageError<T_OBJECT>(Func<Task<T_OBJECT>> action, bool withReturn = true)
        {
            try
            {
                T_OBJECT obj = await action.Invoke().ConfigureAwait(false);

                if (obj == null)
                {
                    Logger.LogError(typeof(T_OBJECT).Name + " is null");
                    return StatusCode(500, typeof(T_OBJECT).Name + " is null");
                }

                if (withReturn)
                {
                    return Ok(obj);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}
