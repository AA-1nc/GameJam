using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                pauseCanvas.SetActive(false);
            }
    }
}
