using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requirItem;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if(!isDone && itemName == requirItem)
        {
            isDone = true;
            //检测到使用了正确的物品，使用并在物品栏移除
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName); //确认了使用该物品
        }
    }
    //默认是正确的物品执行
    protected virtual void OnClickedAction()
    {

    }
    public virtual void EmptyCliked()  //要放在cursor里执行
    {
        Debug.Log("空点");
    }

}
