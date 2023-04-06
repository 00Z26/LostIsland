using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{

    [SceneName] public string startScene;

    private bool isFade;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration; //��ڻ��͸����ʱ��

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
        if (!isFade && canTransition)//�������״̬��1��˵������û�����л���������ִ���л�
            StartCoroutine(TransitionToScene(from, to));   
    }

    private void OnGameStateChange(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1); //�����仯ǰ���ȱ�ڡ�������yield��ʹfadeִ�н�����������ִ�У��������ͬ��ִ��ʹ��StartCoroutine
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive); //ִ�к�ֻ�г�פ�ͳ���to

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1); //��ȡ�¼��س��������
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();   

        yield return Fade(0);//�仯�����󣬽����
    }

    //���콥����л�����,0��Ϊ͸����1��Ϊ��ɫ
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration; //��ֹ�������������ֵ����ʱ��

        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
