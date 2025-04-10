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
        // MAKE A FILTER METHOD
        // Q4 read for filter


        // Return All Books
        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _booksService.GetBook();



        // Search Individual Book with ID
        [HttpGet, Route("/api/[controller]/{id}")]
        public async Task<Book> GetBook(string id)
        {
            Book? searchedBook = await _booksService.GetABook(id);
            return searchedBook;
        }



        //// Filter search through all and return all matching results
        //[HttpGet]
        //public async Task<ActionResult> FilterBookSearch([FromQuery] BookQueryParamaters queryParamaters)
        //{


        //}






        // Creates a book with user inputs
        [HttpPost]
        public async Task<IActionResult> CreateBook(Book aBook)
        {
            await _booksService.CreateBook(aBook);
            return CreatedAtAction(nameof(Get), new { id = aBook.Id }, aBook);
        }


        // Searches for book and replaces it, if book not found throws error...
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Book updateBook)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var book = _booksService.GetABook(id);
                await _booksService.RemoveBook(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
