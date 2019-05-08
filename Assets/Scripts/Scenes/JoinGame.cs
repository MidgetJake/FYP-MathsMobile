using System;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Socket = Global.Socket;

namespace Scenes {
    public class JoinGame : MonoBehaviour {
        public Text[] textInputs = new Text[4];
        public int[] inputValues = new int[4];

        private int m_Inputted = 0;
        private bool m_AttemptDone = false;
        private bool m_Failed = false;

        [SerializeField] private GameObject m_GameScreen;
        [SerializeField] private Dialog m_Dialog;

        private void Start() {
            foreach (Text text in textInputs) {
                text.text = "";
            }
        }

        private void Awake() {
            foreach (Text text in textInputs) {
                text.text = "";
            }

            m_Inputted = 0;
            inputValues = new int[4];
            m_AttemptDone = false;
            m_Failed = false;
        }

        private void Update() {
            if (m_AttemptDone) {
                m_AttemptDone = false;
                if (m_Failed) {
                    m_Dialog.ShowDialog("Room does not exist, please try a different code", "OK", DeleteValue);
                } else {
                    m_GameScreen.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }

        public void AddNumber(int number) {
            if (m_Inputted < 4) {
                inputValues[m_Inputted] = number;
                textInputs[m_Inputted].text = number.ToString();
                m_Inputted++;
            }

            if (m_Inputted >= 4) {
                try {
                    Socket.Connect();
                    string checker = inputValues[0] + "" + inputValues[1] + inputValues[2] + inputValues[3];
                    Socket.On("failed-join", objects => {
                        m_AttemptDone = true;
                        m_Failed = true;
                    });
                    Socket.On("joined-room", objects => {
                        m_AttemptDone = true;
                        m_Failed = false;
                    });
                    Socket.CheckRoom(int.Parse(checker));
                } catch (Exception e) {
                    FailedToCreate();
                }
            };
        }
        
        public void DeleteValue() {
            if (m_Inputted <= 0) return;
            m_Inputted--;
            textInputs[m_Inputted].text = "";
        }
        
        private void FailedToCreate() {
            m_Dialog.ShowDialog("Failed to connect, check your internet connection and try again", "OK", DeleteValue);
        }
    }
}
