using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebRanking.Interfaces;
using WebRanking.Models;

namespace WebRanking.ViewModels
{
    public class MainWindowViewModel : IViewMainWindowViewModel, INotifyPropertyChanged
    {
        private readonly ISearchService _searchService;
        private string _resultText;
        private string _keyword;
        private string _url;

        public bool IsBusy { get; set; }

        public IAsyncCommand Submit { get; private set; }

        public MainWindowViewModel(ISearchService searchService)
        {
            Keyword = "conveyancing software";
            Url = "www.smokeball.com.au";
            _searchService = searchService;
            Submit = new AsyncCommand(ExecuteSubmitAsync, CanExecuteSubmit);
        }

        public string ResultText
        {
            get
            {
                return _resultText;
            }
            set
            {
                _resultText = value;
                OnPropertyChanged("ResultText");
            }
        }

        public string Keyword
        {
            get
            {
                return _keyword;
            }
            set
            {
                _keyword = value;
                OnPropertyChanged("Keyword");
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async Task ExecuteSubmitAsync()
        {
            try
            {
                IsBusy = true;
                var items = await _searchService.FetchWebItemsAsync(Keyword, Url);

                if (items.Any(_ => _.ContainUrl))
                {
                    ResultText = string.Join(",", items.Where(_ => _.ContainUrl).Select(_ => _.Rank.ToString()));
                }
                else
                {
                    ResultText = "0";
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteSubmit()
        {
            return !IsBusy;
        }
    }
}
