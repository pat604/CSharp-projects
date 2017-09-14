using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    class Program
    {
        /* a txt-k:
         * ékezetek nélkül, szóköz nélkül, vesszőkkel elválasztva, szám esetén pozitív egész szám:
         * 
         *      MAP.TXT:
         * 1. sor:    hosszúság,szélesség   
         * 2. sor:    kezdőpont X koordinátája,kezdőpont Y koordinátája         (X < M, Y < N)
         * 3. sor:    a cél X koordinátája,a cél Y koordinátája                 (X < M, Y < N)
         * [pl.
         * 20,20
         * 0,3
         * 18.18]
         * 
         *      OBSTACLES.TXT                                    
         * minden sor egy-egy akadálynak feleltethető meg. az akadály leírásának módja:
         * akadály típusa (hegy/to/varos),név,kezdőpont X koordinátája,kezdőpont Y koordinátája,szélesség,magasság     
         * [pl. varos,Szolnok,2,3,5,5]
         * 
         * 
         *      TRACKS.TXT
         * egy sor: egyvágányosok száma,kétvágányosok száma,elektromos egyvágányos,elektromos kétvágányos,TGVkompatibilis    
         * [pl. 20,50,100,70,20] 
         * 
         * 
         * -------------------------------------------
         * 
         * a használt algoritmus: a*.
         * egy mező hossza függőlegesen vagy vízszintesen 10 egység, átlósan 14 egység; egy pályaelem egy egységre elegendő. 
         * az útvonal a koordináták közepein áthaladva keresztül lépeget vízszintesen, függőlegesen vagy átlósan.
         */

        static public void BuildingIsReady(int cost)
        {
            Console.WriteLine("\naz építkezés költsége: " + cost); 
        }

        static void Main(string[] args)
        {             
            try
            {
                Map map = new Map();                                    
                map.DrawMap();                                          // kirajzolja a térképet
                map.Route();                                            // meghatározza az optimális útvonalat
             
                Building building = new Building(map.Length);           // a megépítendő útvonal hosszát adom át
                building.building_is_ready_observer += BuildingIsReady; // feliratkoztatás        
                building.BuildingCost();                                // építkezési költség meghatározása 
            }
            
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }            
                 
            Console.ReadKey();
     
        }
    }
}
