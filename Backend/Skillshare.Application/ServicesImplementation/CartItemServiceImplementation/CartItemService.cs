﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.DTOs.CartItem;
using Skillshare.Contracts.ServicesContracts;
using Skillshare.Domain.Entities;

namespace Skillshare.Application.ServicesImplementation.CartItemService
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _CartItemRepository;
        private readonly ICartRepository _CartRepository;

        public CartItemService(ICartItemRepository cartItemRepository, ICartRepository cartRepository)
        {
            _CartItemRepository = cartItemRepository;
            _CartRepository = cartRepository;
        }

        public async Task<bool> AddCartItem(CartItemForCreate cartItemForCreate)
        {
            CartItem cartItem = new CartItem();
            cartItem.courseId = cartItemForCreate.courseId;
            cartItem.coursePrice = cartItemForCreate.price;
            cartItem.cartItemTitle = cartItemForCreate.cartItemTitle;
            cartItem.ApplicationUserId = cartItemForCreate.userId;
            await _CartItemRepository.Add(cartItem);

            var AvailableCart = await _CartRepository.GetFirstOrDefault(c => c.applicationUserId == cartItemForCreate.userId && !c.isPaid, new[] { "cartItems" });
            if (AvailableCart is null)
            {
                //Create new cart
                await _CartRepository.Add(new Cart()
                {
                    applicationUserId = cartItemForCreate.userId,
                    totalPrice = cartItemForCreate.price,
                    cartItems = new List<CartItem>()
                    {
                        cartItem
                    }
                });
            }
            else
            {
                AvailableCart.cartItems.Add(cartItem);
                AvailableCart.totalPrice += cartItemForCreate.price;
                _CartRepository.Update(AvailableCart);
            }
            await _CartItemRepository.SaveChanges();
            return await _CartRepository.SaveChanges();
        }

        public async Task<bool> RemoveCartItem(int CourseId, string userId)
        {
            var Cartitem = await _CartItemRepository.GetFirstOrDefault(c => c.courseId == CourseId && c.ApplicationUserId == userId);

            if (Cartitem is null)
            {
                return false;
            }

            _CartItemRepository.Remove(Cartitem);
            return await _CartRepository.SaveChanges();
        }
    }
}