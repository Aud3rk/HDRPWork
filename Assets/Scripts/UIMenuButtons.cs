using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuButtons : MonoBehaviour
{
    private const string LEVEL_SCENE = "ZavodScene";
    private const string SCENE_MENU = "MainMenu";
    private const string SCENE_END_TRIGGER = "SceneEnd";
    private const string SCENE_START_TRIGGER = "SceneStart";

    [Header("Settings")]
    [SerializeField] private TMP_Text boxCountText;
    [SerializeField] private Slider boxCountSlider;
    [SerializeField] private TMP_Dropdown typeField;
    
    [Header("ProgressLoad")]
    [SerializeField] private TMP_Text loadingPercentage;
    [SerializeField] private Image loadBar;
    
    [SerializeField] private List<GameObject> _gameObjects;
    [SerializeField] private Animator animator;
    
    private int _boxCount = 5;
    private int _boxType = 0;
    private AsyncOperation _loadSceneAsync;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        boxCountSlider.onValueChanged.AddListener(ChangeBoxCount);
        typeField.onValueChanged.AddListener(ChangeBoxType);
        typeField.onValueChanged.AddListener(ChangeBoxType);
    }

    private void ChangeBoxType(int id)
    {
        _boxType = id;
    }

    public void Play()
    {
        PlayerPrefs.SetInt("BoxCount", _boxCount);
        PlayerPrefs.SetInt("BoxType", _boxType);
        animator.SetTrigger(SCENE_START_TRIGGER);
    }

    private void Update()
    {
        if (_loadSceneAsync != null)
        {
            loadingPercentage.text = Mathf.RoundToInt(_loadSceneAsync.progress * 100) + "%";
            loadBar.fillAmount = _loadSceneAsync.progress;
            if (_loadSceneAsync.isDone)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(LEVEL_SCENE));
                animator.SetTrigger(SCENE_END_TRIGGER);
            }
        }
    }

    private void LoadSceneStart()
    {
        foreach (GameObject go in _gameObjects)
        {
            go.SetActive(false);
        }
        _loadSceneAsync = SceneManager.LoadSceneAsync(LEVEL_SCENE, LoadSceneMode.Additive);
    }

    private void ChangeBoxCount(float value)
    {
        boxCountText.text = value.ToString();
        _boxCount = (int)value;
    }

    public void OnAnimationOver() => 
        SceneManager.UnloadSceneAsync(SCENE_MENU);
}
