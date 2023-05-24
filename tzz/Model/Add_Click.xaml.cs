using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace tzz.Model
{
    /// <summary>
    /// Логика взаимодействия для Add_Click.xaml
    /// </summary>
    public partial class Add_Click : Window
    {
        public Add_Click()
        {
            InitializeComponent();
            Products = new Products();
            DataContext = Products;
            Products.ID = Guid.NewGuid();
        }

        public Products Products { get; set; }

        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Sqlite sqlite = new Sqlite();
                Products product = new Products
                {
                    Price = Products.Price,
                    Name = Products.Name,
                    ID = Products.ID,
                    Description = Products.Description
                };

                sqlite.Products.Add(product);
                sqlite.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }

        private void CreateQR_btn_Click(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(tb_id.Text, QRCodeGenerator.ECCLevel.Q);

            QRCode qR = new QRCode(qRCodeData);

            Bitmap qr = qR.GetGraphic(150);

            image_qrcode.Source = Convert(qr);


        }
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
   

       


    
