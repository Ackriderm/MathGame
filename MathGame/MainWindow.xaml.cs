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

    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += TimerTick;
            SetUpGame();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
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
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(pairsOfEmoji.Count);
                    string nextEmoji = pairsOfEmoji[index];

                    textBlock.Text = nextEmoji;
                    pairsOfEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
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
                matchesFound++;
                currentTextBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}