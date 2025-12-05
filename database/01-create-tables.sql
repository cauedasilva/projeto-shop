CREATE TABLE [dbo].[CustomerDetails](
	[CustomerId] INT NULL,
	[FirstName] VARCHAR(50) NULL,
	[LastName] VARCHAR(50) NULL,
	[Email] VARCHAR(50) NULL,
	[RegistrationDate] DATE NULL,
	[Phone] VARCHAR(15) NULL,
	[NewCustomerId] INT IDENTITY(1,1) NOT NULL,
	CONSTRAINT UQ_Inventory_CustomerId UNIQUE (CustomerId)
);
GO

CREATE TABLE [dbo].[Inventory](
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductId] INT NULL,
	[ProductName] VARCHAR(100) NULL,
	[AvailableQuantity] INT NULL,
	[ReorderPoint] INT NULL,
	CONSTRAINT UQ_Inventory_ProductId UNIQUE (ProductId)
);
GO