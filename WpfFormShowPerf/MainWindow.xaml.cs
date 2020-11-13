using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFormShowPerf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        const int n = 30;
        Stopwatch sw = new Stopwatch();

        public MainWindow() {
            InitializeComponent();
        }
        void Measure(string name, Action action) {
            var times = new double[n];
            for (int i = 0; i < n; i++) {
                sw.Restart();
                action();
                sw.Stop();
                times[i] = sw.Elapsed.TotalMilliseconds;
                GC.Collect(2, GCCollectionMode.Forced);
            }
            var result = PrepareResult(name, times);
            PrintResult(result);
        }

        PerfResult PrepareResult(string name, double[] times) {
            Array.Sort(times);
            int n = times.Length;
            var result = new PerfResult() {
                Name = name,
                Min = times[0],
                Max = times[n - 1]
            };
            double total = 0;
            for (int i = 0; i < n; i++)
                total += times[i];
            result.Mean = total / n;
            if (n == 1)
                result.Median = times[0];
            else if (n % 2 == 1)
                result.Median = times[n / 2];
            else
                result.Median = (times[n / 2] + times[n / 2 - 1]) / 2;
            return result;
        }

        void PrintResult(PerfResult result) {
            var sb = new StringBuilder();
            sb.Append(textBox1.Text);
            sb.AppendLine(result.Name);
            sb.AppendLine($"Min:    {result.Min:F3} ms");
            sb.AppendLine($"Max:    {result.Max:F3} ms");
            sb.AppendLine($"Avg:    {result.Mean:F3} ms");
            sb.AppendLine($"Median: {result.Median:F3} ms");
            textBox1.Text = sb.ToString();
        }

        private void butNoRibbon_Click(object sender, RoutedEventArgs e) {
            Measure("XpfSpreadsheet w/o ribbon", RunNoRibbon);
        }

        void RunNoRibbon() {
            var window = new SpreadsheetNoRibbon();
            window.ShowDialog();
        }

        private void butRibbon_Click(object sender, RoutedEventArgs e) {
            Measure("XpfSpreadsheet ribbon designtime", RunRibbonDesigntime);
        }

        void RunRibbonDesigntime() {
            var window = new SpreadsheetRibbonDesigntime();
            window.ShowDialog();
        }
    }

    class PerfResult {
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
    }
}
