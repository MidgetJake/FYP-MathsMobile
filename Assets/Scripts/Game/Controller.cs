using UnityEngine;

namespace Game {
    public class Controller : MonoBehaviour {
        public int[] options = new int[3];
        public string question;
        
        private int m_Answer;

        public void NewQuestion() {
            m_Answer = 5;
        }
        
        public bool CheckAnswer(int answer) {
            return m_Answer == answer;
        }
    }
}
