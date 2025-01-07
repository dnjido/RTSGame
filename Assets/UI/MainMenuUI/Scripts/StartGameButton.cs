using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RTS
{
    public class StartGameButton : MonoBehaviour
    {
        private StartGameProperties startGameProperties;

        private ZenjectSceneLoader sceneLoader;

        [Inject]
        public void Init(StartGameProperties sg) =>
            startGameProperties = sg;

        [Inject]
        public void Loader(ZenjectSceneLoader loader) =>
            sceneLoader = loader;

        public void StartGame()
        {
            sceneLoader.LoadScene(startGameProperties.map.scene, LoadSceneMode.Single, (container) =>
                container.BindInstance(startGameProperties).WhenInjectedInto<StartGameInstaller>());
        }
    }
}
