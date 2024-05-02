using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Interval = TimeSpan.FromSeconds(.1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            SetUpGame();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                dispatcherTimer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>
            {
                "🐵","🐵",
                "🐶","🐶",
                "🐺","🐺",
                "🐱","🐱",
                "🦁","🦁",
                "🐯","🐯",
                "🦉","🦉",
                "🦊","🦊",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;

                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            dispatcherTimer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                if (findingMatch == false)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    lastTextBlockClicked = textBlock;
                    findingMatch = true;
                }
                else if (textBlock.Text == lastTextBlockClicked.Text)
                {
                    matchesFound++;
                    textBlock.Visibility = Visibility.Hidden;
                    findingMatch = false;
                }
                else
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                    findingMatch = false;
                }
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
