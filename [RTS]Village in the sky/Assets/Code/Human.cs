using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village
{
    class Human
    {
        private static Random random = new Random();
        public readonly string Name;
        public int Age { get; private set; }
        public char Gender { get; private set; }

        public Human()
        {
            Gender = RandomGender();
            Name = RandomName(Gender);
            Age = default(int);
        }

        private static string RandomName(char gender)
        {

            string[] male = { "Don", "Gregory", "Anton", "Alex", "Dmitru", "Felix", "Gysmond", "Anton" }; //Заменить имена на нормальные
            string[] female = { "Mona", "Lisa", "Galya", "Janett", "Katrin", "Katia" };

            if (gender == 'm')
            {
                return male[random.Next(male.Length)];
            }
            else if (gender == 'f')
            {
                return male[random.Next(female.Length)];
            }
            else
            {
                return default(string);
            }
        }

        private static char RandomGender()
        {
            int check = random.Next(1);

            if (check == 0)
            {
                return 'm';
            }
            else if (check == 1)
            {
                return 'f';
            }
            else
            {
                return default(char);
            }
        }
    }
}
