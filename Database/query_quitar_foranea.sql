ALTER TABLE tbl_DetallesOrdenes
DROP CONSTRAINT FK__tbl_Detal__Codig__14270015;

ALTER TABLE tbl_DetallesOrdenes
DROP COLUMN CodigoOrdenEnc;


Select * from tbl_DetallesOrdenes;
Select * from tbl_EncabezadoOrdenes;

SELECT 
    fk.name AS nombre_restriccion,
    tp.name AS tabla_padre,
    cp.name AS columna_padre,
    tr.name AS tabla_referenciada,
    cr.name AS columna_referenciada
FROM 
    sys.foreign_keys AS fk
INNER JOIN 
    sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN 
    sys.tables AS tp ON fk.parent_object_id = tp.object_id
INNER JOIN 
    sys.columns AS cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN 
    sys.tables AS tr ON fk.referenced_object_id = tr.object_id
INNER JOIN 
    sys.columns AS cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
WHERE 
    tp.name = 'tbl_DetallesOrdenes';

Select CodigoOrdenEnc, CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrd, Estado, UsuarioSistema, FechaSistema from tbl_EncabezadoOrdenes;

Insert into tbl_EncabezadoOrdenes(CodigoOrdenDet, CodigoCliente, CodigoMesa, CodigoEmpleado, FechaOrden, MontoTotalOrd, Estado, UsuarioSistema, FechaSistema) 
values (15, 1, 2, 1, '04/04/2025', 200, 'activo', 'AugustoMoran', '12/12/2025')

TRUNCATE TABLE tbl_DetallesOrdenes;
DBCC CHECKIDENT('tbl_DetallesOrdenes', RESEED, 1)
DBCC CHECKIDENT ('esquema.nombretabla', RESEED, 0);

TRUNCATE TABLE tbl_Menus;
DBCC CHECKIDENT('tbl_Menus', RESEED, 0)

DBCC CHECKIDENT('tbl_EncabezadoOrdenes', RESEED, 1)

sELEct * from tbl_Menus;