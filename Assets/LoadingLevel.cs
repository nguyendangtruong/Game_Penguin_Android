using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingLevel : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text numberProgressText;

    public void LoadingLevel1 (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            //Làm tròn số
            double progressRound = Math.Round(progress, 0);
            numberProgressText.text = progressRound * 100f + "%";

            yield return null;
        }
    }
}
