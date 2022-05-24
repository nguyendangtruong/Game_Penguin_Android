
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    
    public void PlayGame()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(1);
        Invoke("InPlayGame", 2f);
        
    }
    
    private void InPlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

 

    public void QuitGame()
    {
        Application.Quit();
    }



}
