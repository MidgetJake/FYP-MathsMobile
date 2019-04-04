using UI;
using UnityEngine;

namespace Scenes {
    public class CreateLobby : MonoBehaviour {
        [SerializeField] private Dialog m_Dialog;
        
        public void FailedToCreate() {
            m_Dialog.ShowDialog("Failed to create a room, check your internet connection and try again");
        }
    }
}
