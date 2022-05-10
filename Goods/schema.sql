create table stock (
	id bigserial primary key,
	name varchar(255) not null check(length(name) > 2),
	creation_date date not null check(extract(year from creation_date) >= 2000 and extract(year from creation_date) <= extract(year from current_date)),
	days_valid_to smallint not null check(days_valid_to > 0),
	price decimal not null check(price > 0),
	measure varchar(63) not null
);

create table provider (
	id bigserial primary key,
	name varchar(255) not null check(length(name) > 2),
	address varchar(511) not null check(length(address) > 2),
	phone char(10) not null check(length(phone) = 10),
	contact_person varchar(255)
);

create table delivery (
	stock_id bigint,
	provider_id bigint,
	document_number bigint not null check(document_number > 0),
	delivery_date timestamp not null,
	quantity real not null check(quantity > 0),
	primary key(stock_id, provider_id),
	foreign key(stock_id) references stock(id),
	foreign key(provider_id) references provider(id)
);

insert into stock (name, creation_date, days_valid_to, price, measure)
values
	('Олио', current_date - 3, 7, 4.5, 'Литри'),
	('Яйце', current_date - 5, 10, 0.4, 'Броя'),
	('Хляб', current_date - 2, 5, 1.2, 'Килограми'),
	('Сирене', current_date - 2, 3, 6.2, 'Килограми'),
	('Бира', current_date - 1, 20, 2.5, 'Литри');

insert into provider (name, address, phone, contact_person)
values 
	('Иванов ЕОД', 'Николаевска 12', '0892423124', 'Иван Иванов'),
	('Стефанови ЕОД', 'Ивановска 44', '0883442334', 'Стефан Петров'),
	('Петрови ЕОД', 'Стамболийска 18', '0893452367', 'Петър Иванов'),
	('Мая ЕОД', 'Петровска 23', '0897005345', 'Мая Маринова'),
	('Георгиев ЕОД', 'Обеля 11', '0893442576', 'Георги Георгиев');

insert into delivery (stock_id, provider_id, delivery_date, document_number, quantity)
values 
	(1, 2, now() - interval '1 day', 103, 2),
	(1, 3, now() - interval '2 day', 104, 3),
	(1, 4, now() - interval '3 day', 105, 5),
	(1, 5, now() - interval '4 day', 106, 22),
	(2, 1, now() - interval '5 day', 107, 11),
	(2, 2, now() - interval '6 day', 108, 23),
	(3, 1, now() - interval '7 day', 109, 55),
	(4, 3, now() - interval '8 day', 110, 32),
	(5, 5, now() - interval '9 day', 111, 7),
	(5, 2, now() - interval '10 day', 112, 6);