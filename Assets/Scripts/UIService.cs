using System;
using DefaultNamespace;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject miniCam;
    [SerializeField] private Transform positionToSpawn;
    
    [SerializeField] private TMP_Text boxInCartText;
    [SerializeField] private GameObject boxCountUI;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject cartSelection;

    private int _needBox;
    private bool _isHooking;
    private GameObject spawnedObject;
    private Animator _animator;

    private void Start()
    {
        _animator = endMenu.GetComponent<Animator>();
    }

    public void ShowOverlay()
    {
        boxInCartText.text = 0 + "/" + _needBox;
        overlay.SetActive(true);
        boxCountUI.SetActive(true);
        cartSelection.layer = 6;
    }
    public void ChangeCartUI(float countPast, float countNow)
    {
        boxInCartText.text = countNow + "/" + _needBox;
    }

    public void ShowMiniCam(BoxComponent boxComponent)
    {
        if ( _isHooking) return;
        spawnedObject = Instantiate(boxComponent.ObjectInside, positionToSpawn.position, quaternion.identity);
        miniCam.SetActive(true);
    }

    public void HideMiniCam()
    {
        miniCam.SetActive(false);
        Destroy(spawnedObject);
    }

    public void DontShow(BoxComponent boxComponent) 
        => _isHooking = true;

    public void Show() 
        => _isHooking = false;

    public void ShowEndMenu()
    {
        overlay.SetActive(false);
        endMenu.SetActive(true);
        _animator.SetTrigger("EndGame");
    }

    public void SetParametr(int boxCount) 
        => _needBox = boxCount;
}