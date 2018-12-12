use CarRepairShop;
go

create or alter procedure AddClient @name nvarchar(50), @surname nvarchar(50), @phone nvarchar(9), @clientId int out
as
begin
	begin transaction
		begin try
	
			insert into Persons
			values (@name, @surname);

			declare @personId int;
			select @personId = Id from Persons where id = @@Identity;

			insert into Phones
			values (@phone);

			declare @phoneId int;
			select @phoneId = Id from Phones where id = @@Identity;

			insert into Clients
			values (@personId, @phoneId);

			select @clientId = Id from Clients where id = @@Identity;

			commit transaction

		end try
		begin catch
			rollback transaction;
		end catch
end
go

create or alter procedure AddCar @model nvarchar(50), @year smallint, @number nvarchar(15), @carId int out
as
begin
	begin transaction
		begin try
	
			insert into Cars
			values (@model, @year, @number);

			select @carId = Id from Cars where id = @@Identity;

			commit transaction

		end try
		begin catch
			rollback transaction;
		end catch
end
go

create or alter procedure AddOrder @clientId int, @carId int, @description nvarchar(256), @startDate date
as
begin
	insert into Orders (ClientId, CarId, Description, StartDate)
	values (@clientId, @carId, @description, @startDate);
end