using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RS.InternetTime
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Timer _timer;

		public MainWindow()
		{
			InitializeComponent();

			SetWindowPosition();
			UpdateTime();

			InitTimer();
		}

		private void InitTimer()
		{
			_timer = new Timer(250);
			_timer.Elapsed += _timer_Elapsed;
			_timer.Start();
		}

		private void SetWindowPosition()
		{
			// Get settings
			Int32 posX = Properties.Settings.Default.PosX;
			Int32 posY = Properties.Settings.Default.PosY;

			// Set
			this.Left =  posX == -1 ? System.Windows.SystemParameters.WorkArea.TopRight.X - this.Width : posX;
			this.Top = posY == -1 ? 0 : posY;

			// Persist - in case first time through
			SaveWindowLocation();
		}

		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			// Prevent overlapping events
			_timer.Stop();
			UpdateTime();
			_timer.Start();
		}

		private void UpdateTime()
		{
			// Update the UI on the UI thread
			Dispatcher.Invoke(() =>
			{
				timeDisplay.Content = DateTime.Now.ToInternetTimeStr();
			});
		}

		private void timeDisplay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ExitApplication();
		}

		private void ExitApplication()
		{
			_timer.Stop();
			Application.Current.Shutdown();
		}

		private void LaunchAbout()
		{
			System.Diagnostics.Process.Start("http://richstokoe.com/blog/?id=28");
		}

		private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
		{
			LaunchAbout();
		}

		private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ExitApplication();
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void Window_MouseUp(object sender, MouseButtonEventArgs e)
		{
			SaveWindowLocation();
		}

		private void SaveWindowLocation()
		{
			Properties.Settings.Default.PosX = (Int32) this.Left;
			Properties.Settings.Default.PosY = (Int32) this.Top;
			Properties.Settings.Default.Save();
		}
	}
}
