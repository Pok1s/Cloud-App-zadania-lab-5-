using CloudApp.Api.Data;
using CloudApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
    {
        var list = await _db.Tasks.AsNoTracking().OrderBy(t => t.Id).ToListAsync();
        var dtos = list.Select(MapToReadDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskReadDto>> GetById(int id)
    {
        var entity = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        if (entity == null)
            return NotFound();
        return Ok(MapToReadDto(entity));
    }

    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> Create([FromBody] TaskCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest();

        var entity = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = dto.Description?.Trim(),
            Done = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Tasks.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToReadDto(entity));
    }

    private static TaskReadDto MapToReadDto(TaskItem t) => new()
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        Done = t.Done,
        CreatedAt = t.CreatedAt
    };
}
