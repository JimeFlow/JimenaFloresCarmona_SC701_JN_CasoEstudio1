-- ==================================================
-- ========== PROCEDIMIENTOS ALMACENADOS ============
-- ==================================================
USE [CasoEstudio]
GO

-- ==================================================
-- ============== Insertar Ejercicios ===============
-- ==================================================
CREATE PROCEDURE InsertEjercicio_SP
    @Identificacion VARCHAR(15),
    @Monto DECIMAL(10,2),
    @TipoDescripcion VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TipoId INT;

    -- Verificar que el tipo exista
    SELECT @TipoId = TipoEjercicio
    FROM TiposEjercicio
    WHERE DescripcionTipoEjercicio = @TipoDescripcion;

    IF @TipoId IS NULL
    BEGIN
        -- Tipo no existe
        SELECT -1 AS Result; 
        RETURN;
    END

    -- Contar tipos distintos ya solicitados por la identificación
    -- Si la solicitud actual es de un tipo nuevo y ya existen 2 tipos distintos => prohibir
    IF EXISTS (
        SELECT 1
        FROM (
            SELECT DISTINCT TipoEjercicio
            FROM Ejercicios
            WHERE Identificacion = @Identificacion
        ) AS d
        HAVING COUNT(*) >= 2
    )
    BEGIN
        -- Si ya tiene 2 tipos distintos, pero tal vez ya solicitó este mismo tipo: permitir solo si ya existe el tipo actual
        IF NOT EXISTS (
            SELECT 1
            FROM Ejercicios
            WHERE Identificacion = @Identificacion AND TipoEjercicio = @TipoId
        )
        BEGIN
            SELECT -2 AS Result; -- Límite de 2 tipos alcanzado
            RETURN;
        END
    END

    -- Insertar (Fecha se registra en el servidor)
    INSERT INTO Ejercicios (Identificacion, Fecha, Monto, TipoEjercicio)
    VALUES (@Identificacion, GETDATE(), @Monto, @TipoId);

    SELECT 1 AS Result; -- OK
END
GO

-- ==================================================
-- ============== Consultar Ejercicios ==============
-- ==================================================
CREATE PROCEDURE GetEjercicios_SP
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.Consecutivo,
        e.Identificacion,
        e.Fecha,
        e.Monto,
        t.DescripcionTipoEjercicio
    FROM Ejercicios e
    INNER JOIN TiposEjercicio t ON e.TipoEjercicio = t.TipoEjercicio
    ORDER BY e.Fecha DESC;
END
GO