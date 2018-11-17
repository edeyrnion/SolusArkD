using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFade
{
    public List<Renderer> fadeOut = new List<Renderer>(8);
    public List<Renderer> fadeIn = new List<Renderer>(8);

    public void Fade(Renderer[] renderes)
    {
        var onlyInFadeOut = fadeOut.Except(renderes).ToArray();
        var onlyInFadeIn = fadeIn.Except(renderes).ToArray();

        fadeOut.Clear();
        fadeIn.Clear();

        fadeOut.AddRange(renderes);
        fadeIn.AddRange(onlyInFadeOut);
        fadeIn.AddRange(onlyInFadeIn);

        for (int i = 0; i < renderes.Length; i++)
        {
            var color = renderes[i].material.color;
            if (color.a >= 0.3f)
            {
                var a = color.a - Time.deltaTime * 2;
                a = Mathf.Clamp(a, 0.3f, 1f);
                renderes[i].material.color = new Color(1f, 1f, 1f, a);
            }
        }

        for (int i = 0; i < fadeIn.Count; i++)
        {
            var color = fadeIn[i].material.color;
            if (color.a < 1f)
            {
                var a = color.a + Time.deltaTime * 2;
                a = Mathf.Clamp(a, 0.3f, 1f);
                fadeIn[i].material.color = new Color(1f, 1f, 1f, a);
            }
            else
            {
                fadeIn.RemoveAt(i);
            }
        }
    }
}
