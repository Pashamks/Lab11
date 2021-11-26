using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int semaphoreCount = 20;
        bool checkWeatherForecast = false;
        bool checkExchangeRates = false;
        bool checkStockPrices = false;
        private static Semaphore _pool;
        public MainWindow()
        {
            _pool = new Semaphore(0, semaphoreCount, "SynchronizationSemaphore");
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (checkWeatherForecast)
                OpenExistingMMF("MMF_WeatherForecast", lbWeatherForecast);
            if (checkExchangeRates)
                OpenExistingMMF("MMF_ExchangeRate", lbExchangeRates);
            if (checkStockPrices)
                OpenExistingMMF("MMF_StockPrices", lbStockPrices);
        }
        private void OpenExistingMMF(string MMF_Name, System.Windows.Controls.Label label)
        {
            char[] fileInfo;
            StringBuilder fileInfoString = new StringBuilder();
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.OpenExisting(MMF_Name))
            using (MemoryMappedViewStream stream = memoryMappedFile.CreateViewStream())
            using (BinaryReader binReader = new BinaryReader(stream))
            {
                fileInfo = binReader.ReadChars((int)stream.Length);
                for (int i = 0; i < fileInfo.Length; ++i)
                    if (fileInfo[i] != '\0')
                        fileInfoString.Append(fileInfo[i]);
            }
            label.Content = fileInfoString;
        }

        private void bWeatherForecast_Click(object sender, RoutedEventArgs e)
        {
            _pool.WaitOne();
            if (!checkWeatherForecast)
            {
                bWeatherForecast.Content = "Unsubscribe to the \n  weather forecast";
                checkWeatherForecast = true;
            }
            else
            {
                bWeatherForecast.Content = "Subscribe to the \nweather forecast";
                lbWeatherForecast.Content = "";
                checkWeatherForecast = false;
            }
        }

        private void bExchangeRates_Click(object sender, RoutedEventArgs e)
        {
            _pool.WaitOne();
            if (!checkExchangeRates)
            {
                bExchangeRates.Content = "Unsubscribe to the \n  exchange rates";
                checkExchangeRates = true;
            }
            else
            {
                bExchangeRates.Content = "Subscribe to the \n  exchange rates";
                lbExchangeRates.Content = "";
                checkExchangeRates = false;
            }
        }

        private void bStockPrices_Click(object sender, RoutedEventArgs e)
        {
            _pool.WaitOne();
            if (!checkStockPrices)
            {
                bStockPrices.Content = "Unsubscribe to the \n  stock prices";
                checkStockPrices = true;
            }
            else
            {
                bStockPrices.Content = "Subscribe to the \n    stock prices";
                lbStockPrices.Content = "";
                checkStockPrices = false;
            }
        }
    }
}
