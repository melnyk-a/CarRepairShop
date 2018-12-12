using CarRepairShop.Domain.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using CarRepairShop.Data.SqlClient.Dtos;

namespace CarRepairShop.Data.SqlClient
{
    internal sealed class SqlClientOrderDataGateway : DisposableObject, IOrderDataGateway
    {
        private readonly Lazy<SqlConnection> connection;

        public SqlClientOrderDataGateway()
        {
            connection = new Lazy<SqlConnection>(() => TryOpenConnection());
        }

        private SqlConnection Connection => connection.Value;

        private SqlConnection TryOpenConnection()
        {
            try
            {
                string connectionString = ConfigurationManager
                    .ConnectionStrings["SqlConnection"]
                    .ConnectionString;

                var connection = new SqlConnection(connectionString);
                connection.Open();

                return connection;
            }
            catch (SqlException ex)
            {
                throw new DataException(ex.Message);
            }
        }

        public async Task AddOrder(Order order)
        {
            await Task.Run(() =>
            {
                int clientId = GetClientId(order.Client);
                if (clientId == -1)
                {
                    clientId = AddClient(order.Client);
                }
                int carId = GetCarId(order.Car);
                if (carId == -1)
                {
                    carId = AddCar(order.Car);
                }
                AddOrder(clientId, carId, order.StartDate, order.Description);
            });
        }

        private void AddOrder(int clientId, int carId, DateTime startDate, string description)
        {
            SqlTransaction transaction = Connection.BeginTransaction();
            SqlCommand command = Connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "AddOrder";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter clientIdParameter = command.Parameters.Add("@clientId", SqlDbType.Int);
                clientIdParameter.Value = clientId;

                SqlParameter carIdParameter = command.Parameters.Add("@carId", SqlDbType.Int);
                carIdParameter.Value = carId;

                SqlParameter descriptionParameter = command.Parameters.Add("@description", SqlDbType.NVarChar, 256);
                descriptionParameter.Value = description;

                SqlParameter startDateParameter = command.Parameters.Add("@startDate", SqlDbType.Date);
                startDateParameter.Value = startDate;

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new DataException(e.Message);
            }
        }

        private int AddCar(Car car)
        {
            SqlTransaction transaction = Connection.BeginTransaction();
            SqlCommand command = Connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "AddCar";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter modelParameter = command.Parameters.Add("@model", SqlDbType.NVarChar, 50);
                modelParameter.Value = car.Model;

                SqlParameter yearParameter = command.Parameters.Add("@year", SqlDbType.SmallInt);
                yearParameter.Value = car.Year;

                SqlParameter numberParameter = command.Parameters.Add("@number", SqlDbType.NVarChar, 15);
                numberParameter.Value = car.Number;

                SqlParameter carId = command.Parameters.AddWithValue("@carId", SqlDbType.Int);
                carId.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                transaction.Commit();

                return (int)carId.Value;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new DataException(e.Message);
            }
        }

        private int AddClient(Client client)
        {
            SqlTransaction transaction = Connection.BeginTransaction();
            SqlCommand command = Connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "AddClient";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParameter = command.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                nameParameter.Value = client.Person.Name;

                SqlParameter surnameParameter = command.Parameters.Add("@surname", SqlDbType.NVarChar, 50);
                surnameParameter.Value = client.Person.Surname;

                SqlParameter phoneParameter = command.Parameters.Add("@phone", SqlDbType.NVarChar, 9);
                phoneParameter.Value = client.Phone;

                SqlParameter clientId = command.Parameters.AddWithValue("@clientId", SqlDbType.Int);
                clientId.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                transaction.Commit();

                return (int)clientId.Value;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new DataException(e.Message);
            }
        }

        private int GetCarId(Car car)
        {
            int carId = -1;

            var command = new SqlCommand(
               $@"select Cars.Id
                  from Cars 
                  where Cars.Model = N'{car.Model}' and Cars.Number = N'{car.Number}' and Cars.Year = '{car.Year}'",
               Connection
           );
            object result = command.ExecuteScalar();
            if (result != null)
            {
                carId = (int)result;
            }

            return carId;
        }

        private int GetClientId(Client client)
        {
            int clientId = -1;

            var command = new SqlCommand(
               $@"select Clients.Id
                 from Clients 
                 inner join Persons on (Clients.PersonId = Persons.Id)
                 inner join Phones on (Clients.PhoneId = Phones.Id)
                 where Persons.Name = N'{client.Person.Name}' and Persons.Surname = N'{client.Person.Surname}' and Phones.Number = '{client.Phone}'",
               Connection
           );
            object result = command.ExecuteScalar();
            if (result != null)
            {
                clientId = (int)result;
            }

            return clientId;
        }

        protected override void Dispose(bool disposing)
        {
            if (connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await Task.Run(() => GetOrders());
        }

        private IEnumerable<Order> GetOrders()
        {
            var orderDtos = new List<OrderDto>();

            var command = new SqlCommand(
                @"select Orders.Id,
                         p1.Name as [Client name] ,
                         p1.Surname as [Client surname],
						 Phones.Number as [Phone],
                         Cars.Model,
						 Cars.Year,
						 Cars.Number,
                         Orders.StartDate,
                         Orders.FinishDate,
                         p2.Name as [Mechanic name],
                         p2.Surname as [Mechanic surname],
                         Orders.Price,
						 Description
                from Orders
                inner join Cars on (Orders.CarId = Cars.Id)
                inner join Clients on (Orders.ClientId = Clients.Id)
                inner join Persons p1 on (Clients.PersonId = p1.Id)
				inner join Phones  on (Clients.PhoneId = Phones.Id)
                left join Mechanics on (Orders.MechanicId = Mechanics.Id)
                left join Persons p2 on (Mechanics.PersonId = p2.Id)",
            Connection);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var orderDto = new OrderDto
                        {
                            Id = (int)reader["Id"],
                            Client = new Client(new Person(reader["Client name"].ToString(), reader["Client surname"].ToString()), reader["Phone"].ToString()),
                            Car = new Car(reader["Model"].ToString(), Convert.ToInt16(reader["Year"]), reader["Number"].ToString()),
                            StartDate = (DateTime)reader["StartDate"],
                            Description = reader["Description"].ToString()
                        };

                        string mechanicName = reader["Mechanic name"].ToString();
                        string mechanicSurname = reader["Mechanic surname"].ToString();
                        if (mechanicName != string.Empty && mechanicSurname != string.Empty)
                        {
                            orderDto.Mechanic = new Person(mechanicName, mechanicSurname);
                        }

                        string finishDate = reader["FinishDate"].ToString();
                        if (finishDate != string.Empty)
                        {
                            orderDto.FinishDate = DateTime.Parse(finishDate);
                        }

                        string price = reader["Price"].ToString();
                        if (price != string.Empty)
                        {
                            orderDto.Price = double.Parse(price);
                        }

                        orderDtos.Add(orderDto);
                    }
                }
            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }

            var orders = new List<Order>();

            foreach (OrderDto orderDto in orderDtos)
            {
                Order order = new Order(orderDto.Id, orderDto.Client, orderDto.Car, orderDto.Description)
                {
                    Mechanic = orderDto.Mechanic,
                    StartDate = orderDto.StartDate,
                    FinishDate = orderDto.FinishDate,
                    Price = orderDto.Price
                };
                orders.Add(order);
            }

            return orders;
        }
    }
}