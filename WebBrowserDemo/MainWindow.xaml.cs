using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Microsoft.Toolkit.Wpf.UI.Controls;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using WebBrowser = System.Windows.Controls.WebBrowser;

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
            WebControlTypeComboBox.SelectedIndex = 1;
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWebUri();
        }

        private WebBrowser _webBrowser;
        //private WebView _webViewBrowser;
        private WebViewCompatible _webViewCompatibleBrowser;
        private System.Windows.Forms.WebBrowser _winformWebBrowser;
        private WindowsFormsHost _windowsFormsHost;
        private WindowsXamlHost _windowsXamlHost;

        private void WebControlTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowWebUri();
        }

        private void ShowWebUri()
        {
            try
            {
                if (WebControlTypeComboBox.SelectedItem is ComboBoxItem comboBoxItem)
                {
                    if (comboBoxItem.Name == "WinformItem")
                    {
                        if (_winformWebBrowser == null)
                        {
                            //winform
                            _windowsFormsHost = new WindowsFormsHost();
                            _winformWebBrowser = new System.Windows.Forms.WebBrowser()
                            {
                                Url = new Uri("http://www.baidu.com"),
                                AllowWebBrowserDrop = false,
                                WebBrowserShortcutsEnabled = false,
                                IsWebBrowserContextMenuEnabled = false
                            };
                            _winformWebBrowser.NewWindow += (s, e1) => { e1.Cancel = true; };
                            _windowsFormsHost.Child = _winformWebBrowser;
                        }
                        BrowserGrid.Child = _windowsFormsHost;
                        _winformWebBrowser.Navigate("http://www.baidu.com");
                    }
                    else if (comboBoxItem.Name == "WPFItem")
                    {
                        if (_webBrowser == null)
                        {
                            //WPF
                            _webBrowser = new WebBrowser();
                        }
                        BrowserGrid.Child = _webBrowser;
                        _webBrowser.Navigate("http://www.baidu.com");
                    }
                    else if (comboBoxItem.Name == "WebViewItem")
                    {
                        //if (_webViewBrowser == null)
                        //{
                        //    //UWP
                        //    //注：如果WebView在后台初始化，无法显示网页，缩放窗口后则正常显示，可能因为宽高未自适应。此处暂时通过界面初始化
                        //    _webViewBrowser = new WebView();
                        //    _webViewBrowser.Visibility = Visibility.Visible;
                        //}
                        //BrowserGrid.Child = _webViewBrowser;
                        //_webViewBrowser.Navigate("http://www.baidu.com");
                    }
                    else if (comboBoxItem.Name == "WebViewCompatibleItem")
                    {
                        if (_webViewCompatibleBrowser == null)
                        {
                            //UWP兼容所有windows(WebViewCompatible控件)
                            _webViewCompatibleBrowser = new WebViewCompatible(){};
                        }
                        BrowserGrid.Child = _webViewCompatibleBrowser;
                        _webViewCompatibleBrowser.Navigate("http://www.baidu.com");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
