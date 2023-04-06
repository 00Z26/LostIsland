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
            dialogueController.ShowDialogueFinish();//δ���е��ߣ��Ѹ�����ȷ���ߣ���ȡ�����Ļ����Ի�
        else
            dialogueController.ShowDialogueEmpty(); //δ���е��ߣ���ȡ����������Ի�
    }

    protected override void OnClickedAction()
    {
        //finsh�����ݣ�B��
        dialogueController.ShowDialogueFinish();//������ȷ���ߣ���ȡ���������Ի�
    }

}
