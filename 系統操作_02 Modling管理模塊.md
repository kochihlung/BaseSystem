此模塊介由「參數管理」功能，建立「參數設定」內功能選項

![image](https://user-images.githubusercontent.com/104553653/192915056-5ee987fb-5d78-4fb9-89de-ed59da234912.png)

新增參數

參數名稱：於選單上顯示的名稱

對應表名：後端DB所對應的Table(僅支援以M_及S_開頭，且不以_HT結尾的資料表)

類型：該功能表單類型(單表、主/明細表)

![image](https://user-images.githubusercontent.com/104553653/192915229-2d1c9ce4-eb7d-4f70-9b4d-b369707e6499.png)

注意：此功能於DB內所建立的資料表，必需依照指定的命名規則，其命名規則如下

1.每張表均以X_開頭，目前定義為M_:Modling表、W_:WIP表、S_:系統表。

2.主/明細表之組合，其明細表命名必需於「主表名+DTL」

3.每張表都均配合一張歷史表，歷史表名固定為原表後加上_HT

單表維護介面如下

![image](https://user-images.githubusercontent.com/104553653/192930521-99fbc271-a951-4729-83f7-ec8c1a308286.png)



