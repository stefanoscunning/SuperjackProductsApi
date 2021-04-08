using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperjackProducts.Api.DataAccess;
using SuperjackProducts.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperjackProducts.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class TagsController : ControllerBase
  {
    private ITagService _service;

    public TagsController(ITagService service)
    {
      _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {

      var items = _service.GetAll();

      return Ok(items);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {

      var item = _service.GetById(id);
      return Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Tag item)
    {
      try
      {
        var newitem = _service.Create(item);
        return Ok(newitem);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Tag item)
    {

      try
      {
        // save 
        _service.Update(item);
        return Ok();
      }
      catch (Exception ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      _service.Delete(id);
      return Ok();
    }
  }
}
