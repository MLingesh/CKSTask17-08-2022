/****** Object:  StoredProcedure [dbo].[sp_UpdateUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_UpdateUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_SaveUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUserAddressDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_SaveUserAddressDetails]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetailsByID]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetUserDetailsByID]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetUserDetails]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteUserDetails]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP TABLE IF EXISTS [dbo].[UserDetails]
GO
/****** Object:  Table [dbo].[UserAddressDetails]    Script Date: 17-08-2022 20:53:06 ******/
DROP TABLE IF EXISTS [dbo].[UserAddressDetails]
GO
/****** Object:  Table [dbo].[UserAddressDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddressDetails](
	[UserAddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserDetailID] [bigint] NULL,
	[Address1] [varchar](200) NULL,
	[Address2] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Pincode] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
 CONSTRAINT [PK_UserAddressDetails] PRIMARY KEY CLUSTERED 
(
	[UserAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DOB] [datetime] NULL,
	[Mobile] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[LastModfiedOn] [datetime] NULL,
	[LastModifiedBy] [bigint] NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [bigint] NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteUserDetails]   
@UserDetailID BIGINT
AS    
BEGIN    
UPDATE UserDetails SET IsActive = 0,DeletedBy = 1,DeletedOn = GETDATE()  
WHERE UserDetailID = @UserDetailID  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserDetails]
AS
BEGIN
SELECT UserDetailID,Name,DOB,Mobile,Gender,Email FROM UserDetails
WHERE IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserDetailsByID]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserDetailsByID]   
@UserDetailID BIGINT
AS
BEGIN
SELECT UserDetailID,Name,DOB,Mobile,Gender,Email FROM UserDetails
WHERE UserDetailID = @UserDetailID AND IsActive= 1

SELECT UserAddressID,UserDetailID,Address1,Address2,City,State,Pincode FROM UserAddressDetails
WHERE UserDetailID = @UserDetailID AND IsActive= 1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUserAddressDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveUserAddressDetails]  
@Flag VARCHAR(50),  
@UserDetailID BIGINT,  
@Address1 VARCHAR(50) =NULL,   
@Address2 VARCHAR(50) =NULL,  
@City VARCHAR(50) =NULL,  
@State VARCHAR(50) =NULL,  
@Pincode VARCHAR(50) =NULL  
AS  
BEGIN  
IF(UPPER(@Flag) = UPPER('Save'))  
  
BEGIN  
INSERT INTO UserAddressDetails(UserDetailID,Address1,Address2,City,State,Pincode,IsActive,CreatedOn,CreatedBy)  
VALUES(@UserDetailID,@Address1,@Address2,@City,@State,@Pincode,1,GETDATE(),1)  
END  
ELSE IF(UPPER(@Flag) = UPPER('Update'))  
BEGIN  
UPDATE UserAddressDetails SET Address1= @Address1, Address2=@Address2,City=@City,  
State=@State,Pincode = @Pincode,LastModifiedOn = GETDATE(),LastModifiedBy=1
WHERE UserAddressID = @UserDetailID  
END  
ELSE IF(UPPER(@Flag) = UPPER('Delete'))  
BEGIN  
UPDATE UserAddressDetails SET IsActive = 0,
DeletedOn=GETDATE(),DeletedBy=1 WHERE UserAddressID = @UserDetailID  
END  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveUserDetails]
@Name VARCHAR(50),
@DOB VARCHAR(50),
@Gender VARCHAR(50),
@Mobile VARCHAR(50),
@Email VARCHAR(50)
AS
BEGIN
DECLARE @ID BIGINT  

INSERT INTO UserDetails(Name,DOB,Mobile,Gender,Email,IsActive,CreatedOn,CreatedBy)
VALUES(@Name,@DOB,@Mobile,@Gender,@Email,1,GETDATE(),1)
SET @ID = SCOPE_IDENTITY()  
  
 SELECT @ID AS CaseDetailID 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserDetails]    Script Date: 17-08-2022 20:53:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateUserDetails] 
@UserDetailID BIGINT,
@Name VARCHAR(50),  
@DOB VARCHAR(50),  
@Gender VARCHAR(50),  
@Mobile VARCHAR(50),  
@Email VARCHAR(50)  
AS  
BEGIN  
UPDATE UserDetails SET Name = @Name,DOB=@DOB,Mobile=@Mobile,Gender = @Gender,Email = @Email
,LastModfiedOn = GETDATE(),LastModifiedBy = 1
WHERE UserDetailID = @UserDetailID
END  
GO
