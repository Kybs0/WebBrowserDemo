using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;

namespace WebBrowserDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WebControlTypeComboBox.SelectedIndex = 0;
        }

        private void ShowWebUri()
        {
            try
            {
                if (WebControlTypeComboBox.SelectedItem is ComboBoxItem comboBoxItem)
                {
                    Window window = null;
                    if (comboBoxItem.Name == "WinformItem")
                    {
                        window = new WinformBrowserWindow() { Owner = this };
                    }
                    else if (comboBoxItem.Name == "WPFItem")
                    {
                        window = new WPFBrowserWindow() { Owner = this };
                    }
                    else if (comboBoxItem.Name == "WebViewItem")
                    {
                        window = new UWPBrowserWindow() { Owner = this };
                    }
                    else if (comboBoxItem.Name == "WebViewCompatibleItem")
                    {
                        window = new UWPCompatibleBrowserWindow() { Owner = this };
                    }
                    if (TransparentStyleButton.IsChecked == true)
                    {
                        window.AllowsTransparency = true;
                        window.WindowStyle = WindowStyle.None;
                        window.Background = Brushes.Transparent;
                    }

                    window.ShowDialog();
                }
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

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWebUri();
        }
    }
}
