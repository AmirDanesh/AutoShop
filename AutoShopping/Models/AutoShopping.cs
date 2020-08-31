using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Ticket> Tickets { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }

    public class Status
    {
        public int ID { get; set; }
        public string StatusName { get; set; }
        public List<Ticket> Tickets { get; set; }
    }

    public class Brand
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public string LogoNameImg { set; get; }
        public List<Model> Models { get; set; }
    }

    public class Model
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public List<Car> Cars { get; set; }
    }

    public class Color
    {
        public int ID { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public List<Car> Cars { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<Car> Cars { get; set; }
    }

    public class Car
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string FileNameImage { get; set; }
        public List<Images> Images { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public int ID { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

    public class Ticket
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public List<TicketDetails> TicketDetails { get; set; }
    }

    public class TicketDetails
    {
        public int ID { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class Images
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public string Name { get; set; }
        public Car Car { get; set; }
    }
}
