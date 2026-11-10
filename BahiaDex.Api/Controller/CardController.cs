using BahiaDex.Api.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BahiaDex.Api.Controller
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CardController(ICardService service) : ControllerBase
    {
        private ICardService _service = service;
        
          [HttpGet("{id}")]
          public async Task<ActionResult<Card>> Get(Guid id)
          {
              var obj = await _service.Get(id);
              if (obj == null)
                  return NotFound();
              return obj;
          }

          [HttpGet("all")]
          public async Task<ActionResult<List<Card>>> GetAll()
          {
              var res = await _service.GetAll();
              return res;
          }

          [HttpPost()]
          public async Task<ActionResult<Card>> UpSert(Card card)
          {
              if(card.Id == Guid.Empty)
              {
                  var obj = await _service.Insert(card);
                  return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);
              }
              else
              {
                  var obj = await _service.Update(card);
                  return Ok(obj);
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