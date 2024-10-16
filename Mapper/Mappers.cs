using AutoMapper;
using OnlineLibrarySystem.DTOs;
using OnlineLibrarySystem.Models;
namespace OnlineLibrarySystem.Mapper
{
    public class Mappers : Profile
    {
        public Mappers() 
        { 
        
            // borrowed mappings
            CreateMap<BorrowedBooks, BorrowedBookDTO>().ReverseMap();
        
        }
    }
}
