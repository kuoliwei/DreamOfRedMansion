using UnityEngine;
using UnityEngine.UI;
using System.Collections; // ← 新增

namespace DreamOfRedMansion
{
    /// <summary>
    /// 控制左右螢幕的顯示內容，根據遊戲狀態顯示對應畫面。
    /// </summary>
    public class ScreenUIController : MonoBehaviour
    {
        [Header("左右螢幕物件")]
        public GameObject leftScreen;
        public GameObject rightScreen;

        [Header("螢幕對應面板")]
        public GameObject idlePanel;
        public GameObject questionPanel;
        public GameObject resultPanel;

        //public GameObject scrollPanel;

        [Header("結果畫面 UI 元件")]
        public Text resultNameText;
        public Text resultTitleText;
        public Text resultIntroductionText;
        public Text resultDescriptionText;
        public Image resultImage;

        private void Awake()
        {
            // 啟動時先顯示 Idle 畫面
            ShowIdleScreen();
        }

        public void ShowIdleScreen()
        {
            RevealIdel();
            SetActivePanel(idlePanel);
            Debug.Log("[ScreenUI] 顯示 Idle 畫面");
        }

        public void ShowQuestion(string questionText = null)
        {
            StartCoroutine(FadeOutIdle(2));
            //SetActivePanel(questionPanel);

            // 如果有題目文字，印出（未接UI文字前以Log示意）
            if (!string.IsNullOrEmpty(questionText))
                Debug.Log($"[ScreenUI] 顯示題目：{questionText}");
        }

        public void ShowResult(string resultName = null)
        {
            SetActivePanel(resultPanel);

            if (!string.IsNullOrEmpty(resultName))
                Debug.Log($"[ScreenUI] 顯示結果角色：{resultName}");
        }

        /// <summary>
        /// 更新結果畫面顯示的文字與圖片內容
        /// </summary>
        public void UpdateResultContent(string name, string title, string introduction, string description, Sprite image)
        {
            if (resultNameText != null)
                resultNameText.text = name;

            if (resultTitleText != null)
                resultTitleText.text = title;

            if (resultIntroductionText != null)
                resultIntroductionText.text = introduction;

            if (resultDescriptionText != null)
                resultDescriptionText.text = description;

            if (resultImage != null)
            {
                resultImage.sprite = image;
                resultImage.gameObject.SetActive(image != null);
            }

            Debug.Log($"[ScreenUI] 更新結果內容 → {name}");
        }

        private void SetActivePanel(GameObject targetPanel)
        {
            if (idlePanel != null ) idlePanel.SetActive(false);
            if (questionPanel != null) questionPanel.SetActive(false);
            if (resultPanel != null) resultPanel.SetActive(false);
            //if (scrollPanel != null) scrollPanel.SetActive(false);
            

            if (targetPanel != null) targetPanel.SetActive(true);
            //if (targetPanel == questionPanel) resultPanel.SetActive(true);
        }
        private void RevealIdel()
        {
            if (idlePanel == null) return;
            var rawImage = idlePanel.GetComponent<RawImage>();
            if (rawImage == null)
            {
                Debug.LogWarning("[ScreenUI] IdlePanel 上找不到 RawImage，無法淡出。");
                return;
            }
            Color color = rawImage.color;
            color.a = 1;
            rawImage.color = color;
        }
        private IEnumerator FadeOutIdle(float duration = 1f)
        {
            questionPanel.SetActive(true);
            if (idlePanel == null) yield break;

            var rawImage = idlePanel.GetComponent<RawImage>();
            if (rawImage == null)
            {
                Debug.LogWarning("[ScreenUI] IdlePanel 上找不到 RawImage，無法淡出。");
                yield break;
            }

            Color color = rawImage.color;
            float startAlpha = color.a;
            float time = 0f;

            // 線性漸變 alpha
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                color.a = Mathf.Lerp(startAlpha, 0f, t);
                rawImage.color = color;
                yield return null;
            }

            // 確保最後完全透明
            color.a = 0f;
            rawImage.color = color;
            //SetActivePanel(questionPanel);
            idlePanel.SetActive(false);
        }
    }
}
