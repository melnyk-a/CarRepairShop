use CarRepairShop;
go

insert into Persons(Name, Surname) values
	(N'Heather', N'Parker'),
	(N'Becky', N'Hogan'),
	(N'Sheila', N'Miles'),
	(N'Gwen', N'Newton'),
	(N'Emma', N'Rodriquez'),
	(N'Courtney', N'Lane'),
	(N'Carmen', N'Smith'),
	(N'Jenna', N'Cummings'),
	(N'Roxanne', N'Flores'),
	(N'Harriet', N'Christensen'),
	(N'Kathryn', N'Pena'),
	(N'Pat', N'Schneider'),
	(N'Larry', N'Thompson'),
	(N'Hubert', N'Harrington'),
	(N'Austin', N'Ballard'),
	(N'Garrett', N'Freeman'),
	(N'Matt', N'Rodgers'),
	(N'Domingo', N'Gordon'),
	(N'Arturo', N'Ramos'),
	(N'Clarence', N'Mckenzie');
go

insert into Phones(Number) values
	(N'932573146'),
	(N'937625413'),
	(N'932136745'),
	(N'936732541'),
	(N'932736145'),
	(N'946374152'),
	(N'947213645'),
	(N'936417532'),
	(N'935234176'),
	(N'933715462'),
	(N'932514763'),
	(N'934521637'),
	(N'675123476'),
	(N'675674132'),
	(N'673571624'),
	(N'937513624'),
	(N'937546321'),
	(N'937654132'),
	(N'933176254'),
	(N'675461237');
go


insert into Clients (PersonId, PhoneId) values
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4),
	(5, 5),
	(6, 6),
	(7, 7),
	(8, 8),
	(9, 9),
	(10, 10),
	(11, 11),
	(12, 12),
	(13, 13),
	(14, 14),
	(15, 15);
go

insert into Mechanics(PersonId, PhoneId) values
	(16, 16),
	(17, 17),
	(18, 18),
	(19, 19),
	(20, 20);
go

insert into Cars(Model, Year, Number) values
	(N'Caerham', 1942, N'EDD 8015'),
	(N'SSC', 1965, N'EVO 5499'),
	(N'Superformance', 1966, N'IYO 5978'),
	(N'Daihatsu', 1967, N'OOZ 1434'),
	(N'Proton', 1968, N'OLD 2984'),
	(N'Chevrolet', 1968, N'NOB 2656'),
	(N'Shelby', 1971, N'FAX 4438'),
	(N'Tesla', 1979, N'WEE 1935'),
	(N'Honda', 1987, N'KKI 4222'),
	(N'Kia', 1996, N'GAP 8485'),
	(N'Lotus', 1998, N'SOB 1258'),
	(N'Bentley', 2006, N'LLL 1463');
go

insert into Orders(ClientId, CarId, MechanicId, Description, Price, StartDate, FinishDate) values
	(1, 1, 1, N'Oxygen Sensor Replacements', 3600, '01-01-2018', '01-07-2018'),
	(2, 2, 1, N'Catalytic Converter Replacements', 905, '01-09-2018', '01-20-2018'),
	(3, 3, 2, N'Ignition Coil and Spark Plug Replacements (Both Together)', 6500, '04-08-2018', '04-14-2018'),
	(4, 4, 5, N'Fuel Cap Replacements', 500, '07-12-2018', '07-15-2018'),
	(5, 5, 1, N'Thermostat Replacement', 8200, '09-08-2018', '09-25-2018'),
	(6, 6, 4, N'Ignition Coil Replacements', 250, '03-01-2018', '03-16-2018'),
	(7, 7, 1, N'Mass Air Flow Sensor Replacements', 2900, '06-06-2018', '06-25-2018'),
	(8, 8, 3, N'Spark Plug Replacements', 960, '12-08-2017', '12-10-2017'),
	(9, 9, 2, N'EVAP Purge Control Valve / Solenoid', 1260, '12-25-2017', '12-28-2017'),
	(5, 5, 1, N'Thermostat Replacement', 8200, '04-08-2018', '04-25-2018');
go

insert into Orders(ClientId, CarId, Description) values
	(10, 10, N'Removing Aftermarket Alarm'),
	(11, 11, N'Replacing Intake Manifold Gaskets');
go

insert into Orders(ClientId, CarId, MechanicId, Description) values
	(12, 12, 5, N'Replacing Spark Plugs');
go

insert into OrderDetails (OrderId, Discount, RepeatedIncorrectOrder) values
	(2, 50.0, 1),
	(5, 35, 1),
	(6, 5.0, 0),
	(5, 50.0, 1);
go