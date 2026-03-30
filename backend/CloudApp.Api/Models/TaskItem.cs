namespace CloudApp.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Done { get; set; }
    public DateTime CreatedAt { get; set; }
}
