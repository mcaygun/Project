using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Data.Entities;
using BookStore.Data.ViewModels.Products;

namespace BookStore.Service.Automapper
{
    public class ProductMap : Profile
    {
        public ProductMap() 
        
        {
            CreateMap<Product, ProductAddViewModel>().ReverseMap();
        }
    }
}
