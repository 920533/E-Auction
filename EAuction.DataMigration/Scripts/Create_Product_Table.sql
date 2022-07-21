Create Table [dbo].[Product](
[Id][bigint] Identity(1,1) Not Null,
[ProductId] nvarchar(50)Not Null,
[ProductName] nvarchar(30)Not Null,
[ShortDescription] nvarchar(30)  Null,
[DetailedDescription] nvarchar(30)  Null,
[Category] int Not Null,
[StartingPrice] decimal(20,2) Null,
[BidEndDate] nvarchar(30) Not Null,
[CreatedDate] datetime Null,
[LastModifiedDate] datetime Null,
CONSTRAINT PK_Product Primary key clustered 
(
[ProductId]
)
with(PAD_INDEX=OFF,STATISTICS_NORECOMPUTE=OFF,IGNORE_DUP_KEY=OFF,ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON[PRIMARY]
)on [primary]
GO

Create Nonclustered index IX_Product_ProductId
ON
dbo.Product(ProductId)
GO
