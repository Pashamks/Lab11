using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace LinuxClient
{
    public partial class MainWindow : Window
    {
        int semaphoreCount = 20;

        private Dictionary<string, bool> checks = new Dictionary<string, bool>()
        {
            {"WeatherForecast", false},
            {"ExchangeRate", false},
            {"StockPrice", false}
        };
        bool checkWeatherForecast = false;
        bool checkExchangeRates = false;
        bool checkStockPrices = false;

        private Label lbWeatherForecast;

        private Label lbExchangeRates;

        private Label lbStockPrices;

        private Button bWeatherForecast;

        private Button bExchangeRates;

        private Button bStockPrices;
        //private static Semaphore _pool;
        public MainWindow()
        {
            //_pool = new Semaphore(0, semaphoreCount, "SynchronizationSemaphore");
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            lbWeatherForecast = this.FindControl<Label>("lbWeatherForecast");

            lbExchangeRates = this.FindControl<Label>("lbExchangeRates");

            lbStockPrices = this.FindControl<Label>("lbStockPrices");

             bWeatherForecast = this.FindControl<Button>("bWeatherForecast");

             bExchangeRates = this.FindControl<Button>("bExchangeRates");

             bStockPrices = this.FindControl<Button>("bStockPrices");
        }
        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (checks["WeatherForecast"])
                await PipeConnect("WeatherForecast.pipe", lbWeatherForecast);
            if (checks["ExchangeRate"])
                await PipeConnect("ExchangeRate.pipe", lbExchangeRates);
            if (checks["StockPrice"])
                await PipeConnect("StockPrice.pipe", lbStockPrices);
        }

        private async Task PipeConnect(string PipeName, Label label)
        {
            string fileInfo = "";
            StringBuilder fileInfoString = new StringBuilder();
            using (NamedPipeClientStream pipe =
                new NamedPipeClientStream(".", PipeName, PipeDirection.InOut)) {
                await pipe.ConnectAsync();
                using (StreamReader sr = new StreamReader(pipe))
                {
                    // Display the read text to the console
                    string temp;

                    // Read the server data and echo to the console.
                    while ((temp = (await sr.ReadLineAsync())) != null)
                    {
                        
                        fileInfo += await sr.ReadToEndAsync();
                    }
                }
                fileInfoString.Append(fileInfo);
                label.Content = fileInfoString;
                pipe.Close();
            }
        }

        private void bWeatherForecast_Click(object sender, RoutedEventArgs e)
        {
            //_pool.WaitOne();
            if (!checks["WeatherForecast"])
            {
                bWeatherForecast.Content = "Unsubscribe to the \n  weather forecast";
                checks["WeatherForecast"] = true;
            }
            else
            {
                bWeatherForecast.Content = "Subscribe to the \nweather forecast";
                lbWeatherForecast.Content = "";
                checks["WeatherForecast"] = false;
            }
        }

        private void bExchangeRates_Click(object sender, RoutedEventArgs e)
        {
            //_pool.WaitOne();
            if (!checks["ExchangeRate"])
            {
                bExchangeRates.Content = "Unsubscribe to the \n  exchange rates";
                checks["ExchangeRate"] = true;
            }
            else
            {
                bExchangeRates.Content = "Subscribe to the \n  exchange rates";
                lbExchangeRates.Content = "";
                checks["ExchangeRate"] = false;
            }
        }

        private void bStockPrices_Click(object sender, RoutedEventArgs e)
        {
            //_pool.WaitOne();
            if (!checks["StockPrice"])
            {
                bStockPrices.Content = "Unsubscribe to the \n  stock prices";
                checks["StockPrice"] = true;
            }
            else
            {
                bStockPrices.Content = "Subscribe to the \n    stock prices";
                lbStockPrices.Content = "";
                checks["StockPrice"] = false;
            }
        }
    }
}