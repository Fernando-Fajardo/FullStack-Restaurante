CREATE TABLE tbl_Menus
(	
	CodigoMenu int primary key identity(1,1) not null,
	Nombre Varchar(50),
	Ingredientes Varchar(255),
	Categoria Varchar(50),
	Precio Decimal(10,2),
	Estado Varchar(20),
	UsuarioSistema Varchar(50),
	FechaSistema Datetime
);

Select * from tbl_Menus;

Insert into tbl_Menus(Nombre, Ingredientes, Categoria, Precio, Estado, UsuarioSistema, FechaSistema)
values ('Hamburguesas', 'Carne de res, queso chedar,lechuga, tomate, especias', 'Almuerzos', 100, 'Activo', 'Gerber Canahuí', '11/05/2025');

Update tbl_Menus set Nombre = 'Campero', Ingredientes = 'Pollo y papas', Categoria = 'Cenas', Precio = 75, Estado = 'Inactivo', UsuarioSistema = 'Fernando Fajardo', FechaSistema = '12/05/2025' where CodigoMenu = 1;

CREATE TABLE tbl_EncabezadoOrdenes
(
	CodigoOrdenEnc int primary key identity(1,1) not null,
	CodigoCliente INT,
	CodigoMesa INT,
	CodigoEmpleado INT,
	FechaOrden DATETIME,
	MontoTotalOrd DECIMAL(10,2),
	Estado VARCHAR(20),
	UsuarioSistema VARCHAR(50),
	FechaSistema DATETIME
);

Select * From tbl_EncabezadoOrdenes;

ALTER TABLE tbl_EncabezadoOrdenes ADD Foreign key (CodigoCliente) references tbl_clientes (CodigoCliente);
ALTER TABLE tbl_EncabezadoOrdenes ADD Foreign key (CodigoMesa) references tbl_mesas (CodigoMesa);
ALTER TABLE tbl_EncabezadoOrdenes ADD Foreign key (CodigoEmpleado) references tbl_empleados (CodigoEmpleado);

Insert into tbl_EncabezadoOrdenes(FechaOrden, MontoTotalOrd, Estado, UsuarioSistema, FechaSistema)
values ('11/05/2025', 100, 'Activo', 'Fernando Fajardo', '12/05/2025');

ALTER TABLE tbl_EncabezadoOrdenes
ADD CodigoOrdenDet INT;

ALTER TABLE tbl_EncabezadoOrdenes Add Foreign key (CodigoOrdenDet) references tbl_DetallesOrdenes (CodigoOrdenDet);

CREATE TABLE tbl_DetallesOrdenes
(
	CodigoOrdenDet int primary key identity(1,1) not null,
	CodigoOrdenEnc INT,
	CodigoMenu INT,
	Cantidad INT,
	PrecioUnitario DECIMAL(10,2),
	PrecioTotal DECIMAL(10,2),
	UsuarioSistema VARCHAR(20),
	FechaSistema DATETIME,
	Foreign key (CodigoOrdenEnc) references tbl_EncabezadoOrdenes (CodigoOrdenEnc),
	Foreign key (CodigoMenu) references tbl_Menus (CodigoMenu)
);

Insert into tbl_DetallesOrdenes(CodigoOrdenEnc, CodigoMenu, Cantidad, PrecioUnitario, PrecioTotal, UsuarioSistema, FechaSistema) values (@CodigoOrdenEnc, @CodigoMenu, @Cantidad, @PrecioUnitario, @PrecioTotal, @UsuarioSistema, @FechaSistema);

Update tbl_DetallesOrdenes
set CodigoOrdenEnc = @CodigoOrdenEnc,
	CodigoMenu = @CodigoMenu,
	Cantidad = @Cantidad,
	PrecioUnitario = @PrecioUnitario,
	PrecioTotal = @PrecioTotal,
	UsuarioSistema = @UsuarioSistema,
	FechaSistema = @FechaSistema
where CodigoOrdenDet = @CodigoOrdenDet;

Select * From tbl_DetallesOrdenes;

Select * From tbl_clientes;

Select CodigoCliente, Nombre from tbl_clientes;

Select * from tbl_DetallesOrdenes;

