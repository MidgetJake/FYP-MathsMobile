using UnityEngine;

namespace Global {
    public static class Statistics {
        public static bool IsReady;
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
                PlayerPrefs.SetInt("Wins", m_GamesPlayed.Positive);
            } else {
                m_GamesPlayed.Negative++;
                PlayerPrefs.SetInt("Losses", m_GamesPlayed.Negative);
            }
        }

        public static void UpdateQuestion(string type, bool correct) {
            switch (type) {
                case "+":
                    if (correct) {
                        m_Addition.Positive++;
                    } else {
                        m_Addition.Negative++;
                    }
                    break;
                case "-":
                    if (correct) {
                        m_Subtraction.Positive++;
                    } else {
                        m_Subtraction.Negative++;
                    }
                    break;
                case "x":
                    if (correct) {
                        m_Multiplication.Positive++;
                    } else {
                        m_Multiplication.Negative++;
                    }
                    break;
                case "/":
                default:
                    if (correct) {
                        m_Division.Positive++;
                    } else {
                        m_Division.Negative++;
                    }
                    break;
            }
            
            SetAll();
        }

        public static void ResetStats() {
            m_GamesPlayed.Positive = 0;
            m_GamesPlayed.Negative = 0;
            m_Addition.Positive = 0;
            m_Addition.Negative = 0;
            m_Subtraction.Positive = 0;
            m_Subtraction.Negative = 0;
            m_Multiplication.Positive = 0;
            m_Multiplication.Negative = 0;
            m_Division.Positive = 0;
            m_Division.Negative = 0;
            SetAll();
        }
        
        private static void SetAll() {
            PlayerPrefs.SetInt("Wins", m_GamesPlayed.Positive);
            PlayerPrefs.SetInt("Losses", m_GamesPlayed.Negative);
            PlayerPrefs.SetInt("AdditionCorrect", m_Addition.Positive);
            PlayerPrefs.SetInt("AdditionIncorrect", m_Addition.Negative);
            PlayerPrefs.SetInt("SubtractionCorrect", m_Subtraction.Positive);
            PlayerPrefs.SetInt("SubtractionIncorrect", m_Subtraction.Negative);
            PlayerPrefs.SetInt("MultiplicationCorrect", m_Multiplication.Positive);
            PlayerPrefs.SetInt("MultiplicationIncorrect", m_Multiplication.Negative);
            PlayerPrefs.SetInt("DivisionCorrect", m_Division.Positive);
            PlayerPrefs.SetInt("DivisionIncorrect", m_Division.Negative);
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

            if (PlayerPrefs.HasKey("AdditionCorrect")) {
                m_Addition.Positive = PlayerPrefs.GetInt("AdditionCorrect");
                m_Addition.Negative = PlayerPrefs.GetInt("AdditionIncorrect");
            } else {
                m_Addition.Positive = 0;
                m_Addition.Negative = 0;
                PlayerPrefs.SetInt("AdditionCorrect", m_Addition.Positive);
                PlayerPrefs.SetInt("AdditionIncorrect", m_Addition.Negative);
            }
            
            if (PlayerPrefs.HasKey("SubtractionCorrect")) {
                m_Subtraction.Positive = PlayerPrefs.GetInt("SubtractionCorrect");
                m_Subtraction.Negative = PlayerPrefs.GetInt("SubtractionIncorrect");
            } else {
                m_Subtraction.Positive = 0;
                m_Subtraction.Negative = 0;
                PlayerPrefs.SetInt("SubtractionCorrect", m_Subtraction.Positive);
                PlayerPrefs.SetInt("SubtractionIncorrect", m_Subtraction.Negative);
            }
            
            if (PlayerPrefs.HasKey("MultiplicationCorrect")) {
                m_Multiplication.Positive = PlayerPrefs.GetInt("MultiplicationCorrect");
                m_Multiplication.Negative = PlayerPrefs.GetInt("MultiplicationIncorrect");
            } else {
                m_Multiplication.Positive = 0;
                m_Multiplication.Negative = 0;
                PlayerPrefs.SetInt("MultiplicationCorrect", m_Multiplication.Positive);
                PlayerPrefs.SetInt("MultiplicationIncorrect", m_Multiplication.Negative);
            }
            
            if (PlayerPrefs.HasKey("DivisionCorrect")) {
                m_Division.Positive = PlayerPrefs.GetInt("DivisionCorrect");
                m_Division.Negative = PlayerPrefs.GetInt("DivisionIncorrect");
            } else {
                m_Division.Positive = 0;
                m_Division.Negative = 0;
                PlayerPrefs.SetInt("DivisionCorrect", m_Division.Positive);
                PlayerPrefs.SetInt("DivisionIncorrect", m_Division.Negative);
            }

            IsReady = true;
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
