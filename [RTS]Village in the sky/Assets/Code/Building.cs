using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public abstract class Building : MonoBehaviour
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



    //public class Mill : Building
    //{
    //    public Mill()
    //    {
    //        CurrentSize = 3;
    //    }

    //    public override void Work()
    //    {

    //    }
    //}

    //public class Hut : Building
    //{
    //    public Hut()
    //    {
    //        CurrentSize = 2;
    //    }
    //    public override void Work()
    //    {

    //    }
    //}

    //public class Wheat : Building
    //{
    //    public Wheat()
    //    {
    //        CurrentSize = 1;
    //    }
    //    public override void Work()
    //    {

    //    }
    //}
}
