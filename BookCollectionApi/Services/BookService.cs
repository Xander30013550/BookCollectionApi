using BookCollectionApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Xml.Linq;

namespace BookCollectionApi.Services;

public class BookService
{
    private readonly MongoClient mongoClient;
    private readonly List<Book> _books;

    public BookService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var dbList = mongoClient.ListDatabaseNames().ToList();

        var dbName = bookStoreDatabaseSettings.Value.DatabaseName;
        var dbCollectionName = bookStoreDatabaseSettings.Value.BooksCollectionName;

        var mongoDatabase = mongoClient.GetDatabase(dbName);

        var mongoCollection = mongoDatabase.GetCollection<Book>(dbCollectionName);

        _books = mongoCollection.Find(_ => true).ToList();
    }

    public async Task<List<Book>> GetBook() =>
        await Task.FromResult(_books);

    public async Task<Book?> GetBook(string id) =>
        await Task.FromResult(_books.FirstOrDefault(x => x.Id == id));

    public async Task CreateBook(Book newBook)
    {
        var mongoDatabase = mongoClient.GetDatabase("BooksLibrary");
        var mongoCollection = mongoDatabase.GetCollection<Book>("Books");
        await mongoCollection.InsertOneAsync(newBook);

        _books.Add(newBook);
        await Task.CompletedTask;
    }


    //public async Task UpdateBook(string id, Book updatedBook)
    //{

    //}

    //public async Task RemoveBook(string id) =>
    //    await _booksCollection.DeleteOneAsync(x => x.Id == id);
}