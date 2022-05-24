using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    
    public void LoadLevel01()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(1);
    }
    public void LoadLevel02()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(2);
    }
    public void LoadLevel03()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(3);
    }
    public void LoadLevel04()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(4);
    }
    public void LoadLevel05()
    {
        FindObjectOfType<LoadingLevel>().LoadingLevel1(5);
    }
}
