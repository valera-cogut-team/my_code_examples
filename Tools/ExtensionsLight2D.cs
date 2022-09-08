using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace _Main._Core.Scripts.Tools
{
    public static class ExtensionsLight2D
    {
        /// <summary>Tweens a Light's color to the given value.
        /// Also stores the light as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="duration">The duration of the tween</param>
        public static TweenerCore<Color, Color, ColorOptions> DOColor(
            this Light2D target,
            Color endValue,
            float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration);
            t.SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);
            return t;
        }

        /// <summary>Tweens a Light's intensity to the given value.
        /// Also stores the light as the tween's target so it can be used for filtered operations</summary>
        /// <param name="endValue">The end value to reach</param>
        /// <param name="duration">The duration of the tween</param>
        public static TweenerCore<float, float, FloatOptions> DOIntensity(
            this Light2D target,
            float endValue,
            float duration)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To((DOGetter<float>) (() => target.intensity), (DOSetter<float>) (x => target.intensity = x), endValue, duration);
            t.SetTarget<TweenerCore<float, float, FloatOptions>>((object) target);
            return t;
        }
    }
}