using AppNET.App;
using AppNET.Infrastructure;
using AppNET.Domain.Entities;


namespace AppNET.Presentation.WinForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        ICategoryService categoryService = IOCContainer.Resolve<ICategoryService>();
  
        private void FillCategoryGrid()
        {
            grdCategory.DataSource = categoryService.GetAll(); 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillCategoryGrid();
        }
        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(txtCategoryId.Text);
            categoryService.Create(id, txtCategoryName.Text);
            FillCategoryGrid();
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoryName = grdCategory.CurrentRow.Cells["Name"].Value.ToString();

           DialogResult result= MessageBox.Show($"{categoryName} kategorisini silmek istediğinizden emin misiniz?", 
                "Silme Onayı", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            int id = Convert.ToInt32(grdCategory.CurrentRow.Cells["Id"].Value);
            categoryService.Delete(id);
            FillCategoryGrid();

            private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
            {
                string id = gridCategory.CurrentRow.Cells["Id"].Value.ToString();
                string categoryName = gridCategory.CurrentRow.Cells["Name"].Value.ToString();
                txtCategoryId.Text = id;
                txtCategoryName.Text = categoryName;

                txtCategoryId.Enabled = false;
                Kaydet.Text = "Güncelle";
                groupBox1.Text = "Kategori Güncelle";


            }

            private void label4_Click(object sender, EventArgs e)
            {

            }

            private void label5_Click(object sender, EventArgs e)
            {

            }
            IProductService productService = IOCContainer.Resolve<IProductService>();

            private void save_urun_Click(object sender, EventArgs e)
            {
                if (save_urun.Text == "URUN KAYDET")
                {
                    productService.Create(Convert.ToInt32(txtProductId.Text),
                                          txtProductName.Text,
                                          Convert.ToInt32(txtStock.Text),
                                          Convert.ToDecimal(txtPrice.Text),
                                          Convert.ToInt32(txtCtgryId.Text),
                                          DateTime.Now
                                          );
                    Temizle();
                }
                else
                {
                    productService.Update(Convert.ToInt32(txtProductId.Text),
                                      txtProductName.Text,
                                      Convert.ToInt32(txtStock.Text),
                                      Convert.ToDecimal(txtPrice.Text),
                                      Convert.ToInt32(txtCtgryId.Text),
                                      Convert.ToDateTime(grdProduct.CurrentRow.Cells["CreatedDate"].Value),
                                      DateTime.Now);
                    txtProductId.Enabled = true;
                    save_urun.Text = "URUN KAYDET";
                    groupBox2.Text = "Yeni Ürün";

                    Temizle();
                }
                FillProductGrid();
            }
            private void Temizle()
            {
                txtProductId.Text = "";
                txtProductName.Text = "";
                txtStock.Text = "";
                txtPrice.Text = "";
                txtCtgryId.Text = "";
            }

            private void groupBox2_Enter(object sender, EventArgs e)
            {

            }

            private void silToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                string name = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
                DialogResult result = MessageBox.Show($"{Name} adlı ürünü silmek istediðinizden emin misiniz", "Ürün Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
                int id = Convert.ToInt32(grdProduct.CurrentRow.Cells["Id"].Value);
                productService.Delete(id);
                FillProductGrid();
            }

            private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
            {
                string id = grdProduct.CurrentRow.Cells["Id"].Value.ToString();
                string name = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
                string price = grdProduct.CurrentRow.Cells["Price"].Value.ToString();
                string stok = grdProduct.CurrentRow.Cells["Stock"].Value.ToString();
                string ctgry_id = grdProduct.CurrentRow.Cells["CategoryId"].Value.ToString();

                txtProductId.Text = id;
                txtProductName.Text = name;
                txtPrice.Text = price;
                txtStock.Text = stok;
                txtCtgryId.Text = ctgry_id;

                txtProductId.Enabled = false;
                save_urun.Text = "Güncelle";
                groupBox2.Text = "Ürün Güncelle";
            }
        }
    }
}