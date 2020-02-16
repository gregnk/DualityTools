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

using Duality.Input;

namespace Gregnk.Plugins.DualityTools
{
    /// <summary>
    /// The keybindings used to control the camera in manual mode
    /// </summary>
    public class ManualCameraKeys
    {
        private Key up = Key.Keypad8;
        private Key left = Key.Keypad4;
        private Key down = Key.Keypad2;
        private Key right = Key.Keypad6;
        private Key zoomIn = Key.KeypadAdd;
        private Key zoomOut = Key.KeypadSubtract;
        private Key increaseCamSpeed = Key.Keypad9;
        private Key decreaseCamSpeed = Key.Keypad3;
        private Key toggleManualControl = Key.Keypad5;

        public Key Up { get => up; set => up = value; }
        public Key Left { get => left; set => left = value; }
        public Key Down { get => down; set => down = value; }
        public Key Right { get => right; set => right = value; }
        public Key ZoomIn { get => zoomIn; set => zoomIn = value; }
        public Key ZoomOut { get => zoomOut; set => zoomOut = value; }
        public Key IncreaseCamSpeed { get => increaseCamSpeed; set => increaseCamSpeed = value; }
        public Key DecreaseCamSpeed { get => decreaseCamSpeed; set => decreaseCamSpeed = value; }
        public Key ToggleManualControl { get => toggleManualControl; set => toggleManualControl = value; }
    }
}
