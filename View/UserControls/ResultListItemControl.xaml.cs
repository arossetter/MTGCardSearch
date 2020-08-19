using MTGCardSearch.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTGCardSearch.View.UserControls
{
    /// <summary>
    /// Interaction logic for ResultListItem.xaml
    /// </summary>
    public partial class ResultListItem : UserControl
    {


        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Card.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CardProperty =
            DependencyProperty.Register("Card", typeof(Card), typeof(ResultListItem), new PropertyMetadata(null, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ResultListItem item = d as ResultListItem;

            if(item != null)
            {
                item.cardNameTextBlock.Text = (e.NewValue as Card).name;

                // TODO: Get list of images from Card's mana cost.
                List<string> manaCostList = GetManaCostFromText((e.NewValue as Card).manaCost);
                for(int i = 0; i < manaCostList.Count; ++i)
                {
                    Image img = item.manaCostPanel.Children[i] as Image;
                    img.Source = new BitmapImage(new Uri(manaCostList[i], UriKind.Relative));
                }
            }
        }

        private static List<string> GetManaCostFromText(string manaCost)
        {
            List<string> iconList = new List<string>();
            string iconString = "Icons/{0}.jfif";

            if(!string.IsNullOrWhiteSpace(manaCost))
            {
                // Parse manaCost, grabbing all values between curly brackets { } and saving off as images.
                char[] separators = { '{', '}' };
                string[] icons = manaCost.Split(separators);

                foreach (var icon in icons)
                {
                    if (!string.IsNullOrWhiteSpace(icon))
                    {
                        iconList.Add(string.Format(iconString, icon));
                    }
                }
            }

            return iconList;
        }

        public ResultListItem()
        {
            InitializeComponent();
        }
    }
}
