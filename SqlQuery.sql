use CarRepairShop;
go

select Persons.Name + N' ' + Persons.Surname as Name, Cars.Model, Orders.StartDate
from  Orders 
	inner join Clients on (Orders.ClientId = Clients.Id)
	inner join Persons on (Clients.PersonId = Persons.Id)
	inner join Cars on (Orders.CarId = Cars.Id)
where Orders.MechanicId is null;
go

create or alter view ExpandOrders
as
select Orders.StartDate, 
		p1.Name + N' ' + p1.Surname as [Client name], 
		p2.Name + N' ' + p2.Surname as [Mechanic name], 
		Cars.Model, 
		Orders.Price
from  Orders 
	inner join Cars on (Orders.CarId = Cars.Id)
	inner join Clients on (Orders.ClientId = Clients.Id)
	inner join Persons p1 on (Clients.PersonId = p1.Id)
	left join Mechanics on (Orders.MechanicId = Mechanics.Id)
	left join Persons p2 on (Mechanics.PersonId = p2.Id)
go

select StartDate, 
		case when [Mechanic name] is null then '-' else [Mechanic name] end as [Mechanic name], 
		[Client name],
		Model
from ExpandOrders
where price is null;
go