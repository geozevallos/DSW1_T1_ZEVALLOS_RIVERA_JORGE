CREATE DATABASE DSW1_T1;

-------- Pregunta 1
CREATE TABLE Cliente(
    id INT PRIMARY KEY,
    nom_cli VARCHAR(255),
    direccion VARCHAR(255)
);

CREATE TABLE Vendedor(
    id INT PRIMARY KEY,
    nom_ven VARCHAR(255) NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    ciudad VARCHAR(100)
);

CREATE TABLE Ventas_Cab(
    num_vta INT PRIMARY KEY,
    fec_vta DATE,
    id_cliente INT,
    id_vendedor INT,
    tot_vta DECIMAL(10,2),
    FOREIGN KEY (id_cliente) REFERENCES Cliente(id),
    FOREIGN KEY (id_vendedor) REFERENCES Vendedor(id)
);


INSERT INTO Cliente (id, nom_cli, direccion) VALUES
(1, 'Juan Perez', 'Av. Lima 123'),
(2, 'Maria Lopez', 'Jr. Cusco 456'),
(3, 'Carlos Ruiz', 'Av. Arequipa 789'),
(4, 'Ana Torres', 'Jr. Puno 321'),
(5, 'Luis Gomez', 'Av. Tacna 654'),
(6, 'Sofia Diaz', 'Jr. Piura 987'),
(7, 'Pedro Castillo', 'Av. Brasil 111'),
(8, 'Lucia Ramos', 'Jr. Ica 222'),
(9, 'Miguel Castro', 'Av. Grau 333'),
(10, 'Elena Vargas', 'Jr. Ancash 444');



INSERT INTO Vendedor (id, nom_ven, direccion, email, ciudad) VALUES
(1, 'Jose Ramirez', 'Av. Norte 100', 'jose@correo.com', 'Lima'),
(2, 'Carmen Flores', 'Jr. Sur 200', 'carmen@correo.com', 'Arequipa'),
(3, 'Diego Soto', 'Av. Este 300', 'diego@correo.com', 'Cusco'),
(4, 'Patricia Leon', 'Jr. Oeste 400', 'patricia@correo.com', 'Trujillo'),
(5, 'Ricardo Vega', 'Av. Central 500', 'ricardo@correo.com', 'Piura');



INSERT INTO Ventas_Cab (num_vta, fec_vta, id_cliente, id_vendedor, tot_vta) VALUES
(1, '2024-01-10', 1, 1, 150.50),
(2, '2024-02-15', 2, 2, 200.00),
(3, '2024-03-20', 3, 3, 350.75),
(4, '2024-04-05', 4, 1, 120.00),
(5, '2024-05-18', 5, 2, 500.90),
(6, '2024-06-25', 6, 3, 80.40),
(7, '2024-07-30', 7, 4, 220.10),
(8, '2024-08-12', 8, 5, 310.60),
(9, '2024-09-14', 9, 1, 95.00),
(10, '2024-10-01', 10, 2, 450.00),
(11, '2023-11-11', 1, 3, 130.25),
(12, '2023-12-22', 2, 4, 275.80),
(13, '2024-01-05', 3, 5, 600.00),
(14, '2024-02-28', 4, 1, 75.50),
(15, '2024-03-15', 5, 2, 180.75);


CREATE PROCEDURE usp_obtener_ventas_por_anio
    @year INT
AS
BEGIN
    SELECT num_vta, fec_vta, c.nom_cli,
	v.nom_ven, tot_vta
	from Ventas_Cab vc
	join Cliente c
	on c.id = vc.id_cliente
	join Vendedor v
	on v.id = vc.id_vendedor
	where YEAR(fec_vta) = @year;
END;
GO

EXEC usp_obtener_ventas_por_anio @year = 2024;



-------- Pregunta 2

CREATE TABLE Categorias (
    cod_cat INT NOT NULL PRIMARY KEY,
    nombre VARCHAR(15) NOT NULL,
    descripcion TEXT NULL
);

INSERT INTO Categorias (cod_cat, nombre, descripcion) VALUES
(1, 'Bebidas', 'Gaseosas, cafe, te, cervezas y maltas'),
(2, 'Condimentos', 'Salsas dulces y picantes, delicias, comida para untar y aderezos'),
(3, 'Reposteria', 'Postres, dulces y pan dulce'),
(4, 'Lacteos', 'Quesos'),
(5, 'Granos/Cereales', 'Pan, galletas, pasta y cereales'),
(6, 'Carnes', 'Carnes preparadas'),
(7, 'Frutas/Verduras', 'Frutas secas y queso de soja'),
(8, 'Pescado/Marisco', 'Pescados, mariscos y algas');


Create table Articulos(
cod_art char(5) primary key,
nom_art varchar(50) not null,
uni_med varchar(25) not null,
pre_art decimal(7,2),
stk_art int,
cod_cat int references Categorias
)
go


CREATE PROCEDURE usp_obtener_categorias
AS
BEGIN
    SELECT * from Categorias;
END;
GO

EXEC usp_obtener_categorias;



INSERT INTO Articulos (cod_art, nom_art, uni_med, pre_art, stk_art, cod_cat) VALUES
('A0001', 'Coca Cola', 'Botella 500ml', 1.50, 100, 1),
('A0002', 'Pepsi', 'Botella 500ml', 1.40, 120, 1),
('A0003', 'Te Lipton', 'Caja 20 sobres', 2.00, 80, 1),
('A0004', 'Ketchup Heinz', 'Botella 250ml', 3.00, 50, 2),
('A0005', 'Mostaza French', 'Botella 200ml', 2.50, 60, 2),
('A0006', 'Pan de Molde', 'Paquete 500g', 2.20, 40, 5),
('A0007', 'Galletas Oreo', 'Paquete 12 uds', 1.80, 70, 5),
('A0008', 'Queso Manchego', 'Bloque 250g', 4.50, 30, 4),
('A0009', 'Yogurt Natural', 'Botella 1L', 2.00, 50, 4),
('A0010', 'Chocolate', 'Barra 100g', 1.50, 90, 3),
('A0011', 'Brownie', 'Unidad 50g', 2.50, 60, 3),
('A0012', 'Pollo Asado', 'Unidad 1kg', 7.00, 20, 6),
('A0013', 'Carne Molida', 'Paquete 500g', 5.50, 25, 6),
('A0014', 'Manzana Roja', 'Kg', 0.80, 100, 7),
('A0015', 'Salmón Fresco', 'Filete 200g', 12.00, 15, 8);


CREATE OR ALTER PROCEDURE usp_obtener_articulos
AS
BEGIN
    SELECT a.*, c.nombre from Articulos a
	join Categorias c
	on a.cod_cat = c.cod_cat;
END;
GO

EXEC usp_obtener_articulos;


CREATE PROCEDURE usp_insertar_articulos
    @CodArt CHAR(5),
    @NomArt VARCHAR(50),
    @UniMed VARCHAR(25),
    @PreArt DECIMAL(7,2),
    @StkArt INT,
    @CodCat INT
AS
BEGIN
    INSERT INTO Articulos (cod_art, nom_art, uni_med, pre_art, stk_art, cod_cat)
    VALUES (@CodArt, @NomArt, @UniMed, @PreArt, @StkArt, @CodCat);
END
GO;


EXEC usp_insertar_articulos
    @CodArt = 'A0016',
    @NomArt = 'Jugo Naranja',
    @UniMed = 'Botella 1L',
    @PreArt = 2.50,
    @StkArt = 50,
    @CodCat = 1; 