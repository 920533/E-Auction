CREATE PROCEDURE [dbo].[InsertOrUpdateProductBuyer]
@BuyerProductId nvarchar(50),
@ProductId nvarchar(50),
@UserId nvarchar(50),
@BidAmount decimal(20,2)
AS 
BEGIN
	BEGIN TRANSACTION tran1
	BEGIN TRY
		DECLARE @IdToUpdate bigint
		SET @IdToUpdate= (SELECT TOP 1 Id from ProductBuyer Where ProductId=@ProductId order by Id desc)
		IF(@IdToUpdate IS NOT NULL)
		  BEGIN
			Update ProductBuyer set BuyerProductId=@BuyerProductId,ProductId=@ProductId,UserId=@UserId,
			BidAmount=@BidAmount,LastModifiedDate=GETUTCDATE()
			WHERE Id= @IdToUpdate
		  END
		ELSE
		  BEGIN
		    Insert into ProductBuyer(BuyerProductId,ProductId,UserId,BidAmount,CreatedDate,LastModifiedDate)
			values(@BuyerProductId,@ProductId,@UserId,@BidAmount,GETUTCDATE(),GETUTCDATE())
		  END
	COMMIT TRANSACTION tran1
	END TRY
	BEGIN CATCH
	ROLLBACK TRANSACTION tran1
	END CATCH
END
GO