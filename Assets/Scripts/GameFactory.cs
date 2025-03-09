using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DefaultNamespace
{
    public class GameFactory : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject boxPrefab;
        [SerializeField] private List<GameObject> objectInsideOzon;
        [SerializeField] private List<GameObject> objectInsideWildBerries;
        [SerializeField] private List<GameObject> objectInsideYandex;

        public GameObject InstantiateBox(BoxType boxType)
        {
            GameObject box = InstantiateGameObject(boxPrefab, spawnPoint.position, spawnPoint.rotation);
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
            return Instantiate(prefab, position, quaternion.identity);
        }
        private GameObject InstantiateGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation);
        }

        private GameObject GetRandomItem(BoxType boxType)
        {
            Random random = new Random();
            int randomId;
            switch (boxType)
            {
                case BoxType.Ozon:
                    randomId = random.Next(objectInsideOzon.Count);
                    return objectInsideOzon[randomId];
                case BoxType.WildBerries:
                    randomId = random.Next(objectInsideWildBerries.Count);
                    return objectInsideWildBerries[randomId];
                case BoxType.Yandex:
                    randomId = random.Next(objectInsideYandex.Count);
                    return objectInsideYandex[randomId];
            }
            return null;
        }
    }
}