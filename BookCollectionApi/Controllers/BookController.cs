using Microsoft.AspNetCore.Http;
using BookCollectionApi.Model;
using BookCollectionApi.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace BookCollectionApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _booksService;

        public BookController(BookService booksService) =>
            _booksService = booksService;




        // This method returns all books, optionally filtered by query parameters.
        // If no books are found, it returns NoContent.
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<ActionResult> GetAllBooks([FromQuery] BookQueryParamaters queryParameters)
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



        // This method returns a single book by its ID.
        // If the book is not found, it returns null.
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            try
            {
                Book? searchedBook = await _booksService.GetABook(id);
                return searchedBook != null ? Ok(searchedBook) : NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }




        // This method creates a new book record using provided data.
        // Returns a 201 Created response with a reference to the new book.
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> CreateBook(Book aBook)
        {
            await _booksService.CreateBook(aBook);
            return CreatedAtAction(nameof(GetBook), new { id = aBook.Id }, aBook);
        }



        // This method updates a book by its ID with new data.
        // If the book is not found, it returns NotFound.
        [MapToApiVersion("1.0")]
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


        // This method deletes up to three books based on given IDs.
        // Returns NoContent if deletions are successful or NotFound otherwise.
        [MapToApiVersion("1.0")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(
        [FromQuery] string idOne,
        [FromQuery] string? idTwo = null,
        [FromQuery] string? idThree = null)
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
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
