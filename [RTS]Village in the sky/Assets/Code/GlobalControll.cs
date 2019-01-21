using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Village
{
    public class GlobalControll : MonoBehaviour
    {
        private static GlobalControll globalControll { get; set; }
        private List<Human> sitizens;
        private List<Building> buildings;

        private GlobalControll()
        {
            sitizens = new List<Human>();
            buildings = new List<Building>();
        }

        public void AddSitizen()
        {
            sitizens.Add(new Human());
        }

        public void AddBuilding(Building building)
        {
            buildings.Add(building);
        }

        public static GlobalControll Singleton()
        {
            if (globalControll == null) globalControll = new GlobalControll();
            return globalControll;
        }

        public void Start()
        {
            GlobalControll globalControll = GlobalControll.Singleton();

        }
    }
}
