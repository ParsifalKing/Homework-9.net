
create table Customers
(
    CustomerId serial primary key,
    FullName varchar(200),
	Email varchar(150)
);

create table Menu
(
    FoodId serial primary key,
    FoodName varchar(300),
	FoodPrice decimal,
	AffordableFood bool
);

create table Orders
(
	OrderId serial primary key,
	CustomerId int references Customers(CustomerId),
	OrderInfo varchar(100),
	TotalAmount decimal,
	Status bool  
);

create table OrderItems
(
    OrderItemId serial primary key,
	OrderId int references Orders(OrderId),
	FoodId int references Menu(FoodId),
	Quantity int,
	UnitPrice decimal
);


