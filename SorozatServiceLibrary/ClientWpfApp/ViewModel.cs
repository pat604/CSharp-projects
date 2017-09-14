using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClientWpfApp.ServiceReference1;
using System.Windows;

namespace ClientWpfApp
{
    class ViewModel
    {

        List<string> sorozatok;
        public List<string> Sorozatok
        {
            get { return sorozatok; }
            set { sorozatok = value; }
        }

        List<Epizod> epizodok;
        public List<Epizod> Epizodok
        {
            get { return epizodok; }
            set { epizodok = value; }
        }

        SorozatServiceClient client;

        public ViewModel()
        {
            client = new SorozatServiceClient();
            kivalasztottEpizodok = new List<Epizod>();
        }

        public void SorozatRefresh()
        {
            client.SorozatokAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                        MessageBox.Show("ERROR");
                    else
                        sorozatok = task.Result.ToList();
                }).Wait();

        }

        string selectedSorozatCim;
        public string SelectedSorozatCim
        {
            get { return selectedSorozatCim; }
            set { selectedSorozatCim = value; }
        }

        public void EpizodokRefresh()
        {
            client.EpizodokAsync(selectedSorozatCim).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                        MessageBox.Show("ERROR");
                    else
                        epizodok = task.Result.ToList();
                }).Wait();
        }

        List<Epizod> kivalasztottEpizodok;
        public List<Epizod> KivalasztottEpizodok
        {
            get { return kivalasztottEpizodok; }
            set { kivalasztottEpizodok = value; }
        }
    }
}
