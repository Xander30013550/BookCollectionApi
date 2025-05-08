using Microsoft.AspNetCore.Http;
using BookCollectionApi.Model;
using BookCollectionApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCollectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _booksService;

        public BookController(BookService booksService) =>
            _booksService = booksService;

        // ADD ERROR HANDLING FOR ALL BELOW
        // Q4 read for filter


        // Return All Books
        [HttpGet]
        public async Task<ActionResult> GetAllBooks([FromQuery]BookQueryParamaters queryParameters)
        {
            try
            {
                var books = await _booksService.GetBooks(queryParameters);
                if (books == null || !books.Any())
                {
                    return NoContent(); 
                }

                return Ok(books);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); 
            }
        }



        // Search Individual Book with ID
        [HttpGet, Route("/api/[controller]/{id}")]
        public async Task<Book> GetBook(string id)
        {
            Book? searchedBook = await _booksService.GetABook(id);
            return searchedBook;
        }



        // Creates a book with user inputs
        [HttpPost]
        public async Task<IActionResult> CreateBook(Book aBook)
        {
            await _booksService.CreateBook(aBook);
            return CreatedAtAction(nameof(GetBook), new { id = aBook.Id }, aBook);
        }


        // Searches for book and replaces it, if book not found throws error...
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, Book updateBook)
        {
            try
            {
                var book = await _booksService.GetABook(id);
                updateBook.Id = book.Id;
                await _booksService.UpdateBook(id, updateBook);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // Searches book ObjectID and deletes the book found, returns Not Found it no book found.
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string idOne, string? idTwo = null, string? idThree = null)
        {
            try
            {
                var book = await _booksService.GetABook(idOne);
                if (book != null)
                {
                    await _booksService.RemoveBook(idOne);
                }

                if (idTwo != null)
                {
                    book = await _booksService.GetABook(idTwo);
                    if (book != null)
                    {
                        await _booksService.RemoveBook(idTwo);
                    }
                }

                if (idThree != null)
                {
                    book = await _booksService.GetABook(idThree);
                    if (book != null)
                    {
                        await _booksService.RemoveBook(idThree);
                    }
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
