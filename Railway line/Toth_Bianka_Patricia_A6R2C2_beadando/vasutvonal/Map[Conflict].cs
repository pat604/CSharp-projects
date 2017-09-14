using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vasutvonal
{
    class Map
    {

        private int M;                          // a térkép magassága
        private int N;                          // a térkép szélessége
        Coordinate A = new Coordinate();        // start
        Coordinate B = new Coordinate();        // cél
        Coordinate[] coordinates;               // koordinátákat tartalmazó tömb, ami a térképet illusztrálja
        Obstacle[] obstacles;                   // akadályok (tó, hegy, város)
        
        int length;                             // az útvonal hossza. átló = 14, vízszintes/függőleges lépés = 10.
        public int Length
        {
            get { return length; }
        }


        public Map()                            // térkép akadályokkal
        {
            ReadingMapTxt();

            coordinates = new Coordinate[M * N];
            CoordinateInitializing(coordinates);            // a coordinates[] tömb feltöltése Coordinate-kel: (0,0), (0,1), (0,2) ... stb.

            ReadingObstaclesTxt();
            ObstacleCoordinates();                          // az akadályok által elfoglalt terüleket státuszát átállítom foglaltra

            // cél koordinátát megkeresem a coordinates[] tömbben és az állapotát átállítom available-re, 
            // hogy az A* algoritmus ne tekintse foglaltnak, ha azt véletlenül egy már foglalt terület helyére adta meg a felhasználó
            int end_index = IndexByCoordinate(B);                   // a cél (B) koordinátájának indexe a coordinates[] tömbben
            coordinates[end_index].Reset();
        }

        private void ReadingMapTxt()
        {
            StreamReader sr = new StreamReader("Map.txt");
            string[] T = sr.ReadLine().Split(',');
            M = int.Parse(T[0]);
            N = int.Parse(T[1]);
            T = sr.ReadLine().Split(',');
            A.X = int.Parse(T[0]);
            A.Y = int.Parse(T[1]);
            T = sr.ReadLine().Split(',');
            B.X = int.Parse(T[0]);
            B.Y = int.Parse(T[1]);
            sr.Close();
        }

        private void ReadingObstaclesTxt()      // beolvassa az akadályokat tartalmazó txt-t és ez alapján létrehozza a tavakat, hegyeket, városokat 
        {
            int number_of_obstacles = NumberOfObstacles();      // ha van legalább egy akadály, akkor létrehozza az akadály tömböt...
            if (number_of_obstacles > 0)
            {
                obstacles = new Obstacle[number_of_obstacles];

                StreamReader sr = new StreamReader("Obstacles.txt");
                string[] T;
                int i = 0;

                while (!sr.EndOfStream)                         // ... és feltölti a megfelelő akadályokkal (tó, hegy, város)
                {
                    T = sr.ReadLine().Split(',');
                    Coordinate starting_point = new Coordinate(int.Parse(T[2]), int.Parse(T[3]));
                    switch (T[0])
                    {
                        case "to":
                            obstacles[i] = new Lake(T[1], starting_point, int.Parse(T[4]), int.Parse(T[5]));
                            break;
                        case "hegy":
                            obstacles[i] = new Mountain(T[1], starting_point, int.Parse(T[4]), int.Parse(T[5]));
                            break;
                        case "varos":
                            obstacles[i] = new City(T[1], starting_point, int.Parse(T[4]), int.Parse(T[5]));
                            break;
                    }
                    i++;
                }
                sr.Close();
            }
        }

        private int NumberOfObstacles()             // megszámolja, hány akadályt adott be a felhasználó a txt-ben 
        {
            StreamReader sr = new StreamReader("Obstacles.txt");
            int number = 0;
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                number++;
            }
            return number;
        }

        private void ObstacleCoordinates()      // átállítja az akadályok által elfoglalt coordináták státuszát foglaltra 
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                for (int j = 0; j < obstacles[i].Width; j++)
                {
                    for (int k = 0; k < obstacles[i].Height; k++)
                    {
                        int x = obstacles[i].Starting_point.X + j;
                        int y = obstacles[i].Starting_point.Y + k;
                        
                        Coordinate current = new Coordinate(x, y);          // segédkoordináta a megfelelő koordináták kikereséséhez
                        try                       // átállítom a coordinates[] tömb valamilyen akadály által elfoglalt koordinátáit foglaltra
                        {
                            if (obstacles[i] is Lake)
                                coordinates[IndexByCoordinate(current)].Status = status.occupied_lake;
                            if (obstacles[i] is Mountain)
                                coordinates[IndexByCoordinate(current)].Status = status.occupied_mountain;
                            if (obstacles[i] is City)
                                coordinates[IndexByCoordinate(current)].Status = status.occupied_city;
                        }
                        // catch (NullReferenceException) { }  // ez elvileg nem kell... dobhat ilyet?
                        catch (IndexOutOfRangeException) { }          // ha a pont a térképen kívül esik, nem csinálok vele semmit
                    }
                }
            }


        }

        private int IndexByCoordinate(Coordinate coordinate)            // egy adott koordináta alapján visszaadja a koordináta tömbbeli sorszámát 
        {
            return (coordinate.X * N) + coordinate.Y;
        }

        private void CoordinateInitializing(Coordinate[] coordinates)   // a coordinates[] tömb feltöltése Coordinate-kel: (0,0), (0,1), (0,2) ... stb. 
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = new Coordinate((i / M), (i % N));
            }
        }   

        public void DrawMapCoordinate() // kirajzolja a térképet úgy, hogy a koordinátákat írja ki a megfelelő színnel
                                        // üres mező: zöld, tó: kék, hegy: szürke, város: lila. start: fehér, cél: piros. 
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (i != 0 && i % N == 0) Console.WriteLine();          // ha sor végéhez ért, új sort nyit
                if (coordinates[i].Status == status.occupied_lake)
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                else if (coordinates[i].Status == status.occupied_mountain)
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                else if (coordinates[i].Status == status.occupied_city)
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                else
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("(" + coordinates[i].X + "," + coordinates[i].Y + ")");
            }

            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].Equals(A))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(A.Y*5 + 5, A.X);
                    Console.Write("\b\b\b\b\b");
                    Console.Write("(" + coordinates[i].X + "," + coordinates[i].Y + ")");
                }

                else if (coordinates[i].Equals(B))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(B.Y * 5 + 5, B.X);
                    Console.Write("\b\b\b\b\b");
                    Console.Write("(" + coordinates[i].X + "," + coordinates[i].Y + ")");
                }

            }
            Console.ResetColor();
            Console.SetCursorPosition(0, M + 2);
        }

        public void DrawMap()           // kirajzolja a térképet.
                                        // üres mező: zöld, tó: kék, hegy: szürke, város: lila. start: fehér, cél: piros. 
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (i != 0 && i % N == 0) Console.WriteLine();
                if (coordinates[i].Status == status.occupied_lake)
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                else if (coordinates[i].Status == status.occupied_mountain)
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                else if (coordinates[i].Status == status.occupied_city)
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                else
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("  ");
            }

            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].Equals(A))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(A.Y*2 + 2, A.X);
                    Console.Write("\b\b  ");
                }

                else if (coordinates[i].Equals(B))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition((B.Y)*2 + 2, B.X);
                    Console.Write("\b\b  ");
                }

            }
            Console.ResetColor();
            Console.SetCursorPosition(0, M + 2);
        }

        public bool CoordinateExists(int x, int y)  // igazat ad vissza, ha a (x,y) koordináta létezik, azaz rajta van a térképen 
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].X == x && coordinates[i].Y == y)
                    return true;
            }

            return false;
        }

        private bool BlankCoordinate(int x, int y)  // igazat ad vissza, ha a (x,y) koordinátára lehet építkezni (nem tó, nem város, nem hegy) 
        {
            if (coordinates[x * N + y].Status == status.available)
                return true;
            else return false;
        }

        private bool CheckedField(Field field)      // igazat ad vissza, ha ad adott mező már rajta van az ellenőrzöttek listáján 
        {
            bool found;
            checked_list.Find(field, out found);
            if (found) return true;
            else return false;
        }

        private void AddingToWaitingList(Field original, int x, int y, int g)
        {
            if (CoordinateExists(x, y) && BlankCoordinate(x, y))
            {
                int h = (Math.Abs(x - B.X) + Math.Abs(y - B.Y)) * 10;
                Field field = new Field(original, x, y, g, h);

                if (!CheckedField(field))                           // ha nem szerepel a már vizsgáltak között, és...
                {
                    bool found = false;
                    waiting_list.Find(field, out found);
                    if (!found)                                     // ... ha nincs benne a várólistában, akkor hozzáadja
                        waiting_list.Add(field);
                    
                    else                                            // ... ha várólistás volt, akkor megnézi, hogy jobb-e az új útvonal ide
                    {
                        field = waiting_list.Find(field, out found);
                        if (field.G > original.G + g)
                        {
                            field.Parent = original;
                            field.G = original.G + g;
                        }
                    }
                }       
            }
            
        }

        private void UpdateWaitingList(Field original)  
        {
            waiting_list.Delete(original);
            checked_list.Add(original);

            AddingToWaitingList(original, original.X - 1, original.Y, 10);
            AddingToWaitingList(original, original.X - 1, original.Y + 1, 14);
            AddingToWaitingList(original, original.X, original.Y + 1, 10);
            AddingToWaitingList(original, original.X + 1, original.Y + 1, 14);
            AddingToWaitingList(original, original.X + 1, original.Y, 10);
            AddingToWaitingList(original, original.X + 1, original.Y - 1, 14);
            AddingToWaitingList(original, original.X, original.Y - 1, 10);
            AddingToWaitingList(original, original.X - 1, original.Y - 1, 14);
        }

        private Field FindingTheBest()  
        {
            bool found = false;
            Field best = waiting_list.FindByIndex(0, out found);
            if (found)                          // azaz nem üres a lista, tehát van még nem megvizsgált, elérhető pont
                foreach (Field item in waiting_list)
                {
                    if (item.F < best.F)
                        best = item;
                }
            return best;                        // null-al tér vissza, ha kiürült a lista
        }

        private MyLinkedList<Field> waiting_list = new MyLinkedList<Field>();
        private MyLinkedList<Coordinate> checked_list = new MyLinkedList<Coordinate>();

        private bool AccessibleEnd()    // igazat ad vissza, ha a célra a közvetlen szomszédai bármelyikéből el lehet jutni. 
                                        // (ez még nem biztosítja azt, hogy a cél megközelíthető) 
        {
            if ((CoordinateExists(B.X - 1, B.Y) && BlankCoordinate(B.X - 1, B.Y)) ||
                (CoordinateExists(B.X - 1, B.Y + 1) && BlankCoordinate(B.X - 1, B.Y + 1)) ||
                (CoordinateExists(B.X, B.Y + 1) && BlankCoordinate(B.X, B.Y + 1)) ||
                (CoordinateExists(B.X + 1, B.Y + 1) && BlankCoordinate(B.X + 1, B.Y + 1)) ||
                (CoordinateExists(B.X + 1, B.Y) && BlankCoordinate(B.X + 1, B.Y)) ||
                (CoordinateExists(B.X + 1, B.Y - 1) && BlankCoordinate(B.X + 1, B.Y - 1)) ||
                (CoordinateExists(B.X, B.Y - 1) && BlankCoordinate(B.X, B.Y - 1)) ||
                (CoordinateExists(B.X - 1, B.Y - 1) && BlankCoordinate(B.X - 1, B.Y - 1)))
                return true;
            else return false;
        }

        public void Route()      // meghatározza az optimális útvonalat. 
        {
            if (!AccessibleEnd())
                throw new Exception("az útvonal megtervezése nem lehetséges: a cél nem elérhető a térkép adottságai miatt vagy a start/cél kívül esik a térkép határain.");
            else
            {
                Field Start = new Field(A);
                waiting_list.Add(Start);
                UpdateWaitingList(Start);

                Field best = FindingTheBest();

                bool found = false;
                while (!found && best != null)                  // nincs a checked listen a cél és nem üres a várólista
                {
                    UpdateWaitingList(best);
                    checked_list.Find(B, out found);
                    if (!found)
                        best = FindingTheBest();
                }

                if (found)
                {
                    length = best.G;
                    if (A.Equals(B))    // start = cél
                        length = 0;     // ez azért kell, mert ebben az esetben nem léptünk, 
                                        //viszont a best mező a start/cél mellett van, ahonnan 10 lenne a lépés költsége
                    Console.WriteLine("a koordináták visszafelé sorrendben:");
                    Console.WriteLine("(" + B.X + "," + B.Y + ")");

                    while (!best.Equals(A))
                    {
                        best = best.Parent;
                        Console.WriteLine("(" + best.X + "," + best.Y + ")");
                    }
                }
                else
                    throw new Exception("az útvonal megtervezése nem lehetséges: a cél nem elérhető a térkép adottságai miatt vagy a start/cél kívül esik a térkép határain.");
            }



        }





    }
}
