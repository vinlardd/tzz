using System;
using System.Collections.Generic;
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

namespace tzz.Model
{
    /// <summary>
    /// Логика взаимодействия для Edit_Click.xaml
    /// </summary>
    public partial class Edit_Click : Window
    {
        private Products Products { get; set; }
        public Edit_Click(Products products)
        {
            InitializeComponent();
            Products = products;
            DataContext = Products;
            Products = products;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Products products = new Products
            {
                Price = Products.Price,
                Name = Products.Name,
                ID = Guid.NewGuid(),
                Description = Products.Description
            };
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
