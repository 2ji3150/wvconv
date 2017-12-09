using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace wvconv {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = vm;
        }
        public static RoutedCommand Start = new RoutedCommand();
        public static RoutedCommand Cancel = new RoutedCommand();
        ViewModel vm = new ViewModel();
        CancellationTokenSource cts;
        ParallelOptions po;
        const string encode_arg = @"/c wavpack.exe -hhx6 -m";
        const string decode_arg = @"/c wvunpack";
        int total = 0, current = 0;
        string path, ext, arg;
        private async void Start_Executed(object sender, ExecutedRoutedEventArgs e) {
            using (var dialog = new CommonOpenFileDialog() { IsFolderPicker = true }) {
                if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;
                path = dialog.FileName;
            }
            vm.Idle.Value = false;
            if (vm.Index.Value == 0) {
                ext = "*.wav";
                arg = encode_arg;
            }
            else {
                ext = "*.wv";
                arg = decode_arg;
            }
            cts = new CancellationTokenSource();
            po = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = cts.Token };
            await Task.Run(() => {
                try {
                    var files = Directory.GetFiles(path, ext, SearchOption.AllDirectories);
                    total = files.Length;
                    vm.Ptext.Value = $"0 / {total}";
                    Parallel.ForEach(files, po, f => SetProcess($"{arg} {f.WQ()}"));
                }
                catch (OperationCanceledException ex) {
                    MessageBox.Show(ex.Message);
                }
                finally {
                    SystemSounds.Asterisk.Play();
                    if (!cts.IsCancellationRequested) MessageBox.Show("complete");
                    cts.Dispose();
                    vm.Pvalue.Value = total = current = 0;
                    vm.Ptext.Value = null;
                    vm.Idle.Value = true;
                }
            });
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (MessageBox.Show("Are you sure to cancel?", "AudioSync", MessageBoxButton.YesNo, MessageBoxImage.Information) != MessageBoxResult.Yes) return;
            vm.Cancelable.Value = false;
            cts.Cancel();
        }

        void SetProcess(string arg) {
            var psi = new ProcessStartInfo() {
                FileName = "cmd.exe", Arguments = arg,
                UseShellExecute = false, CreateNoWindow = true,
            };
            Process.Start(psi).WaitForExit();
            Interlocked.Increment(ref current);
            vm.Pvalue.Value = (double)current / total;
            vm.Ptext.Value = $"{current} / {total}";
        }
    }
}
