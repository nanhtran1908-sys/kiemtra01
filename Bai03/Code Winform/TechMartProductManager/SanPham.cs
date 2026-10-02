using System.Drawing;

namespace Ứng_dụng_Quản_lý_Danh_mục_Thiết_bị_Công_nghệ
{
    public class SanPham
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Image Avatar { get; set; }
    }
}