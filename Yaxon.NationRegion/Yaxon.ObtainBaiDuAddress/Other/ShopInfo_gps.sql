CREATE TABLE [dbo].[ShopInfo_gps](
	[ShopID] [varchar](50) NULL,
	[CustomerAddress] [varchar](500) NULL,
	[GPSState] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
	[Latitude] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
	[CityID] [varchar](50) NULL,
	[County] [varchar](50) NULL,
	[Town] [varchar](50) NULL,
	[Address] [varchar](500) NULL,
	[IsUpload] [int] NULL,
	[bd_Longitude] [varchar](50) NULL,
	[bd_Latitude] [varchar](50) NULL,
	[Uri] [varchar](max) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未上报1:上报成功2:webAPI异常3:程序内部异常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ShopInfo_gps', @level2type=N'COLUMN',@level2name=N'IsUpload'
GO


