CREATE PROCEDURE [dbo].[InsertOrUpdateUser]
@UserId nvarchar(50),
@FirstName nvarchar(30),
@LastName nvarchar(30),
@Address nvarchar(max),
@City nvarchar(30),
@State nvarchar(30),
@Pin nvarchar(30),
@Phone nvarchar(30),
@Email nvarchar(30),
@UserName nvarchar(50),
@Password nvarchar(50),
@UserType nvarchar(50)
AS 
BEGIN
	BEGIN TRANSACTION tran1
	BEGIN TRY
		DECLARE @IdToUpdate bigint
		SET @IdToUpdate= (SELECT TOP 1 Id from [User] Where UserId=@UserId order by Id desc)
		IF(@IdToUpdate IS NOT NULL)
		  BEGIN
			Update [User] set UserId=@UserId,LastName=@LastName,Address=@Address,City=@City,State=@State,Pin=@Pin,Phone=@Phone,
			Password=@Password,UserType=@UserType,LastModifiedDate=GETUTCDATE()
			WHERE Id= @IdToUpdate
		  END
		ELSE
		  BEGIN
		    Insert into [User](UserId,FirstName,LastName,Address,City,State,Pin,Phone,Email,UserName,Password,UserType,CreatedDate,LastModifiedDate)
			values(@UserId,@FirstName,@LastName,@Address,@City,@State,@Pin,@Phone,@Email,@UserName,@Password,@UserType,GETUTCDATE(),GETUTCDATE())
		  END
	COMMIT TRANSACTION tran1
	END TRY
	BEGIN CATCH
	ROLLBACK TRANSACTION tran1
	END CATCH
END
GO