using UnityEngine;

public class Mgr_Game : MonoBehaviour
{



    public static Mgr_Game Inst;

    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        // �׽�Ʈ
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
    }
}
