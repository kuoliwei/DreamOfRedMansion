using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DreamOfRedMansion.Data
{
    [CreateAssetMenu(fileName = "QuestionSet", menuName = "DreamOfRedMansion/Question Set")]
    public class QuestionSet : ScriptableObject
    {
        [Tooltip("�Ҧ��i���D�� ScriptableObject")]
        public List<QuestionData> questions = new List<QuestionData>();

        /// <summary>
        /// �H��������w�ƶq���D�ء]�����ơ^
        /// </summary>
        public List<QuestionData> GetRandomQuestions(int count)
        {
            var available = questions.Where(q => q != null && q.enabled).ToList();
            if (available.Count <= count)
                return available;

            var selected = new List<QuestionData>();
            while (selected.Count < count)
            {
                var q = available[Random.Range(0, available.Count)];
                if (!selected.Contains(q))
                    selected.Add(q);
            }
            return selected;
        }
    }
}
