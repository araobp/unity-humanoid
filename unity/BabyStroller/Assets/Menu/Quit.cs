using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ButtonQuit").GetComponent<Button>().onClick.AddListener(
            delegate { SceneManager.LoadScene("Scenes/Menu"); });
    }
}
