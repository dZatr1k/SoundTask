using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image progressBar;

    private CanvasGroup _canvasGroup;
    
    public static LoadingScreen instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        Utility.SetCanvasGroupEnabled(_canvasGroup, false);
    }

    IEnumerator LoadingCoroutine(string name)
    {
        Utility.SetCanvasGroupEnabled(_canvasGroup, true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            progressBar.fillAmount = operation.progress;
            yield return null;
        }
        
        Utility.SetCanvasGroupEnabled(_canvasGroup, false);
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadingCoroutine(name));
    }
}