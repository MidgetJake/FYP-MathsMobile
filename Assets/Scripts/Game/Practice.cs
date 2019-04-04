using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game {
    public class Practice : MonoBehaviour {
        public int maxHealth = 10;
        public int correctAnswers = 0;
        public int[] options = new int[3];
        public string question;
        public string operation;
        public PracticeChoice[] choices = new PracticeChoice[3];
        public bool canAnswer;
        public Color buttonColour;
        public Color correctAnswerColour;
        public Color wrongAnswerColour;

        [SerializeField] private Text m_QuestionText;
        [SerializeField] private Text m_CorrectAnswersText;
        
        private int m_Answer;

        private void Start() {
            NewQuestion();
        }
        
        private void NewQuestion() {
            foreach (PracticeChoice option in choices) {
                option.SetColour(buttonColour);
            }
            
            int operationChoice = Mathf.FloorToInt(Random.Range(0,4));
            int firstNumberChoice = Mathf.FloorToInt(Random.Range(-25,25));
            int secondNumberChoice = Mathf.FloorToInt(Random.Range(-25,25));
            
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
                    firstNumberChoice = Mathf.FloorToInt(Random.Range(2,12));
                    secondNumberChoice = Mathf.FloorToInt(Random.Range(2,12));
                    m_Answer = firstNumberChoice * secondNumberChoice;
                    break;
                default:
                    operation = "/";
                    firstNumberChoice = Mathf.FloorToInt(Random.Range(2, 150));
                    secondNumberChoice = Mathf.FloorToInt(Random.Range(2, 15));
                    while (firstNumberChoice % secondNumberChoice != 0) {
                        firstNumberChoice = Mathf.FloorToInt(Random.Range(2, 150));
                        secondNumberChoice = Mathf.FloorToInt(Random.Range(2, 15));
                    }

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
            canAnswer = true;
        }
        
        public bool CheckAnswer(int answer) {
            canAnswer = false;
            foreach (PracticeChoice option in choices) {
                option.SetColour(option.answer == m_Answer ? correctAnswerColour : wrongAnswerColour);
            }

            if (m_Answer == answer) {
                correctAnswers++;
                m_CorrectAnswersText.text = "Correct Answers: " + correctAnswers;
            }
            
            StartCoroutine(WaitForQuestion());
            
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

        private IEnumerator WaitForQuestion() {
            yield return new WaitForSecondsRealtime(2f);
            NewQuestion();
        }
    }
}
