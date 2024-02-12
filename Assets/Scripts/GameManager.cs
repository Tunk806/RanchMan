using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Lose;
    public Button retry;
    public Button menu;
    GameObject juan;
    JuanController juanC;
    private void Awake()
    {
        juan = GameObject.FindGameObjectWithTag("Player");
        juanC = juan.GetComponent<JuanController>();
        Lose.enabled = false;retry.gameObject.SetActive(false);menu.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(juanC.health == 0)
        {
            LOSER();
        }
    }
    void LOSER()
    {
        Lose.enabled = true; retry.gameObject.SetActive(true); menu.gameObject.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
