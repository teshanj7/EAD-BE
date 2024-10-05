﻿using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IOrderRepository
    {
        string CreateOrder(Order order);
        bool DeleteOrderById(string orderId);
        Order GetOrderById(string orderId);
        Task<IEnumerable<Order>> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(string userId);
        bool UpdateOrderById(string orderId, Order order);
        bool UpdatePartialDeliveryStatus(string orderId, string productVendor);
        bool MarkOrderAsDelivered(string orderId);
        bool CancelOrderById(string orderId, string cancellationNote);
        Order TrackOrderById(string orderId);
    }
}
