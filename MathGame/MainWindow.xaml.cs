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

namespace MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            var pairsOfEmoji = new List<string>
            {
                "❤","❤",
                "🎂","🎂",
                "😜","😜",
                "👀", "👀",
                "🤘","🤘",
                "👂","👂",
                "👎","👎",
                "👍","👍",
            };

            var random = new Random();

            foreach (TextBlock textBlock in MainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(pairsOfEmoji.Count);
                string nextEmoji = pairsOfEmoji[index];

                textBlock.Text = nextEmoji;
                pairsOfEmoji.RemoveAt(index);
            }
        }

        TextBlock lastClickedTextBlock;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock? currentTextBlock = sender as TextBlock;

            if (findingMatch == false)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock = currentTextBlock;
                findingMatch = true;
            }
            else if (currentTextBlock.Text == lastClickedTextBlock.Text)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}