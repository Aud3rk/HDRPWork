using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DefaultNamespace
{
    public class ServiceController : MonoBehaviour
    {
        [SerializeField] private UIService uiService;
        [SerializeField] private ControlItems controlItems;
        [SerializeField] private Cart cart;
        
        [Header("For Game Factory")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject boxPrefab;
        [SerializeField] private List<TypeList> gameObjectInsideBoxList;

        private GameFactory _gameFactory;
        private ActionBus _actionBus;
        
        private int _boxCount;
        private BoxType _boxType;
        private float _spawnDelay = 0.3f;
        private int _spawnForce = 25;


        public void Construct(BoxType boxType, int boxCount)
        {
            _actionBus = new ActionBus();
            _gameFactory = new GameFactory(spawnPoint, boxPrefab, gameObjectInsideBoxList);
            SetParametrs(boxType, boxCount);
            SubscribeServices();
        }

        public void StartScene()
        {
            StartCoroutine(GenerateBoxes());
            uiService.ShowOverlay();
        }

        public void AddBoxToList(GameObject boxGameObject) => 
            _actionBus.AddBoxToList(boxGameObject);

        private void SubscribeServices()
        {
            SubscribeControlItem();
            SubscribeUIService();
        }

        private void SubscribeControlItem()
        {
            _actionBus.AddListenerToMouseDown(controlItems.HookBox);
            _actionBus.AddListenerToMouseUp(controlItems.DropBox);
        }

        private void SubscribeUIService()
        {
            _actionBus.AddListenerToMouseEnter(uiService.ShowMiniCam);
            _actionBus.AddListenerToMouseExit(uiService.HideMiniCam);
            _actionBus.AddListenerToMouseDown(uiService.DontShow);
            _actionBus.AddListenerToMouseUp(uiService.Show);
            cart.CartChange += uiService.ChangeCartUI;
            cart.CartIsDone += uiService.ShowEndMenu;
        }

        private void SetParametrs(BoxType boxType, int boxCount)
        {
            cart.SetParametr(boxType, boxCount);
            uiService.SetParametr(boxCount);
            _boxType = boxType;
            _boxCount = boxCount;
        }

        private IEnumerator GenerateBoxes()
        {
            for (int i = 0; i < _boxCount; i++)
            {
                SpawnBox(_boxType);
                yield return new WaitForSeconds(_spawnDelay);
                SpawnBox(GetRandomType(_boxType));
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
        
        private void SpawnBox(BoxType boxType)
        {
            GameObject boxGameObject = _gameFactory.InstantiateBox(boxType);
            AddBoxToList(boxGameObject);
            boxGameObject.GetComponent<Rigidbody>().AddForce(boxGameObject.transform.forward * _spawnForce, ForceMode.Impulse);
        }

        private BoxType GetRandomType(BoxType boxType)
        {
            Random random = new Random();
            BoxType randomType = (BoxType)random.Next(3);
            if (randomType == boxType) randomType = GetRandomType(boxType);
            return randomType;
        }
    }
}