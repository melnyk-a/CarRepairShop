use master;
go

create database CarRepairShop;
go

use CarRepairShop;
go

create table Persons(
	Id int not null identity(1, 1) primary key,
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null
);
go

create table Phones(
	Id int not null identity(1, 1) primary key,
	Number nvarchar(9) not null
);
go

alter table Phones
add constraint CHK_Phone check(Number like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');
go


create table Clients(
	Id int not null identity(1, 1) primary key,
	PersonId int not null,
	PhoneId int not null,
);
go

alter table Clients
add foreign key (PersonId) references Persons(Id);
go

alter table Clients
add foreign key (PhoneId) references Phones(Id);
go


create table Mechanics(
	Id int not null identity(1, 1) primary key,
	PersonId int not null,
	PhoneId int not null,
);
go

alter table Mechanics
add foreign key (PersonId) references Persons(Id);
go

alter table Mechanics
add foreign key (PhoneId) references Phones(Id);
go

create table Cars(
	Id int not null identity(1, 1) primary key,
	Model nvarchar(50) not null,
	Year smallint not null,
	Number nvarchar(15) not null
);
go

alter table Cars
add constraint CHK_Year check(Year > 0);
go

create table Orders(
	Id int not null identity(1, 1) primary key,
	ClientId int not null,
	CarId int not null,
	MechanicId int,
	Description nvarchar(256) not null,
	Price money,
	StartDate date,
	FinishDate date,
);
go

alter table Orders
add constraint CHK_FinishFate check(FinishDate >= StartDate);
go

alter table Orders
add constraint DF_StartDate default GetDate() for StartDate;
go

alter table Orders
add foreign key (ClientId) references Clients(Id);
go

alter table Orders
add foreign key (CarId) references Cars(Id);
go

alter table Orders
add foreign key (MechanicId) references Mechanics(Id);
go

create table OrderDetails(
	Id int not null identity(1, 1) primary key,
	OrderId int not null,
	Discount decimal,
	RepeatedIncorrectOrder bit
);
go

alter table OrderDetails
add constraint CHK_Discount check(Discount > 0);
go

alter table OrderDetails
add foreign key (OrderId) references Orders(Id);
go