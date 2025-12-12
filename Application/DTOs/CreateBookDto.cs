namespace BookStoreApi.Application.DTOs;

public class CreatedBookDto
{
    // ID is left empty because the database will auto genrate it
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
}