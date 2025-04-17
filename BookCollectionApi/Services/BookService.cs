using BookCollectionApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace BookCollectionApi.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _books;

    public BookService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _books = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetBooks([FromQuery] QueryParameters queryParameters)
    {
        IQueryable<Book> booksQuery = _books.AsQueryable();
        booksQuery = booksQuery.Skip(queryParameters.Size *(queryParameters.Page - 1))
                     .Take(queryParameters.Size);

        // Sort the booksQuery by desc or asc using Title 


        return await booksQuery.ToListAsync();
    }


    public async Task<Book?> GetABook(string id) =>
        await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateBook(Book newBook)
    {
        if (newBook.Id == null || newBook.Id =="string" )
        {
            var objectId = ObjectId.GenerateNewId();
            string objectIdString = objectId.ToString();
            newBook.Id = objectIdString;
        }
            await _books.InsertOneAsync(newBook);
    }


    public async Task UpdateBook(string id, Book updatedBook) =>
        await _books.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveBook(string id) =>
        await _books.DeleteOneAsync(x => x.Id == id);

}
