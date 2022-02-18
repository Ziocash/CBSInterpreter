using CBSInterpreter.Core;
using CBSInterpreter.Models;
using CBSInterpreter.WPF.Converters;
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
        public MainWindow()
        {
            InitializeComponent();
            _collection = ObservableCollectionConverter.ConvertIEnumerable(new CBSReader().GetEntries());
            MainDataGrid.ItemsSource = _collection;
        }
    }
}
