using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ActionBus
    {
        private event Action<BoxComponent> MouseButtonDown;
        private event Action MouseButtonUp;
        private event Action<BoxComponent> MouseButtonEnter;
        private event Action MouseButtonExit;
        
        public void AddBoxToList(GameObject gameObject)
        {
            BoxComponent boxComponent = gameObject.GetComponent<BoxComponent>();
            boxComponent.MouseButtonDrag += MouseButtonDown.Invoke;
            boxComponent.MouseButtonEnter += MouseButtonEnter.Invoke;
            boxComponent.MouseButtonExit += MouseButtonExit.Invoke;
            boxComponent.MouseButtonUp += MouseButtonUp.Invoke;
        }

        public void AddListenerToMouseDown(Action<BoxComponent> function)
        {
            MouseButtonDown += function;
        }

        public void AddListenerToMouseEnter(Action<BoxComponent> function)
        {
            MouseButtonEnter += function;
        }

        public void AddListenerToMouseExit(Action function)
        {
            MouseButtonExit += function;
        }

        public void AddListenerToMouseUp(Action function)
        {
            MouseButtonUp += function;
        }

    }
}