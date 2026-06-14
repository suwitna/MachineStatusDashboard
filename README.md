# Machine Status Dashboard

<img width="1536" height="1024" alt="ChatGPT Image 14 มิ ย  2569 14_28_07" src="https://github.com/user-attachments/assets/fb4848b3-1f72-480c-a424-5a53c20fadb1" />

### ประกอบด้วย 3 ส่วนหลักๆ คือ
1. Editor เพิ่ม/แก้ไข ข้อมูลตำแหน่งของเครื่องจักร
2. Database เก็บข้อมูลข้อมูลตำแหน่งของเครื่องจักร
3. Dashbard แสดงข้อมูลตำแหน่งของเครื่องจักร
---

## Editor 
<img width="1920" height="955" alt="image" src="https://github.com/user-attachments/assets/4c681ebd-8256-44b4-b385-1d56396957c7" />

1. เลือกภาพเพื่อใช้เป็นตัวตำแหน่งอ้างอิงในการจัดวางข้อมูลเครื่องจักร
2. เพิ่มข้อมูลเครื่องจักรทีละ 1 เครื่อง
``` sql
CNC-FAN-4XN-014
```
3. เพิ่มข้อมูลเครื่องจักรได้หลายเครื่อง พร้อมกัน
``` sql
CNC-FAN-4XN-014
CNC-HER-5XN-021
CNC-HER-5XN-022
CNC-HER-5XN-023
CNC-HER-5XN-020
CNC-MAZ-3XN-017
CNC-FAN-4XN-006
```
4. เลือก Plant และกด Load Resource เพื่อ เพิ่ม/แก้ไข ข้อมูลตำแหน่งของเครื่องจักร  
    4.1 เพิ่มหรือแก้ไขข้อมูลตำแหน่งของเครื่องจักร  
    4.2 ส่วนของ SQL Update command ที่ถูกสร้างขึ้นเพื่อนำไปอัพเดทข้อมูลใน Database  

<img width="1920" height="955" alt="image" src="https://github.com/user-attachments/assets/766c8c69-99ed-4926-aa90-758425ccac80" />


---
## Database
<img width="362" height="551" alt="image" src="https://github.com/user-attachments/assets/a33cd652-4030-4390-9301-7928450177b8" />
<img width="1862" height="780" alt="image" src="https://github.com/user-attachments/assets/48f6a42f-d0be-4624-ad69-0afa9d040698" />

รายละเอียดและหน้าที่ของฟิลด์ทั้งหมดในตารางข้อมูล `MachineLayouts` แบ่งตามกลุ่มลักษณะการใช้งานเพื่อความเข้าใจในลอจิกพิกัด:

### 1. ข้อมูลพื้นฐานและการคัดกรอง (Metadata & Filters)
* **`MachineId`** รหัสเครื่องจักรปัจจุบัน (Primary Key) เช่น `CNC-01`
* **`ResourceLocation`** ตำแหน่งพิกัดพื้นที่ติดตั้งจริงภายในโรงงาน เช่น `F1 LEAP CELL 20`
* **`Plant`** รหัสตึก/อาคาร เช่น `F1`, `F2` (ใช้สำหรับคัดกรองข้อมูลและสลับรูปพิมพ์เขียวพื้นหลัง)
* **`Area`** โซนพื้นที่แผนกผลิตในโรงงาน เช่น `EDM`, `CNC`, `ASSEMBLY`
* **`OldResourceName`** ชื่ออ้างอิงเดิมของเครื่องจักร (ใช้สำหรับการสอบทานและเชื่อมโยงข้อมูลกับระบบเก่า)

