using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    public class ObstacleFade
    {
        private List<Renderer> fadeOut = new List<Renderer>(8);
        private List<Renderer> fadeIn = new List<Renderer>(8);

        public IList<Renderer> FadeOut => fadeOut.AsReadOnly();
        public IList<Renderer> FadeIn => fadeIn.AsReadOnly();

        public void Fade(List<Renderer> renderes)
        {
            // Get all former obstacles.
            var onlyInFadeOut = fadeOut.Except(renderes).ToArray();
            var onlyInFadeIn = fadeIn.Except(renderes).ToArray();

            fadeOut.Clear();
            fadeIn.Clear();

            // Add current obstacles to fade out.
            fadeOut.AddRange(renderes);

            // Add former obstacles to fade in.
            fadeIn.AddRange(onlyInFadeOut);
            fadeIn.AddRange(onlyInFadeIn);

            // Fade out obstacles.
            for (int i = 0; i < renderes.Count; i++)
            {
                if (renderes[i] == null) { continue; }
                var color = renderes[i].material.color;
                var r = color.r;
                var g = color.g;
                var b = color.b;
                if (color.a >= 0.3f)
                {
                    var a = color.a - Time.deltaTime * 2;
                    a = Mathf.Clamp(a, 0.3f, 1f);
                    renderes[i].material.color = new Color(r, g, b, a);
                }
            }

            // Fade in former obstacles.
            for (int i = 0; i < fadeIn.Count; i++)
            {
                if (fadeIn[i] == null) { continue; }
                var color = fadeIn[i].material.color;
                var r = color.r;
                var g = color.g;
                var b = color.b;
                if (color.a < 1f)
                {
                    var a = color.a + Time.deltaTime * 2;
                    a = Mathf.Clamp(a, 0.3f, 1f);
                    fadeIn[i].material.color = new Color(r, g, b, a);
                }
                else
                {
                    fadeIn.RemoveAt(i);
                }
            }
        }

        private enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,
            Transparent
        }

        private void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 2450;
                    break;
                case BlendMode.Fade:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
                case BlendMode.Transparent:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
            }

        }
    }
}
