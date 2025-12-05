CREATE PROCEDURE [dbo].[sp_DeleteCustomerDetails]
    @CustomerId INT
AS
BEGIN
    DELETE FROM CustomerDetails
    WHERE CustomerId = @CustomerId;
END
GO

CREATE PROCEDURE [dbo].[sp_DeleteInventoryDetails]
    @ProductId INT
AS
BEGIN
    DELETE FROM Inventory
    WHERE ProductId = @ProductId;
END
GO

CREATE PROCEDURE [dbo].[sp_GetCustomerDetails]
AS
BEGIN
    SELECT * FROM CustomerDetails;
END
GO

CREATE PROCEDURE [dbo].[sp_GetInventoryData]
AS
BEGIN
    SELECT * FROM Inventory;
END
GO

CREATE PROCEDURE [dbo].[sp_SaveCustomerDetails]
    @CustomerId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(50),
    @RegistrationDate DATE,
    @Phone VARCHAR(15)
AS
BEGIN
    INSERT INTO CustomerDetails
    (
        CustomerId,
        FirstName,
        LastName,
        Email,
        RegistrationDate,
        Phone
    )
    VALUES
    (
        @CustomerId,
        @FirstName,
        @LastName,
        @Email,
        @RegistrationDate,
        @Phone
    );
END
GO

CREATE PROCEDURE [dbo].[sp_SaveInventoryData]
    @ProductId INT,
    @ProductName VARCHAR(100),
    @AvailableQuantity INT,
    @ReorderPoint INT
AS
BEGIN
    INSERT INTO Inventory
    (
        ProductId,
        ProductName,
        AvailableQuantity,
        ReorderPoint
    )
    VALUES
    (
        @ProductId,
        @ProductName,
        @AvailableQuantity,
        @ReorderPoint
    );
END
GO

CREATE PROCEDURE [dbo].[sp_UpdateCustomerDetails]
    @CustomerId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(50),
    @Phone VARCHAR(50),
    @RegistrationDate DATE
AS
BEGIN
    UPDATE CustomerDetails
    SET
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        Phone = @Phone,
        RegistrationDate = @RegistrationDate
    WHERE CustomerId = @CustomerId;
END
GO

CREATE PROCEDURE [dbo].[sp_UpdateInventoryData]
    @ProductId INT,
    @ProductName VARCHAR(100),
    @AvailableQuantity INT,
    @ReorderPoint INT
AS
BEGIN
    UPDATE Inventory
    SET
        ProductName = @ProductName,
        AvailableQuantity = @AvailableQuantity,
        ReorderPoint = @ReorderPoint
    WHERE ProductId = @ProductId;
END
GO
