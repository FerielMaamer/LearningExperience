using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]

        public async Task<ActionResult<IEnumerable<IndividualTask>>> GetTasks()
        {
            var tasks = await _context.IndividualTask.ToListAsync();
            return Ok(tasks);

        }


        // GET: api/tasks/{userId}
        [HttpGet("{userId}")]
        
        public async Task<ActionResult<IEnumerable<IndividualTask>>> GetTasks(int userId)
        {
            var tasks = await _context.IndividualTask.Where(t => t.UserId == userId).ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);

        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IndividualTask>> GetTask(int id)
        {
            var task = await _context.IndividualTask.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/tasks
        
        [HttpPost]
        public async Task<ActionResult<IndividualTask>> CreateTask(IndividualTask task)
        {
            _context.IndividualTask.Add(task);
            await _context.SaveChangesAsync();
            Console.WriteLine(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId}, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, IndividualTask task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.IndividualTask.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.IndividualTask.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.IndividualTask.Any(e => e.TaskId == id);
        }
    }
}
