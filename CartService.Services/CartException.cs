using System;

namespace CartService.Services
{
    public class CartException : Exception
    {
        public CartException(string message) : base(message)
        {
        }
    }
}