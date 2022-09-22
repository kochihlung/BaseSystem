GO
/****** Object:  Table [dbo].[H_ATTREC_LOG]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[H_ATTREC_LOG](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](100) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[S_USERINFO_ID] [varchar](40) NULL,
	[DATASTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_H_ATTREC_LOG] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[H_ATTREC_LOG_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[H_ATTREC_LOG_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](100) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[S_USERINFO_ID] [varchar](40) NULL,
	[DATASTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_H_ATTREC_LOG_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMITEMDTL]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMITEMDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_M_FORMITEMDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMITEMDTL_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMITEMDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_M_FORMITEMDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMSET]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMSET](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_ROUTE_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_FORMSET] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMSET_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMSET_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_ROUTE_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_FORMSET_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMSETDTL]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMSETDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_FORMSET_ID] [varchar](40) NOT NULL,
	[CONTYPE] [varchar](50) NULL,
	[SOURCE] [varchar](50) NULL,
	[REQUIRED] [int] NULL,
	[SORT] [int] NULL,
	[M_FORMITEMDTLID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_FORMSETDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_FORMSETDTL_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_FORMSETDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_FORMSET_ID] [varchar](40) NOT NULL,
	[CONTYPE] [varchar](50) NULL,
	[SOURCE] [varchar](50) NULL,
	[REQUIRED] [int] NULL,
	[SORT] [int] NULL,
	[M_FORMITEMDTLID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_FORMSETDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_HOLDRESON]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_HOLDRESON](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_HOLDTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_HOLDRESON] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_HOLDRESON_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_HOLDRESON_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_HOLDTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_HOLDRESON_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_HOLDTYPE]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_HOLDTYPE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_W_HOLDTYPE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_HOLDTYPE_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_HOLDTYPE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_W_HOLDTYPE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_OPER]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_OPER](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_OPERTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_TABLENAME] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_OPER_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_OPER_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_OPERTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_TABLENAME_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_OPERTYPE]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_OPERTYPE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_M_OPERTYPE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_OPERTYPE_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_OPERTYPE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_M_OPERTYPE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PROD]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PROD](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_PRODTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_PROD] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PROD_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PROD_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_PRODTYPE] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_PROD_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PRODTYPE]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PRODTYPE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_M_PRODTYPE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PRODTYPE_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PRODTYPE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_M_PRODTYPE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_ROUTE]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ROUTE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
 CONSTRAINT [PK_BS_M_ROUTE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_ROUTE_HT]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ROUTE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
 CONSTRAINT [PK_BS_M_ROUTE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_ROUTEDTL]    Script Date: 2022/9/22 下午 04:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ROUTEDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_ROUTE_ID] [varchar](40) NOT NULL,
	[SQE] [varchar](50) NULL,
	[M_OPER_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_ROUTEDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_ROUTEDTL_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ROUTEDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_ROUTE_ID] [varchar](40) NOT NULL,
	[SQE] [varchar](50) NULL,
	[M_OPER_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_M_ROUTEDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_SIGNOWNER]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_SIGNOWNER](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_OPER_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_M_SINGOWNER] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_SIGNOWNER_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_SIGNOWNER_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_OPER_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_M_SINGOWNER_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_SIGNOWNERDTL]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_SIGNOWNERDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_SIGNOWNER_ID] [varchar](40) NOT NULL,
	[S_USERINFO_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_M_SINGOWNERDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_SIGNOWNERDTL_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_SIGNOWNERDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_SIGNOWNER_ID] [varchar](40) NOT NULL,
	[S_USERINFO_ID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_BS_M_SINGOWNERDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMDTL]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMDTL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[W_FORMMAIN_ID] [varchar](40) NULL,
	[M_FORMSETDTL_ID] [varchar](40) NULL,
	[VALUE] [varchar](max) NULL,
 CONSTRAINT [PK_BS_W_FORMDTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMDTL_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMDTL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[W_FORMMAIN_ID] [varchar](40) NULL,
	[M_FORMSETDTL_ID] [varchar](40) NULL,
	[VALUE] [varchar](max) NULL,
 CONSTRAINT [PK_BS_W_FORMDTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMFILE]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMFILE](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](max) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[W_FORMMAIN_ID] [varchar](40) NULL,
	[PATH] [varchar](max) NULL,
	[M_ROUTEDTL_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FORMFILE] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMFILE_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMFILE_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](max) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[W_FORMMAIN_ID] [varchar](40) NULL,
	[PATH] [varchar](max) NULL,
	[M_ROUTEDTL_ID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FORMFILE_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMMAIN]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMMAIN](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[CRT_UID] [varchar](40) NULL,
	[STATUS] [varchar](40) NULL,
	[M_ROUTEDTL_ID] [varchar](40) NULL,
	[M_ROUTE_ID] [varchar](40) NULL,
	[M_FORMSET_ID] [varchar](40) NULL,
	[CDT] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FormMain] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMMAIN_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMMAIN_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[CRT_UID] [varchar](40) NULL,
	[STATUS] [varchar](40) NULL,
	[M_ROUTEDTL_ID] [varchar](40) NULL,
	[M_ROUTE_ID] [varchar](40) NULL,
	[M_FORMSET_ID] [varchar](40) NULL,
	[CDT] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FormMain_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMSIGN]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMSIGN](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[W_FORMMAIN_ID] [varchar](40) NOT NULL,
	[M_ROUTEDTL_ID] [varchar](40) NOT NULL,
	[M_USERINFO_ID] [varchar](40) NOT NULL,
	[RESULT] [varchar](40) NULL,
	[W_FORMMAIN_HID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FORMSIGN] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_FORMSIGN_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_FORMSIGN_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[W_FORMMAIN_ID] [varchar](40) NOT NULL,
	[M_ROUTEDTL_ID] [varchar](40) NOT NULL,
	[M_USERINFO_ID] [varchar](40) NOT NULL,
	[RESULT] [varchar](40) NULL,
	[W_FORMMAIN_HID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_FORMSIGN_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_HOLDCONTROL]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_HOLDCONTROL](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[HOLD_SID] [varchar](40) NULL,
	[M_HOLDRESON] [varchar](40) NULL,
	[UID] [varchar](40) NULL,
	[STATUS] [varchar](40) NULL,
	[FROMSTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_HOLDCONTROL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_HOLDCONTROL_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_HOLDCONTROL_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[HOLD_SID] [varchar](40) NULL,
	[M_HOLDRESON] [varchar](40) NULL,
	[UID] [varchar](40) NULL,
	[STATUS] [varchar](40) NULL,
	[FROMSTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_HOLDCONTROL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WIPLOT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WIPLOT](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[STATUS] [varchar](40) NULL,
	[M_ROUTE] [varchar](40) NULL,
	[M_OPER] [varchar](40) NULL,
	[M_PROD] [varchar](40) NULL,
	[W_WO] [varchar](40) NULL,
	[QTY] [numeric](10, 3) NULL,
	[UID] [varchar](40) NULL,
	[CUSTSN] [varchar](50) NULL,
	[PrevID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_WIPSTATUS] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WIPLOT_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WIPLOT_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[STATUS] [varchar](40) NULL,
	[M_ROUTE] [varchar](40) NULL,
	[M_OPER] [varchar](40) NULL,
	[M_PROD] [varchar](40) NULL,
	[W_WO] [varchar](40) NULL,
	[QTY] [numeric](10, 3) NULL,
	[UID] [varchar](40) NULL,
	[CUSTSN] [varchar](50) NULL,
	[PrevID] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_WIPSTATUS_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WO]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WO](
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[M_PROD] [varchar](40) NULL,
	[M_ROUTE] [varchar](40) NULL,
	[QTY] [numeric](10, 3) NULL,
	[STATUS] [varchar](40) NULL,
	[UID] [varchar](40) NULL,
	[FROMSTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_WO] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WO_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WO_HT](
	[HID] [varchar](40) NOT NULL,
	[SID] [varchar](40) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[M_PROD] [varchar](40) NULL,
	[M_ROUTE] [varchar](40) NULL,
	[QTY] [numeric](10, 3) NULL,
	[STATUS] [varchar](40) NULL,
	[UID] [varchar](40) NULL,
	[FROMSTATUS] [varchar](40) NULL,
 CONSTRAINT [PK_BS_W_WO_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WODTL]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WODTL](
	[SID] [varchar](15) NOT NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[DTLTYPE] [int] NULL,
	[REQUIRED] [int] NULL,
 CONSTRAINT [PK_BS_W_WODTL] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[W_WODTL_HT]    Script Date: 2022/9/22 下午 04:52:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[W_WODTL_HT](
	[HID] [varchar](15) NOT NULL,
	[SID] [varchar](40) NULL,
	[CODE] [varchar](40) NULL,
	[NAME] [varchar](40) NULL,
	[REMARK] [varchar](max) NULL,
	[UDT] [datetime] NULL,
	[TRANS] [varchar](20) NULL,
	[DTLTYPE] [int] NULL,
	[REQUIRED] [int] NULL,
 CONSTRAINT [PK_BS_W_WODTL_HT] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
