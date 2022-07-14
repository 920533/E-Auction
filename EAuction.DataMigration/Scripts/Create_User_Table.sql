Create Table [dob].[User](
[Id][bigint] Identity(1,1) Not Null,
[UserId] [nvarchar](50)Not Null,
[FirstName] [nvarchar](30)Not Null,
[LastName] [nvarchar](30)  Null,
[Address] [nvarchar](max)  Null,
[City] [nvarchar](30)  Null,
[State] [nvarchar](30) Null,
[Pin] [nvarchar](30) Not Null,
[Phone] [nvarchar](30) Not Null,
[Email] [nvarchar](30) Not Null,
[UserName] [nvarchar](30) Not Null,
[Password] [nvarchar](30) Not Null,
[UserType] [nvarchar](30) Not Null,
[CreatedDate] [datetime] Null,
[LastModifiedDate][datetime] Null,
CONSTRAINT PK_User Primary key clustered 
(
[Id] Asc
)
with(PAD_INDEX=OFF,STATISTICS_NORECOMPUTE=OFF,IGNORE_DUP_KEY=OFF,ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON[PRIMARY]
)on [primary] TEXTIMAGE_ON [Primary]
GO

Create Nonclustered index IX_User_UserId
ON
[dbo].[User](UserId)
GO