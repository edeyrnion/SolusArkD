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

        public void Fade(Renderer[] renderes)
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
            for (int i = 0; i < renderes.Length; i++)
            {
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
    }
}
