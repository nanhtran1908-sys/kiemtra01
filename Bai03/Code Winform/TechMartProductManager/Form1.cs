using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ứng_dụng_Quản_lý_Danh_mục_Thiết_bị_Công_nghệ;

namespace HeThongQuanLyPhuongTienGiaoThong
{
    public partial class Form1 : Form
    {
        // =====================================================
        // DATA
        // =====================================================

        private readonly BindingList<SanPham> danhSachSanPham =
            new BindingList<SanPham>();

        private readonly BindingSource bindingSource =
            new BindingSource();

        private SanPham sanPhamDangChon = null;

        // =====================================================
        // CONTROLS
        // =====================================================

        private TableLayoutPanel tableLayoutPanelMain;

        private TextBox txtProductId;
        private TextBox txtProductName;
        private TextBox txtUnitPrice;
        private TextBox txtQuantity;

        // Tìm kiếm bên phải
        private TextBox txtSearchRight;
        private Button btnSearchRight;

        private ComboBox cboCategory;

        private PictureBox picAvatar;

        private Button btnChooseImage;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private DataGridView dgvProducts;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuExportCsv;
        private ToolStripMenuItem menuExit;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;

        private ErrorProvider errorProvider1;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public Form1()
        {
            errorProvider1 =
                new ErrorProvider();

            errorProvider1.ContainerControl =
                this;

            SetupForm();

            CreateMenu();
            CreateStatusBar();
            CreateMainLayout();

            CreateLeftPanel();
            CreateRightPanel();

            LoadCategories();
            SetupDataGridView();
            SetupDataBinding();
            SetupEvents();

            UpdateStatus();
        }


        // =====================================================
        // FORM
        // =====================================================

        private void SetupForm()
        {
            Text =
                "TechMart Product Manager";

            StartPosition =
                FormStartPosition.CenterScreen;

            Size =
                new Size(1100, 650);

            MinimumSize =
                new Size(1000, 600);

            Font =
                new Font(
                    "Segoe UI",
                    9F);

            // Dùng màu mặc định của Windows

            FormBorderStyle =
                FormBorderStyle.Sizable;

            MaximizeBox =
                true;

            MinimizeBox =
                true;
        }

        // =====================================================
        // MENU
        // =====================================================

        private void CreateMenu()
        {
            menuStrip1 =
                new MenuStrip();

            menuStrip1.Dock =
                DockStyle.Top;

            // Dùng màu mặc định của Windows

            menuFile =
                new ToolStripMenuItem(
                    "File");

            menuExportCsv =
                new ToolStripMenuItem(
                    "Export CSV");

            menuExit =
                new ToolStripMenuItem(
                    "Exit");

            menuExportCsv.ShortcutKeys =
                Keys.Control | Keys.E;

            menuExit.ShortcutKeys =
                Keys.Control | Keys.X;

            menuFile.DropDownItems.Add(
                menuExportCsv);

            menuFile.DropDownItems.Add(
                new ToolStripSeparator());

            menuFile.DropDownItems.Add(
                menuExit);

            menuStrip1.Items.Add(
                menuFile);

            Controls.Add(
                menuStrip1);

            MainMenuStrip =
                menuStrip1;
        }

        // =====================================================
        // STATUS BAR
        // =====================================================

        private void CreateStatusBar()
        {
            statusStrip1 =
                new StatusStrip();

            statusStrip1.Dock =
                DockStyle.Bottom;

            statusStrip1.SizingGrip =
                false;

            // Dùng màu mặc định của Windows

            lblStatus =
                new ToolStripStatusLabel();

            lblStatus.Text =
                "Tổng số sản phẩm: 0";

            lblStatus.Font =
                new Font(
                    "Segoe UI",
                    9F);

            statusStrip1.Items.Add(
                lblStatus);

            Controls.Add(
                statusStrip1);
        }

        // =====================================================
        // MAIN LAYOUT
        // =====================================================

