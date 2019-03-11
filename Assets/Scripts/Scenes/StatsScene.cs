using Global;
using UnityEngine;

namespace Scenes {
    public class StatsScene : MonoBehaviour {
        // Start is called before the first frame update
        private ThreeHold m_GameStats;
        private ThreeHold m_AdditionStats;
        private ThreeHold m_SubtractionStats;
        private ThreeHold m_MultiplicationStats;
        private ThreeHold m_DivisionStats;
        
        void Start() {
            m_GameStats = Statistics.Games;
            m_AdditionStats = Statistics.AdditionQuestions;
            m_SubtractionStats = Statistics.SubtractionQuestions;
            m_MultiplicationStats = Statistics.MultiplicationQuestions;
            m_DivisionStats = Statistics.DivisionQuestions;
        }
    }
}
