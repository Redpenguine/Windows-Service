using Redpenguin.UiManager.UIWindows.Windows;
using UnityEngine.SceneManagement;

namespace Redpenguin.UiManager.Sample1.ExamplesWindows
{
    public class ExampleWindowLanguage : Window
    {
        private const string NextSceneName = "Second";

        public void OnErrorClick()
        {
            //_windowsService.OpenInHigherLayer<WindowError, float>(5);

        }

        public void NextScene()
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}