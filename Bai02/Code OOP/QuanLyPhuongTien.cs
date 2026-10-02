using System;
using System.Collections.Generic;
using System.Linq;

namespace HeThongQuanLyPhuongTienGiaoThong
{
    public class QuanLyPhuongTien
    {
        private List<PhuongTien> _danhSach = new List<PhuongTien>();

        // 1. Thêm phương tiện
        public void AddPhuongTien(PhuongTien pt)
        {
            _danhSach.Add(pt);
        }

        // 2. Hiển thị toàn bộ phương tiện kèm giá lăn bánh
        public void DisplayAll()
        {
            foreach (PhuongTien pt in _danhSach)
            {
                Console.WriteLine(pt.GetInfo());
                Console.WriteLine(
                    $"Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
                Console.WriteLine("--------------------------------");
            }
        }

        // 3. Tìm phương tiện có giá lăn bánh cao nhất
        public PhuongTien FindMaxGiaLanBanh()
        {
            return _danhSach
                .OrderByDescending(pt => pt.TinhGiaLanBanh())
                .FirstOrDefault();
        }

        // 4. Tìm phương tiện theo tên hãng
        public List<PhuongTien> SearchByName(string keyword)
        {
            return _danhSach
                .Where(pt => pt.TenHang.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
