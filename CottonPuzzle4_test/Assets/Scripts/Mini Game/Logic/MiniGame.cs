using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameFinish;
    [SceneName] public string gameName; //用字符匹配的，和场景一个模式，这样读小游戏的场景
    public bool isPass; //判断游戏是否通关

    public void UpdateMiniGameState()
    {
        if (isPass)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            OnGameFinish?.Invoke();
        }
    }

}
