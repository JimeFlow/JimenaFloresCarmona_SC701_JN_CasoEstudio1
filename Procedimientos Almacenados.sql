-- PROCEDIMIENTOS ALMACENADOS
USE [CasoEstudio]
GO

-- Insertar Ejercicio
CREATE PROCEDURE InsertarEjercicio
    @Identificacion VARCHAR(15),
    @Fecha DATETIME,
    @Monto DECIMAL(10,2),
    @TipoEjercicio INT
AS
BEGIN
    INSERT INTO Ejercicios (Identificacion, Fecha, Monto, TipoEjercicio)
    VALUES (@Identificacion, @Fecha, @Monto, @TipoEjercicio)
END
GO

-- Consultar Ejercicios
CREATE PROCEDURE ConsultarEjercicios
AS
BEGIN
    SELECT e.Fecha, e.Monto, t.DescripcionTipoEjercicio, e.Identificacion
    FROM Ejercicios e
    INNER JOIN TiposEjercicio t ON e.TipoEjercicio = t.TipoEjercicio
END
GO