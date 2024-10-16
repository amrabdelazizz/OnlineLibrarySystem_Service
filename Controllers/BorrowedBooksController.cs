using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using OnlineLibrarySystem.DTOs;
using OnlineLibrarySystem.Models;

namespace OnlineLibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBooksController : ControllerBase
    {
        private readonly LibraryDbCotext _context;
        private readonly IMapper _mapper;

        public BorrowedBooksController(LibraryDbCotext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowedBooks>>> GetBorrowedBooks()
        {
            return await _context.BorrowedBooks.ToListAsync();
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowedBooks>> GetBorrowedBooks(int id)
        {
            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);

            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return borrowedBooks;
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowedBooks(int id, BorrowedBookDTO borrowedBooks)
        {
            if (id != borrowedBooks.BorrowId)
            {
                return BadRequest();
            }

            _context.Entry(borrowedBooks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowedBooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost("BorrowBook")]
        public async Task<ActionResult<BorrowedBooks>> PostBorrowedBooks(BorrowedBookDTO borrowedBooks)
        {
            var borrowMapped = _mapper.Map<BorrowedBooks>(borrowedBooks);
            _context.BorrowedBooks.Add(borrowMapped);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrowedBooks", new { id = borrowedBooks.BorrowId }, borrowedBooks);
        }
        [HttpPost("ReturnBook")]
        public async Task<ActionResult> ReturnBook(int borrowerId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
                return NotFound(new { message = "the book ID is not found" });

            var borrower = await _context.Borrowers.FindAsync(borrowerId);

            if (borrower == null)
                return NotFound(new { message = "Borrower ID is not found" });

            var borrowedBook = await _context.BorrowedBooks
                                .FirstOrDefaultAsync( b => b.BorrowerId == borrowerId && b.BookId == bookId);
            if (borrowedBook == null)
                return NotFound(new {message = "no borrow for those ids."});

            borrowedBook.IsReturned = true;

            await _context.SaveChangesAsync();

            var datenow = DateTime.Now;

            if (datenow.Date > borrowedBook.ReturnDate)
                return Ok(new {message = $"the book returned successfully , but you are late , your return date is {borrowedBook.ReturnDate} so you will pay us a fees for latency"});

            return Ok(new {message = "thanks for returning the book ontime ."});


        }

        [HttpGet("BorrowedHistory")]
        public async Task<ActionResult<IEnumerable<BorrowedBooks>>> GetHistoryBorrowedBooks()
        {
            return await _context.BorrowedBooks
                        .Where(b => b.IsReturned == true)
                        .ToListAsync();
        }

        [HttpPost("UserBorrowedBooks/{id}")]
        public async Task<ActionResult<IEnumerable<BorrowedBooks>>> GetHistoryBorrowedBooksForUer(int id)
        {
            return await _context.BorrowedBooks
                        .Where(b => b.IsReturned == true && b.BorrowerId == id)
                        .ToListAsync();
        }

        // DELETE: api/BorrowedBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBooks(int id)
        {
            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            _context.BorrowedBooks.Remove(borrowedBooks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowedBooksExists(int id)
        {
            return _context.BorrowedBooks.Any(e => e.BorrowId == id);
        }
    }
}
