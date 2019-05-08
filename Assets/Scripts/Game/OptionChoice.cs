using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class OptionChoice : MonoBehaviour {
        public int answer;

        [SerializeField] private Controller m_GameScreen;
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_ButtonImage;

        private int choiceIndex = -1;

        public void CheckAnswer() {
            if (!m_GameScreen.canAnswer) return;
            m_GameScreen.CheckAnswer(answer, choiceIndex);
        }

        public void SetAnswer(int newAnswer, int index) {
            m_Text.text = newAnswer.ToString();
            answer = newAnswer;
            choiceIndex = index;
        }

        public void SetColour(Color colour) {
            m_ButtonImage.color = colour;
        }
    }
}