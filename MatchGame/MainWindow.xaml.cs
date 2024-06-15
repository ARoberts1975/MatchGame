using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MatchGame {
	public partial class MainWindow : Window {
		TextBlock lastTextBlockClicked;
		bool findingMatch = false;
		DispatcherTimer timer = new DispatcherTimer();
		int tenthsOfSecondsElapse;
		int matchesFound;

		public MainWindow() {
			InitializeComponent();
			timer.Interval = TimeSpan.FromSeconds(.1);
			timer.Tick += Timer_Tick;
			SetupGame();
		}

		private void Timer_Tick(object sender, EventArgs e) {
			tenthsOfSecondsElapse++;
			timeTextBlock.Text = (tenthsOfSecondsElapse / 10F).ToString("0.0s");
			if (matchesFound == 8) {
				timer.Stop();
				timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
			}
		}

		private void SetupGame() {
			List<string> animalEmoji = new List<string>() { "🐈", "🐈", "🦒", "🦒", "🐟", "🐟", "🐎", "🐎", "🐓", "🐓", "🦓", "🦓", "🦬", "🦬", "🐢", "🐢" };
			Random random = new Random();
			foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) {
				if(textBlock.Name != "timeTextBlock") {
					textBlock.Visibility = Visibility.Visible;
					int index = random.Next(animalEmoji.Count);
					string nextEmoji = animalEmoji[index];
					textBlock.Text = nextEmoji;
					animalEmoji.RemoveAt(index);
				}
			}
			timer.Start();
			tenthsOfSecondsElapse = 0;
			matchesFound = 0;
		}

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
			TextBlock textBlock = sender as TextBlock;
			if(findingMatch == false) {
				textBlock.Visibility = Visibility.Hidden;
				lastTextBlockClicked = textBlock;
				findingMatch = true;
			}
			else if(textBlock.Text == lastTextBlockClicked.Text) {
				matchesFound++;
				textBlock.Visibility = Visibility.Hidden;
				findingMatch = false;
			}
			else {
				lastTextBlockClicked.Visibility = Visibility.Visible;
				findingMatch = false;
			}
		}

		private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
			if (matchesFound == 8) { SetupGame(); }
		}
	}
}
