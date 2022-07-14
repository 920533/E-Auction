Create Table [dob].[Product](
[Id][bigint] Identity(1,1) Not Null,
[ProductId] [nvarchar](50)Not Null,
[ProductName] [nvarchar](30)Not Null,
[ShortDeceription] [nvarchar](30)  Null,
[DetailedDeceription] [nvarchar](30)  Null,
[Category] [int] Not Null,
[StartingPrice] [int] Null,
[BidEndDate] [nvarchar](30) Not Null,
[CreatedDate] [datetime] Null,
[LastModifiedDate][datetime] Null,
CONSTRAINT PK_Product Primary key clustered 
(
[Id] Asc
)
with(PAD_INDEX=OFF,STATISTICS_NORECOMPUTE=OFF,IGNORE_DUP_KEY=OFF,ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON[PRIMARY]
)on [primary] TEXTIMAGE_ON [Primary]
GO

Create Nonclustered index IX_Product_ProductId
ON
dbo.Product(ProductId)
GO