### 2. สถานะและการจัดรูปแบบป้าย (Status & Styles)
* **`recordType`** ประเภทการบันทึกหรืออาการเสียของเครื่องจักร เช่น `Breakdown`, `PM`, `Modify`
* **`Status`** สถานะงานปัจจุบันหน้างาน เช่น `Job Completed`, `Running`, `Stop`, `Disconnect`
* **`RequestDate`** วันที่และเวลาที่มีการอัปเดตสถานะงานล่าสุดเข้าสู่ระบบ
* **`ColorCode`** รหัสสีฐานสิบหก (Hex Color) ตามสถานะ เช่น `#2ECC71` (เขียว), `#E74C3C` (แดง)
* **`FontSize`** ขนาดตัวอักษรของรหัสเครื่องจักรบนป้ายข้อความ (ค่าเริ่มต้นระบบคือ `11.0`)
* **`FontWeight`** ความหนาของฟอนต์ตัวอักษรบนป้าย รองรับค่า `normal` และ `bold`
* **`IsPlotted`** แฟล็กตรวจสอบสถานะการจัดวาง (`Y` = ถูกลากวางบนผังแล้ว, `N` / `NULL` = ข้อมูลใหม่ยังไม่ได้ปักหมุด)

### 3. กรอบคอนเทนเนอร์ภายนอก (Global Percentages Framework - หน่วยเป็น %)
* **`LeftPct`** ระยะห่างของมุมซ้ายบนสุดของกรอบงาน วัดจากขอบซ้ายสุดของรูปภาพภาพพิมพ์เขียว
* **`TopPct`** ระยะห่างของมุมซ้ายบนสุดของกรอบงาน วัดจากขอบบนสุดของรูปภาพภาพพิมพ์เขียว
* **`WidthPct`** ความกว้างรวมของกรอบพื้นที่ครอบชิ้นงาน (คำนวณคลุมพื้นที่ทั้งตัวป้ายและเส้นชี้ทั้งหมด)
* **`HeightPct`** ความสูงรวมของกรอบพื้นที่ครอบชิ้นงานทั้งหมด

### 4. ตำแหน่งเวกเตอร์ภายในกล่องย่อย (Localized Inside Coordinates - หน่วยเป็น Pixel)
* **`ViewBoxW`** ความกว้างอ้างอิงภายในระบบ SVG ViewBox เพื่อล็อคอัตราส่วนมาตราส่วนเวกเตอร์
* **`ViewBoxH`** ความสูงอ้างอิงภายในระบบ SVG ViewBox เพื่อล็อคอัตราส่วนมาตราส่วนเวกเตอร์
* **`X1`, `Y1`** พิกัดจุดเริ่มต้นของเส้นชี้สายสัญญาณ (แสดงเป็นจุดวงกลมคอนโทรลสีน้ำเงินในหน้าจอ Editor)
* **`X2`, `Y2`** พิกัดจุดปลายเส้นชี้/หัวลูกศรสามเหลี่ยมที่ชี้จิ้มตัวเครื่องจักรจริง (แสดงเป็นจุดวงกลมสีแดงในหน้าจอ Editor)
* **`BoxX`, `BoxY`** พิกัดจุดมุมซ้ายบนสุดของตัวกล่องป้ายข้อความสี่เหลี่ยม (คำนวณนับระยะพิกเซลเยื้องภายในกรอบย่อย)
* **`BoxW`, `BoxH`** มิติกว้าง x สูง ของกล่องป้ายข้อความสี่เหลี่ยม (ค่าเริ่มต้นมาตรฐานของระบบคือ `125` x `24`)

---

## Dashboard
แสดงข้อมูลตำแหน่งของเครื่องจักร สามารถเลือก Plant ที่ต้องการแสดงข้อมูลได้
<img width="1919" height="954" alt="image" src="https://github.com/user-attachments/assets/b37c6177-fb8a-4b88-949c-460307fe487a" />

---
## 🚀 ขั้นตอนการติดตั้งและทดสอบการใช้งาน

