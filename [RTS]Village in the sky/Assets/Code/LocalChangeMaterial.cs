using UnityEngine;

namespace BuildSpace
{
    public class LocalChangeMaterial : MonoBehaviour
    {
        private Material material;
        private bool LoopStop { get; set; }

        private void Start()
        {
            material = Resources.Load("ThatchRoof", typeof(Material)) as Material;
            LoopStop = true;
        }

        void Update()
        {
            if (Builder.StopFlag && LoopStop)
            {
                gameObject.GetComponent<MeshRenderer>().material = material;
                //Builder.ChangeFlag = false;
                Debug.Log(Builder.StopFlag);
                LoopStop = false;
                //enabled = false;
            }
        }
    }
}
