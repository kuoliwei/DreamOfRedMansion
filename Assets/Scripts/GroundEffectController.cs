using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// ����a�O�S����ܡA�ثe�H�󴫶K�ϩ��C�⬰�D�C
    /// ����i�X�R�� Shader / VFXGraph�C
    /// </summary>
    public class GroundEffectController : MonoBehaviour
    {
        [Header("�a�O Mesh �� Renderer")]
        public Renderer groundRenderer;

        [Header("���P���q�ϥΪ��C��")]
        public Color idleColor = Color.black;
        public Color questionColor = Color.blue;
        public Color resultColor = Color.red;

        public void SetIdle()
        {
            SetGroundColor(idleColor);
            Debug.Log("[GroundEffect] ������ Idle ���A");
        }

        public void SetQuestion()
        {
            SetGroundColor(questionColor);
            Debug.Log("[GroundEffect] ����D�ؤ��ʶ��q�S��");
        }

        public void SetResult()
        {
            SetGroundColor(resultColor);
            Debug.Log("[GroundEffect] ��ܵ��G���q�S��");
        }

        private void SetGroundColor(Color color)
        {
            if (groundRenderer != null && groundRenderer.material != null)
                groundRenderer.material.color = color;
        }
    }
}