```text
STEP 1: เตรียมฐานข้อมูล 
 └── [ALTER Table] ──> เพิ่มคอลัมน์เก็บพิกัดเป้าหมาย (LeftPct, TopPct) ในตารางหลัก
          │
          ▼
 STEP 2: จัดการระบบรูปภาพ 
 └── [Save to wwwroot/images] ──> วางไฟล์ชื่อฟอร์แมต "factory_floor_{Plant}.png"
          │
          ▼
 STEP 3: อัปเดตโครงสร้าง Code 
 └── [Edit Variable] ──> ปรับเปลี่ยน bgImgWidth(1889) และ bgImgHeight(689) ในหน้าจอ 
                         Dashboard.cshtml ให้ตรงพิกเซลรูปจริง
          │
          ▼
 STEP 4: เพิ่มหรือแก้ไขตำแหน่งข้อมูลเครื่องจักร 
 └── [Editor Workspace] ──> แอดมินกด "Load Resource" ดึงข้อมูลจาก DB มาปรับขยับพิกัด
                            แล้วก๊อปปี้ SQL Update Script กลับไปรันอัปเดตข้อมูลจริง
          │
          ▼
 STEP 5: แสดงผลหน้า Dashboard 
 └── [Dashboard View] ──> ดึงข้อมูลพิกัดล่าสุดมาพล็อตตำแหน่งเครื่องจักร
```


### STEP 1: เตรียมฐานข้อมูล 
* SQL Create DB

```sql
-- สำหรับใช้สร้างตารางใหม่ในฐานข้อมูล seniorth_db
CREATE TABLE [dbo].[MachineLayouts] (
    -- 1. คีย์หลักและข้อมูลพื้นฐาน (Identity & Metadata)
    [Id]                 INT IDENTITY(1,1) NOT NULL,
    [MachineId]          VARCHAR(50)       NOT NULL,
    [ResourceLocation]   NVARCHAR(150)     NULL,
    [Plant]              VARCHAR(20)       NOT NULL,
    [Area]               NVARCHAR(100)     NULL,
    [OldResourceName]    NVARCHAR(100)     NULL,

    -- 2. ข้อมูลสถานะและรูปแบบ (Status & Styles)
    [recordType]         VARCHAR(50)       NULL,
    [Status]             NVARCHAR(100)     NULL,
    [RequestDate]        DATETIME          NULL,
    [ColorCode]          VARCHAR(10)       NULL,
    [FontSize]           FLOAT             NOT NULL DEFAULT 11.0,
    [FontWeight]         VARCHAR(20)       NOT NULL DEFAULT 'normal',
    
    -- 3. โครงสร้างพิกัดภายนอก (Global Percentages - % Framework)
    [LeftPct]            FLOAT             NULL,
    [TopPct]             FLOAT             NULL,
    [WidthPct]           FLOAT             NULL,
    [HeightPct]          FLOAT             NULL,

    -- 4. โครงสร้างพิกัดเวกเตอร์ภายใน (Localized Component Space - Pixels)
    [ViewBoxW]           FLOAT             NULL,
    [ViewBoxH]           FLOAT             NULL,
    [X1]                 FLOAT             NULL,
    [Y1]                 FLOAT             NULL,
    [X2]                 FLOAT             NULL,
    [Y2]                 FLOAT             NULL,
    [BoxX]               FLOAT             NULL,
    [BoxY]               FLOAT             NULL,
    [BoxW]               FLOAT             NOT NULL DEFAULT 125.0,
    [BoxH]               FLOAT             NOT NULL DEFAULT 24.0,

    -- 5. บันทึกเวลาและแฟล็กสถานะ (Audit Logs & Flags)
    [UpdatedAt]          DATETIME          NOT NULL DEFAULT GETDATE(),
    [IsPlotted]          CHAR(1)           NOT NULL DEFAULT 'N',

    -- การตั้งค่า Constraints
    CONSTRAINT [PK_MachineLayouts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_MachineLayouts_MachineId] UNIQUE ([MachineId]),
    CONSTRAINT [CK_MachineLayouts_IsPlotted] CHECK ([IsPlotted] IN ('Y', 'N'))
);
GO

-- สร้าง Non-Clustered Index เพื่อเร่งความเร็วในการโหลดข้อมูลแยกตาม Plant (ฝั่ง Dashboard/Editor Fetch)
CREATE NONCLUSTERED INDEX [IX_MachineLayouts_Plant]
ON [dbo].[MachineLayouts] ([Plant])
INCLUDE ([MachineId], [IsPlotted]);
GO
```
* ในกรณีมีตารางข้อมูลอยูแล้ว SQL Alter DB

