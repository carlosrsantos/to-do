using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpGet("/")]
    public IActionResult Get([FromServices] AppDbContext context)
    => Ok(context.Todos.ToList());
    

    [HttpGet("/{id:int}")]
    public IActionResult GetById(
      [FromRoute] int id,
      [FromServices] AppDbContext context)
    {
      var todo = context.Todos.FirstOrDefault(x => x.Id == id);
      if(todo is null)
        return NotFound();
      
      return Ok(todo);
      
    }

    [HttpPost("/")]
    public IActionResult Post(
      [FromBody] TodoModel todo,
      [FromServices] AppDbContext context
    )
    {
      context.Todos.Add(todo);
      context.SaveChanges();
      return Created($"/{todo.Id}", todo);
    }

    [HttpPut("/{id:int}")]
    public IActionResult Put(
      [FromRoute] int id,
      [FromBody] TodoModel oldTodo,
      [FromServices] AppDbContext context
    )    
    {
      var todo = context.Todos.FirstOrDefault(x => x.Id == id);
      if(todo == null)
        return NotFound();
      todo.Title = oldTodo.Title;
      todo.Done = oldTodo.Done;
      context.SaveChanges();
      return Ok(todo);
    }

    [HttpDelete("/{id:int}")]
    public IActionResult Delete(
      [FromRoute] int id,
      [FromServices] AppDbContext context
    )    
    {
      var todo = context.Todos.FirstOrDefault(x => x.Id == id);
      if(todo == null)
        return NotFound();
      context.Todos.Remove(todo);
      context.SaveChanges();
      return Ok(todo);
    }

  }  
}