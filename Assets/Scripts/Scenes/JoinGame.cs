using System;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes {
    public class JoinGame : MonoBehaviour {
        public Text[] textInputs = new Text[4];
        public int[] inputValues = new int[4];
        public int[] roomCode = new int[] {1, 3, 5, 4};

        private int m_Inputted = 0;

        [SerializeField] private GameObject m_GameScreen;
        [SerializeField] private Dialog m_Dialog;

        private void Start() {
            foreach (Text text in textInputs) {
                text.text = "";
            }
        }

        public void AddNumber(int number) {
            bool hasCorrectCode = true;
            if (m_Inputted >= 4) {
                if (inputValues.Where((t, i) => t != roomCode[i]).Any()) {
                    hasCorrectCode = false;
                }

                if (!hasCorrectCode) return;
                m_GameScreen.SetActive(true);
                gameObject.SetActive(false);
                return;
            };
            
            inputValues[m_Inputted] = number;
            textInputs[m_Inputted].text = number.ToString();
            m_Inputted++;
            
            if (m_Inputted >= 4) {
                m_Dialog.ShowDialog("Room does not exist, please try a different code");
                return;
                if (inputValues.Where((t, i) => t != roomCode[i]).Any()) {
                    hasCorrectCode = false;
                }

                if (!hasCorrectCode) return;
                m_GameScreen.SetActive(true);
                gameObject.SetActive(false);
                return;
            };
        }

        public void DeleteValue() {
            if (m_Inputted <= 0) return;
            m_Inputted--;
            textInputs[m_Inputted].text = "";
        }
    }
}
