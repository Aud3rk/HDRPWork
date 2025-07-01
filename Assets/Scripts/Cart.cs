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
        private int _wrongBoxCount = 0;
        private int _boxInCartCount = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BoxComponent box))
            {
                BoxCartChange(box, 1);
                TryToEndLevel();
            }
        }

        public void SetParametr(BoxType boxType, int boxCount)
        {
            _boxType = boxType;
            _needBox = boxCount;
        }

        private void BoxCartChange(BoxComponent boxComponent, int incr)
        {
            if (boxComponent.Type == _boxType)
            {
                _boxInCartCount += incr;
                CartChange?.Invoke(_boxInCartCount - incr, _boxInCartCount);
            }
            else
                _wrongBoxCount+=incr;
        }

        private void TryToEndLevel()
        {
            if (_boxInCartCount == _needBox && _wrongBoxCount == 0)
                CartIsDone?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BoxComponent box))
            {
                BoxCartChange(box, -1);
                TryToEndLevel();
            }
        }
    }
}