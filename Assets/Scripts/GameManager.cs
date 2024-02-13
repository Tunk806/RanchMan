using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject LoseUI;
    public GameObject MenuUI;
    public GameObject canvas;
    public GameObject WinUI;
    public Button WinButton;
    public Button LoseButton;
    public GameObject EventSystem;
    public EventSystem ES;
    GameObject juan;
    JuanController juanC;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);DontDestroyOnLoad(canvas);DontDestroyOnLoad(EventSystem);
        LoseUI.SetActive(false);WinUI.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
        ES = EventSystem.GetComponent<EventSystem>();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        { 
            if (juanC.health == 0)
            {
                LOSER();
            }
        }
    }
    void LOSER()
    {
        LoseUI.SetActive(true);
        ES.SetSelectedGameObject(LoseButton.gameObject);
    }
    public void WIN()
    {
        WinUI.SetActive(true);
        ES.SetSelectedGameObject(WinButton.gameObject);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            juan = GameObject.FindGameObjectWithTag("Player");
            juanC = juan.GetComponent<JuanController>();
            MenuUI.SetActive(false);
            LoseUI.SetActive(false);
            WinUI.SetActive(false);
        }
    }
}
