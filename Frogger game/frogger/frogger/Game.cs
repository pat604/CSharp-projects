using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace frogger
{
    class Game
    {
        public const int rows = 12;     // sorok száma = pálya magassága
        public int RowsXaml             // a sorok pixelszámmal felszorzott értéke - a seawindow.xaml fogja beolvasni    
        {
            get { return (Game.rows + 2) * p; }
        }

        public const int columns = 15;  // oszlopok száma = a pálya szélessége
        public int ColumnsXaml          // az oszlopok pixelszámmal felszorzott értéke - a seawindow.xaml fogja beolvasni 
        {
            get { return Game.columns * p + 15; }
        }        

        public const int p = 40;        // egy mező magassága és szélessége pixelben

        protected bool end;
        public bool End
        {
            get { return end; }
            set { end = value; }
        }

        protected int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        protected Frog frog;
        public Frog Frog
        {
            get { return frog; }
        }

        public void FrogMoves(Direction dir)
        {
            if (!end)
                frog.Moves(dir);
        }

        List<Fish> fishes;
        public List<Fish> Fishes
        {
            get { return fishes; }
        }

        List<SeaAnimal> seaanimals;
        public List<SeaAnimal> Seaanimals
        {
            get { return seaanimals; }
        }

        public Game(int level)
        {
            this.level = level;

            frog = new Frog(((columns) / 2), (rows - 2), 1, 1);     // béka a kiindulópontra

            fishes = new List<Fish>();
            seaanimals = new List<SeaAnimal>(); 

            // a pálya feltöltése állatokkal (Actor) a szintnek megfelelően

            if (level == 1)
            {
                // 1. sor, halak, jobbra
                fishes.Add(new Fish(0, rows - 3, 1, 1, true));
                fishes.Add(new Fish(4, rows - 3, 1, 1, true));
                fishes.Add(new Fish(8, rows - 3, 1, 1, true));

                // 2. sor, tengeri állatok, balra
                seaanimals.Add(new SeaAnimal(columns, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 9, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 4, 1, 1, false));

                // 3. sor, halak, jobbra
                fishes.Add(new Fish(1, rows - 5, 1, 1, true));
                fishes.Add(new Fish(5, rows - 5, 1, 1, true));

                // 4. és 5. sor, tengeri állatok, balra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 6, 1, 1, false));

                seaanimals.Add(new SeaAnimal(columns, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 7, 1, 1, false));

                // 6. sor üres

                // 7. sor, tengeri állatok, jobbra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 9, 1, 1, true));

                // 8. sor, halak, balra
                fishes.Add(new Fish(0, rows - 10, 1, 1, false));
                fishes.Add(new Fish(2, rows - 10, 1, 1, false));
                fishes.Add(new Fish(3, rows - 10, 1, 1, false));
                fishes.Add(new Fish(4, rows - 10, 1, 1, false));
                fishes.Add(new Fish(8, rows - 10, 1, 1, false));
            }

            else if (level == 2)
            {
                // 1. sor, halak, jobbra
                fishes.Add(new Fish(3, rows - 3, 1, 1, true));
                fishes.Add(new Fish(4, rows - 3, 1, 1, true));
                fishes.Add(new Fish(6, rows - 3, 1, 1, true));
                fishes.Add(new Fish(9, rows - 3, 1, 1, true));

                // 2. sor, tengeri állatok, balra
                seaanimals.Add(new SeaAnimal(columns, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 4, 1, 1, false));                
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 7, rows - 4, 1, 1, false));                
                seaanimals.Add(new SeaAnimal(columns - 9, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 4, 1, 1, false));


                // 3. sor, halak, jobbra
                fishes.Add(new Fish(4, rows - 5, 1, 1, true));
                fishes.Add(new Fish(8, rows - 5, 1, 1, true));

                // 4. és 5. sor, tengeri állatok, balra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 6, 1, 1, false));

                seaanimals.Add(new SeaAnimal(columns, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 7, 1, 1, false));


                // 6. sor üres

                // 7. sor, tengeri állatok, jobbra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 7, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 9, 1, 1, true));


                // 8. sor, halak, balra
                fishes.Add(new Fish(1, rows - 10, 1, 1, false));
                fishes.Add(new Fish(2, rows - 10, 1, 1, false));
                fishes.Add(new Fish(3, rows - 10, 1, 1, false));
                fishes.Add(new Fish(7, rows - 10, 1, 1, false));
                fishes.Add(new Fish(8, rows - 10, 1, 1, false));
                fishes.Add(new Fish(12, rows - 10, 1, 1, false));              
            }

            if (level == 3)
            {
                // vegyes sorok (is)

                // 1. sor, jobbra
                fishes.Add(new Fish(1, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(2, rows - 3, 1, 1,  true));
                fishes.Add(new Fish(4, rows - 3, 1, 1, true));
                fishes.Add(new Fish(6, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(8, rows - 3, 1, 1, true));
                fishes.Add(new Fish(9, rows - 3, 1, 1, true));

                // 2. sor, balra
                seaanimals.Add(new SeaAnimal(columns, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 4, 1, 1, false));


                // 3. sor, halak, jobbra
                fishes.Add(new Fish(2, rows - 5, 1, 1, true));
                fishes.Add(new Fish(9, rows - 5, 1, 1, true));
                fishes.Add(new Fish(13, rows - 5, 1, 1, true));


                // 4. sor, balra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 6, 1, 1, false));

                // 5. sor, jobbra
                seaanimals.Add(new SeaAnimal(columns, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 7, 1, 1, true));


                // 6. sor, jobbra
                fishes.Add(new Fish(0, rows - 8, 1, 1, true));
                fishes.Add(new Fish(3, rows - 8, 1, 1, true));
                fishes.Add(new Fish(8, rows - 8, 1, 1, true));
                fishes.Add(new Fish(12, rows - 8, 1, 1, true));

                // 7. sor, balra
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 7, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 9, 1, 1, false));

                // 8. sor, balra
                fishes.Add(new Fish(1, rows - 10, 1, 1, false));
                fishes.Add(new Fish(2, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(5, rows - 10, 1, 1, false));
                fishes.Add(new Fish(7, rows - 10, 1, 1, false));
                fishes.Add(new Fish(8, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(10, rows - 10, 1, 1, false));
                fishes.Add(new Fish(12, rows - 10, 1, 1, false));
                fishes.Add(new Fish(13, rows - 10, 1, 1, false));
            }

            if (level == 4)
            {
                // vegyes összetételű és irányú sorok

                // 1. sor
                fishes.Add(new Fish(1, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(2, rows - 3, 1, 1, true));
                fishes.Add(new Fish(4, rows - 3, 1, 1, true));
                fishes.Add(new Fish(6, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(8, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(9, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(10, rows - 3, 1, 1, true));
                fishes.Add(new Fish(9, rows - 3, 1, 1, true));

                // 2. sor
                seaanimals.Add(new SeaAnimal(columns, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 4, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 4, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 4, 1, 1, false));

                // 3. sor, csak halak
                fishes.Add(new Fish(3, rows - 5, 1, 1, true));
                fishes.Add(new Fish(6, rows - 5, 1, 1, true));
                fishes.Add(new Fish(12, rows - 5, 1, 1, true));

                // 4. sor
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 6, 1, 1, false));

                // 5. sor
                fishes.Add(new Fish(0, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(2, rows - 7, 1, 1, true));
                fishes.Add(new Fish(4, rows - 7, 1, 1, true));
                fishes.Add(new Fish(6, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(7, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(10, rows - 7, 1, 1, false));
                fishes.Add(new Fish(11, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(13, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(14, rows - 7, 1, 1, false));

                // 6. sor, csak halak
                fishes.Add(new Fish(3, rows - 8, 1, 1, true));
                fishes.Add(new Fish(4, rows - 8, 1, 1, false));

                // 7. sor
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 7, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 9, 1, 1, false));

                // 8. sor
                fishes.Add(new Fish(1, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(3, rows - 10, 1, 1, true));
                fishes.Add(new Fish(4, rows - 10, 1, 1, true));
                fishes.Add(new Fish(7, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(7, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(9, rows - 7, 1, 1, false));
                fishes.Add(new Fish(11, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(13, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(14, rows - 10, 1, 1, true));
            }

            if (level == 5)
            {
                // hardcore
                // vegyes összetételű és irányú sorok

                // 1. sor
                fishes.Add(new Fish(1, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(2, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(2, rows - 3, 1, 1, false));
                seaanimals.Add(new SeaAnimal(3, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(5, rows - 3, 1, 1, true));
                fishes.Add(new Fish(4, rows - 3, 1, 1, false));
                fishes.Add(new Fish(6, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(8, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(9, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(11, rows - 3, 1, 1, true));
                seaanimals.Add(new SeaAnimal(11, rows - 3, 1, 1, false));            
                fishes.Add(new Fish(9, rows - 3, 1, 1, false));
                fishes.Add(new Fish(10, rows - 3, 1, 1, false));
                fishes.Add(new Fish(11, rows - 3, 1, 1, false));
                fishes.Add(new Fish(13, rows - 3, 1, 1, false));

                // 2. sor
                fishes.Add(new Fish(1, rows - 4, 1, 1, false));
                fishes.Add(new Fish(2, rows - 4, 1, 1, true));
                fishes.Add(new Fish(4, rows - 4, 1, 1, true));
                seaanimals.Add(new SeaAnimal(5, rows - 4, 1, 1, false));
                fishes.Add(new Fish(7, rows - 4, 1, 1, false));
                fishes.Add(new Fish(8, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(9, rows - 4, 1, 1, false));
                seaanimals.Add(new SeaAnimal(10, rows - 4, 1, 1, true));
                fishes.Add(new Fish(12, rows - 4, 1, 1, true));
                fishes.Add(new Fish(13, rows - 4, 1, 1, false));

                // 3. sor, csak halak
                fishes.Add(new Fish(1, rows - 5, 1, 1, true));
                fishes.Add(new Fish(3, rows - 5, 1, 1, true));
                fishes.Add(new Fish(10, rows - 5, 1, 1, true));

                // 4. sor
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 2, rows - 6, 1, 1, false));
                fishes.Add(new Fish(3, rows - 6, 1, 1, true));
                fishes.Add(new Fish(6, rows - 6, 1, 1, true));
                fishes.Add(new Fish(9, rows - 6, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 6, rows - 6, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 10, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 6, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 14, rows - 6, 1, 1, false));

                // 5. sor
                fishes.Add(new Fish(0, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(2, rows - 7, 1, 1, true));
                fishes.Add(new Fish(4, rows - 7, 1, 1, true));
                fishes.Add(new Fish(6, rows - 7, 1, 1, false));
                fishes.Add(new Fish(7, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(7, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(10, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(11, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(11, rows - 7, 1, 1, true));
                fishes.Add(new Fish(11, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(13, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(14, rows - 7, 1, 1, false));

                // 6. sor, csak halak
                fishes.Add(new Fish(3, rows - 8, 1, 1, true));
                fishes.Add(new Fish(4, rows - 8, 1, 1, false));

                // 7. sor
                seaanimals.Add(new SeaAnimal(columns - 1, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 3, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 4, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 5, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 7, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 8, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 11, rows - 9, 1, 1, true));
                seaanimals.Add(new SeaAnimal(columns - 12, rows - 9, 1, 1, false));
                seaanimals.Add(new SeaAnimal(columns - 13, rows - 9, 1, 1, false));

                // 8. sor
                fishes.Add(new Fish(1, rows - 10, 1, 1, true));
                fishes.Add(new Fish(3, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(2, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(3, rows - 7, 1, 1, true));
                seaanimals.Add(new SeaAnimal(3, rows - 10, 1, 1, false));
                fishes.Add(new Fish(4, rows - 10, 1, 1, true));
                fishes.Add(new Fish(7, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(7, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(9, rows - 7, 1, 1, false));
                seaanimals.Add(new SeaAnimal(10, rows - 7, 1, 1, false));
                fishes.Add(new Fish(11, rows - 10, 1, 1, true));
                seaanimals.Add(new SeaAnimal(13, rows - 10, 1, 1, false));
                seaanimals.Add(new SeaAnimal(14, rows - 10, 1, 1, true));

            }

        }

    


    }
}