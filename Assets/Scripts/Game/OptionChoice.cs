using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class OptionChoice : MonoBehaviour {
        private int m_Answer;

        [SerializeField] private Controller m_GameScreen;
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_ButtonImage;

        public void CheckAnswer() {
            if (m_GameScreen.CheckAnswer(m_Answer)) {
                SetColour(Color.green);
            } else {
                SetColour(Color.red);
            }
        }

        public void SetAnswer(int answer) {
            m_Text.text = answer.ToString();
            m_Answer = answer;
        }

        public void SetColour(Color colour) {
            m_ButtonImage.color = colour;
        }
    }
}