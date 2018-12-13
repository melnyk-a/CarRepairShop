using CarRepairShop.Domain.Models;
using System;

namespace CarRepairShop.Data.SqlClient.Dtos
{
    internal sealed class OrderDto
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Client Client { get; set; }
        public string Description { get; set; }
        public DateTime FinishDate { get; set; }
        public Person Mechanic { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
    }
}