using AutoMapper;
using CartService.Cart.Requests;
using CartService.Core;
using CartService.Services.Commands.Cart;
using CartService.Services.Commands.WebHook;

namespace CartService
{
    public class WebApiProfile : Profile
    {
        public WebApiProfile()
        {
            CreateMap<UpdateCartItemsRequest, AddCartItemsCommand>();
            CreateMap<DeleteCartItemsRequest, DeleteCartItemsCommand>();
            CreateMap<CartItemEntryRequest, CartItemEntry>();

            CreateMap<WebHookRequest, AddWebHookCommand>();
            CreateMap<WebHookRequest, DeleteWebHookCommand>();
        }
    }
}