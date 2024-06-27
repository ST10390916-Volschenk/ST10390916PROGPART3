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

namespace ST10390916PROGPART3
{
    /// <summary>
    /// Interaction logic for ScaleWindow.xaml
    /// </summary>
    public partial class ScaleWindow : Window
    {
        public ScaleWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double recipeScale;
            bool valid = true;

            try
            {
                recipeScale = double.Parse(txtScale.Text);
                if (recipeScale <= 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show("Enter a valid amount. Eg. 2", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtScale.Focus();
                return;
            }

            MainWindow.recipeScale = recipeScale;
            this.Close();
        }
    }
}
