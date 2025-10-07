using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// ����k�ù���ܡA�ثe�H�ܷN�覡�ާ@�C
    /// ����i�X�R����ڱ��� UI Text / RawImage / Video�C
    /// </summary>
    public class ScreenUIController : MonoBehaviour
    {
        [Header("���k�ù� UI ����]�i�� RawImage �� Text�^")]
        public GameObject leftScreen;
        public GameObject rightScreen;

        public void ShowIdleScreen()
        {
            Debug.Log("[ScreenUI] ��� Idle �w�]�e��");
            // �o�̥i�[�W���k�ù���ܹw�]�Ϥ����޿�
        }

        public void ShowQuestion(string questionText)
        {
            Debug.Log($"[ScreenUI] ����D�ءG{questionText}");
        }

        public void ShowResult(string resultName)
        {
            Debug.Log($"[ScreenUI] ��ܵ��G����G{resultName}");
        }
    }
}
