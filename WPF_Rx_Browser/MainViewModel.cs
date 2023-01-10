using System;
using System.Reactive.Disposables;
using Microsoft.Web.WebView2.Wpf;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Rx_Browser
{
    public class MainViewModel : IDisposable, INotifyPropertyChanged
    {
        private readonly CompositeDisposable disposable = new();
        public ICommand SearchCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ForwardCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public bool backBtnEnabled { get; set; } = false;
        public bool BackBtnEnabled
        {
            get
            {
                return backBtnEnabled;
            }
            set
            {
                backBtnEnabled = value;
                OnPropertyChanged("BackBtnEnabled");
            }
        }
        public bool forwardBtnEnabled { get; set; } = false;
        public bool ForwardBtnEnabled
        {
            get
            {
                return forwardBtnEnabled;
            }
            set
            {
                forwardBtnEnabled = value;
                OnPropertyChanged("ForwardBtnEnabled");
            }
        }
        public ICommand URLTextBoxEnterKeyCommand { get; set; }
        public string searchBarURL { get; set; } = "https://www.duckduckgo.com";
        public string SearchBarURL
        {
            get
            {
                return searchBarURL;
            }
            set
            {
                searchBarURL = value;
                OnPropertyChanged("SearchBarURL");
            }
        }

        public MainViewModel(WebView2 Browser)
        {
            SearchCommand = new SimpleCommand(() => Browser.CoreWebView2.Navigate(SearchBarURL));
            BackCommand = new SimpleCommand(() => Browser.CoreWebView2.GoBack());
            ForwardCommand = new SimpleCommand(() => Browser.CoreWebView2.GoForward());
            RefreshCommand = new SimpleCommand(() => Browser.CoreWebView2.Reload());
            URLTextBoxEnterKeyCommand = new SimpleCommand(() => Browser.CoreWebView2.Navigate(SearchBarURL));
            Browser.SourceChanged += (s, e) =>
            {
                BackBtnEnabled = Browser.CoreWebView2.CanGoBack;
                ForwardBtnEnabled = Browser.CoreWebView2.CanGoForward;
                SearchBarURL = Browser.Source.ToString();
            };
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}
