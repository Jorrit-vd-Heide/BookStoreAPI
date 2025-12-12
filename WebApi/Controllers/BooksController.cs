using BookStoreApi.Application.DTOs;
using BookStoreApi.Application.Services;
using BookStoreApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreApi.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    // Declare a private field for the service
    private readonly BookService _bookService;
    private readonly ILogger<BooksController> _logger;

    // Constructor to receive the injected BookService
    public BooksController(BookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        _logger.LogInformation("Returned {Count} books", books.Count());
        return Ok(books);
    }
        

    // GET: api/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> Get(int id)
    {
        var book = await _bookService.GetAsync(id);

        if (book is null) { 
        
            _logger.LogWarning("Book with ID {Id} not found", id);
            return NotFound();
        }
         return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult> Create(CreatedBookDto dto)
    {
        _logger.LogInformation("Request: Create new book {@Book}", dto);
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Year = dto.Year
        };

        await _bookService.AddAsync(book);
        _logger.LogInformation("Book created with ID {Id}", book.Id);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreatedBookDto dto)
    {
        _logger.LogInformation("Request: Update book with ID {Id}", id);
        var existingBook = await _bookService.GetAsync(id);
        if (existingBook is null)
        {
            _logger.LogWarning("Cannot update book. Book with ID {Id} not found", id);
            return NotFound();
        }

        existingBook.Title = dto.Title;
        existingBook.Author = dto.Author;
        existingBook.Year = dto.Year;

        await _bookService.UpdateAsync(existingBook);
        _logger.LogInformation("Book with ID {Id} successfully updated", id);
        return NoContent();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Request: Delete book with ID {Id}", id);

        var existingBook = await _bookService.GetAsync(id);

        if (existingBook is null)
        {
            _logger.LogWarning("Cannot delete. Book with ID {Id} not found", id);
            return NotFound();
        }

        await _bookService.DeleteAsync(id);
        _logger.LogInformation("Book with ID {Id} deleted", id);
        return NoContent();
    }
}