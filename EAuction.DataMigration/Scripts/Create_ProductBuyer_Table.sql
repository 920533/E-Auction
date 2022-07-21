Create Table [dbo].[ProductBuyer](
[Id][bigint] Identity(1,1) Not Null,
[BuyerProductId] nvarchar(50)Not Null,
[ProductId] nvarchar(50)Not Null,
[UserId] nvarchar(50)  Not Null,
[BidAmount] decimal(20,2)  Not Null,
[CreatedDate] datetime Null,
[LastModifiedDate] datetime Null,
CONSTRAINT PK_ProductBuyer PRIMARY KEY clustered([BuyerProductId] ),

CONSTRAINT fkey_name FOREIGN KEY (UserId)
    REFERENCES [User] (UserId)
    ON DELETE CASCADE
    ON UPDATE CASCADE

)

Create Nonclustered index IX_ProductBuyer_BuyerProductId
ON
[dbo].[ProductBuyer](BuyerProductId)
GO