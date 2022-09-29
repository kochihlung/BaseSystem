此模塊介由「參數管理」功能，建立「參數設定」內功能選項

![image](https://user-images.githubusercontent.com/104553653/192915056-5ee987fb-5d78-4fb9-89de-ed59da234912.png)


參數名稱：於選單上顯示的名稱

對應表名：後端DB所對應的Table(僅支援以M_及S_開頭，且不以_HT結尾的資料表)

類型：該功能表單類型(單表、主/明細表)

![image](https://user-images.githubusercontent.com/104553653/192915229-2d1c9ce4-eb7d-4f70-9b4d-b369707e6499.png)

注意：此功能於DB內所建立的資料表，必需依照指定的命名規則，其命名規則如下

1.每張表均以X_開頭，目前定義為M_:Modling表、W_:WIP表、S_:系統表。

2.主/明細表之組合，其明細表命名必需於「主表名+DTL」

3.每張表都均配合一張歷史表，歷史表名固定為原表後加上_HT

維護介面如下

![image](https://user-images.githubusercontent.com/104553653/192930521-99fbc271-a951-4729-83f7-ec8c1a308286.png)


![image](https://user-images.githubusercontent.com/104553653/192934231-9b9e4632-5213-44ec-8b87-073b940d0495.png)

若為類型為主/明細表之項目，其中間Grid將會顯示主表及明細表的欄位名稱

於指定欄位於右側Grid進行對應的參數設定後，擊點右上角保存即可。

![image](https://user-images.githubusercontent.com/104553653/192934468-1aa460de-36a1-41a3-a85b-35177c975aa1.png)

若UI類型為「下拉選單」，則必需選擇其「數據來源」，使齊獲取對應資料。

![image](https://user-images.githubusercontent.com/104553653/192934821-a7f393eb-4bda-4738-9ef4-25aba9f5201f.png)

其內容可以「資料來源」功能內進行維護

![image](https://user-images.githubusercontent.com/104553653/192935162-0227c74f-43ca-4142-a402-06be3fc8b08b.png)

若為固定資料，則於最下方填入其資料，格式為以半型逗號分隔資料，以冒號分隔id及text。例：id01:text01,id02:text02

若為資料單源，則選擇其對應的資料表即可，系統固定會帶出該資料表的CODE及NAME作為資料

