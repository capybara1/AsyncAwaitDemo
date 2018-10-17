using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AsyncAwait.WindowsFormsApplication
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnSimpleExampleButtonClick(object sender, EventArgs args)
        {
            Debug.WriteLine("Start of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");

            var stream = File.Open("Test.txt", FileMode.Open);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var text = await reader.ReadLineAsync();
                Debug.WriteLine($"The read text is '{text}'");
            }

            Debug.WriteLine("Continuation of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
