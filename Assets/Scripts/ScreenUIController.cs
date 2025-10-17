using UnityEngine;
using UnityEngine.UI;
using System.Collections; // �� �s�W

namespace DreamOfRedMansion
{
    /// <summary>
    /// ����k�ù�����ܤ��e�A�ھڹC�����A��ܹ����e���C
    /// </summary>
    public class ScreenUIController : MonoBehaviour
    {
        [Header("���k�ù�����")]
        public GameObject leftScreen;
        public GameObject rightScreen;

        [Header("�ù��������O")]
        public GameObject idlePanel;
        public GameObject questionPanel;
        public GameObject resultPanel;

        //public GameObject scrollPanel;

        [Header("���G�e�� UI ����")]
        public Text resultNameText;
        public Text resultTitleText;
        public Text resultIntroductionText;
        public Text resultDescriptionText;
        public Image resultImage;

        private void Awake()
        {
            // �Ұʮɥ���� Idle �e��
            ShowIdleScreen();
        }

        public void ShowIdleScreen()
        {
            RevealIdel();
            SetActivePanel(idlePanel);
            Debug.Log("[ScreenUI] ��� Idle �e��");
        }

        public void ShowQuestion(string questionText = null)
        {
            StartCoroutine(FadeOutIdle(2));
            //SetActivePanel(questionPanel);

            // �p�G���D�ؤ�r�A�L�X�]����UI��r�e�HLog�ܷN�^
            if (!string.IsNullOrEmpty(questionText))
                Debug.Log($"[ScreenUI] ����D�ءG{questionText}");
        }

        public void ShowResult(string resultName = null)
        {
            SetActivePanel(resultPanel);

            if (!string.IsNullOrEmpty(resultName))
                Debug.Log($"[ScreenUI] ��ܵ��G����G{resultName}");
        }

        /// <summary>
        /// ��s���G�e����ܪ���r�P�Ϥ����e
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

            Debug.Log($"[ScreenUI] ��s���G���e �� {name}");
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
                Debug.LogWarning("[ScreenUI] IdlePanel �W�䤣�� RawImage�A�L�k�H�X�C");
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
                Debug.LogWarning("[ScreenUI] IdlePanel �W�䤣�� RawImage�A�L�k�H�X�C");
                yield break;
            }

            Color color = rawImage.color;
            float startAlpha = color.a;
            float time = 0f;

            // �u�ʺ��� alpha
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / duration);
                color.a = Mathf.Lerp(startAlpha, 0f, t);
                rawImage.color = color;
                yield return null;
            }

            // �T�O�̫᧹���z��
            color.a = 0f;
            rawImage.color = color;
            //SetActivePanel(questionPanel);
            idlePanel.SetActive(false);
        }
    }
}
