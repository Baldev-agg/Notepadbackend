using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notepad.Api.Data;
using Notepad.Api.Models;
using System.Diagnostics.Contracts;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Savenotes(Note note)
    {
        note.CreatedAt = DateTime.Now;
        _context.Notes.Add(note);
        _context.SaveChanges();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes(int userId) 
    {
        return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
    }

    //Edit Notes
    [HttpPut("{Id}")]
    public async Task<IActionResult> EditNotes(int id, Note note)
    {
        if (id != note.Id) return BadRequest();
        _context.Entry(note).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    // Delete Notes
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}