```sql
SQL ALTER TABLE สำหรับนำไปรันอัปเดตเพื่อเพิ่มคอลัมน์พิกัด เข้าไปในตาราง MachineLayouts เดิมที่มีอยู่แล้ว

ALTER TABLE [dbo].[MachineLayouts]
ADD 
    -- 1. โครงสร้างพิกัดภายนอก (Global Percentages Framework - หน่วยเป็น %)
    [LeftPct]      FLOAT NULL,
    [TopPct]       FLOAT NULL,
    [WidthPct]     FLOAT NULL,
    [HeightPct]    FLOAT NULL,

    -- 2. โครงสร้างพิกัดเวกเตอร์ภายใน (Localized Inside Coordinates - หน่วยเป็น Pixel)
    [ViewBoxW]     FLOAT NULL,
    [ViewBoxH]     FLOAT NULL,
    [X1]           FLOAT NULL,
    [Y1]           FLOAT NULL,
    [X2]           FLOAT NULL,
    [Y2]           FLOAT NULL,
    [BoxX]         FLOAT NULL,
    [BoxY]         FLOAT NULL,
    [BoxW]         FLOAT NOT NULL DEFAULT 125.0,
    [BoxH]         FLOAT NOT NULL DEFAULT 24.0,

    -- 3. ข้อมูลสไตล์ รูปแบบฟอนต์ และตรวจสอบความปลอดภัย
    [FontSize]     FLOAT NOT NULL DEFAULT 11.0,
    [FontWeight]   VARCHAR(20) NOT NULL DEFAULT 'normal',
    [UpdatedAt]    DATETIME NOT NULL DEFAULT GETDATE(),
    [IsPlotted]    CHAR(1) NOT NULL DEFAULT 'N';
GO
```
### STEP 2: จัดการระบบรูปภาพ 
วางไฟล์ชื่อฟอร์แมต "factory_floor_{Plant}.png"  

<img width="430" height="233" alt="image" src="https://github.com/user-attachments/assets/41cf2f1d-ab05-4ade-9aa8-a4f9eb39ff90" />  
<br/>  

<img width="334" height="521" alt="image" src="https://github.com/user-attachments/assets/21f9035f-820d-4b03-bfd4-3ad4fb9b8d04" />  


### STEP 3: อัปเดตโครงสร้าง Code
* ปรับเปลี่ยน bgImgWidth(1889) และ bgImgHeight(689) ในหน้าจอ Dashboard.cshtml ให้ตรงพิกเซลรูปจริง
  
<img width="776" height="239" alt="image" src="https://github.com/user-attachments/assets/e99118e7-d6bc-4d2a-b514-2f94b46b7b9e" />

### STEP 4: เพิ่มหรือแก้ไขตำแหน่งข้อมูลเครื่องจักร 
    * กด "Load Resource" ดึงข้อมูลจาก DB มาปรับขยับพิกัด
    * ก๊อปปี้ SQL Update Script ไปรันอัปเดตข้อมูลจริง
<img width="1920" height="955" alt="image" src="https://github.com/user-attachments/assets/d9bb4e58-2656-4105-9008-c8f3ba1b0103" />

### STEP 5: แสดงผลหน้า Dashboard 
<img width="1920" height="955" alt="image" src="https://github.com/user-attachments/assets/2e272025-8161-41d1-a4a4-da3039b32ca5" />
