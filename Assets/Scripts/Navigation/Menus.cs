using UnityEngine;

namespace Navigation {
    public class Menus : MonoBehaviour {
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private GameObject m_SettingsMenu;
        [SerializeField] private GameObject m_PlayMenu;
        [SerializeField] private GameObject m_JoinGame;

        public void ReturnToMainMenu() {
            m_SettingsMenu.SetActive(false);
            m_PlayMenu.SetActive(false);
            m_JoinGame.SetActive(false);
            m_MainMenu.SetActive(true);
        }

        public void OpenSettingsMenu() {
            m_PlayMenu.SetActive(false);
            m_MainMenu.SetActive(false);
            m_SettingsMenu.SetActive(true);
        }

        public void OpenPlayMenu() {
            m_MainMenu.SetActive(false);
            m_SettingsMenu.SetActive(false);
            m_JoinGame.SetActive(false);
            m_PlayMenu.SetActive(true);
        }

        public void OpenJoinGame() {
            m_PlayMenu.SetActive(false);
            m_JoinGame.SetActive(true);
        }
    }
}
