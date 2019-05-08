using Global;
using UnityEngine;

namespace Navigation {
    public class Menus : MonoBehaviour {
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private GameObject m_SettingsMenu;
        [SerializeField] private GameObject m_PlayMenu;
        [SerializeField] private GameObject m_JoinGame;

        private void Start() {
            Statistics.Init();
        }

        public void OpenScene(GameObject wantedScene) {
            foreach (Transform scene in transform) {
                scene.gameObject.SetActive(false);
            }
            
            wantedScene.SetActive(true);
        }
    }
}
