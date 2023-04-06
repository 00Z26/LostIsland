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
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent; //��������ʱ���ж���Ʒ�������������UI
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
        //��һ��Ʒʹ��Ч��
        if (itemList.Count == 0)
            EventHandler.CallUpdateEvent(null, -1);
    }

    private void OnChangeItemEvent(int index)
    {
        //Ҫ����Index�ķ�Χ
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
                EventHandler.CallUpdateEvent(itemData.GetItemDetails(itemList[i]), i); //ѭ������һ����Ʒ�����ֻ��ʾ���µģ�Ҳȷ��UI����
            }
        }
    }




    public void AddItem(ItemName itemName)
    {
        if(!itemList.Contains(itemName)) //��ǰ�б���������ӽ���
        {
            itemList.Add(itemName);
            // UI��Ӧ��ʾ
            EventHandler.CallUpdateEvent(itemData.GetItemDetails(itemName), itemList.Count - 1); 
            //����Ʒ��ӽ���indexһ������Ʒ�����һ��,����ĺ��У�����Ϣ����iventoryManager�Ȼ���Ǳ߻������У��ʹ�������
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
