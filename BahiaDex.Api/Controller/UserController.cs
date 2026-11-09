using BahiaDex.Api.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BahiaDex.Api.Controller
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService service) : ControllerBase
    {
        private IUserService _service = service;
        
          [HttpGet("{id}")]
          public async Task<ActionResult<User>> Get(string id)
          {
              var res = await _service.Get(id);
              if (res == null)
                  return NotFound();
              return res;
          }

          [HttpPost()]
          public async Task<ActionResult<User>> UpSert(User obj)
          {
              if(obj.IdGoogleAcc == "")
              {
                  var res = await _service.Insert(obj);
                  return CreatedAtAction(nameof(Get), new { id = res.IdGoogleAcc }, res);
              }
              else
              {
                  var res = await _service.Update(obj);
                  return Ok(res);
              }
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult> Delete(string id)
          {
              var obj = await _service.Get(id);
              if (obj == null)
                  return NotFound();
              await _service.Delete(id);
              return NoContent();
          }
    }
}