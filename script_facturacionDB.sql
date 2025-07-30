create database facturacionDB
use facturacionDB

create table Cliente(
	ClienteID int primary key identity(1,1),
	Nombre nvarchar(100) not null,
	CedulaRnc nvarchar(20) not null,
	TipoDocumento int not null --(1 = cedula; 2 = Passaporte, 3 = RNC)
	);

create table Factura(
	FacturaID int primary key identity(1,1),
	ClienteID int not null,
	Fecha datetime not null,
	Total decimal not null,

	foreign key (ClienteID) references Cliente(ClienteID)
	on delete cascade
);

create table DetalleFactura(
	DetalleFacturaID int primary key identity(1,1),
	FacturaID int not null,
	ProductoID int not null,
	Cantidad int not null,
	PrecioUnitario Decimal(18,2) not null,
	Total decimal(18,2) not null,
	
	foreign key (FacturaID) references Factura(FacturaId)
	on delete cascade

);

create table Producto(
	ProductoID int primary key identity(1,1),
	Nombre nvarchar(100) not null,
	Precio decimal(18,2) not null DEFAULT 0,
    PrecioMayor decimal(18,2) not null DEFAULT 0

);


alter table DetalleFactura
add constraint FK_DetalleFactura_Producto
foreign key (ProductoID) references Producto(ProductoId);

insert into Producto (Nombre) values
('Laptop Lenovo'),
('Mouse Logitech'),
('Teclado Redragon'),
('Monitor Samsung'),
('Impresora HP');

UPDATE Producto SET Precio = 1200, PrecioMayor = 950 WHERE Nombre = 'Laptop Lenovo';
UPDATE Producto SET Precio = 40, PrecioMayor = 30 WHERE Nombre = 'Mouse Logitech';
UPDATE Producto SET Precio = 60, PrecioMayor = 45 WHERE Nombre = 'Teclado Redragon';
UPDATE Producto SET Precio = 400, PrecioMayor = 350 WHERE Nombre = 'Monitor Samsung';
UPDATE Producto SET Precio = 300, PrecioMayor = 250 WHERE Nombre = 'Impresora HP';




INSERT INTO Cliente (Nombre, CedulaRnc, TipoDocumento) VALUES
('Ambiorix', '00123456789', 1),
('Wilmer', '132456789', 3),
('Jhonny', 'A1234567', 2);




select * from Producto
SELECT * FROM Factura
SELECT * FROM DetalleFactura