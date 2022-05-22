
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;


    public void Start()
    {
        scoreText.text = SceneManager.GetActiveScene().name;
    }
}
