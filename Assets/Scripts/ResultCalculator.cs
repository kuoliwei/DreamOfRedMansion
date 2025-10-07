using System;
using System.Collections;
using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// �ھڥ|�D���׵��G��ܨ���P�����C
    /// �Ȯɪ����G����ܤ�r Log�A�����^�� Idle�C
    /// </summary>
    public class ResultCalculator : MonoBehaviour
    {
        [Header("��ܮɶ��]��^")]
        public float displayDuration = 20f;

        public IEnumerator ShowResult(Action onComplete)
        {
            Debug.Log("[ResultCalculator] �}�l��ܵ��G�]�ȮɥH��r���N�^");
            yield return new WaitForSeconds(displayDuration);
            Debug.Log("[ResultCalculator] ���G���q���� �� �^ Idle");
            onComplete?.Invoke();
        }
    }
}
