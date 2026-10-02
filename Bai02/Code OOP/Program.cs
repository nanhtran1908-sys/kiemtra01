using System;
using System.Collections.Generic;
using System.Text;

namespace HeThongQuanLyPhuongTienGiaoThong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            QuanLyPhuongTien quanLy = new QuanLyPhuongTien();

            Console.WriteLine("==========================================");
            Console.WriteLine("     HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN");
            Console.WriteLine("==========================================");

            Console.WriteLine("\n===== TC01: KIỂM TRA VALIDATION NĂM SẢN XUẤT =====");

            int namSanXuat = 1850;

            try
            {
                OTo otoLoi = new OTo(
                    "OT001",
                    "Toyota",
                    namSanXuat,
                    1000000000m,
                    5,
                    2.0
                );

                Console.WriteLine("- Năm sản xuất: 1850");
                Console.WriteLine("- Trạng thái: FAIL");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"- Năm sản xuất: {namSanXuat}");
                Console.WriteLine($"- Ngoại lệ: {ex.Message}");
                Console.WriteLine("- Trạng thái: PASS");
            }

            Console.WriteLine("\n===== TC02: TÍNH GIÁ LĂN BÁNH Ô TÔ =====");

            OTo oto = new OTo(
                "OT002",
                "Toyota",
                2022,
                1000000000m,
                5,
                2.0
            );

            decimal giaOTo = oto.TinhGiaLanBanh();

            Console.WriteLine($"- Mã phương tiện: {oto.MaPT}");
            Console.WriteLine($"- Tên hãng: {oto.TenHang}");
            Console.WriteLine($"- Năm sản xuất: {oto.NamSanXuat}");
            Console.WriteLine($"- Giá gốc: {oto.GiaGoc:N0} VNĐ");
            Console.WriteLine($"- Số chỗ ngồi: {oto.SoChoNgoi}");
            Console.WriteLine($"- Dung tích động cơ: {oto.DungTichDongCo} L");
            Console.WriteLine($"- Giá lăn bánh: {giaOTo:N0} VNĐ");

            if (giaOTo == 1420000000m)
            {
                Console.WriteLine("- Trạng thái: PASS");
            }
            else
            {
                Console.WriteLine("- Trạng thái: FAIL");
            }

            Console.WriteLine("\n===== TC03: TÍNH GIÁ LĂN BÁNH XE MÁY =====");

            XeMay xeMay = new XeMay(
                "XM001",
                "Honda",
                2023,
                50000000m,
                150
            );

            decimal giaXeMay = xeMay.TinhGiaLanBanh();

            Console.WriteLine($"- Mã phương tiện: {xeMay.MaPT}");
            Console.WriteLine($"- Tên hãng: {xeMay.TenHang}");
            Console.WriteLine($"- Năm sản xuất: {xeMay.NamSanXuat}");
            Console.WriteLine($"- Giá gốc: {xeMay.GiaGoc:N0} VNĐ");
            Console.WriteLine($"- Dung tích xilanh: {xeMay.DungTichXylanh} cc");
            Console.WriteLine($"- Giá lăn bánh: {giaXeMay:N0} VNĐ");

            if (giaXeMay == 51000000m)
            {
                Console.WriteLine("- Trạng thái: PASS");
            }
            else
            {
                Console.WriteLine("- Trạng thái: FAIL");
            }

            Console.WriteLine("\n===== TC04: KIỂM TRA ĐA HÌNH =====");

            List<PhuongTien> danhSach = new List<PhuongTien>();

            danhSach.Add(oto);
            danhSach.Add(xeMay);

            bool tc04Pass = true;

            foreach (PhuongTien pt in danhSach)
            {
                decimal gia = pt.TinhGiaLanBanh();

                Console.WriteLine(
                    $"- {pt.GetType().Name}: {gia:N0} VNĐ");

                if (pt is OTo && gia != 1420000000m)
                {
                    tc04Pass = false;
                }

                if (pt is XeMay && gia != 51000000m)
                {
                    tc04Pass = false;
                }
            }

            if (tc04Pass)
            {
                Console.WriteLine("- Trạng thái: PASS");
            }
            else
            {
                Console.WriteLine("- Trạng thái: FAIL");
            }

            Console.WriteLine("\n===== DANH SÁCH PHƯƠNG TIỆN =====");

            quanLy.AddPhuongTien(oto);
            quanLy.AddPhuongTien(xeMay);

            foreach (PhuongTien pt in danhSach)
            {
                Console.WriteLine($"- Mã PT: {pt.MaPT}");
                Console.WriteLine($"- Tên hãng: {pt.TenHang}");
                Console.WriteLine($"- Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
                Console.WriteLine();
            }

            Console.WriteLine("===== TC05: TÌM GIÁ LĂN BÁNH CAO NHẤT =====");

            PhuongTien phuongTienMax = quanLy.FindMaxGiaLanBanh();

            if (phuongTienMax != null)
            {
                Console.WriteLine("- Phương tiện có giá lăn bánh cao nhất:");
                Console.WriteLine($"- Mã PT: {phuongTienMax.MaPT}");
                Console.WriteLine($"- Tên hãng: {phuongTienMax.TenHang}");
                Console.WriteLine($"- Năm sản xuất: {phuongTienMax.NamSanXuat}");
                Console.WriteLine($"- Giá gốc: {phuongTienMax.GiaGoc:N0} VNĐ");
                Console.WriteLine(
                    $"- Giá lăn bánh: {phuongTienMax.TinhGiaLanBanh():N0} VNĐ");

                if (phuongTienMax is OTo &&
                    phuongTienMax.TinhGiaLanBanh() == 1420000000m)
                {
                    Console.WriteLine("- Trạng thái: PASS");
                }
                else
                {
                    Console.WriteLine("- Trạng thái: FAIL");
                }
            }
            else
            {
                Console.WriteLine("- Không có phương tiện trong danh sách.");
                Console.WriteLine("- Trạng thái: FAIL");
            }

            Console.WriteLine("\n==========================================");
            Console.WriteLine("        KẾT THÚC CHƯƠNG TRÌNH");
            Console.WriteLine("==========================================");

            Console.ReadKey();
        }
    }
}