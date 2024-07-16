-- Crear la base de datos
CREATE DATABASE ArcadeX;
GO

-- Usar la base de datos
USE ArcadeX;
GO

-- Crear tabla Roles
CREATE TABLE Roles (
    RolID INT PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL
);
GO

-- Crear tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Identificacion NVARCHAR(100) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Contrasenna NVARCHAR(20) NOT NULL,
    RolID INT NOT NULL,
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);
GO

-- Crear tabla Errores
CREATE TABLE Errores (
    ErrorID INT PRIMARY KEY IDENTITY(1,1),
    Mensaje NVARCHAR(4000) NOT NULL,
    FechaHora DATETIME DEFAULT GETDATE()
);
GO

-- Procedimiento almacenado para registrar clientes
CREATE PROCEDURE [dbo].[RegistrarCliente]
    @Identificacion NVARCHAR(100),
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(100),
    @Contrasenna NVARCHAR(20),
    @RolID INT
AS
BEGIN
    BEGIN TRY
        -- Insertar nuevo cliente en la tabla Clientes
        INSERT INTO Clientes (Identificacion, Nombre, Email, Contrasenna, RolID)
        VALUES (@Identificacion, @Nombre, @Email, @Contrasenna, @RolID);
    END TRY
    BEGIN CATCH
        -- Manejar errores
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        -- Registrar el error en la tabla de Errores
        INSERT INTO Errores (Mensaje) VALUES (@ErrorMessage);
        -- Re-lanzar el error
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Procedimiento almacenado para iniciar sesión
CREATE PROCEDURE [dbo].[IniciarSesion]
    @Email NVARCHAR(100),
    @Contrasenna NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ClienteID INT
    DECLARE @Nombre NVARCHAR(100)
    DECLARE @RolID INT

    -- Buscar el cliente con el email y contraseña proporcionados
    SELECT @ClienteID = ClienteId, @Nombre = Nombre, @RolID = RolID
    FROM Clientes
    WHERE Email = @Email AND Contrasenna = @Contrasenna

    -- Si se encontró el cliente
    IF @ClienteID IS NOT NULL
    BEGIN
        -- Devolver la información del cliente
        SELECT 
            @ClienteID AS ClienteID,
            @Nombre AS Nombre,
            @RolID AS RolID,
            r.Nombre AS NombreRol
        FROM Roles r
        WHERE r.RolID = @RolID
    END
    ELSE
    BEGIN
        -- Si no se encontró el cliente, devolver NULL
        SELECT NULL AS ClienteID, NULL AS Nombre, NULL AS RolID, NULL AS NombreRol
    END
END
GO

-- Insertar roles básicos
INSERT INTO Roles (RolID,Nombre) VALUES (1,'Administrador'), (2,'Usuario');
GO