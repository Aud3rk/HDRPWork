using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DefaultNamespace
{
    public class GameFactory 
    {
        private Transform _spawnPoint;
        private GameObject _boxPrefab;
        private List<TypeList> _gameObjectInsideBoxList;

        public GameFactory(Transform spawnPoint, GameObject boxPrefab, List<TypeList> gameObjectInsideBoxList)
        {
            _spawnPoint = spawnPoint;
            _boxPrefab = boxPrefab;
            _gameObjectInsideBoxList = gameObjectInsideBoxList;
        }

        public GameObject InstantiateBox(BoxType boxType)
        {
            GameObject box = InstantiateGameObject(_boxPrefab, _spawnPoint.position, _spawnPoint.rotation);
            SetSettingsToBox(boxType, box);
            return box;
        }

        private void SetSettingsToBox(BoxType boxType, GameObject box)
        {
            if (box.TryGetComponent(out BoxComponent boxComponent))
            {
                boxComponent.Type = boxType;
                boxComponent.ObjectInside = GetRandomItem(boxType);
            }
        }

        private GameObject InstantiateGameObject(GameObject prefab, Vector3 position)
        {
            return GameObject.Instantiate(prefab, position, quaternion.identity);
        }
        private GameObject InstantiateGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(prefab, position, rotation);
        }

        private GameObject GetRandomItem(BoxType boxType)
        {
            Random random = new Random();
            TypeList gameObjectList = _gameObjectInsideBoxList.FirstOrDefault(x=> x.BoxType== boxType);
            int randomId = random.Next(gameObjectList.ItemsInside.Count);
            return gameObjectList.ItemsInside[randomId];
        }
    }
}