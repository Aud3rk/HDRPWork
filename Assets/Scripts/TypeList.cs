using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "ScriptableObject/TypeList")]
    public class TypeList : ScriptableObject
    {
        public List<GameObject> ItemsInside;
        public BoxType BoxType;
    }
}