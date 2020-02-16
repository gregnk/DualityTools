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

using Duality;
using Duality.Input;
using Duality.Components;

namespace Gregnk.Plugins.DualityTools
{   
    /// <summary>
    /// An object that can be used to follow a specified GameObject. 
    /// 
    /// Also comes with a manual controller that can be toggled on and off (see ManualCameraKeybindings for controls)
    /// </summary>
    [RequiredComponent(typeof(Camera))]
    public class CameraController : Component, ICmpUpdatable
    {
        private GameObject target;
        private bool manualControl = false;
        private uint manualSpeed = 2;
        private ManualCameraKeys manualCameraKeybindings = new ManualCameraKeys();
        private bool enableManualControlToggle = false;

        /// <summary>
        /// The GameObject that the CameraController should follow
        /// </summary>
        public GameObject Target { get => target; set => target = value; }

        /// <summary>
        /// Whether manual control is enabled
        /// </summary>
        public bool ManualControl { get => manualControl; set => manualControl = value; }
        
        /// <summary>
        /// The speed that the camera moves at when in manual mode
        /// </summary>
        public uint ManualSpeed { get => manualSpeed; set => manualSpeed = value; }

        /// <summary>
        /// The keybindings used to control the camera in manual mode
        /// </summary>
        public ManualCameraKeys ManualCameraKeybindings { get => manualCameraKeybindings; set => manualCameraKeybindings = value; }

        /// <summary>
        /// Whether ManualControl can be toggled with the key specified in toggleManualControl
        /// </summary>
        public bool EnableManualControlToggle { get => enableManualControlToggle; set => enableManualControlToggle = value; }

        public void OnUpdate()
        {
            // Toggle manual control
            if (DualityApp.Keyboard.KeyHit(Key.Keypad5) && EnableManualControlToggle)
            {
                if (!ManualControl)
                    ManualControl = true;
                else
                    ManualControl = false;
            }

            // Follow the target
            if (Target != null && !ManualControl)
            {
                Vector3 targetLocation = Target.Transform.Pos;
                targetLocation.Z = GameObj.Transform.Pos.Z;

                GameObj.Transform.MoveToLocal(targetLocation);
            }

            // Manual camera controls
            else if (ManualControl)
            {
                Vector3 cameraMovement = Vector3.Zero;
                Vector3 cameraPos = GameObj.Transform.Pos;

                VisualLogs.Default.DrawText(Vector2.Zero, string.Format("Manual camera control: \n\n X: {0:F2} \n Y: {1:F2} \n Z: {2:F2} \n\n Speed: {3}", cameraPos.X, cameraPos.Y, cameraPos.Z, ManualSpeed));

                // Use the numpad to control to camera
                if (DualityApp.Keyboard[ManualCameraKeybindings.Up])                      // Up
                    cameraMovement.Y -= 1;
                if (DualityApp.Keyboard[ManualCameraKeybindings.Left])                    // Left
                    cameraMovement.X -= 1;
                if (DualityApp.Keyboard[ManualCameraKeybindings.Down])                    // Down
                    cameraMovement.Y += 1;
                if (DualityApp.Keyboard[ManualCameraKeybindings.Right])                   // Right
                    cameraMovement.X += 1;
                if (DualityApp.Keyboard[ManualCameraKeybindings.ZoomIn])                  // Zoom In
                    cameraMovement.Z += 1;
                if (DualityApp.Keyboard[ManualCameraKeybindings.ZoomOut])                 // Zoom Out
                    cameraMovement.Z -= 1;
                if (DualityApp.Keyboard.KeyHit(ManualCameraKeybindings.IncreaseCamSpeed)) // Increase Speed
                    ManualSpeed += 1;
                if (DualityApp.Keyboard.KeyHit(ManualCameraKeybindings.DecreaseCamSpeed)) // Decrease Speed
                {
                    if (ManualSpeed > 1)
                        ManualSpeed -= 1;
                }

                GameObj.Transform.MoveBy(cameraMovement * Time.TimeMult * ManualSpeed);
            }
        }
    }
}
