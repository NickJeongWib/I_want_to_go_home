using UnityEngine;

public class Equip_Slot : MonoBehaviour, ISlot
{
    [SerializeField] int SlotNum;
    [SerializeField] Item ItemData;

    // ���� �ε��� �ο�
    void ISlot.Set_SlotNum(int num)
    {
        SlotNum = num;
    }
}
