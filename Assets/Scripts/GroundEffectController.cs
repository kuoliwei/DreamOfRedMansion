using UnityEngine;

namespace DreamOfRedMansion
{
    /// <summary>
    /// 控制地板特效顯示，目前以更換貼圖或顏色為主。
    /// 後續可擴充成 Shader / VFXGraph。
    /// </summary>
    public class GroundEffectController : MonoBehaviour
    {
        [Header("地板 Mesh 或 Renderer")]
        public Renderer groundRenderer;

        [Header("不同階段使用的顏色")]
        public Color idleColor = Color.black;
        public Color questionColor = Color.blue;
        public Color resultColor = Color.red;

        public void SetIdle()
        {
            SetGroundColor(idleColor);
            Debug.Log("[GroundEffect] 切換到 Idle 狀態");
        }

        public void SetQuestion()
        {
            SetGroundColor(questionColor);
            Debug.Log("[GroundEffect] 顯示題目互動階段特效");
        }

        public void SetResult()
        {
            SetGroundColor(resultColor);
            Debug.Log("[GroundEffect] 顯示結果階段特效");
        }

        private void SetGroundColor(Color color)
        {
            if (groundRenderer != null && groundRenderer.material != null)
                groundRenderer.material.color = color;
        }
    }
}
