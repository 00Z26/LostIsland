using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string,bool>(); //记录游戏里的多个小游戏 

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
    }


    void Start()
    {
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
    }
    private void OnAfterSceneLoadedEvent()
    {
        foreach(var miniGame in FindObjectsOfType<MiniGame>()) //当前场景挂载这个脚本的物体的名字
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass)) //找到字典里村的状态
            {
                miniGame.isPass = isPass; //把这个一直存在dict中的值，传到H2A中
                miniGame.UpdateMiniGameState();
            }
        }
    }

    private void OnGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;
    }

}
