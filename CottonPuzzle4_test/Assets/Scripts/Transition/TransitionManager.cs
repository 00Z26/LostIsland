using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{

    [SceneName] public string startScene;

    private bool isFade;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration; //变黑或变透明的时间

    private bool canTransition;
    private void Start()
    {
        StartCoroutine(TransitionToScene(string.Empty, startScene));
    }

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChange;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChange;
    }



    public void Transition(string from, string to)
    {
        if (!isFade && canTransition)//如果渐变状态是1，说明现在没有在切换场景，则执行切换
            StartCoroutine(TransitionToScene(from, to));   
    }

    private void OnGameStateChange(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1); //场景变化前，先变黑。这里用yield会使fade执行结束后再向下执行，如果下述同步执行使用StartCoroutine
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive); //执行后只有常驻和场景to

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1); //获取新加载场景的序号
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();   

        yield return Fade(0);//变化结束后，渐变白
    }

    //创造渐变的切换过程,0变为透明，1变为黑色
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration; //防止负数，渐变过程值除以时间

        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
