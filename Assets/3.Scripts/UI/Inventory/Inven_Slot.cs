using UnityEngine;

public class Inven_Slot : MonoBehaviour, ISlot
{
    [SerializeField] int SlotNum;

    // ���� �ε��� �ο�
    void ISlot.Set_SlotNum(int num)
    {
        SlotNum = num;
    }
}
