using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    private DialogueController dialogueController;

    private void Awake()
    {
        
        dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyCliked()
    {
        if (isDone)
            dialogueController.ShowDialogueFinish();//未持有道具，已给过正确道具，获取结束的互动对话
        else
            dialogueController.ShowDialogueEmpty(); //未持有道具，获取到互动点击对话
    }

    protected override void OnClickedAction()
    {
        //finsh的内容（B）
        dialogueController.ShowDialogueFinish();//持有正确道具，获取到互动并对话
    }

}
