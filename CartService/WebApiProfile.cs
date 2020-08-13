using AutoMapper;
using CartService.Cart.Requests;
using CartService.Core;
using CartService.Services.Commands;

namespace CartService
{
    public class WebApiProfile : Profile
    {
        public WebApiProfile()
        {
            CreateMap<UpdateCartItemsRequest, AddCartItemsCommand>();
            CreateMap<DeleteCartItemsRequest, DeleteCartItemsCommand>();
            CreateMap<CartItemEntryRequest, CartItemEntry>();
        }
    }
}