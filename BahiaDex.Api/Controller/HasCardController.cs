using BahiaDex.Api.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BahiaDex.Api.Controller
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class HasCardController(IHasCardService service) : ControllerBase
    {
        private IHasCardService _service = service;
        
          [HttpGet("{id}")]
          public async Task<ActionResult<HasCard>> Get(Guid id)
          {
              var res = await _service.Get(id);
              if (res == null)
                  return NotFound();
              return res;
          }

          [HttpGet("all")]
          public async Task<ActionResult<List<HasCard>>> GetAll()
          {
              var res = await _service.GetAll();
              return res;
          }

          [HttpPost()]
          public async Task<ActionResult<HasCard>> UpSert(HasCard obj)
          {
              if(obj.Id == Guid.Empty)
              {
                  var res = await _service.Insert(obj);
                  return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
              }
              else
              {
                  var res = await _service.Update(obj);
                  return Ok(res);
              }
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult> Delete(Guid id)
          {
              var obj = await _service.Get(id);
              if (obj == null)
                  return NotFound();
              await _service.Delete(id);
              return NoContent();
          }
    }
}