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
        IProductService productService = IOCContainer.Resolve<IProductService>();
        private object txtProductName;

        private void FillCategoryGrid()
        {
            grdCategory.DataSource = categoryService.GetAll();
        }
        private void FillProductGrid()
        {

        }
        private void FillProductCbb()
        {
            cbbCategory.DataSource = categoryService.GetAll();
            cbbCategory.DisplayMember = nameof(Category.Name);
            cbbCategory.ValueMember = nameof(Category.Id);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            FillCategoryGrid();
            FillProductGrid();
            FillProductCbb();
        }
        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (btnSaveCategory.Text == "KAYDET")
            {
                int id = Convert.ToInt32(txtCategoryId.Text);
                categoryService.Create(id, txtCategoryName.Text);
            }
            else
            {
                categoryService.Update(Convert.ToInt32(txtCategoryId.Text), txtCategoryName.Text);
                btnSaveCategory.Text = "KAYDET";
                groupBox1.Text = "Yeni Kategori";
                txtCategoryId.Enabled = true;
            }
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
            FillCategoryGrid();
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoryName = grdCategory.CurrentRow.Cells["Name"].Value.ToString();

            DialogResult result = MessageBox.Show($"{categoryName} kategorisini silmek istediğinizden emin misiniz?",
                 "Silme Onayı",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            int id = Convert.ToInt32(grdCategory.CurrentRow.Cells["Id"].Value);
            categoryService.Delete(id);
            FillCategoryGrid();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "KAYDET")
            {
                productService.Create(Convert.ToInt32(txtProductId.Text), txtProductName.Text, Convert.ToInt32(txtStock.Text), Convert.ToInt32(cbbCategory.SelectedValue), Convert.ToDecimal(txtPrice.Text));

                cbbCategory.SelectedIndex = 0;
            }
            else
            {
                var id = Convert.ToInt32(txtProductId.Text);
                Product p = productService.GetAll().FirstOrDefault(x => x.Id == id);
                p.Name = txtProductName.Text;
                p.Stock = Convert.ToInt32(txtStock.Text);
                p.Price = Convert.ToDecimal(txtPrice.Text);
                productService.Update(Convert.ToInt32(txtProductId.Text), p);

            }

            FillCategoryGrid();

            txtStock.Text = "";
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtStock.Text = "";
            txtPrice.Text = "";
            cbbCategory.SelectedIndex = 0;
            btnSave.Text = "KAYDET";
            txtProductId.Enabled = true;
        }

        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtProductId.Text = grdProduct.CurrentRow.Cells[nameof(Product.Id)].Value.ToString();
            txtProductName.Text = grdProduct.CurrentRow.Cells[nameof(Product.Name)].Value.ToString();
            txtStock.Text = grdProduct.CurrentRow.Cells[nameof(Product.Stock)].Value.ToString();
            txtPrice.Text = grdProduct.CurrentRow.Cells[nameof(Product.Price)].Value.ToString();
            //cbbCategory.SelectedIndex = cbbCategory.Items.IndexOf(categoryService.GetById(Convert.ToInt32(grdProduct.CurrentRow.Cells[nameof(Product.CategoryId)].Value)));
            btnSave.Text = "GÜNCELLE";
            txtProductId.Enabled = false;
        }

        private void silToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            productService.Delete(
                Convert.ToInt32(grdProduct.CurrentRow.Cells[nameof(Product.Id)].Value));
            FillProductGrid();
        }

        private void save_urun_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
            if (save_urun.Text == "URUN KAYDET")
            {
                productService.Create(Convert.ToInt32(txtProductId.Text),
                                      txtProductName.Text,
                                      Convert.ToInt32(txtStock.Text),
                                      Convert.ToDecimal(txtPrice.Text),
                                      Convert.ToDecimal(txtBuyingPrice.Text),
                                      Convert.ToInt32(cbbCategory.SelectedValue)
                                      );



                transaction.Total = Convert.ToInt32(txtStock.Text) * Convert.ToDecimal(txtBuyingPrice.Text); ;
                transaction.Comment = $"{txtProductName.Text} ürününden {txtStock.Text} satýn alindi";
                transaction.TransactionDate = DateTime.Now;

                salesService.Create(0, gelir: null, transaction);
                Temizle();
            }
            else
            {


                int id = Convert.ToInt32(txtProductId.Text);
                Product newProduct = productService.GetAll().FirstOrDefault(x => x.Id == id);
                newProduct.Name = txtProductName.Text;
                newProduct.SalePrice = Convert.ToDecimal(txtPrice.Text);
                newProduct.Stock = Convert.ToInt32(txtStock.Text);
                newProduct.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
                newProduct.CategoryId = Convert.ToInt32(cbbCategory.SelectedValue);
                productService.Update(Convert.ToInt32(txtProductId.Text), newProduct);

                txtProductId.Enabled = true;
                save_urun.Text = "URUN KAYDET";
                groupBox2.Text = "Yeni Ürün";


                Temizle();
            }
            FillProductGrid();
            FillVaultGrid();
            FillUrunCbb();
        }
        private void Temizle()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtStock.Text = "";
            txtPrice.Text = "";
            txtBuyingPrice.Text = "";
            cbbCategory.SelectedIndex = 0;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void silToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            DialogResult result = MessageBox.Show($"{Name} adlý ürünü silmek istediðinizden emin misiniz", "Ürün Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;
            int id = Convert.ToInt32(grdProduct.CurrentRow.Cells["Id"].Value);
            productService.Delete(id);
            FillProductGrid();
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtProductId.Text = grdProduct.CurrentRow.Cells[nameof(Product.Id)].Value.ToString();
            txtProductName.Text = grdProduct.CurrentRow.Cells["Name"].Value.ToString();
            txtPrice.Text = grdProduct.CurrentRow.Cells[nameof(Product.SalePrice)].Value.ToString();
            txtBuyingPrice.Text = grdProduct.CurrentRow.Cells[nameof(Product.BuyingPrice)].Value.ToString();
            txtStock.Text = grdProduct.CurrentRow.Cells["Stock"].Value.ToString();
            txtBuyingPrice.Text = grdProduct.CurrentRow.Cells["CategoryId"].Value.ToString();
            cbbCategory.SelectedValue = Convert.ToInt32(grdProduct.CurrentRow.Cells[nameof(Product.CategoryId)].Value);

            txtProductId.Enabled = false;
            save_urun.Text = "Güncelle";
            groupBox2.Text = "Ürün Güncelle";
            FillProductGrid();
        }

        private void txtCtgryId_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdKasa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbbUrun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void satis_yap_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
            int id = Convert.ToInt32(cbbUrun.SelectedValue);
            Product product = productService.GetById(id);



            if (txtAdet.Text == "")
            {
                MessageBox.Show("Yapýlacak Satýþ adedi giriniz:", "Dikkat", MessageBoxButtons.OK);
                return;

            }
            else if (Convert.ToInt32(txtAdet.Text) > product.Stock)
            {
                MessageBox.Show($"En fazla {product.Stock} adet ürün satýlabilir ");
                return;
            }
            else
            {
                transaction.Total = Convert.ToInt32(txtAdet.Text) * product.SalePrice;
                transaction.Comment = $"{product.Name} ürününden {txtAdet.Text} ürün satýldý";
                transaction.TransactionDate = DateTime.Now;
                product.Stock = product.Stock - Convert.ToInt32(txtAdet.Text);
                productService.Update(product.Id, product);
                salesService.Create(0, transaction, null);
            }
            FillVaultGrid();
            FillProductGrid();

        }
    }
}
           
       

        
    
