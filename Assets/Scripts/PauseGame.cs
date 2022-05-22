
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{
    public static bool gameIsPause=false;
    public GameObject panelMenuPause;
   
    public void PausePlay()
    {
        Time.timeScale = 0;
        panelMenuPause.SetActive(enabled);
    }

    public void ChooseResume()
    {
        panelMenuPause.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void ChooseMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ChooseQuit()
    {
        Application.Quit();

    }
}
