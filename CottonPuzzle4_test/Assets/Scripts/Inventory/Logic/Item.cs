using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public ItemName itemName;
   //��Ʒ�����ʱִ�еĺ����������ж�tag�Ƿ��ʰȡ��������Ʒ����
   public void ItemClicked()
    {
        InventoryManager.Instance.AddItem(itemName);//��������崫��Manager��
        //ʰȡ�����ظ�����
        this.gameObject.SetActive(false);   
    }

}
