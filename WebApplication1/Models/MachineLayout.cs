namespace WebApplication1.Models
{
    public class MachineLayout
    {
        public int Id { get; set; } // สำหรับ Primary Key Identity(1,1)
        public string MachineId { get; set; } = string.Empty;
        public string ResourceLocation { get; set; }
        public string Plant { get; set; }
        public string Area { get; set; }
        public string OldResourceName { get; set; }
        public string recordType { get; set; }
        public string Status { get; set; }
        public string RequestDate { get; set; }
        public string ColorCode { get; set; }

        // พิกัดร้อยละและมาตรวัดหน้าจอ
        public decimal LeftPct { get; set; }
        public decimal TopPct { get; set; }
        public decimal WidthPct { get; set; }
        public decimal HeightPct { get; set; }
        public int ViewBoxW { get; set; }
        public int ViewBoxH { get; set; }

        // พิกัดองค์ประกอบกราฟิกพิกเซล
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int BoxX { get; set; }
        public int BoxY { get; set; }
        public int BoxW { get; set; }
        public int BoxH { get; set; }

        // ฟอนต์ตกแต่งลายเส้น
        public decimal FontSize { get; set; }
        public string FontWeight { get; set; }
        public string IsPlotted { get; set; }
    }
}
