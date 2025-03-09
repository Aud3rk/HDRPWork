using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Cart : MonoBehaviour
    {
        public event Action CartIsDone;
        public event Action<float, float> CartChange;

        private int _needBox;
        private BoxType _boxType;
        private int _wrongBoxCount;
        private int _boxInCartCount;

        private void Start()
        {
            _wrongBoxCount = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoxComponent box))
                if (box.Type == _boxType)
                {
                    BoxInCart();
                    if (_wrongBoxCount>0) return;
                    if (_boxInCartCount == _needBox) EndLevel();
                }
                else
                    _wrongBoxCount++;
        }

        public void SetParametr(BoxType boxType, int boxCount)
        {
            _boxType = boxType;
            _needBox = boxCount;
        }
        private void BoxInCart()
        {
            _boxInCartCount++;
            CartChange?.Invoke(_boxInCartCount-1,_boxInCartCount);
        }

        private void EndLevel() => 
            CartIsDone?.Invoke();

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BoxComponent box))
                if (box.Type == _boxType)
                {
                    _boxInCartCount--;
                    CartChange?.Invoke(_boxInCartCount + 1, _boxInCartCount);
                }
                else
                {
                    _wrongBoxCount--;
                    if (_boxInCartCount == _needBox && _wrongBoxCount == 0) 
                        EndLevel();
                }
        }
    }
}