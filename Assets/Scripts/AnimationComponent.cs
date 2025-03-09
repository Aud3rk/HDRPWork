using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AnimationComponent : MonoBehaviour
    {
        public event Action AnimationEnd;

        public void OnAnimationEnd()
        {
            AnimationEnd?.Invoke();
        }
    }
}