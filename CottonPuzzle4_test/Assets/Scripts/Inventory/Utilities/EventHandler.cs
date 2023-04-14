using System;
using UnityEngine;

public static class EventHandler //改成静态方法，在任何地方都可以呼叫订阅
{
    public static event Action<ItemDetails, int> updateUIEvent;  //订阅方法
    public static void CallUpdateEvent(ItemDetails itemDetails, int index)
    {
        updateUIEvent?.Invoke(itemDetails, index);
    }
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()  //场景切换前保存物品存在的字典信息
    {
        BeforeSceneUnloadEvent?.Invoke();   
    }

    public static event Action AfterSceneLoadedEvent;  //切换后加载保存的物品字典信息
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

    public static event Action CheckGameStateEvent; //用来检查小游戏是否解完
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
