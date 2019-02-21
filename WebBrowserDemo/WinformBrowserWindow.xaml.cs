using System;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace WebBrowserDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinformBrowserWindow : Window
    {
        public WinformBrowserWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (AllowsTransparency)
            {
                TitleGrid.Visibility = Visibility.Visible;
                Title += "-透明窗口";
            }
            ShowWebUri();
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWebUri();
        }

        private System.Windows.Forms.WebBrowser _winformWebBrowser;
        private WindowsFormsHost _windowsFormsHost = new WindowsFormsHost();

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
                if (_winformWebBrowser == null)
                {
                    //winform
                    _winformWebBrowser = new System.Windows.Forms.WebBrowser()
                    {
                        Url = new Uri(BrowserUriTextBox.Text),
                        AllowWebBrowserDrop = false,
                        WebBrowserShortcutsEnabled = false,
                        IsWebBrowserContextMenuEnabled = false
                    };
                    _winformWebBrowser.NewWindow += (s, e1) => { e1.Cancel = true; };
                    _windowsFormsHost.Child = _winformWebBrowser;
                    BrowserGrid.Children.Add(_windowsFormsHost);
                }

                _winformWebBrowser.Navigate(BrowserUriTextBox.Text);
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
