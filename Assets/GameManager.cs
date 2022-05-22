
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnds = false;
    public float reStartDelay = 2.5f;
    public GameObject completeLevelUI;
    public GameObject completeLevelUI2;
    public GameObject playerDieUI;
    public GameObject playerDieUI2;
    public GameObject tapUI;
    public Character characterWin;
    public MoveCameraWhenWinner moveCameraWhenWinner;
    public FollowCamera followCamera;
    public MeshCollider meshCollider;
    public Text textLevelUpNext;
    public Text textLevelUpNext2;
    public Image imgUIDeath;
    public float positionZSnowBallFall=100f;
    public GameObject audioBG;
    public GameObject audioDeath;


    //rơi Snow Ball
    public Transform characterTranform;
    public GameObject snowBall;

    static private bool startPlayerGame = false; //load game và người chơi cần chạm vào màn hình để bắt đầu,
                                          //tạo ra tránh xung đột với click Pause show ra Menu, cả dừng khi bắt đầu load game và dừng khi nhấn nút Pause đều dùng Time.timescale


    public GameObject huongDanUI;
    void Awake()
    {

    }

    private void Start()
    {

        if (startPlayerGame==true)
        {
            Time.timeScale = 1; //Nếu người dùng đang chơi lại thì không cần stop màn hình khi bắt đầu
        }
        else
        {
            Time.timeScale = 0; // cho dừng màn hình trước khi vào game, click sẽ bắt đầu chạy
            tapUI.SetActive(true);
 

        }
    }
   
    private void Update()
    {
        if (startPlayerGame == false) //kiểm tra khi bắt đầu vào game
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))//kiểm tra người dùng có click hay không?
            {
                Time.timeScale = 0.7f;
                startPlayerGame = true;

                
                huongDanUI.SetActive(true);//bật anim hướng dẫn khi mới chơi
                Invoke("DisableHuongDanUI", 2.5f); //tắt UI hướng dẫn
            }
        }
        if (characterTranform.position.z > positionZSnowBallFall)
        {
            try
            {
                snowBall.SetActive(true);
            }catch(Exception e)
            {
                Debug.Log(e);
            }
        }
       

    }

    void DisableHuongDanUI()
    {
        tapUI.SetActive(false); //tắt TapUI
        huongDanUI.SetActive(false);
        Time.timeScale = 1;

    }

    public void CompleteLevel()
    {
        
        meshCollider.enabled = true; //mặt biển làm điểm tựa tránh nhân vật rơi xuống

          //radom để load giao diện Win
        int num = UnityEngine.Random.Range(0, 2);
        if (num == 0)
        {
            completeLevelUI.SetActive(true);

        }
        else
        {
            completeLevelUI2.SetActive(true);

        }

        //nhân vật dừng di chuyển khi về đích
        characterWin.SetSpeed = 8f;
        Invoke("CharacterStop", 5f);

        if (SceneManager.GetActiveScene().name == "Level 05")
        {
            textLevelUpNext.text = "RESRET GAME";
            textLevelUpNext2.text = "RESRET GAME";
            Invoke("LoadUILevelComplete", 4f);
        }
        else
        {
            textLevelcurrent = Convert.ToString(SceneManager.GetActiveScene().buildIndex + 1); //lấy số buidl của screen để set text level up next
            textLevelUpNext.text = "LEVEL UP " + textLevelcurrent; //set text button khi finish game
            textLevelUpNext2.text = "LEVEL UP " + textLevelcurrent; //set text button khi finish game
            Invoke("LoadUILevelComplete", 4f);
        }
      
        

        followCamera.enabled = false;
        moveCameraWhenWinner.enabled = true;

    }

    private void CharacterStop()
    {
        characterWin.enabled = false;
    }


    private String textLevelcurrent;
    private void LoadUILevelComplete()
    {
        completeLevelUI.SetActive(true);
        
        
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level 05")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void EndGame()
    {
        if(gameHasEnds == false)
        {
            audioBG.SetActive(false);
            audioDeath.SetActive(true);
            Debug.Log("Game Over");
            gameHasEnds = true;
            //Load anime DIE
            FindObjectOfType<Character>().Die();
            //Reset Game
            Invoke("LoadUIDie", reStartDelay);
            //Restart();
            
        }
    
    }

    private int typeDeath;
    public void LoadUIDie()
    {
        if(typeDeath==1)// =1 Death do bị rơi
        {
            playerDieUI2.SetActive(true);
            
        }
        else
        {
            playerDieUI.SetActive(true);
            
        }
    }
    public void SettypeDeath()
    {
        typeDeath = 1;
    }
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void XFireGame()
    {
        if (SceneManager.GetActiveScene().name == "Level 05")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        meshCollider.enabled = true; //mặt biển làm điểm tựa tránh nhân vật rơi xuống

          //radom để load giao diện Win
        int num = UnityEngine.Random.Range(0, 2);
        if (num == 0)
        {
            completeLevelUI.SetActive(true);

        }
        else
        {
            completeLevelUI2.SetActive(true);

        }

        //nhân vật dừng di chuyển khi về đích
        characterWin.SetSpeed = 8f;
        Invoke("CharacterStop", 5f);

        if (SceneManager.GetActiveScene().name == "Level 05")
        {
            textLevelUpNext.text = "RESRET GAME";
            textLevelUpNext2.text = "RESRET GAME";
            Invoke("LoadUILevelComplete", 4f);
        }
        else
        {
            textLevelcurrent = Convert.ToString(SceneManager.GetActiveScene().buildIndex + 1); //lấy số buidl của screen để set text level up next
            textLevelUpNext.text = "LEVEL UP " + textLevelcurrent; //set text button khi finish game
            textLevelUpNext2.text = "LEVEL UP " + textLevelcurrent; //set text button khi finish game
            Invoke("LoadUILevelComplete", 4f);
        }

        if(SceneManager.GetActiveScene().name == "Level 06")
        {
            textLevelUpNext.text = "RESET GAME";
            textLevelUpNext2.text = "RESET GAME";
            Invoke("LoadUILevelComplete", 4f);
        }
        else
        {
            textLevelcurrent = Convert.ToString(SceneManager.GetActiveScene().buildIndex + 1);
            textLevelUpNext.text = "LEVEL UP " + textLevelcurrent;
            textLevelUpNext2.text = "LEVEL UP " + textLevelcurrent;
            Invoke("LoadUILevelComplete", 4f);

        }
      
        

        followCamera.enabled = false;
        moveCameraWhenWinner.enabled = true;
    }
  
}
