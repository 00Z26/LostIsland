using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManger : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private bool canClick;

    private ItemName currentItem;
    public RectTransform hand;//鼠标跟随的
    private bool holdItem;

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }



    private void Update()
    {

        canClick = ObjectAtMousePosition();

        if(hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        if (InteractiveWithUI()) return;
        if (canClick && Input.GetMouseButtonDown(0))
        {
            //存在检测到其他非切换页面的碰撞体的可能，需要对碰撞体进行标签判断
            ClickAction(ObjectAtMousePosition().gameObject);

        }

    }

    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }



    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;  //是否持有
        if(isSelected) //持有获取物品名
        {
            currentItem = itemDetails.itemName;
            
        }
        hand.gameObject.SetActive(holdItem);
    }


    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if(holdItem) //持有物体时才调用，这个bool在订阅触发时发生改变
                {
                    interactive?.CheckItem(currentItem);
                }
                else
                {
                    interactive?.EmptyCliked(); //未持有物体的空点
                }
                
                break;

        }
        //colliderName = ObjectAtMousePosition();
        //tag = colliderName.tag;
        //if(tag == "Teleport")
        //{
        //    return 1;
        //}
    }
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos); //获取与该点重合的碰撞体
    }

    private bool InteractiveWithUI()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
