use CarRepairShop;
go

select Persons.Name + N' ' + Persons.Surname as Name, Cars.Model, Orders.StartDate
from  Orders 
	inner join Clients on (Orders.ClientId = Clients.Id)
	inner join Persons on (Clients.PersonId = Persons.Id)
	inner join Cars on (Orders.CarId = Cars.Id)
where Orders.MechanicId is null;
go