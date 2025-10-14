using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ҥΩҦ��s������ܾ��]Display 0 �H�~�� Display 1, 2, ...�^
/// ������`�n����]�Ҧp GameManager�^�Y�i�C
/// </summary>
public class MultiDisplayActivator : MonoBehaviour
{
    [SerializeField] private InputField mainCameraTargetDisplay;
    [SerializeField] private InputField mainCanvasTargetDisplay;
    [SerializeField] private InputField leftCanvasTargetDisplay;
    [SerializeField] private InputField rightCanvasTargetDisplay;
    [SerializeField] private InputField groundCanvasTargetDisplay;
    [SerializeField] private Button changeGroundCameraRotation;

    [SerializeField] private Text currentMainCameraTargetDisplay;
    [SerializeField] private Text currentMainCanvasTargetDisplay;
    [SerializeField] private Text currentLeftCanvasTargetDisplay;
    [SerializeField] private Text currentRightCanvasTargetDisplay;
    [SerializeField] private Text currentGroundCanvasTargetDisplay;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas leftCanvas;
    [SerializeField] private Canvas rightCanvas;
    [SerializeField] private Canvas groundCanvas;
    [SerializeField] private Transform groundCamera;

    private int displayLength;
    private void Awake()
    {
#if !UNITY_EDITOR
        // Display 0 �O�D��ܾ��A��L�ݭn��ʱҥ�
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
            Debug.Log($"�w�ҥ� Display {i}");
        }
        displayLength = Display.displays.Length;
        Debug.Log($"�h��ܾ��Ұʧ����A�@������ {Display.displays.Length} ����ܾ��C");
#endif

#if UNITY_EDITOR
        displayLength = 4;
#endif
        currentMainCameraTargetDisplay.text = mainCamera.targetDisplay.ToString();
        currentMainCanvasTargetDisplay.text = mainCanvas.targetDisplay.ToString();
        currentLeftCanvasTargetDisplay.text = leftCanvas.targetDisplay.ToString();
        currentRightCanvasTargetDisplay.text = rightCanvas.targetDisplay.ToString();
        currentGroundCanvasTargetDisplay.text = groundCanvas.targetDisplay.ToString();
    }
    public void SetMainCameraTargetDisplay()
    {
        Debug.Log(displayLength);
        if (int.TryParse(mainCameraTargetDisplay.text, out int target) && target < displayLength)
        {
            mainCamera.targetDisplay = target;
            currentMainCameraTargetDisplay.text = mainCamera.targetDisplay.ToString();
            mainCameraTargetDisplay.text = "";
        }
        else
        {
            mainCameraTargetDisplay.text = "��J���~";
        }
    }
    public void SetMainCanvasTargetDisplay()
    {
        Debug.Log(displayLength);
        if (int.TryParse(mainCanvasTargetDisplay.text, out int target) && target < displayLength)
        {
            mainCanvas.targetDisplay = target;
            currentMainCanvasTargetDisplay.text = mainCanvas.targetDisplay.ToString();
            mainCanvasTargetDisplay.text = "";
        }
        else
        {
            mainCanvasTargetDisplay.text = "��J���~";
        }
    }
    public void SetLeftCanvasTargetDisplay()
    {
        Debug.Log(displayLength);
        if (int.TryParse(leftCanvasTargetDisplay.text, out int target) && target < displayLength)
        {
            leftCanvas.targetDisplay = target;
            currentLeftCanvasTargetDisplay.text = leftCanvas.targetDisplay.ToString();
            leftCanvasTargetDisplay.text = "";
        }
        else
        {
            leftCanvasTargetDisplay.text = "��J���~";
        }
    }
    public void SetRightCanvasTargetDisplay()
    {
        Debug.Log(displayLength);
        if (int.TryParse(rightCanvasTargetDisplay.text, out int target) && target < displayLength)
        {
            rightCanvas.targetDisplay = target;
            currentRightCanvasTargetDisplay.text = rightCanvas.targetDisplay.ToString();
            rightCanvasTargetDisplay.text = "";
        }
        else
        {
            rightCanvasTargetDisplay.text = "��J���~";
        }
    }
    public void SetGroundCanvasTargetDisplay()
    {
        Debug.Log(displayLength);
        if (int.TryParse(groundCanvasTargetDisplay.text, out int target) && target < displayLength)
        {
            groundCanvas.targetDisplay = target;
            currentGroundCanvasTargetDisplay.text = groundCanvas.targetDisplay.ToString();
            groundCanvasTargetDisplay.text = "";
        }
        else
        {
            groundCanvasTargetDisplay.text = "��J���~";
        }
    }
    public void ChangeGroundCameraRotation()
    {
        Vector3 currentLocalEulerAngles = new Vector3(0, 0, (int)groundCamera.localEulerAngles.z + 90);

        groundCamera.localEulerAngles = currentLocalEulerAngles;
    }
}
