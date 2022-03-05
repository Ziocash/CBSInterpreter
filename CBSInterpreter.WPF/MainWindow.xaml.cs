using CBSInterpreter.Core;
using CBSInterpreter.Models;
using CBSInterpreter.Models.Exceptions;
using CBSInterpreter.WPF.Converters;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows;

namespace CBSInterpreter.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CBSEntry> _collection;
        private readonly ILogger<MainWindow> _logger;
        public MainWindow(ILogger<MainWindow> logger)
        {
            _logger = logger;
            InitializeComponent();
            try
            {
                _collection = ObservableCollectionConverter.ConvertIEnumerable(new CBSReader().GetEntries());
                MainDataGrid.ItemsSource = _collection;
            }
            catch(CBSReaderException ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                if(ex.InnerException is not null)
                    MessageBox.Show(ex.Message, ex.InnerException.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                _collection = new();
            }
        }
    }
}
