using CarRepairShop.Domain.Models;

namespace CarRepairShop.Data.SqlClient.Dtos
{
    internal sealed class MechanicDto
    {
        public int Id { get; set; }
        public Person Person { get; set; }
    }
}