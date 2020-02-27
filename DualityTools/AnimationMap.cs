/*
 * DualityTools for Duality 3
 * (c) 2019-2020 Gregory Karastergios
 * 
 * Permission to use, copy, modify, and/or distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */

using System.Collections.Generic;

using Duality;
using Duality.Components.Renderers;
using Duality.Editor;

namespace Gregnk.Plugins.DualityTools
{
    /// <summary>
    /// A component that can hold a list of animations at different angles and can be used to control a SpriteAnimator
    /// </summary>
    [RequiredComponent(typeof(SpriteAnimator))]
    [EditorHintCategory("Graphics")]
    public class AnimationMap : Component, ICmpUpdatable
    {
        private float animAngle;
        private List<Animation> animations = new List<Animation>();
        private Animation activeAnimation = null;
        private AnimationDirection activeDirection;

        /// <summary>
        /// The angle that the animation should operate at
        /// </summary>
        public float AnimAngle { get => animAngle; set => animAngle = value; }

        /// <summary>
        /// A list of animations
        /// </summary>
        public List<Animation> Animations { get => animations; set => animations = value; }

        /// <summary>
        /// The animation that is currently being played
        /// </summary>
        public Animation ActiveAnimation { get => activeAnimation; set => activeAnimation = value; }

        /// <summary>
        /// The animation direction that is currently being played
        /// </summary>
        public AnimationDirection ActiveDirection { get => activeDirection; set => activeDirection = value; }

        /// <summary>
        /// Searches for a named animation in the Animation array 
        /// </summary>
        /// <param name="animName">The animation the should be played</param>
        /// <param name="resetTimer">Whether the timer (AnimTime) in the SpriteAnimator should be reset</param>
        public void PlayAnimation(string animName, bool resetTimer = false)
        {
            var animator = GameObj.GetComponent<SpriteAnimator>();

            // Find the animation in the array
            foreach (Animation anim in Animations)
            {
                if (anim != null)
                {
                    if (anim.Name == animName)
                        ActiveAnimation = anim;
                }

                else
                    Logs.Core.WriteError("Animation {0} not found in GameObject {1}", animName, GameObj.FullName);
            }

            // Find the direction in the active animation
            foreach (AnimationDirection direction in ActiveAnimation.Directions)
            {
                if (direction != null)
                {
                    if (direction.Angle == MathF.Round(AnimAngle))
                    {
                        ActiveDirection = direction;
                    }

                    else
                        Logs.Core.WriteError("Animation direction {0} not found in animation in {1} GameObject {2}", direction.Angle, animName, GameObj.FullName);
                }

            }

            // Set the SpriteAnimator parameters
            animator.FirstFrame = ActiveDirection.FirstFrame;
            animator.FrameCount = ActiveAnimation.FrameCount;

            animator.AnimDuration = ActiveAnimation.Duration;
            animator.AnimLoopMode = ActiveAnimation.LoopMode;

            if (resetTimer)
                ResetTimer();

            UnPause();
        }

        /// <summary>
        /// Resets the timer (AnimTime) in the GameObj's SpriteAnimator
        /// </summary>
        public void ResetTimer()
        {
            var animator = GameObj.GetComponent<SpriteAnimator>();
            animator.AnimTime = 0;
        }

        /// <summary>
        /// Pauses the animation that is being played
        /// </summary>
        public void Pause()
        {
            var animator = GameObj.GetComponent<SpriteAnimator>();
            animator.Paused = true;
        }

        /// <summary>
        /// UnPauses the animation that is being played
        /// </summary>
        public void UnPause()
        {
            var animator = GameObj.GetComponent<SpriteAnimator>();
            animator.Paused = false;
        }

        public void OnUpdate()
        {
            // Update the angle in real-time
            if (ActiveDirection != null)
            {
                var animator = GameObj.GetComponent<SpriteAnimator>();

                foreach (AnimationDirection direction in ActiveAnimation.Directions)
                {
                    if (direction != null)
                    {
                        if (direction.Angle == MathF.Round(AnimAngle))
                        {
                            ActiveDirection = direction;
                        }
                    }

                }

                animator.FirstFrame = ActiveDirection.FirstFrame;
            }
        }
    }
}
