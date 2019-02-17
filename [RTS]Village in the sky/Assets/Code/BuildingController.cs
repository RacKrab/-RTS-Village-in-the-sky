using UnityEngine;
using System.Collections;

namespace BuildSpace
{
    public class BuildingController : MonoBehaviour
    {
        //Нужно создать статическую переменную, которая при окончании строительства будет каким то образом вызывать все необходимые функции.
        public static bool StopFlag { get; set; } // Переменная отвечает за изменение материала после постройки здания.
        private bool working; // Отслеживает, был ли уже вызван методи предотвращает появление (инициализацию) нескольких зданий однвременно

        public GameObject[] buildings;

        public void BuildingNumber(int count)
        {
            if (working) return;// Если метод уже был вызван, но постройка не закончена, игнорируем выполнение метода

            working = true;
            Instantiate(buildings[count], new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }

        public void EndBuild()
        { // нужна какая то проверка, сейчас можно вызвать слишком много корутинов
            working = false;
            StopFlag = true;
            Invoke("WaitChangeMaterials", 0.1f);
        }

        public void WaitChangeMaterials()
        {
            StopFlag = false;
        }
    }
}
