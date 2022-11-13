using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;

    public void GoToScene(int sceneIndex) 
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex)); 
    
    }

    IEnumerator GoToSceneRoutine(int aSceneIndex) 
    {
        fadeScreen.FadeOut(); 
        

        // Launch the new scene. 
        AsyncOperation operation = SceneManager.LoadSceneAsync(aSceneIndex);
        operation.allowSceneActivation = false;
        float timer = 0.0f; 
        while (timer <= fadeScreen.fadeDuration && !operation.isDone) 
        {
            timer += Time.deltaTime; 
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}
