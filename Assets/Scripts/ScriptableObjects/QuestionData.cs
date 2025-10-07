using UnityEngine;

namespace DreamOfRedMansion.Data
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "DreamOfRedMansion/Question Data")]
    public class QuestionData : ScriptableObject
    {
        [Header("�D�س]�w")]
        [Tooltip("�D�ؤ�r���e")]
        [TextArea(2, 3)]
        public string questionText;

        [Tooltip("��ﶵ��r�]�Ҧp �O / O�^")]
        public string optionCircle = "��";

        [Tooltip("�e�ﶵ��r�]�Ҧp �_ / X�^")]
        public string optionCross = "�e";

        [Header("����[�v�]�w")]
        [Tooltip("�^����ɡA��C�Ө��⪺���ƥ[�v")]
        public float[] circleWeights;

        [Tooltip("�^���e�ɡA��C�Ө��⪺���ƥ[�v")]
        public float[] crossWeights;

        [Header("��ܳ]�w")]
        [Tooltip("�O�_�ҥΦ��D��")]
        public bool enabled = true;
    }
}
