/*
 * DualityTools for Duality 3
 * (C) 2019 Gregory Karastergios
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
using System.Linq;
using System.Text.RegularExpressions;

using Duality;
using Duality.IO;
using Duality.Input;
using Duality.Resources;
using Duality.Drawing;

namespace Gregnk.Plugins.DualityTools
{
    /// <summary>
    /// A basic scene selector that can be used for debugging
    /// </summary>
    public class SceneSelect : Component, ICmpUpdatable, ICmpRenderer
    {
        private int listAmount = 16;        
        private string path = "Data\\";     
        private List<string> scenePaths;
        private int selectedSceneIndex = 0; 
        private int selectedPos = 0;
        private int listOffset = 0;

        /// <summary>
        /// The amount of scenes to be displayed at once
        /// </summary>
        public int ListAmount { get => listAmount; set => listAmount = value; }

        /// <summary>
        /// The path where the scenes should be loaded from, relative to the game's launcher
        /// </summary>
        public string Path { get => path; set => path = value; }

        public float BoundRadius => DualityApp.WindowSize.X;

        public void Draw(IDrawDevice device)
        {
            // Only render while running
            if (scenePaths == null) return;

            Canvas textRenderer = new Canvas();
            textRenderer.Begin(device);

            // The text to be rendered
            FormattedText outText = new FormattedText();

            outText.SourceText += "/nSCENE SELECT/n";
            outText.SourceText += "=============/n";

            // Display the scenes
            for (int count = 0; count <= ListAmount - 1; count++)
            {
                int renderIndex = count + listOffset;

                if (renderIndex >= scenePaths.Count)
                    outText.SourceText += "/n";

                else
                {
                    // Highlight selected scene
                    if (renderIndex == selectedSceneIndex)
                        outText.SourceText += "/n  >";
                    else
                        outText.SourceText += "/n   ";


                    outText.SourceText += scenePaths[renderIndex];
                }
            }


            outText.SourceText += "/n/n=============/n/n";

            outText.SourceText += "UP//DOWN: Scroll scenes/n";
            outText.SourceText += "ENTER:   Load/n";

            // Set font
            textRenderer.State.TextFont = Font.GenericMonospace10;

            // Render text
            textRenderer.DrawText(outText, 0, 0);
        }

        public void GetCullingInfo(out CullingInfo info)
        {
            info.Position = GameObj.Transform.Pos;
            info.Radius = float.MaxValue;
            info.Visibility = VisibilityFlag.Group0 | VisibilityFlag.ScreenOverlay;
        }

        public void OnUpdate()
        {
            // Get all of the scene files in the path recursively
            // Updates in real-time
            List<string> dirContents = DirectoryOp.GetFiles(Path, true).ToList();
            scenePaths = new List<string>();

            foreach (string path in dirContents)
            {
                // Use regex to filter out non-scene files
                Match match = Regex.Match(path, @"\.Scene\.res$");

                if (match.Success)
                    scenePaths.Add(path);
            }

            // Scroll down
            if (DualityApp.Keyboard.KeyHit(Key.Down))
            {
                if (selectedSceneIndex + 1 < scenePaths.Count)
                {
                    selectedSceneIndex++;

                    if (selectedPos + 1 < ListAmount)
                        selectedPos++;
                    else
                        listOffset++;
                }
            }

            // Scroll up
            else if (DualityApp.Keyboard.KeyHit(Key.Up))
            {
                if (selectedSceneIndex - 1 > -1)
                {
                    selectedSceneIndex--;

                    if (selectedPos - 1 > -1)
                        selectedPos--;
                    else
                        listOffset--;
                }
            }

            // Load scene
            else if (DualityApp.Keyboard.KeyHit(Key.Enter))
            {
                Scene.Current.DisposeLater();
                Scene.SwitchTo(ContentProvider.RequestContent<Scene>(scenePaths[selectedSceneIndex]));
            }
        }
    }
}