Select CodigoOrdenEnc from tbl_DetallesOrdenes;

Select PrecioTotal from tbl_DetallesOrdenes where CodigoOrdenDet = @CodigoOrdenDet;

--Creacion de las tablas

Create table tbl_empleados 
(
	CodigoEmpleado int primary key identity(1,1) not null,
	Nombre Varchar(50),
	Cargo Varchar(50),
	Salario Decimal(10,2),
	FechaContratacion Datetime,
	Estado varchar(20),
	UsuarioSistema Varchar(50),
	FechaSistema Datetime
);

Create table tbl_mesas
(
	CodigoMesa int primary key identity(1,1) not null,
	NumeroMesa int,
	CantidadSillas int,
	Ubicacion Varchar(50),
	TipoMesa Varchar(50),
	Estado varchar(20),
	UsuarioSistema Varchar(50),
	FechaSistema Datetime
);


Create table tbl_clientes
(
	CodigoCliente int primary key identity(1,1) not null,
	Nombre Varchar(50),
	Nit Varchar(20),
	Telefono Varchar(15),
	Categoria Varchar(50),
	Estado varchar(20),
	UsuarioSistema Varchar(50),
	FechaSistema Datetime
);


Create table tbl_usuarios
(
	CodigoUsuario int primary key identity(1,1) not null,
	CodigoEmpleado int,
	NombreUsuario Varchar(50),
	Contrasenia Varchar(50),
	Rol Varchar(50),
	Estado Varchar(20),
	UsuarioSistema Varchar(50),
	FechaSistema Datetime
	Foreign key (CodigoEmpleado) references tbl_empleados(CodigoEmpleado)
);


--Consultar datos de la tabla
 SELECT * FROM	tbl_empleados;
 SELECT * FROM	tbl_mesas;
 SELECT * FROM	tbl_clientes;
 SELECT * FROM	tbl_usuarios;


--script para agregar datos a tabla clientes
  INSERT INTO tbl_empleados(Nombre, Cargo, Salario, FechaContratacion, Estado, UsuarioSistema, FechaSistema)
VALUES
('AugustoSources', 'Gerente', 25000, '2020-04-10', 'Activo', 'GerberCanahui', '2025-05-13');

  INSERT INTO tbl_mesas(CodigoMesa, NumeroMesa, CantidadSillas, Ubicacion, TipoMesa, Estado, UsuarioSistema, FechaSistema)
VALUES
(3, 4, 'Centro', 'Interior', 'Activo', 'GerberCanahui', '2025-05-13');

  INSERT INTO tbl_clientes(Nombre, Nit, Telefono, Categoria, Estado, UsuarioSistema, FechaSistema)
VALUES
('Elmer Moran', '11-22334', '22334455', 'Nuevo', 'Activo', 'GerberCanahui', '2025-05-13');

  INSERT INTO tbl_usuarios(CodigoEmpleado, NombreUsuario, Contrasenia, Rol, Estado, UsuarioSistema, FechaSistema)
VALUES
(6, 'GerberCanahui', '123456', 'Admin', 'Activo', 'AugustoMoran', '2025-05-13');


--Script para actualizar datos
Update tbl_empleados
set Nombre = 'AugustoSources',
	Cargo = 'Cocinero',
	Salario = 15000,
	FechaContratacion = '2020-04-10',
	Estado = 'Activo',
	UsuarioSistema = 'GerberCanahui',
	FechaSistema = '2025-05-13'
where CodigoEmpleado = 1;

Update tbl_mesas
set NumeroMesa = 4,
	CantidadSillas = 3,
	Ubicacion = 'Centro',
	TipoMesa = 'Exterior',
	Estado = 'Activo',
	UsuarioSistema = 'GerberCanahui',
	FechaSistema = '2025-05-13'
where CodigoMesa = 1;

Update tbl_clientes
set Nombre = 'Elmer Moran',
	Nit = '11-22334',
	Telefono = '22334456',
	Categoria = 'Ocasional',
	Estado = 'Activo',
	UsuarioSistema = 'GerberCanahui',
	FechaSistema = '2025-05-13'
where CodigoCliente = 1;

Update tbl_usuarios
set CodigoEmpleado = 7 where CodigoUsuario = 3;