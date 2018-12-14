use CarRepairShop;
go

select Persons.Name + N' ' + Persons.Surname as Name, Cars.Model, Orders.StartDate
from  Orders 
	inner join Clients on (Orders.ClientId = Clients.Id)
	inner join Persons on (Clients.PersonId = Persons.Id)
	inner join Cars on (Orders.CarId = Cars.Id)
where Orders.MechanicId is null;
go

select StartDate, 
		case when [Mechanic name] is null then '-' else [Mechanic name] end as [Mechanic name], 
		[Client name],
		Model
from ExpandedOrders
where price is null;
go

create or alter function MonthNameToNumber(@monthName nvarchar(10))
returns int as
	begin
		declare @number as int;
		select @number =
		case @monthName
			when 'January' then 1
			when 'February' then 2
			when 'March' then 3
			when 'April' then 4
			when 'May' then 5
			when 'June' then 6
			when 'July' then 7
			when 'August' then 8
			when 'September' then 9
			when 'October' then 10
			when 'November' then 11
			when 'December' then 12
	end
return @number
end
go

select Persons.Surname + N' ' + Persons.Name as [Mechanic name],
		count(Orders.Id) as [Order count], 
		sum(Price) as [Total price], 
		IsNull(sum(cast(OrderDetails.RepeatedIncorrectOrder as int)), 0) as [Repeated orders],
		datename(month, FinishDate) as [Fiscal month]
from Orders 
	inner join Mechanics on (Orders.MechanicId = Mechanics.Id)
	inner join Persons on (Mechanics.PersonId = Persons.Id)
	left join OrderDetails on (Orders.Id = OrderDetails.OrderId)
where FinishDate is not null and YEAR(FinishDate) = YEAR(GetDate())
group by Persons.Surname + N' ' + Persons.Name, datename(month, FinishDate)
order by Persons.Surname + N' ' + Persons.Name, dbo.MonthNameToNumber(datename(month, FinishDate));
go