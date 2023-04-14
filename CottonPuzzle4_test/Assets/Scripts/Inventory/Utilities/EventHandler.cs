using System;
using UnityEngine;

public static class EventHandler //�ĳɾ�̬���������κεط������Ժ��ж���
{
    public static event Action<ItemDetails, int> updateUIEvent;  //���ķ���
    public static void CallUpdateEvent(ItemDetails itemDetails, int index)
    {
        updateUIEvent?.Invoke(itemDetails, index);
    }
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()  //�����л�ǰ������Ʒ���ڵ��ֵ���Ϣ
    {
        BeforeSceneUnloadEvent?.Invoke();   
    }

    public static event Action AfterSceneLoadedEvent;  //�л�����ر������Ʒ�ֵ���Ϣ
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }

    public static event Action<int> ChangeItemEvent;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent; //�������С��Ϸ�Ƿ����
    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }
}
