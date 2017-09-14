using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace vasutvonal
{

    delegate void BuildingIsReady(int cost);

    class Building
    {
        int route_length;                       // a megépítendő útvonal hossza
        Tree<MyLinkedCompList<Track>> tracks = new Tree<MyLinkedCompList<Track>>();  
                                                // fa, ami olyan láncolt listákat tárol, amik pályaelemeket tartalmaznak
        int number_of_single_tracked;
        int number_of_double_tracked;
        int number_of_electrified_single;
        int number_of_electrified_double;
        int number_of_TGVcompatible;

        public Building(int length)
        {
            route_length = length;
            ReadingTracksTxt();
            GeneratingTracks();
        }

        private void ReadingTracksTxt()                 // beolvassa a Tracks.txt-ből, hogy melyik pályaelemből hány db van 
        {
            StreamReader sr = new StreamReader("Tracks.txt");
            string[] T = sr.ReadLine().Split(',');
            number_of_single_tracked = int.Parse(T[0]);
            number_of_double_tracked = int.Parse(T[1]);
            number_of_electrified_single = int.Parse(T[2]);
            number_of_electrified_double = int.Parse(T[3]);
            number_of_TGVcompatible = int.Parse(T[4]);
            sr.Close();
        }

        private void Generate(int number, Type type)    // segédmetódus a GeneratingTracks()-hez 
        {
            if (number > 0)
            {
                MyLinkedCompList<Track> list = new MyLinkedCompList<Track>();
                for (int i = 0; i < number; i++)
                    list.Add(new Track(type));
                tracks.Add(list);
            }
        }

        private void GeneratingTracks()                 // hozzáadja a fához a pályaelemeket tartalmazó listákat 
        {
            Generate(number_of_single_tracked, Type.single_tracked);
            Generate(number_of_double_tracked, Type.double_tracked);
            Generate(number_of_electrified_single, Type.electrified_single);
            Generate(number_of_electrified_double, Type.electrified_double);
            Generate(number_of_TGVcompatible, Type.TGVcompatible);
        }

        private MyLinkedCompList<Track> TrackOrder()    // készít a több, fában tárolt listából egy, az összes pályaelemet tartalmazó listát, 
                                                        // ahol a pályaelemek költség szerint növekvő módon rendezettek 
        {
            MyLinkedCompList<Track> order = new MyLinkedCompList<Track>();
            int number = 0;
            Track track = new Track();
            foreach (MyLinkedCompList<Track> l in tracks)
            {
                track = l.GetFirst();
                switch (track.Type)
                {
                    case Type.single_tracked:
                        number = number_of_single_tracked;
                        break;
                    case Type.double_tracked:
                        number = number_of_double_tracked;
                        break;
                    case Type.electrified_single:
                        number = number_of_electrified_single;
                        break;
                    case Type.electrified_double:
                        number = number_of_electrified_double;
                        break;
                    case Type.TGVcompatible:
                        number = number_of_TGVcompatible;
                        break; 
                }
                
                for (int i = 0; i < number; i++)
                    order.Add(track);               
            }
            
            return order;
        }

        public event BuildingIsReady building_is_ready_observer;

        public int BuildingCost()                       // meghatározza a építkezési költséget (ha meg tudjuk építeni végig a vonalat) 
        {
            int cost = 0;                               // költség
            int sum = number_of_single_tracked + number_of_double_tracked + number_of_electrified_single  // a pályaelemek hossza összesen
                + number_of_electrified_double + number_of_TGVcompatible;

            if (sum < route_length)
                throw new Exception("az út nem megépíthető, mert nincs elég pályaelem.");
            else            // ha végig megépíthető az út
            {

                MyLinkedCompList<Track> order = TrackOrder();       // költség szerint rendezett lista, ami az összes pályaelemet tartalmazza
                int i = 0;
                foreach (Track item in order)                       // amíg az út el nem készül, a pályaelemek költségét összegzi
                {
                    if (i < route_length)
                        cost += item.Cost();
                    i++;
                }

                if (building_is_ready_observer != null)             // a költség átadása a figyelőnek, ha van feliratkozott tag
                    building_is_ready_observer(cost);      
            }
            
            return cost;
        }


    }
}
