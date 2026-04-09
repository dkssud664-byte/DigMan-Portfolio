using Unity.VisualScripting;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Vector2 Move { get; private set; }

    public bool SpaceDown { get; private set; }
    public bool SpaceHold { get; private set; }
    public bool LeftClickDown { get; private set; }
    public bool LeftClickHold { get; private set; }
    public bool RightClickDown { get; private set; }
    public bool RightClickHold { get; private set; }
    public bool ESCDown { get; private set; }
    public bool LeftShiftHold {  get; private set; }

    #region Interaction Key
    public bool FDown {  get; private set; }
    public bool LeftMouseDown {  get; private set; }
    public bool LeftMouseHold {  get; private set; }
    public bool QDown { get; private set; }
    public bool EDown { get; private set; }
    public bool NumberOneDown { get; private set; }
    public bool NumberTwoDown { get; private set; }
    public bool NumberThreeDown { get; private set; }
    public bool NumberFourDown { get; private set; }
    public bool NumberFiveDown { get; private set; }
    public bool NumberSixDown { get; private set; }
    #endregion

    void Awake()
    {
       
    }

    void Update()
    {
        Move = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        SpaceDown = Input.GetKeyDown(KeyCode.Space);
        SpaceHold = Input.GetKey(KeyCode.Space);
        LeftClickDown = Input.GetMouseButtonDown(0);
        LeftClickHold = Input.GetMouseButton(0);
        RightClickDown = Input.GetMouseButtonDown(1);
        RightClickHold = Input.GetMouseButton(1);
        ESCDown = Input.GetKeyDown(KeyCode.Escape);
        LeftShiftHold = Input.GetKey(KeyCode.LeftShift);

        FDown = Input.GetKeyDown(KeyCode.F);
        LeftMouseDown = Input.GetMouseButtonDown(0);
        LeftMouseHold = Input.GetMouseButton(0);
        QDown = Input.GetKeyDown(KeyCode.Q);
        EDown = Input.GetKeyDown(KeyCode.E);
        NumberOneDown = Input.GetKeyDown(KeyCode.Alpha1);
        NumberTwoDown = Input.GetKeyDown(KeyCode.Alpha2);
        NumberThreeDown = Input.GetKeyDown(KeyCode.Alpha3);
        NumberFourDown = Input.GetKeyDown(KeyCode.Alpha4);
        NumberFiveDown = Input.GetKeyDown(KeyCode.Alpha5);
        NumberSixDown = Input.GetKeyDown(KeyCode.Alpha6);
    }

    private void LateUpdate()
    {
        //ClearFrameInput();
    }

    public bool TryInteraction()
    {
        bool isTure =
            LeftMouseDown ||
            LeftMouseHold ||
            QDown ||
            EDown ||
            NumberOneDown ||
            NumberTwoDown ||
            NumberThreeDown ||
            NumberFourDown ||
            NumberFiveDown ||
            NumberSixDown;

        return isTure;
    }

    public int GetNumberKeyDown()
    {
        if (NumberOneDown) return 0;
        if (NumberTwoDown) return 1;
        if (NumberThreeDown) return 2;
        if (NumberFourDown) return 3;
        if (NumberFiveDown) return 4;
        if (NumberSixDown) return 5;
        return -1;
    }

    // 프레임 입력 소비 후 초기화
    public void ClearFrameInput()
    {
        SpaceDown = false;
        LeftClickDown = false;
        ESCDown = false;
    }

    public void ClearSpaceDownInput()
    {
        SpaceDown = false;
    }
}
