using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfFormShowPerf {
    /// <summary>
    /// Interaction logic for RichEditRibbonDesigntime.xaml
    /// </summary>
    public partial class RichEditRibbonDesigntime : Window {
        public RichEditRibbonDesigntime() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Dispatcher.BeginInvoke((Action)Close, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }
    }
}
