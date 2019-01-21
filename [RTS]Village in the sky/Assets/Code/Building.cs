using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village
{
    public abstract class Building
    {
        private List<Human> workers;
        protected int CurrentSize { get; set; }

        protected Building()
        {
            workers = new List<Human>();
            CurrentSize = default(int);
        }

        public void AddWorker()
        {
            if (CurrentSize <= workers.Count) return;
            workers.Add(new Human());
        }

        public void SubWorker()
        {
            if (workers.Count == 0) return;

            workers.RemoveAt(workers.Count - 1);
        }

        public virtual void Work() { }
    }



    class Mill : Building
    {
        public Mill()
        {
            CurrentSize = 3;
        }

        public override void Work()
        {
            Console.WriteLine("Mill work.");
        }
    }

    class Hut : Building
    {
        public Hut()
        {
            CurrentSize = 2;
        }
        public override void Work()
        {
            Console.WriteLine("It is hut , it is can not work.");
        }
    }

    class Wheat : Building
    {
        public Wheat()
        {
            CurrentSize = 1;
        }
        public override void Work()
        {
            Console.WriteLine("Wheat work.");
        }
    }
}
