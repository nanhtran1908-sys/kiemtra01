using System;

namespace HeThongQuanLyPhuongTienGiaoThong
{
    public abstract class PhuongTien
    {
        // Private Fields
        private string _maPT = "PT000";
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        // Properties
        public string MaPT
        {
            get { return _maPT; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Mã phương tiện không được để trống!");
                }

                _maPT = value.Trim();
            }
        }

        public string TenHang
        {
            get { return _tenHang; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tên hãng không được để trống!");
                }

                _tenHang = value.Trim();
            }
        }

        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set
            {
                if (value < 1900 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                }

                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get { return _giaGoc; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Giá gốc phải lớn hơn 0!");
                }

                _giaGoc = value;
            }
        }

        // Constructor
        protected PhuongTien(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        // Abstract Method
        public abstract decimal TinhGiaLanBanh();

        // Virtual Method
        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT} | " +
                   $"Tên hãng: {TenHang} | " +
                   $"Năm sản xuất: {NamSanXuat} | " +
                   $"Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }
}