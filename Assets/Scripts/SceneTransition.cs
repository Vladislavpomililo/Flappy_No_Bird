using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text loadingText;

    private static SceneTransition instanceSceneTransition;

    private static bool playAnimation = false;

    private Animator comAnimator;
    private AsyncOperation loadingSceneOperation;

    public static void ToScene(string sceneName)
    {
        instanceSceneTransition.comAnimator.SetTrigger("sceneStart");

        instanceSceneTransition.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instanceSceneTransition.loadingSceneOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instanceSceneTransition = this;

        comAnimator = GetComponent<Animator>();

        if(playAnimation)
        {
            comAnimator.SetTrigger("sceneClosing");
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            loadingText.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
        }
    }

    public void OnAnimation()
    {
        playAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
