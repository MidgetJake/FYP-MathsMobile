using UI;
using UnityEngine;
using Global;

namespace Scenes {
    public class CreateLobby : MonoBehaviour {
        [SerializeField] private Dialog m_Dialog;

        public void CreateRoom() {
            try {
                Socket.Connect();
                Socket.On("start-room", objects => { Debug.Log(objects[0]); });
                Socket.On("error", objects => { FailedToCreate(); });
                int roomCode = Socket.CreateRoom();
                WaitingForPlayer(roomCode);
            } catch {
                FailedToCreate();
            }
        }

        private void CancelSearch() {
            Socket.CancelSearch();
        }
        
        private void WaitingForPlayer(int roomcode) {
            m_Dialog.ShowDialog("Room code: " + roomcode + "\n\nWaiting for another player. Press the button below to cancel", "CANCEL", CancelSearch);
        }
        
        private void FailedToCreate() {
            m_Dialog.ShowDialog("Failed to create a room, check your internet connection and try again");
        }
    }
}
