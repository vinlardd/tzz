using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using tzz.Model;

namespace tzz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Products> ProductsList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ProductsList = new();
            DataContext = this;
            this.Loaded += Sqlite_Loaded;
        }

        private void Sqlite_Loaded(object sender, RoutedEventArgs e)
        {
            Sqlite sqlite = new Sqlite();
            sqlite.Database.Migrate();
            List<Products> products = sqlite.Products.ToList();
            ProductList.ItemsSource = products;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            foreach (Products product in products)
            {
                string combined = "Уникальный идентификатор: " + product.ID + "\r\n" + "Имя товара: " + product.Name + "\r\n" + "Описание товара: " + product.Description + "\r\n" + "Цена товара: " + product.Price + " RUB";
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(combined, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                BitmapImage qrCodeImage = new BitmapImage();
                using (MemoryStream stream = new MemoryStream())
                {
                    qrCode.GetGraphic(20).Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    qrCodeImage.BeginInit();
                    qrCodeImage.CacheOption = BitmapCacheOption.OnLoad;
                    qrCodeImage.StreamSource = stream;
                    qrCodeImage.EndInit();
                }

                ProductsList.Add(new Products { Name = product.Name, Price = product.Price, QRCode = qrCodeImage, Description = product.Description, ID = product.ID });
            }
            ProductList.ItemsSource = ProductsList;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
         
                Model.Add_Click add = new Model.Add_Click();
                Close();
                add.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem != null)
            {

                var product = ProductList.SelectedItem as Products;
                if (new Model.Edit_Click(product).ShowDialog() == true)
                {
                    using (var context = new Sqlite())
                    {
                        context.Entry(product).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    ProductList.Items.Refresh();
                }
            }
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var product = ProductList.SelectedItem as Products;
                    using (var context = new Sqlite())
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        ProductList.ItemsSource = context.Products.ToList();


                    }
                }
            }
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }

    }
}

