using Global;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes {
    public class StatsScene : MonoBehaviour {
        // Start is called before the first frame update
        private ThreeHold m_GameStats;
        private ThreeHold m_AdditionStats;
        private ThreeHold m_SubtractionStats;
        private ThreeHold m_MultiplicationStats;
        private ThreeHold m_DivisionStats;

        [SerializeField] private Text m_WinText;
        [SerializeField] private Text m_LossText;
        [SerializeField] private Text m_AdditionText;
        [SerializeField] private Text m_AdditionWrongText;
        [SerializeField] private Text m_SubtractionText;
        [SerializeField] private Text m_SubtractionWrongText;
        [SerializeField] private Text m_MultiplicationText;
        [SerializeField] private Text m_MultiplicationWrongText;
        [SerializeField] private Text m_DivisionText;
        [SerializeField] private Text m_DivisionWrongText;
        
        void Start() {
            Setup();
        }

        private void Awake() {
            Setup();
        }

        private void Setup() {
            m_GameStats = Statistics.Games;
            m_AdditionStats = Statistics.AdditionQuestions;
            m_SubtractionStats = Statistics.SubtractionQuestions;
            m_MultiplicationStats = Statistics.MultiplicationQuestions;
            m_DivisionStats = Statistics.DivisionQuestions;

            m_WinText.text = m_GameStats.Positive.ToString();
            m_LossText.text = m_GameStats.Negative.ToString();
            m_AdditionText.text = m_AdditionStats.Positive.ToString();
            m_AdditionWrongText.text = m_AdditionStats.Negative.ToString();
            m_SubtractionText.text = m_SubtractionStats.Positive.ToString();
            m_SubtractionWrongText.text = m_SubtractionStats.Negative.ToString();
            m_MultiplicationText.text = m_MultiplicationStats.Positive.ToString();
            m_MultiplicationWrongText.text = m_MultiplicationStats.Negative.ToString();
            m_DivisionText.text = m_DivisionStats.Positive.ToString();
            m_DivisionWrongText.text = m_DivisionStats.Negative.ToString();
        }
    }
}
