using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string,bool>(); //��¼��Ϸ��Ķ��С��Ϸ 

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
        foreach(var miniGame in FindObjectsOfType<MiniGame>()) //��ǰ������������ű������������
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass)) //�ҵ��ֵ�����״̬
            {
                miniGame.isPass = isPass; //�����һֱ����dict�е�ֵ������H2A��
                miniGame.UpdateMiniGameState();
            }
        }
    }

    private void OnGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;
    }

}
