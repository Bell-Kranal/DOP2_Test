using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly LoadingSlider _loadingSlider;
        private readonly LoadingScreen _loadingScreen;

        public Func<bool> DataIsLoaded;

        public SceneLoader(LoadingSlider loadingSlider, LoadingScreen loadingScreen)
        {
            _loadingSlider = loadingSlider;
            _loadingScreen = loadingScreen;
        }

        public async Task  Load(string name, Action onLoaded = null) =>
            await LoadScene(name, onLoaded);

        private async Task LoadScene(string sceneName, Action action = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(sceneName))
            {
                action?.Invoke();
                return;
            }
      
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!loadOperation.isDone && !DataIsLoaded.Invoke())
            {
                _loadingSlider.SetFillAmount(loadOperation.progress);
                await Task.Yield();
            }
            
            _loadingSlider.SetFillAmount(1f);
            _loadingScreen.Hide();
            loadOperation.allowSceneActivation = true;
            action?.Invoke();
        }
    }
}