using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button _start;
    [SerializeField] Button _quit;


    public void Awake()
    {
        _start.onClick.AddListener(StartGame);
        _quit.onClick.AddListener(Quit);

    }
    private void Quit()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
