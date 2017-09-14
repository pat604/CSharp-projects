using ClientWpfApp.ServiceReference1;
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

namespace ClientWpfApp
{

    public partial class MainWindow : Window
    {
        ViewModel VM;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            VM = new ViewModel();

            idoSum = 0;

            SorozatokBetoltese();
        }

        private void SorozatokBetoltese()
        {
            VM.SorozatRefresh();
            List<string> sorozatok = VM.Sorozatok;

            foreach (string item in sorozatok)
            {
                Dispatcher.Invoke(() =>
                    {
                        comboBoxSorozatok.Items.Add(item);
                    });
            }
        }

        private void comboBoxSorozatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.SelectedSorozatCim = comboBoxSorozatok.SelectedItem.ToString();
            VM.EpizodokRefresh();
            List<Epizod> epizodok = VM.Epizodok;

            listBoxElerhetoEpizodok.Items.Clear();

            foreach (Epizod item in epizodok)
            {
                Dispatcher.Invoke(() =>
                    {
                        listBoxElerhetoEpizodok.Items.Add(item.Cim);
                    });
            }
        }

        int idoSum;

        private void Button_Click_1(object sender, RoutedEventArgs e) // hozzáad gomb
        {
            //VM.KivalasztottEpizodok.Add((Epizod)listBoxElerhetoEpizodok.SelectedItem);

            foreach (Epizod item in VM.Epizodok)
            {
                if (item.Cim == listBoxElerhetoEpizodok.SelectedItem)
                    VM.KivalasztottEpizodok.Add(item);
            }

            listBoxKivalasztottEpizodok.Items.Clear();

            foreach (Epizod item in VM.KivalasztottEpizodok)
            {
                Dispatcher.Invoke(() =>
                    {
                        listBoxKivalasztottEpizodok.Items.Add(item.Cim);
                        idoSum += item.Hossz;
                        labelIdo.Content = idoSum;
                    });
            }


        }


    }
}
