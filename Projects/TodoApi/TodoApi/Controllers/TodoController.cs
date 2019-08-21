using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if(_context.Jobs.Count() == 0)
            { 
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Jobs.Add(new Rider { FirstName = "Benjamin" });
                _context.Jobs.Add(new Rider { LastName = "J" });
                _context.Jobs.Add(new Rider { PhoneNumber= 0460000000 });
                _context.Jobs.Add(new Rider { Email = "defalt email" });
                _context.Jobs.Add(new Rider { StartDate = "today" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rider>>> GetTodoItems()
        {
            return await _context.Jobs.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rider>> GetTodoItem(long id)
        {
            var todoItem = await _context.Jobs.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Rider>> PostTodoItem(Rider item)
        {
            _context.Jobs.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, Rider item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Jobs.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
