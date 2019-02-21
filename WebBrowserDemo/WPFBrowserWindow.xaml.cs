using System;
using System.Windows;
using System.Windows.Input;

namespace WebBrowserDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WPFBrowserWindow : Window
    {
        public WPFBrowserWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (AllowsTransparency)
            {
                TitleGrid.Visibility = Visibility.Visible; Title += "-透明窗口";
            }
            ShowWebUri();
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWebUri();
        }

        private void BrowserUriTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowWebUri();
            }
        }

        private void ShowWebUri()
        {
            try
            {
                WPFWebBrowser.Navigate(BrowserUriTextBox.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #region 窗口事件

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Hide(sender, e);
        }
        private void Hide(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Visibility = Visibility.Hidden;
        }

        private void HeaderGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

    }
}
