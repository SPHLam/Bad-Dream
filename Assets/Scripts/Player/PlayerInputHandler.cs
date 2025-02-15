using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float MoveInput { get; private set; }
    public bool IsRunning { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool FlashlightPressed { get; private set; }

    // Update is called once per frame
    void Update()
    {
        MoveInput = Input.GetAxisRaw("Horizontal");
        IsRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        JumpPressed = Input.GetKey(KeyCode.Space);
        FlashlightPressed = Input.GetKeyDown(KeyCode.F);
    }
}
