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

        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _booksService.GetBook();

        [HttpPost]
        public async Task<IActionResult> Post(Book aBook)
        {
            await _booksService.CreateBook(aBook);
            return CreatedAtAction(nameof(Get), new { id = aBook.Id }, aBook);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, Book updateBook)
        //{
        //    var book = await _booksService.GetBook(id);
        //    if(book is null)
        //    {
        //        return NotFound();
        //    }
            
        //    updateBook.Id = book.Id;
        //    await _booksService.UpdateBook(id, updateBook);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var book = _booksService.GetBook(id);
        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    await _booksService.RemoveBook(id);
        //    return NoContent();
        //}
    }
}
