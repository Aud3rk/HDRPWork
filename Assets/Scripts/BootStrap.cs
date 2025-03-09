using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DefaultNamespace
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private ServiceController serviceController;
        [SerializeField] private AnimationComponent animationComponent;
        
        private void OnEnable()
        {
            int boxCount = PlayerPrefs.GetInt("BoxCount");
            BoxType boxtype = (BoxType)PlayerPrefs.GetInt("BoxType");
            serviceController.Construct(boxtype, boxCount);
            animationComponent.AnimationEnd += serviceController.StartScene;

        }
    }
}