using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game {
    public class Controller : MonoBehaviour {
        public int[] options = new int[3];
        public string question;
        public string operation;
        public OptionChoice[] choices = new OptionChoice[3];

        [SerializeField] private Text m_QuestionText;
        
        private int m_Answer;

        private void Start() {
            NewQuestion();
        }
        
        public void NewQuestion() {
            int operationChoice = Mathf.FloorToInt(Random.Range(0,4));
            int firstNumberChoice = Mathf.FloorToInt(Random.Range(0,25));
            int secondNumberChoice = Mathf.FloorToInt(Random.Range(0,25));
            
            switch(operationChoice) {
                case 0:
                    operation = "+";
                    m_Answer = firstNumberChoice + secondNumberChoice;
                    break;
                case 1:
                    operation = "-";
                    m_Answer = firstNumberChoice - secondNumberChoice;
                    break;
                case 2:
                    operation = "x";
                    m_Answer = firstNumberChoice * secondNumberChoice;
                    break;
                default:
                    operation = "/";
                    m_Answer = firstNumberChoice / secondNumberChoice;
                    break;
            }

            question = firstNumberChoice + operation + secondNumberChoice;
            options[0] = m_Answer;
            
            int otherOption = m_Answer + Mathf.FloorToInt(Random.Range(-10, 10));
            if (otherOption == m_Answer) otherOption++;
            options[1] = otherOption;
            
            otherOption = m_Answer + Mathf.FloorToInt(Random.Range(-10, 10));
            if (otherOption == m_Answer) otherOption--;
            options[2] = otherOption;
            
            Shuffle(options);
            choices[0].SetAnswer(options[0]);
            choices[1].SetAnswer(options[1]);
            choices[2].SetAnswer(options[2]);
            m_QuestionText.text = question;
        }
        
        public bool CheckAnswer(int answer) {
            foreach (OptionChoice option in choices) {
                option.SetColour(Color.red);
            }
            return m_Answer == answer;
        }
        
        private void Shuffle(int[] list) {
            for (int i = 0; i < list.Length; i++) {
                int tmp = list[i];
                int r = Random.Range(i, list.Length);
                list[i] = list[r];
                list[r] = tmp;
            }
        }
    }
}
