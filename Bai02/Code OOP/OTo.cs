using System;

namespace HeThongQuanLyPhuongTienGiaoThong
{
    public class OTo : PhuongTien
    {
        public int SoChoNgoi { get; set; }
        public double DungTichDongCo { get; set; }

        public OTo(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc,
            int soChoNgoi,
            double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            if (soChoNgoi <= 0)
            {
                throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
            }

            if (dungTichDongCo <= 0)
            {
                throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
            }

            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                return GiaGoc + (GiaGoc * 0.12m) + (GiaGoc * 0.30m);
            }

            return GiaGoc + (GiaGoc * 0.10m);
        }

        public override string GetInfo()
        {
            return base.GetInfo()
                + $" | Số chỗ ngồi: {SoChoNgoi}"
                + $" | Dung tích động cơ: {DungTichDongCo} L";
        }
    }
}
