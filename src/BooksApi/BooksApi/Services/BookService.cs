using BooksApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
	public class BookService
	{
		private readonly IMongoCollection<Book> books;

		public BookService(IBookstoreDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			books = database.GetCollection<Book>(settings.BooksCollectionName);
		}

		public List<Book> Get() => books.Find(book => true).ToList();

		public Book Get(string id) => books.Find<Book>(book => book.Id == id).FirstOrDefault();

		public Book Create(Book book)
		{
			books.InsertOne(book);
			return book;
		}

		public void Update(string id, Book bookIn) => books.ReplaceOne(book => book.Id == id, bookIn);

		public void Remove(Book bookIn) => books.DeleteOne(book => book.Id == bookIn.Id);

		public void Remove(string id) => books.DeleteOne(book => book.Id == id);
	}
}
