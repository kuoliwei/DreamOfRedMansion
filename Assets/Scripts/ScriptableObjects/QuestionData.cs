using UnityEngine;

namespace DreamOfRedMansion.Data
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "DreamOfRedMansion/Question Data")]
    public class QuestionData : ScriptableObject
    {
        [Header("題目設定")]
        [Tooltip("題目文字內容")]
        [TextArea(2, 3)]
        public string questionText;

        [Tooltip("圈選項文字（例如 是 / O）")]
        public string optionCircle = "圈";

        [Tooltip("叉選項文字（例如 否 / X）")]
        public string optionCross = "叉";

        [Header("角色加權設定")]
        [Tooltip("回答圈時，對每個角色的分數加權")]
        public float[] circleWeights;

        [Tooltip("回答叉時，對每個角色的分數加權")]
        public float[] crossWeights;

        [Header("顯示設定")]
        [Tooltip("是否啟用此題目")]
        public bool enabled = true;
    }
}
