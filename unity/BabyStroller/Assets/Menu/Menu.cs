using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    void AddOnClickListeners(string scene)
    {
        GameObject.Find("Button" + scene).GetComponent<Button>().onClick.AddListener(
            delegate { SceneManager.LoadScene("Scenes/" + scene); }
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        AddOnClickListeners("Push");
        AddOnClickListeners("Walk");
        AddOnClickListeners("Stuck");
        AddOnClickListeners("PinchDetection");

        GameObject.Find("ButtonQuit").GetComponent<Button>().onClick.AddListener(
            delegate { Application.Quit(); }
        );
    }

}
