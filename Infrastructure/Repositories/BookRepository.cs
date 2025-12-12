using BookStoreApi.Application.Interfaces;
using BookStoreApi.Domain.Entities;
using BookStoreApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    public async Task<Book?> GetAsync(int id) =>
        await _context.Books.FindAsync(id);

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetAsync(id);
        if (book is null) return;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}