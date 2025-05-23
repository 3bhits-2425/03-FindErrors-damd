using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TMPro.Examples {
    public class Benchmark01_UGUI : MonoBehaviour {
        public int BenchmarkType = 0;

        public Canvas canvas;
        public TMP_FontAsset TMProFont;
        public Font TextMeshFont;

        private TextMeshProUGUI m_textMeshPro;
        private TextMesh m_textMesh;

        private const string label01 = "The <color=#0050FF>count is: </color>";
        private const string label02 = "The <color=#0050FF>count is: </color>";

        private Material m_material01;
        private Material m_material02;

        IEnumerator Start() {
            if (BenchmarkType == 0) // TextMesh Pro Component
            {
                m_textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();

                if (TMProFont != null)
                    m_textMeshPro.font = TMProFont;

                m_textMeshPro.fontSize = 48;
                m_textMeshPro.alignment = TextAlignmentOptions.Center;
                m_textMeshPro.extraPadding = true;

                m_material01 = m_textMeshPro.font.material;
                m_material02 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - BEVEL");
            } else if (BenchmarkType == 1) // Legacy TextMesh
              {
                m_textMesh = gameObject.AddComponent<TextMesh>();

                if (TextMeshFont != null) {
                    m_textMesh.font = TextMeshFont;
                    GetComponent<Renderer>().sharedMaterial = TextMeshFont.material;
                }

                m_textMesh.fontSize = 48;
                m_textMesh.alignment = TextAlignment.Center;
            }

            for (int i = 0; i <= 1000000; i++) {
                if (BenchmarkType == 0) {
                    m_textMeshPro.text = label01 + (i % 1000);
                    if (i % 1000 == 999)
                        m_textMeshPro.fontSharedMaterial = (m_textMeshPro.fontSharedMaterial == m_material01) ? m_material02 : m_material01;
                } else if (BenchmarkType == 1) {
                    m_textMesh.text = label02 + (i % 1000).ToString();
                }

                yield return null;
            }

            yield return null;
        }
    }
}
