using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();//�����л�����״̬����

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.updateUIEvent += OnUpdateUIEvennt;

    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.updateUIEvent -= OnUpdateUIEvennt;
    }

    private void OnUpdateUIEvennt(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }


    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName)) //���ڲ�������������Ʒ�����
            {
                itemAvailableDict.Add(item.itemName, true);
            }
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone; //�����¼״̬
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }

    }

    private void OnAfterSceneLoadedEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())  //����Ѱ�������˸ýű��ģ�
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true); //����ҵ����ֵ���û�еģ���ӽ�ȥ�����ʱ�򳡾����Ѿ���ʾ�ˣ�
            }
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]); //�����ֵ�����ص�״̬����
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name];  //���ֵ������֮ǰ���״̬

            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone); 
            }
        }
    }


}
