using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MTGCardSearch.View
{
    /// <summary>
    /// Interaction logic for CardSearchWindow.xaml
    /// </summary>
    public partial class CardSearchWindow : Window
    {
        public CardSearchWindow()
        {
            InitializeComponent();
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[0-9]+");
            return regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            String text = (String)e.DataObject.GetData(typeof(String));
            if (!IsTextAllowed(text))
            {
                e.CancelCommand();
            }
        }
    }
}
