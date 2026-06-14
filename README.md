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

