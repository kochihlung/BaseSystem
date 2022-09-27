# BaseSystem
系統基礎架構(含客製範例)

a.建置系統


1.安裝IIS

https://learn.microsoft.com/en-us/iis/install/installing-iis-7/installing-iis-on-windows-vista-and-windows-7?source=recommendations

2.安裝SQL Server

https://learn.microsoft.com/zh-tw/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver16

3.解壓app.publish.zip

  ![image](https://user-images.githubusercontent.com/104553653/192424348-c6a95aa0-fb5b-47e3-94c3-2f2b80364d53.png)
  
4.IIS新增站台並指向對應路徑

  ![image](https://user-images.githubusercontent.com/104553653/192424937-f22e6025-fa7c-45b0-9b20-1f251fa13b1d.png)
  
5.初始化資料庫

初始化指令路徑：BaseSystem\SysService\Doc\DB_Command，執行00_InitTable.sql及01_CreateCustTable

![image](https://user-images.githubusercontent.com/104553653/192425391-98f5d3da-0ae4-4b0b-b087-df4de2cd6999.png)

6.修改WebConfig

![image](https://user-images.githubusercontent.com/104553653/192425662-2e4cc33f-7232-4434-bbb2-9a2f4fb354de.png)

修改DB地址、名稱、登入帳戶等相關資料

![image](https://user-images.githubusercontent.com/104553653/192427590-17b4fad0-4af3-4125-a77b-8cb2221e73e0.png)


