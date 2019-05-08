using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Global;
using UI;
using Navigation;

namespace Game {
    public class Controller : MonoBehaviour {
        public int maxHealth = 10;
        public int health;
        public int opponentHealth;
        
        public int[] options = new int[3];
        public string question;
        public string operation;
        public OptionChoice[] choices = new OptionChoice[3];
        public bool canAnswer;
        public Color buttonColour;
        public Color correctAnswerColour;
        public Color wrongAnswerColour;

        [SerializeField] private Text m_QuestionText;
        [SerializeField] private RectTransform m_HealthBar;
        [SerializeField] private RectTransform m_OpponentHealthBar;
        [SerializeField] private GameObject m_ReadyText;
        [SerializeField] private GameObject m_Content;
        [SerializeField] private Dialog m_Dialog;
        [SerializeField] private GameObject m_MainMenu;
        [SerializeField] private Menus m_Navigation;
        [SerializeField] private ParticleSystem m_DamagedSystem;
        [SerializeField] private ParticleSystem m_AttackSystem;
        
        private int m_Answer;
        private bool m_NewQuestion = false;
        private bool m_HasAnswered = false;
        private bool m_CorrectAnswer = false;
        private bool m_IncorrectAnswer = false;
        private bool m_QuestionAnswered = false;
        private bool m_QuestionUnanswered = false;
        private string m_LastQuestionType;
        private int m_AnsweredIndex = 0;
        private int m_ActualAnswer;
        private bool m_Started;
        private bool m_Win;
        private bool m_Lose;
        private float m_Offset = ((Screen.width) / 10f) + 6.5f;
        private float m_CurrEnemyOffset = 0;
        private float m_CurrOffset = 0;

        private void Start() {
            Setup();
        }

        private void Awake() {
            Setup();
        }

        private void Setup() {
            m_ReadyText.SetActive(true);
            m_Content.SetActive(false);
            health = maxHealth;
            opponentHealth = maxHealth;
            Socket.On("get-question", objects => {
                question = (string) objects[0];
                options = new int[] {(int) objects[1], (int) objects[2], (int) objects[3]};
                m_NewQuestion = true;
                m_LastQuestionType = (string) objects[4];
            });
            Socket.On("question-answered", objects => {
                m_QuestionAnswered = true;
                m_ActualAnswer = (int) objects[0];
            });
            Socket.On("question-unanswered", objects => {
                m_QuestionUnanswered = true;
                m_ActualAnswer = (int) objects[0];
            });
            Socket.On("correct-answer", objects => { m_CorrectAnswer = true; });
            Socket.On("incorrect-answer", objects => { m_IncorrectAnswer = true; });
            Socket.On("win-game", objects => { m_Win = true;});
            Socket.On("lose-game", objects => { m_Lose = true;});
        }

        private void Update() {
            if (m_NewQuestion) {
                NewQuestion(question, options);
                m_NewQuestion = false;
                if (!m_Started) {
                    m_ReadyText.SetActive(false);
                    m_Content.SetActive(true);
                }
            }

            if (m_QuestionAnswered || m_QuestionUnanswered) {
                Statistics.UpdateQuestion(m_LastQuestionType, false);
                foreach (OptionChoice option in choices) {
                    option.SetColour(option.answer == m_ActualAnswer ? correctAnswerColour : wrongAnswerColour);
                }

                if (m_QuestionAnswered) {
                    m_DamagedSystem.Play();
                    StartCoroutine(WaitForParticle(false));
                    
                }
                
                m_QuestionAnswered = false;
                m_QuestionUnanswered = false;
            }

            if (m_IncorrectAnswer) {
                m_IncorrectAnswer = false;
                foreach (OptionChoice option in choices) {
                    option.SetColour(wrongAnswerColour);
                }
            }

            if (m_CorrectAnswer) {
                m_CorrectAnswer = false;
                foreach (OptionChoice option in choices) {
                    option.SetColour(wrongAnswerColour);
                }
                
                choices[m_AnsweredIndex].SetColour(correctAnswerColour);
                m_AttackSystem.Play();
                Statistics.UpdateQuestion(m_LastQuestionType, true);
                StartCoroutine(WaitForParticle(true));
            }

            if (m_Win) {
                m_Win = false;
                Statistics.PlayGame(true);
                StartCoroutine(WaitForEnd(true));
            }
            
            if (m_Lose) {
                m_Lose = false;
                Statistics.PlayGame(false);
                StartCoroutine(WaitForEnd(false));
            }
        }

        private void CloseGame() {
            m_Navigation.OpenScene(m_MainMenu);
        }
        
        private void NewQuestion(string questionString, int[] answers) {
            foreach (OptionChoice option in choices) {
                option.SetColour(buttonColour);
            }
            
            Shuffle(options);
            choices[0].SetAnswer(options[0], 0);
            choices[1].SetAnswer(options[1], 1);
            choices[2].SetAnswer(options[2], 2);
            m_QuestionText.text = question;
            canAnswer = true;
        }
        
        public void CheckAnswer(int answer, int index) {
            if (!canAnswer) return;
            foreach (OptionChoice option in choices) {
                option.SetColour(wrongAnswerColour);
            }
            canAnswer = false;
            m_AnsweredIndex = index;
            Socket.SubmitAnswer(answer);
            m_HasAnswered = true;
        }
        
        private void Shuffle(int[] list) {
            for (int i = 0; i < list.Length; i++) {
                int tmp = list[i];
                int r = Random.Range(i, list.Length);
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        private IEnumerator WaitForEnd(bool win) {
            yield return new WaitForSecondsRealtime(4f);
            if (win) {
                m_Dialog.ShowDialog("You Win!", "BACK TO MENU", CloseGame);
            } else {
                m_Dialog.ShowDialog("You Lose!", "BACK TO MENU", CloseGame);
            }
        }
        
        private IEnumerator WaitForParticle(bool enemy) {
            yield return new WaitForSecondsRealtime(3f);
            if (enemy) {
                m_OpponentHealthBar.offsetMin = new Vector2(0, 0 - 25);
                opponentHealth--;
                m_CurrEnemyOffset -= m_Offset;
                m_OpponentHealthBar.offsetMax = new Vector3(m_CurrEnemyOffset, 25);
            } else {
                m_HealthBar.offsetMin = new Vector2(0, 0 - 25);
                health--;
                m_CurrOffset -= m_Offset;
                m_HealthBar.offsetMax = new Vector3(m_CurrOffset, 25);
            }
        }
    }
}
