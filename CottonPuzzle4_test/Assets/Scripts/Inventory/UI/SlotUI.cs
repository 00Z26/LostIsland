using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;
    private bool isSelected; //物品栏里的该物品被选中的状态
    public ItemTooltip tooltip;

    public void SetItem(ItemDetails itemDetails) //设置显示物品的方法，其他位置传参并调用
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        //设置为原有尺寸
        //itemImage.SetNativeSize();  

    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //点击
    {
        isSelected = !isSelected; //改变被选的状态 
        Debug.Log(isSelected);
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData) //鼠标滑入
    {
        if(this.gameObject.activeInHierarchy) //slotUI内有显示物品
        {
            tooltip.gameObject.SetActive(true);
            tooltip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData) //鼠标滑出
    {
        tooltip?.gameObject.SetActive(false);  
    }
}
