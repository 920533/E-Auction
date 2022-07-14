CREATE PROCEDURE [dbo].[InsertOrUpdateProduct]
@ProductId nvarchar(50),
@ProductName nvarchar(30),
@ShortDeceription nvarchar(30),
@DetailedDeceription nvarchar(30),
@Category int,
@StartingPrice decimal(20,2),
@BidEndDate nvarchar(30)
AS 
BEGIN
	BEGIN TRANSACTION tran1
	BEGIN TRY
		DECLARE @IdToUpdate bigint
		SET @IdToUpdate= (SELECT TOP 1 Id from Product Where ProductId=@ProductId order by Id desc)
		IF(@IdToUpdate IS NOT NULL)
		  BEGIN
			Update Product  set ProductName=@ProductName,ShortDescription=@ShortDescription,DetailedDescription=@DetailedDescription,
			Category=@Category,StartingPrice=@StartingPrice,BidEndDate=@BidEndDate,LastModifiedDate=GETUTCDATE()
			WHERE Id= @IdToUpdate
		  END
		ELSE
		  BEGIN
		    Insert into Product(ProductId,ProductName,ShortDescription,DetailedDescription,Category,StartingPrice,BidEndDate,CreatedDate,LastModifiedDate)
			values(@ProductId,@ProductName,@ShortDescription,@DetailedDescription,@Category,@StartingPrice,@BidEndDate,GETUTCDATE(),GETUTCDATE())
		  END
	COMMIT TRANSACTION tran1
	END TRY
	BEGIN CATCH
	ROLLBACK TRANSACTION tran1
	END CATCH
END
GO