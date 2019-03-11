using UnityEngine;
using UnityEngine.WSA;

namespace Global {
    public static class Statistics {
        private static bool m_IsReady;
        private static ThreeHold m_GamesPlayed; // Wins/Losses/GamesPlayed
        private static ThreeHold m_Addition; // Correct/Wrong/Total
        private static ThreeHold m_Subtraction; // Correct/Wrong/Total
        private static ThreeHold m_Multiplication; // Correct/Wrong/Total
        private static ThreeHold m_Division; // Correct/Wrong/Total
   
        // Publicly visible variables
        // These only allow the values to be retrieved, they cannot be modified from this reference.
        public static ThreeHold Games => m_GamesPlayed;
        public static ThreeHold AdditionQuestions => m_Addition;
        public static ThreeHold SubtractionQuestions => m_Subtraction;
        public static ThreeHold MultiplicationQuestions => m_Multiplication;
        public static ThreeHold DivisionQuestions => m_Division;

        public static void PlayGame(bool win) {
            if (win) {
                m_GamesPlayed.Positive++;
            } else {
                m_GamesPlayed.Negative++;
            }
        }

        public static void UpdateQuestion(string type, bool correct) {
            ThreeHold toUpdate;
            switch (type) {
                case "+":
                    toUpdate = m_Addition;
                    break;
                case "-":
                    toUpdate = m_Subtraction;
                    break;
                case "x":
                    toUpdate = m_Multiplication;
                    break;
                case "/":
                default:
                    toUpdate = m_Division;
                    break;
            }

            if (correct) {
                toUpdate.Positive++;
            } else {
                toUpdate.Negative++;
            }
        }

        public static void Init() {
            if (PlayerPrefs.HasKey("Wins")) {
                m_GamesPlayed.Positive = PlayerPrefs.GetInt("Wins");
                m_GamesPlayed.Negative = PlayerPrefs.GetInt("Losses");
            } else {
                m_GamesPlayed.Positive = 0;
                m_GamesPlayed.Negative = 0;
                PlayerPrefs.SetInt("Wins", m_GamesPlayed.Positive);
                PlayerPrefs.SetInt("Losses", m_GamesPlayed.Negative);
            }

            m_IsReady = true;

        }
    }

    // This holds 2 values but can give 3
    // An example of a use is Wins/Losses/Games played
    // Requiring only Wins/Losses to be stored it can easily calculate the number played
    public struct ThreeHold {
        public int Positive;
        public int Negative;
        public int Total  =>  Positive + Negative;
    }
}
