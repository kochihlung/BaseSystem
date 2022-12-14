GO
/****** Object:  Table [dbo].[S_DATASOURCE]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_DATASOURCE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[DATATYPE] [int] NOT NULL,
	[TABLENAME] [nvarchar](50) NULL,
	[COLUMNNAME] [nvarchar](50) NULL,
	[DATAS] [nvarchar](50) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_S_DataSource] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_DATASOURCE_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_DATASOURCE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[TABLENAME] [nvarchar](50) NULL,
	[COLUMNNAME] [nvarchar](50) NULL,
	[DATAS] [nvarchar](50) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[DATATYPE] [int] NOT NULL,
 CONSTRAINT [PK_BS_S_DataSource_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MDSETUP]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MDSETUP](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[SETTYPE] [varchar](20) NULL,
 CONSTRAINT [PK_BS_S_MDSETUP] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MDSETUP_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MDSETUP_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[SETTYPE] [varchar](20) NULL,
 CONSTRAINT [PK_BS_S_MDSETUP_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MDSETUPDTL]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MDSETUPDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[S_MDSETUP_ID] [varchar](40) NOT NULL,
	[UI] [varchar](50) NULL,
	[ISMODLING] [int] NULL,
	[SOURCE] [varchar](50) NULL,
	[READONLY] [int] NULL,
	[REQUIRED] [int] NULL,
	[WIDTH] [int] NULL,
	[ALIGN] [varchar](50) NULL,
	[COLNAME] [varchar](50) NULL,
	[TEXT] [varchar](50) NULL,
	[VAL] [varchar](50) NULL,
 CONSTRAINT [PK_BS_S_MDSETUPDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MDSETUPDTL_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MDSETUPDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[S_MDSETUP_ID] [varchar](40) NOT NULL,
	[UI] [varchar](50) NULL,
	[ISMODLING] [int] NULL,
	[SOURCE] [varchar](50) NULL,
	[READONLY] [int] NULL,
	[REQUIRED] [int] NULL,
	[WIDTH] [int] NULL,
	[ALIGN] [varchar](50) NULL,
	[COLNAME] [varchar](50) NULL,
	[TEXT] [varchar](50) NULL,
	[VAL] [varchar](50) NULL,
 CONSTRAINT [PK_BS_S_MDSETUPDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MENU]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MENU](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[ISACTION] [int] NULL,
	[IsPublic] [int] NULL,
	[ICONCLS] [varchar](50) NULL,
	[URL] [varchar](50) NULL,
	[SORT] [varchar](40) NULL,
 CONSTRAINT [PK_BS_S_MENU] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_MENU_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_MENU_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[ISACTION] [int] NULL,
	[IsPublic] [int] NULL,
	[ICONCLS] [varchar](50) NULL,
	[URL] [varchar](50) NULL,
	[SORT] [varchar](40) NULL,
 CONSTRAINT [PK_BS_S_MENU_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_ROLE]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_ROLE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_S_ROLE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_ROLE_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_ROLE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_S_ROLE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_ROLEDTL]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_ROLEDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[S_ROLE_ID] [varchar](40) NOT NULL,
	[S_MENU_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_S_ROLEDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_ROLEDTL_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_ROLEDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[S_ROLE_ID] [varchar](40) NOT NULL,
	[S_MENU_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_S_ROLEDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_SNCONTROL]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_SNCONTROL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[INDEXTARGET] [varchar](200) NULL,
	[INDEXDATE] [varchar](40) NULL,
	[SEQUENCE] [int] NULL,
 CONSTRAINT [PK_BS_S_SNCONTROL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_SNCONTROL_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_SNCONTROL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[INDEXTARGET] [varchar](200) NULL,
	[INDEXDATE] [varchar](40) NULL,
	[SEQUENCE] [int] NULL,
 CONSTRAINT [PK_BS_S_SNCONTROL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERINFO]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERINFO](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[PWD] [varchar](50) NULL,
	[TOKEN] [varchar](100) NULL,
	[ExpTime] [datetime] NULL,
	[LAT] [float] NULL,
	[LON] [float] NULL,
	[ISPASS] [int] NULL,
 CONSTRAINT [PK_BS_S_UserInfo] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERINFO_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERINFO_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[PWD] [varchar](50) NULL,
	[TOKEN] [varchar](100) NULL,
	[ExpTime] [datetime] NULL,
	[LAT] [float] NULL,
	[LON] [float] NULL,
	[ISPASS] [int] NULL,
 CONSTRAINT [PK_BS_S_UserInfo_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERROLE]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERROLE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_S_USERROLE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERROLE_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERROLE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_S_USERROLE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERROLEDTL]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERROLEDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[S_USERROLE_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_S_USERROLEDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[S_USERROLEDTL_HT]    Script Date: 2022/9/22 下午 04:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[S_USERROLEDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[S_USERROLE_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_S_USERROLEDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:固定值、1:欄位資料' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_DATASOURCE', @level2type=N'COLUMN',@level2name=N'DATATYPE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存放USERID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_USERROLE', @level2type=N'COLUMN',@level2name=N'CODE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存放USERNAME' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_USERROLE', @level2type=N'COLUMN',@level2name=N'NAME'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存MenuID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_USERROLEDTL', @level2type=N'COLUMN',@level2name=N'CODE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存MenuName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'S_USERROLEDTL', @level2type=N'COLUMN',@level2name=N'NAME'
GO
