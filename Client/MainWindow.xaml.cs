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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool checkWeatherForecast = false;
        bool checkExchangeRates = false;
        bool checkStockPrices = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bWeatherForecast_Click(object sender, RoutedEventArgs e)
        {
            if (!checkWeatherForecast)
            {
                bWeatherForecast.Content = "Unsubscribe to the \n  weather forecast";
                checkWeatherForecast = true;
            }
            else
            {
                bWeatherForecast.Content = "Subscribe to the \nweather forecast";
                checkWeatherForecast = false;
            }
        }

        private void bExchangeRates_Click(object sender, RoutedEventArgs e)
        {
            if (!checkExchangeRates)
            {
                bExchangeRates.Content = "Unsubscribe to the \n  exchange rates";
                checkExchangeRates = true;
            }
            else
            {
                bExchangeRates.Content = "Subscribe to the \n  exchange rates";
                checkExchangeRates = false;
            }
        }

        private void bStockPrices_Click(object sender, RoutedEventArgs e)
        {
            if (!checkStockPrices)
            {
                bStockPrices.Content = "Unsubscribe to the \n  stock prices";
                checkStockPrices = true;
            }
            else
            {
                bStockPrices.Content = "Subscribe to the \n    stock prices";
                checkStockPrices = false;
            }
        }
    }
}
