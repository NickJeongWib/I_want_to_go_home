using UnityEngine;

public class Interaction_Item : Interaction
{
    public int Item_Amount = 0;

    public override void OnInteraction()
    {
        Debug.Log(this.InteractionName);
        // �κ��丮�� ���� ������ ȹ�� �ڵ�
    }
}
