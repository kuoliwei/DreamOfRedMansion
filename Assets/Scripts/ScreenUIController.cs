using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// 控制左右螢幕顯示，目前以示意方式操作。
    /// 後續可擴充成實際控制 UI Text / RawImage / Video。
    /// </summary>
    public class ScreenUIController : MonoBehaviour
    {
        [Header("左右螢幕 UI 物件（可為 RawImage 或 Text）")]
        public GameObject leftScreen;
        public GameObject rightScreen;

        public void ShowIdleScreen()
        {
            Debug.Log("[ScreenUI] 顯示 Idle 預設畫面");
            // 這裡可加上左右螢幕顯示預設圖片的邏輯
        }

        public void ShowQuestion(string questionText)
        {
            Debug.Log($"[ScreenUI] 顯示題目：{questionText}");
        }

        public void ShowResult(string resultName)
        {
            Debug.Log($"[ScreenUI] 顯示結果角色：{resultName}");
        }
    }
}
