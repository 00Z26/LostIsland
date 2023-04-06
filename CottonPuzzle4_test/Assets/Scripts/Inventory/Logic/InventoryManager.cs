using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData; 

    [SerializeField]private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent; //场景加载时，判断物品持有情况，更改UI
    }
    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }


    private void OnItemUsedEvent(ItemName itemName)
    {
        
        var index = itemList.IndexOf(itemName);
        itemList.RemoveAt(index);
        //单一物品使用效果
        if (itemList.Count == 0)
            EventHandler.CallUpdateEvent(null, -1);
    }

    private void OnChangeItemEvent(int index)
    {
        //要考虑Index的范围
      if (index >= 0 && index < itemList.Count)
        {
            ItemDetails itemDetails = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateEvent(itemDetails, index);
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        if(itemList.Count == 0)
        {
            EventHandler.CallUpdateEvent(null, -1);
        } else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateEvent(itemData.GetItemDetails(itemList[i]), i); //循环加载一遍物品，最后只显示最新的，也确保UI正常
            }
        }
    }




    public void AddItem(ItemName itemName)
    {
        if(!itemList.Contains(itemName)) //当前列表不存在则添加进来
        {
            itemList.Add(itemName);
            // UI对应显示
            EventHandler.CallUpdateEvent(itemData.GetItemDetails(itemName), itemList.Count - 1); 
            //新物品添加进来index一定是物品的最后一项,这里的呼叫，把信息给了iventoryManager里，然后那边或许到呼叫，就触发函数
        }
    }

    private int GetItemIndex(ItemName itemName)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i]== itemName)
            {
                return i;
            }
        }
        return -1;
    }
}
