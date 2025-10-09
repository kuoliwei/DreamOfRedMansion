using UnityEngine;

namespace DreamOfRedMansion.Data
{
    [CreateAssetMenu(fileName = "ResultEntry", menuName = "DreamOfRedMansion/Result Entry")]
    public class ResultEntrySO : ScriptableObject
    {
        [Header("��������")]
        [Tooltip("���ײզX�A�Ҧp 1100�]�O �O �_ �_�^")]
        public string answerPattern;

        [Header("�����T")]
        [Tooltip("����W��")]
        public string characterName;

        [Tooltip("���⻡����r")]
        [TextArea(2, 5)]
        public string description;

        [Tooltip("����Ϥ��]�i��^")]
        public Sprite resultImage;
    }
}
