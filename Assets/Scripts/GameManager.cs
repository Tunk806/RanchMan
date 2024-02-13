using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    public GameObject LoseUI;
    public GameObject MenuUI;
    public GameObject canvas;
    public GameObject WinUI;
    GameObject juan;
    JuanController juanC;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);DontDestroyOnLoad(canvas);
        LoseUI.SetActive(false);WinUI.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    }
    void WIN()
    {
        WinUI.SetActive(true);
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
