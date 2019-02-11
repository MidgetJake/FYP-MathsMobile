using UnityEngine;

namespace Game {
    public class OptionChoice : MonoBehaviour {
        public int answer;

        [SerializeField] private Controller m_GameScreen;

        public void CheckAnswer() {
            m_GameScreen.CheckAnswer(answer);
        }
    }
}