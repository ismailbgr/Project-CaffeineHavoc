using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 
    public UnityEngine.UI.Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);

    }

    // Update is called once per frame
    void Update()
    {
        // rotate camera between 130 and 160 degrees in y axis using lerp
        Camera.main.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 130, 0), Quaternion.Euler(0, 160, 0), Mathf.PingPong(Time.time/10, 1));
    }

    void startGame(){

        //load game scene
        SceneManager.LoadScene("Main Scene");

    }
}
