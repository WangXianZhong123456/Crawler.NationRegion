CREATE TABLE [dbo].[UCML_NationRegion](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentNode] [varchar](50) NULL,
	[Node] [varchar](50) NULL,
	[Code] [varchar](50) NULL,
	[TypeCode] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Href] [varchar](200) NULL,
	[IsLowerUp] [int] NULL,
	[Level] [int] NULL,
 CONSTRAINT [PK_UCML_NationRegion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Index [Index_Code]    Script Date: 02/08/2018 17:38:56 ******/
CREATE NONCLUSTERED INDEX [Index_Code] ON [dbo].[UCML_NationRegion] 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO



