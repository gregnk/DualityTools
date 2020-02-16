﻿/*
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

using Duality;

namespace Gregnk.Plugins.DualityTools
{
    public class AnimationDirection
    {
        private string name = "Untitled Direction";
        private int firstFrame = 0;
        private float angle = 0f;

        public string Name { get => name; set => name = value; }
        public int FirstFrame { get => firstFrame; set => firstFrame = value; }
        public float Angle { get => angle; set => angle = value; }

        public override string ToString()
        {
            return string.Format("{0} ({1}°) at frame {2}", Name, MathF.RoundToInt(Angle), FirstFrame);
        }

    }
}
