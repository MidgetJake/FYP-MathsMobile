using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class PracticeChoice : MonoBehaviour {
        public int answer;

        [SerializeField] private Practice m_GameScreen;
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_ButtonImage;

        public void CheckAnswer() {
            if (!m_GameScreen.canAnswer) return;
            SetColour(m_GameScreen.CheckAnswer(answer)
                ? m_GameScreen.correctAnswerColour
                : m_GameScreen.wrongAnswerColour);
        }

        public void SetAnswer(int newAnswer) {
            m_Text.text = newAnswer.ToString();
            answer = newAnswer;
        }

        public void SetColour(Color colour) {
            m_ButtonImage.color = colour;
        }
    }
}