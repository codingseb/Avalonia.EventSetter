using Avalonia.Controls;
using Avalonia.Input;

namespace Samples
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Source is TextBox textBox)
            {
                if(e.Key == Key.Down)
                {
                    if(double.TryParse(textBox.Text, out double value));
                    {
                        if(value <= 0)
                        {
                            textBox.Text = "0";
                        }
                        else
                        {
                            textBox.Text = (value - 1).ToString();
                        }
                    }

                    textBox.SelectAll();
                }
                else if(e.Key == Key.Up)
                {
                    if(double.TryParse(textBox.Text, out double value));
                    {
                        textBox.Text = (value + 1).ToString();
                    }

                    textBox.SelectAll();
                }
            }
        }
    }
}
