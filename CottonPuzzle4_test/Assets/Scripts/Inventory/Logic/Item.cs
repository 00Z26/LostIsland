using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public ItemName itemName;
   //物品被点击时执行的函数方法：判断tag是否可拾取，加入物品栏？
   public void ItemClicked()
    {
        InventoryManager.Instance.AddItem(itemName);//把这个物体传到Manager里
        //拾取后隐藏该物体
        this.gameObject.SetActive(false);   
    }

}
