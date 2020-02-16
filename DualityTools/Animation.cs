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

using Duality.Components.Renderers;

namespace Gregnk.Plugins.DualityTools
{
    public class Animation
    {
        private string name = "Untitled Animation";
        private int frameCount = 1;
        private float duration = 1f;
        private SpriteAnimator.LoopMode loopMode;
        private List<AnimationDirection> directions = new List<AnimationDirection>();

        public string Name { get => name; set => name = value; }
        public int FrameCount { get => frameCount; set => frameCount = value; }
        public float Duration { get => duration; set => duration = value; }
        public SpriteAnimator.LoopMode LoopMode { get => loopMode; set => loopMode = value; }
        public List<AnimationDirection> Directions { get => directions; set => directions = value; }

        public override string ToString()
        {
            return string.Format("{0} ({1} frames)", Name, FrameCount);
        }
    }
}
