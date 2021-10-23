using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly BookService bookService;

		public BooksController(BookService bookService)
		{
			this.bookService = bookService;
		}

		[HttpGet]
		public ActionResult<List<Book>> Get() => bookService.Get();

		[HttpGet("{id:length(24)}", Name = "GetBook")]
		public ActionResult<Book> Get(string id)
		{
			var book = bookService.Get(id);
			if (book == null)
				return NotFound();
			return book;
		}

		[HttpPost]
		public ActionResult<Book> Create(Book book)
		{
			bookService.Create(book);
			return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, Book bookIn)
		{
			var book = bookService.Get(id);
			if (book == null)
				return NotFound();
			bookService.Update(id, bookIn);
			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var book = bookService.Get(id);
			if (book == null)
				return NotFound();
			bookService.Remove(book.Id);
			return NoContent();
		}
	}
}
