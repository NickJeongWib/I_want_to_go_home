using UnityEngine;

public class Mgr_Input : MonoBehaviour
{
    public Vector2 InputMove;
    public Vector2 InputLook;

    public static Mgr_Input Inst;

    void Awake()
    {
        Inst = this;
    }

    void Update()
    {
        // ���� �Է�
        InputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // ���콺 �Է�
        InputLook = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }
}
