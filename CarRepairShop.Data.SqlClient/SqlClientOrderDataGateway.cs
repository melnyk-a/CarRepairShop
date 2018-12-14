using CarRepairShop.Data.SqlClient.Dtos;
using CarRepairShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        private int AddCar(Car car)
        {
            var command = new SqlCommand(
                $@"insert into Cars(Model, Year, Number)
                values(N'{car.Model}', {car.Year}, N'{car.Number}');
                select scope_identity()",
              Connection
            );
            try
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (SqlException e)
            {
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
                command.CommandText = $@"insert into Persons
                    values(N'{client.Person.Name}', N'{client.Person.Surname}'); 
                    select scope_identity()";
                int personId = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = $@"insert into Phones
                    values (N'{client.Phone}');
                    select scope_identity()";
                int phoneId = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = $@"insert into Clients
                values({personId}, {phoneId});
                select scope_identity()";
                int clientId = Convert.ToInt32(command.ExecuteScalar());

                transaction.Commit();
                return clientId;
            }
            catch (SqlException e)
            {
                transaction.Rollback();
                throw new DataException(e.Message);
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
            var command = new SqlCommand(
                $@"insert into Orders(ClientId, CarId, Description, StartDate)
                values({clientId}, {carId}, N'{description}', N'{startDate}')",
                Connection
            );
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new DataException(e.Message);
            }
        }

        public async Task AssignMechanicAsync(int orderId, int mechanicId)
        {
            await Task.Run(() =>
            {
                var command = new SqlCommand(
                    $@"update Orders
                    set MechanicId = {mechanicId}
                    where id = {orderId}",
                    Connection
                );
                command.ExecuteScalar();
            });
        }

        public async Task CompleteOrderAsync(int orderId, DateTime finishDate)
        {
            await Task.Run(() =>
            {
                var command = new SqlCommand(
                    $@"update Orders
                    set FinishDate = N'{finishDate}'
                    where id = {orderId}",
                    Connection
                );
                command.ExecuteScalar();
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }

        private int GetCarId(Car car)
        {
            int carId = -1;

            var command = new SqlCommand(
                $@"select Cars.Id
                from Cars 
                where Cars.Model = N'{car.Model}' and 
                Cars.Number = N'{car.Number}' and 
                Cars.Year = '{car.Year}'",
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
                where Persons.Name = N'{client.Person.Name}' and 
                Persons.Surname = N'{client.Person.Surname}' and 
                Phones.Number = '{client.Phone}'",
               Connection
           );
            object result = command.ExecuteScalar();
            if (result != null)
            {
                clientId = (int)result;
            }

            return clientId;
        }

        private IEnumerable<Order> GetFreeOrders()
        {
            var command = new SqlCommand(
                @"select *
                from ExpandedOrders
                where [Mechanic Name] is null and 
                [Mechanic surname] is null",
                Connection
            );

            IEnumerable<OrderDto> ordersDtos = GetOrderDtos(command);

            return GetOrders(ordersDtos);
        }

        public async Task<IEnumerable<Order>> GetFreeOrdersAsync()
        {
            return await Task.Run(() => GetFreeOrders());

        }

        private IEnumerable<Person> GetMechanics()
        {
            var mechanicsDtos = new List<MechanicDto>();

            var command = new SqlCommand(
               @"select Mechanics.id, Name, Surname
                from Mechanics inner join Persons on (Mechanics.PersonId = Persons.Id)",
               Connection
            );

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mechanicDto = new MechanicDto
                        {
                            Id = (int)reader["Id"],
                            Person = new Person(reader["Name"].ToString(), reader["Surname"].ToString())
                        };
                        mechanicsDtos.Add(mechanicDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DataException(e.Message);
            }

            var mechanics = new List<Person>();

            foreach (MechanicDto mechanicDto in mechanicsDtos)
            {
                Person mechanic = new Person(mechanicDto.Id, mechanicDto.Person.Name, mechanicDto.Person.Surname);
                mechanics.Add(mechanic);
            }

            return mechanics;
        }

        public async Task<IEnumerable<Person>> GetMechanicsAsync()
        {
            return await Task.Run(() => GetMechanics());
        }

        private IEnumerable<OrderDto> GetOrderDtos(SqlCommand command)
        {
            var orderDtos = new List<OrderDto>();
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var orderDto = new OrderDto
                        {
                            Id = (int)reader["Id"],
                            Client = new Client(new Person(
                                                        reader["Client name"].ToString(),
                                                        reader["Client surname"].ToString()),
                                                        reader["Phone"].ToString()
                                                ),
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
                    return orderDtos;
                }
            }
            catch (SqlException e)
            {
                throw new DataException(e.Message);
            }
        }

        private IEnumerable<Order> GetOrders()
        {
            var command = new SqlCommand(
                @"select *
                from ExpandedOrders",
                Connection
            );

            IEnumerable<OrderDto> ordersDtos = GetOrderDtos(command);

            return GetOrders(ordersDtos);

        }

        private IEnumerable<Order> GetOrders(IEnumerable<OrderDto> orderDtos)
        {
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

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await Task.Run(() => GetOrders());
        }

        public IEnumerable<Order> GetUnompleteOrders()
        {
            var command = new SqlCommand(
                @"select *
                from ExpandedOrders
                where FinishDate is null and 
                [Mechanic Name] is not null and 
                [Mechanic surname] is not null",
                Connection
            );

            IEnumerable<OrderDto> ordersDtos = GetOrderDtos(command);

            return GetOrders(ordersDtos);
        }

        public Task<IEnumerable<Order>> GetUnompleteOrdersAsync()
        {
            return Task.Run(() => GetUnompleteOrders());
        }

        public async Task SetPriceAsync(int orderId, double price)
        {
            await Task.Run(() =>
            {
                var command = new SqlCommand(
                    $@"update Orders
                    set Price = {price}
                    where id = {orderId}",
                    Connection
                );
                command.ExecuteScalar();
            });
        }

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
            catch (SqlException e)
            {
                throw new DataException(e.Message);
            }
        }
    }
}