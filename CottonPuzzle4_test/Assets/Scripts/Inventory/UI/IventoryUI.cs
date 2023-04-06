using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IventoryUI : MonoBehaviour
{
  //控制整个右下角的部分
  //我会写，获取到名字，图片，manager切换在这里调用然后单独写一个类。

    public Button leftButton, rightButton;
    public int currentIndex; //当前显示的物品序号
    public SlotUI slotUI;
    private void OnEnable()
    {
        EventHandler.updateUIEvent += OnUpdateUIEvent;
        
    }
    private void OnDisable()
    {
        EventHandler.updateUIEvent -= OnUpdateUIEvent;

    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if(itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if(index > 0)
            {
                leftButton.interactable = true; //添加的是最新的，所以只激活左侧按钮就可以
            }
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }



    }

    public void SwitchItem(int amount) //amount为增减量
    {
        var index = currentIndex + amount; //currentIndex从上面OnUpdateUIEvent里获得值，当前显示的物品一定是最新的物品，所以是current

        if (index < currentIndex) //这里指移动完的结果，也许可以用list的首尾值进行比较？
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if(index > currentIndex) //移动完左侧有物体，右边没有
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else //多于两个物体的情况
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        //呼叫事件，让manager传sprite
        EventHandler.CallChangeItemEvent(index);
    }

}
