CREATE TABLE [dbo].[UCML_NationRegionExpetion](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Uri] [varchar](200) NULL,
	[Level] [int] NULL,
	[IsUpdate] [int] NULL,
	[CreateTime] [datetime] NULL,
	[ModifyTime] [datetime] NULL,
 CONSTRAINT [PK_UCML_NationRegionExpetion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