        private void CreateMainLayout()
        {
            tableLayoutPanelMain =
                new TableLayoutPanel();

            tableLayoutPanelMain.Dock =
                DockStyle.Fill;

            tableLayoutPanelMain.ColumnCount =
                2;

            tableLayoutPanelMain.RowCount =
                1;

            tableLayoutPanelMain.Padding =
                new Padding(
                    10,
                    5,
                    10,
                    5);

            tableLayoutPanelMain.Margin =
                new Padding(0);

            // LEFT 38%
            tableLayoutPanelMain.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    38F));

            // RIGHT 62%
            tableLayoutPanelMain.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    62F));

            tableLayoutPanelMain.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent,
                    100F));

            Controls.Add(
                tableLayoutPanelMain);

            // Đưa main layout xuống dưới MenuStrip
            Controls.SetChildIndex(
                tableLayoutPanelMain,
                Controls.Count - 1);
        }

        // =====================================================
        // LEFT PANEL
        // =====================================================

        private void CreateLeftPanel()
        {
            TableLayoutPanel leftPanel =
                new TableLayoutPanel();

            leftPanel.Dock =
                DockStyle.Fill;

            leftPanel.Margin =
                new Padding(5);

            leftPanel.Padding =
                new Padding(12);

            // Dùng màu mặc định của Windows

            leftPanel.BorderStyle =
                BorderStyle.FixedSingle;

            leftPanel.ColumnCount =
                2;

            leftPanel.RowCount =
                9;

            // =================================================
            // COLUMNS
            // =================================================

            leftPanel.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Absolute,
                    90F));

            leftPanel.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    100F));

            // =================================================
            // ROWS
            // =================================================

            // Title
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    42F));

            // Mã
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Tên
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Giá
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Số lượng
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Danh mục
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Image
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    145F));

            // Choose image
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    38F));

            // Buttons
            leftPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    40F));

            // =================================================
            // TITLE
            // =================================================

            Label lblTitle =
                new Label();

            lblTitle.Text =
                "NHẬP THÔNG TIN SẢN PHẨM";

            lblTitle.Dock =
                DockStyle.Fill;

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold);

            // Dùng màu chữ mặc định

            lblTitle.TextAlign =
                ContentAlignment.MiddleLeft;

            lblTitle.Margin =
                new Padding(0);

            leftPanel.Controls.Add(
                lblTitle,
                0,
                0);

            leftPanel.SetColumnSpan(
                lblTitle,
                2);

            // =================================================
            // MÃ SP
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Mã SP:"),
                0,
                1);

            txtProductId =
                CreateTextBox();

            leftPanel.Controls.Add(
                txtProductId,
                1,
                1);

            // =================================================
            // TÊN SP
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Tên SP:"),
                0,
                2);

            txtProductName =
                CreateTextBox();

            leftPanel.Controls.Add(
                txtProductName,
                1,
                2);

            // =================================================
            // ĐƠN GIÁ
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Đơn giá:"),
                0,
                3);

            txtUnitPrice =
                CreateTextBox();

            txtUnitPrice.TextAlign =
                HorizontalAlignment.Left;

            leftPanel.Controls.Add(
                txtUnitPrice,
                1,
                3);

            // =================================================
            // SỐ LƯỢNG
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Số lượng:"),
                0,
                4);

            txtQuantity =
                CreateTextBox();

            txtQuantity.TextAlign =
                HorizontalAlignment.Left;

            leftPanel.Controls.Add(
                txtQuantity,
                1,
                4);

            // =================================================
            // DANH MỤC
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Danh mục:"),
                0,
                5);

            cboCategory =
                new ComboBox();

            cboCategory.Dock =
                DockStyle.Fill;

            cboCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboCategory.Font =
                new Font(
                    "Segoe UI",
                    9F);

            cboCategory.Margin =
                new Padding(
                    2,
                    4,
                    2,
                    4);

            leftPanel.Controls.Add(
                cboCategory,
                1,
                5);

            // =================================================
            // ẢNH
            // =================================================

            leftPanel.Controls.Add(
                CreateLabel("Ảnh:"),
                0,
                6);

            picAvatar =
                new PictureBox();

            picAvatar.Dock =
                DockStyle.Fill;

            picAvatar.SizeMode =
                PictureBoxSizeMode.Zoom;

            picAvatar.BorderStyle =
                BorderStyle.FixedSingle;

            // Dùng màu nền mặc định

            picAvatar.Margin =
                new Padding(2);

            leftPanel.Controls.Add(
                picAvatar,
                1,
                6);

            // =================================================
            // CHỌN ẢNH
            // =================================================

            btnChooseImage =
                new Button();

            btnChooseImage.Text =
                "📁  Chọn ảnh";

            btnChooseImage.Dock =
                DockStyle.Fill;

            btnChooseImage.Font =
                new Font(
                    "Segoe UI",
                    9F);

            btnChooseImage.Margin =
                new Padding(
                    2,
                    3,
                    2,
                    3);

            leftPanel.Controls.Add(
                btnChooseImage,
                1,
                7);

            // =================================================
            // BUTTON PANEL
            // =================================================

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();

            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonPanel.WrapContents = false;
            buttonPanel.AutoSize = false;
            buttonPanel.Padding = new Padding(0);
            buttonPanel.Margin = new Padding(0);

            // THÊM
            btnAdd = new Button();
            btnAdd.Text = "Thêm mới";
            btnAdd.Width = 90;
            btnAdd.Height = 35;
            btnAdd.Margin = new Padding(2, 2, 5, 2);

            // CẬP NHẬT
            btnUpdate = new Button();
            btnUpdate.Text = "Cập nhật";
            btnUpdate.Width = 90;
            btnUpdate.Height = 35;
            btnUpdate.Margin = new Padding(2, 2, 5, 2);

            // XÓA
            btnDelete = new Button();
            btnDelete.Text = "Xóa";
            btnDelete.Width = 90;
            btnDelete.Height = 35;
            btnDelete.Margin = new Padding(2, 2, 5, 2);

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnUpdate);
            buttonPanel.Controls.Add(btnDelete);

            leftPanel.Controls.Add(
                buttonPanel,
                1,
                8);

            // =================================================
            // ADD TO MAIN
            // =================================================

            tableLayoutPanelMain.Controls.Add(
                leftPanel,
                0,
                0);
        }

        // =====================================================
        // RIGHT PANEL
        // =====================================================

        private void CreateRightPanel()
        {
            Panel rightPanel =
                new Panel();

            rightPanel.Dock =
                DockStyle.Fill;

            rightPanel.Margin =
                new Padding(5);

            rightPanel.Padding =
                new Padding(10);

            // Dùng màu nền mặc định

            rightPanel.BorderStyle =
                BorderStyle.FixedSingle;

            // =================================================
            // RIGHT LAYOUT
            // =================================================

            TableLayoutPanel rightLayout =
                new TableLayoutPanel();

            rightLayout.Dock =
                DockStyle.Fill;

            rightLayout.ColumnCount =
                1;

            rightLayout.RowCount =
                2;

            rightLayout.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute,
                    42F));

            rightLayout.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent,
                    100F));

            // =================================================
            // SEARCH PANEL
            // =================================================

            TableLayoutPanel searchPanel =
                new TableLayoutPanel();

            searchPanel.Dock =
                DockStyle.Fill;

            searchPanel.ColumnCount =
                3;

            searchPanel.RowCount =
                1;

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Absolute,
                    70F));

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    100F));

            searchPanel.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Absolute,
                    60F));

            Label lblSearch =
                new Label();

            lblSearch.Text =
                "Tìm kiếm:";

            lblSearch.Dock =
                DockStyle.Fill;

            lblSearch.TextAlign =
                ContentAlignment.MiddleLeft;

            lblSearch.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            searchPanel.Controls.Add(
                lblSearch,
                0,
                0);

            txtSearchRight =
                new TextBox();

            txtSearchRight.Dock =
                DockStyle.Fill;

            txtSearchRight.Margin =
                new Padding(
                    2,
                    4,
                    2,
                    4);

            txtSearchRight.Font =
                new Font(
                    "Segoe UI",
                    9F);

            searchPanel.Controls.Add(
                txtSearchRight,
                1,
                0);

            btnSearchRight =
                new Button();

            btnSearchRight.Text =
                "Tìm";

            btnSearchRight.Dock =
                DockStyle.Fill;

            btnSearchRight.Margin =
                new Padding(
                    2,
                    3,
                    2,
                    3);

            searchPanel.Controls.Add(
                btnSearchRight,
                2,
                0);

            rightLayout.Controls.Add(
                searchPanel,
                0,
                0);

            // =================================================
            // DATA GRID
            // =================================================

            dgvProducts =
                new DataGridView();

            dgvProducts.Dock =
                DockStyle.Fill;

            dgvProducts.AutoGenerateColumns =
                false;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.MultiSelect =
                false;

            dgvProducts.ReadOnly =
                true;

            dgvProducts.AllowUserToAddRows =
                false;

            dgvProducts.AllowUserToDeleteRows =
                false;

            dgvProducts.AllowUserToResizeRows =
                false;

            dgvProducts.RowHeadersVisible =
                false;

            // Dùng màu nền mặc định

            dgvProducts.BorderStyle =
                BorderStyle.FixedSingle;

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvProducts.RowTemplate.Height =
                32;

            dgvProducts.ColumnHeadersHeight =
                36;

            dgvProducts.EnableHeadersVisualStyles =
                false;

            // =================================================
            // HEADER STYLE
            // =================================================

            dgvProducts.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    Font =
                        new Font(
                            "Segoe UI",
                            9F,
                            FontStyle.Bold),

                    Alignment =
                        DataGridViewContentAlignment.MiddleCenter
                };

            // =================================================
            // CELL STYLE
            // =================================================

            dgvProducts.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    Font =
                        new Font(
                            "Segoe UI",
                            9F),

                    Alignment =
                        DataGridViewContentAlignment.MiddleLeft
                };

            // =================================================
            // ALTERNATING ROW
            // =================================================

            // Không dùng màu xen kẽ dòng

            rightLayout.Controls.Add(
                dgvProducts,
                0,
                1);

            // =================================================
            // ADD
            // =================================================

            rightPanel.Controls.Add(
                rightLayout);

            tableLayoutPanelMain.Controls.Add(
                rightPanel,
                1,
                0);
        }

        // =====================================================
        // CATEGORY
        // =====================================================

        private void LoadCategories()
        {
            BindingList<DanhMuc> categories =
                new BindingList<DanhMuc>();

            categories.Add(
                new DanhMuc
                {
                    Id =
                        "DM01",

                    Name =
                        "Điện thoại"
                });

            categories.Add(
                new DanhMuc
                {
                    Id =
                        "DM02",

                    Name =
                        "Laptop"
                });

            categories.Add(
                new DanhMuc
                {
                    Id =
                        "DM03",

                    Name =
                        "Phụ kiện"
                });

            cboCategory.DisplayMember =
                "Name";

            cboCategory.ValueMember =
                "Id";

            cboCategory.DataSource =
                categories;

            cboCategory.SelectedIndex =
                0;
        }

        // =====================================================
        // DATAGRIDVIEW
        // =====================================================

        private void SetupDataGridView()
        {
            dgvProducts.Columns.Clear();

            // =================================================
            // MÃ SP
            // =================================================

            DataGridViewTextBoxColumn colProductId =
                new DataGridViewTextBoxColumn();

            colProductId.Name =
                "ProductId";

            colProductId.HeaderText =
                "Mã SP";

            colProductId.DataPropertyName =
                "ProductId";

            colProductId.FillWeight =
                15;

            colProductId.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns.Add(
                colProductId);

            // =================================================
            // TÊN SP
            // =================================================

            DataGridViewTextBoxColumn colProductName =
                new DataGridViewTextBoxColumn();

            colProductName.Name =
                "ProductName";

            colProductName.HeaderText =
                "Tên SP";

            colProductName.DataPropertyName =
                "ProductName";

            colProductName.FillWeight =
                28;

            dgvProducts.Columns.Add(
                colProductName);

            // =================================================
            // DANH MỤC
            // =================================================

            DataGridViewTextBoxColumn colCategoryName =
                new DataGridViewTextBoxColumn();

            colCategoryName.Name =
                "CategoryName";

            colCategoryName.HeaderText =
                "Danh mục";

            colCategoryName.DataPropertyName =
                "CategoryName";

            colCategoryName.FillWeight =
                20;

            dgvProducts.Columns.Add(
                colCategoryName);

            // =================================================
            // ĐƠN GIÁ
            // =================================================

            DataGridViewTextBoxColumn colUnitPrice =
                new DataGridViewTextBoxColumn();

            colUnitPrice.Name =
                "UnitPrice";

            colUnitPrice.HeaderText =
                "Đơn giá";

            colUnitPrice.DataPropertyName =
                "UnitPrice";

            colUnitPrice.FillWeight =
                22;

            colUnitPrice.DefaultCellStyle.Format =
                "N0";

            colUnitPrice.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvProducts.Columns.Add(
                colUnitPrice);

            // =================================================
            // SỐ LƯỢNG
            // =================================================

            DataGridViewTextBoxColumn colQuantity =
                new DataGridViewTextBoxColumn();

            colQuantity.Name =
                "Quantity";

            colQuantity.HeaderText =
                "Số lượng";

            colQuantity.DataPropertyName =
                "Quantity";

            colQuantity.FillWeight =
                15;

            colQuantity.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvProducts.Columns.Add(
                colQuantity);
        }

        // =====================================================
        // DATA BINDING
        // =====================================================

        private void SetupDataBinding()
        {
            bindingSource.DataSource =
                danhSachSanPham;

            dgvProducts.DataSource =
                bindingSource;
        }

        // =====================================================
        // EVENTS
        // =====================================================

        private void SetupEvents()
        {
            btnAdd.Click +=
                BtnAdd_Click;

            btnUpdate.Click +=
                BtnUpdate_Click;

            btnDelete.Click +=
                BtnDelete_Click;

            btnChooseImage.Click +=
                BtnChooseImage_Click;

            // Tìm kiếm bên phải
            btnSearchRight.Click +=
                BtnSearchRight_Click;

            txtSearchRight.TextChanged +=
                TxtSearchRight_TextChanged;

            dgvProducts.SelectionChanged +=
                DgvProducts_SelectionChanged;

            menuExportCsv.Click +=
                MenuExportCsv_Click;

            menuExit.Click +=
                MenuExit_Click;
        }

        // =====================================================
        // ADD
        // =====================================================

        private void BtnAdd_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string productId =
                txtProductId.Text.Trim();

            // Tự sinh mã nếu bỏ trống
            if (string.IsNullOrWhiteSpace(
                productId))
            {
                int number =
                    danhSachSanPham.Count + 1;

                productId =
                    "SP" +
                    number.ToString("D3");

                while (
                    danhSachSanPham.Any(
                        x =>
                            x.ProductId.Equals(
                                productId,
                                StringComparison.OrdinalIgnoreCase)))
                {
                    number++;

                    productId =
                        "SP" +
                        number.ToString("D3");
                }
            }

            // Kiểm tra trùng mã
            bool trungMa =
                danhSachSanPham.Any(
                    x =>
                        x.ProductId.Equals(
                            productId,
                            StringComparison.OrdinalIgnoreCase));

            if (trungMa)
            {
                MessageBox.Show(
                    "Mã sản phẩm đã tồn tại!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DanhMuc danhMuc =
                cboCategory.SelectedItem
                as DanhMuc;

            if (danhMuc == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn danh mục!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal price;

            int quantity;

            decimal.TryParse(
                txtUnitPrice.Text,
                out price);

            int.TryParse(
                txtQuantity.Text,
                out quantity);

            SanPham sanPham =
                new SanPham();

            sanPham.ProductId =
                productId;

            sanPham.ProductName =
                txtProductName.Text.Trim();

            sanPham.CategoryId =
                danhMuc.Id;

            sanPham.CategoryName =
                danhMuc.Name;

            sanPham.UnitPrice =
                price;

            sanPham.Quantity =
                quantity;

            sanPham.Avatar =
                picAvatar.Image;

            danhSachSanPham.Add(
                sanPham);

            bindingSource.DataSource =
                danhSachSanPham;

            bindingSource.ResetBindings(
                false);

            UpdateStatus();

            ClearInputs();

            MessageBox.Show(
                "Thêm sản phẩm thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // UPDATE
        // =====================================================

        private void BtnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (sanPhamDangChon == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần cập nhật!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateInput())
            {
                return;
            }

            DanhMuc danhMuc =
                cboCategory.SelectedItem
                as DanhMuc;

            if (danhMuc == null)
            {
                return;
            }

            string newId =
                txtProductId.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                newId))
            {
                newId =
                    sanPhamDangChon.ProductId;
            }

            bool trungMa =
                danhSachSanPham.Any(
                    x =>
                        x != sanPhamDangChon
                        &&
                        x.ProductId.Equals(
                            newId,
                            StringComparison.OrdinalIgnoreCase));

            if (trungMa)
            {
                MessageBox.Show(
                    "Mã sản phẩm đã tồn tại!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal price;

            int quantity;

            decimal.TryParse(
                txtUnitPrice.Text,
                out price);

            int.TryParse(
                txtQuantity.Text,
                out quantity);

            sanPhamDangChon.ProductId =
                newId;

            sanPhamDangChon.ProductName =
                txtProductName.Text.Trim();

            sanPhamDangChon.CategoryId =
                danhMuc.Id;

            sanPhamDangChon.CategoryName =
                danhMuc.Name;

            sanPhamDangChon.UnitPrice =
                price;

            sanPhamDangChon.Quantity =
                quantity;

            sanPhamDangChon.Avatar =
                picAvatar.Image;

            bindingSource.DataSource =
                danhSachSanPham;

            bindingSource.ResetBindings(
                false);

            UpdateStatus();

            MessageBox.Show(
                "Cập nhật sản phẩm thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // DELETE
        // =====================================================

        private void BtnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (sanPhamDangChon == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa sản phẩm '"
                    +
                    sanPhamDangChon.ProductName
                    +
                    "' không?",

                    "Xác nhận xóa",

                    MessageBoxButtons.YesNo,

                    MessageBoxIcon.Question);

            if (result ==
                DialogResult.Yes)
            {
                danhSachSanPham.Remove(
                    sanPhamDangChon);

                sanPhamDangChon =
                    null;

                bindingSource.DataSource =
                    danhSachSanPham;

                bindingSource.ResetBindings(
                    false);

                ClearInputs();

                UpdateStatus();
            }
        }

        // =====================================================
        // CHOOSE IMAGE
        // =====================================================

        private void BtnChooseImage_Click(
            object sender,
            EventArgs e)
        {
            using (
                OpenFileDialog openFileDialog =
                new OpenFileDialog())
            {
                openFileDialog.Title =
                    "Chọn ảnh sản phẩm";

                openFileDialog.Filter =
                    "File ảnh (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

                openFileDialog.Multiselect =
                    false;

                if (openFileDialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    try
                    {
                        using (
                            Image image =
                            Image.FromFile(
                                openFileDialog.FileName))
                        {
                            picAvatar.Image =
                                new Bitmap(image);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Lỗi khi nạp ảnh: "
                            +
                            ex.Message,

                            "Lỗi",

                            MessageBoxButtons.OK,

                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        // =====================================================
        // SEARCH BUTTON RIGHT
        // =====================================================

        private void BtnSearchRight_Click(
            object sender,
            EventArgs e)
        {
            SearchProducts();
        }

        // =====================================================
        // LIVE SEARCH RIGHT
        // =====================================================

        private void TxtSearchRight_TextChanged(
            object sender,
            EventArgs e)
        {
            SearchProducts();
        }

        // =====================================================
        // SEARCH
        // =====================================================

        private void SearchProducts()
        {
            string keyword =
                txtSearchRight.Text.Trim();

            // Không tìm kiếm
            if (string.IsNullOrWhiteSpace(
                keyword))
            {
                bindingSource.DataSource =
                    danhSachSanPham;

                bindingSource.ResetBindings(
                    false);

                return;
            }

            var filtered =
                danhSachSanPham
                    .Where(
                        x =>
                            // Mã SP
                            (
                                !string.IsNullOrWhiteSpace(
                                    x.ProductId)
                                &&
                                x.ProductId.IndexOf(
                                    keyword,
                                    StringComparison.OrdinalIgnoreCase)
                                >= 0
                            )

                            ||

                            // Tên SP
                            (
                                !string.IsNullOrWhiteSpace(
                                    x.ProductName)
                                &&
                                x.ProductName.IndexOf(
                                    keyword,
                                    StringComparison.OrdinalIgnoreCase)
                                >= 0
                            )

                            ||

                            // Danh mục
                            (
                                !string.IsNullOrWhiteSpace(
                                    x.CategoryName)
                                &&
                                x.CategoryName.IndexOf(
                                    keyword,
                                    StringComparison.OrdinalIgnoreCase)
                                >= 0
                            )
                    )
                    .ToList();

            BindingList<SanPham> result =
                new BindingList<SanPham>(
                    filtered);

            bindingSource.DataSource =
                result;

            bindingSource.ResetBindings(
                false);
        }

        // =====================================================
        // SELECT ROW
        // =====================================================

        private void DgvProducts_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count ==
                0)
            {
                return;
            }

            SanPham sanPham =
                dgvProducts
                    .SelectedRows[0]
                    .DataBoundItem
                    as SanPham;

            if (sanPham == null)
            {
                return;
            }

            sanPhamDangChon =
                sanPham;

            txtProductId.Text =
                sanPham.ProductId;

            txtProductName.Text =
                sanPham.ProductName;

            txtUnitPrice.Text =
                sanPham.UnitPrice.ToString(
                    "N0");

            txtQuantity.Text =
                sanPham.Quantity.ToString();

            cboCategory.SelectedValue =
                sanPham.CategoryId;

            picAvatar.Image =
                sanPham.Avatar;
        }

        // =====================================================
        // VALIDATION
        // =====================================================

        private bool ValidateInput()
        {
            bool isValid =
                true;

            errorProvider1.Clear();

            // =================================================
            // TÊN
            // =================================================

            if (string.IsNullOrWhiteSpace(
                txtProductName.Text))
            {
                errorProvider1.SetError(
                    txtProductName,
                    "Tên sản phẩm không được để trống!");

                isValid =
                    false;
            }

            // =================================================
            // ĐƠN GIÁ
            // =================================================

            decimal price;

            if (!decimal.TryParse(
                txtUnitPrice.Text,
                out price)
                ||
                price <= 0)
            {
                errorProvider1.SetError(
                    txtUnitPrice,
                    "Đơn giá phải lớn hơn 0!");

                isValid =
                    false;
            }

            // =================================================
            // SỐ LƯỢNG
            // =================================================

            int quantity;

            if (!int.TryParse(
                txtQuantity.Text,
                out quantity)
                ||
                quantity < 0)
            {
                errorProvider1.SetError(
                    txtQuantity,
                    "Số lượng phải >= 0!");

                isValid =
                    false;
            }

            // =================================================
            // DANH MỤC
            // =================================================

            if (cboCategory.SelectedItem == null)
            {
                errorProvider1.SetError(
                    cboCategory,
                    "Vui lòng chọn danh mục!");

                isValid =
                    false;
            }

            return isValid;
        }

        // =====================================================
        // CLEAR INPUTS
        // =====================================================

        private void ClearInputs()
        {
            errorProvider1.Clear();

            txtProductId.Clear();

            txtProductName.Clear();

            txtUnitPrice.Clear();

            txtQuantity.Clear();

            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex =
                    0;
            }

            picAvatar.Image =
                null;

            sanPhamDangChon =
                null;

            dgvProducts.ClearSelection();
        }

        // =====================================================
        // STATUS
        // =====================================================

        private void UpdateStatus()
        {
            lblStatus.Text =
                "Tổng số sản phẩm: "
                +
                danhSachSanPham.Count;
        }

        // =====================================================
        // EXPORT CSV
        // =====================================================

        private void MenuExportCsv_Click(
            object sender,
            EventArgs e)
        {
            using (
                SaveFileDialog saveFileDialog =
                new SaveFileDialog())
            {
                saveFileDialog.Title =
                    "Xuất danh sách sản phẩm";

                saveFileDialog.Filter =
                    "CSV Files (*.csv)|*.csv";

                saveFileDialog.DefaultExt =
                    "csv";

                saveFileDialog.AddExtension =
                    true;

                saveFileDialog.FileName =
                    "DanhSachSanPham.csv";

                if (saveFileDialog.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                try
                {
                    StringBuilder sb =
                        new StringBuilder();

                    // Header
                    sb.AppendLine(
                        "Mã SP,Tên SP,Danh Mục,Đơn Giá,Số Lượng");

                    foreach (
                        SanPham sp
                        in danhSachSanPham)
                    {
                        sb.AppendLine(
                            "\""
                            +
                            EscapeCsv(
                                sp.ProductId)
                            +
                            "\","

                            +

                            "\""
                            +
                            EscapeCsv(
                                sp.ProductName)
                            +
                            "\","

                            +

                            "\""
                            +
                            EscapeCsv(
                                sp.CategoryName)
                            +
                            "\","

                            +

                            sp.UnitPrice.ToString(
                                "0.##")

                            +

                            ","

                            +

                            sp.Quantity.ToString());
                    }

                    File.WriteAllText(
                        saveFileDialog.FileName,
                        sb.ToString(),
                        Encoding.UTF8);

                    MessageBox.Show(
                        "Xuất CSV thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Lỗi khi xuất CSV: "
                        +
                        ex.Message,

                        "Lỗi",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================
        // ESCAPE CSV
        // =====================================================

        private string EscapeCsv(
            string value)
        {
            if (string.IsNullOrEmpty(
                value))
            {
                return "";
            }

            return value.Replace(
                "\"",
                "\"\"");
        }

        // =====================================================
        // EXIT
        // =====================================================

        private void MenuExit_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        // =====================================================
        // CREATE LABEL
        // =====================================================

        private Label CreateLabel(
            string text)
        {
            return new Label
            {
                Text =
                    text,

                Dock =
                    DockStyle.Fill,

                AutoSize =
                    false,

                TextAlign =
                    ContentAlignment.MiddleLeft,

                Font =
                    new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold),

                Margin =
                    new Padding(
                        2,
                        2,
                        5,
                        2),

                Padding =
                    new Padding(0)
            };
        }

        // =====================================================
        // CREATE TEXTBOX
        // =====================================================

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                Dock =
                    DockStyle.Fill,

                Multiline =
                    false,

                Font =
                    new Font(
                        "Segoe UI",
                        9F),

                BorderStyle =
                    BorderStyle.FixedSingle,

                Margin =
                    new Padding(
                        2,
                        4,
                        2,
                        4)
            };
        }
    }
}
