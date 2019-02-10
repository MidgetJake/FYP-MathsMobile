using UnityEngine;
using UnityEngine.UI;

namespace Scenes {
    public class JoinGame : MonoBehaviour {
        public Text[] textInputs = new Text[4];
        public int[] inputValues = new int[4];

        private int m_Inputted = 0;

        private void Start() {
            foreach (Text text in textInputs) {
                text.text = "";
            }
        }

        public void AddNumber(int number) {
            if (m_Inputted >= 4) return;
            inputValues[m_Inputted] = number;
            textInputs[m_Inputted].text = number.ToString();
            m_Inputted++;
        }

        public void DeleteValue() {
            if (m_Inputted <= 0) return;
            m_Inputted--;
            textInputs[m_Inputted].text = "";
        }
    }
}
