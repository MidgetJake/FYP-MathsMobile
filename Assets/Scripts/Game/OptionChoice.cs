﻿using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class OptionChoice : MonoBehaviour {
        public int answer;

        [SerializeField] private Controller m_GameScreen;
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_ButtonImage;

        public void CheckAnswer() {
            if (!m_GameScreen.canAnswer) return;
            if (m_GameScreen.CheckAnswer(answer)) {
                SetColour(m_GameScreen.correctAnswerColour);
            } else {
                SetColour(m_GameScreen.wrongAnswerColour);
            }
        }

        public void SetAnswer(int newAnswer) {
            m_Text.text = newAnswer.ToString();
            answer = newAnswer;
        }

        public void SetColour(Color colour) {
            print("test colouring");
            m_ButtonImage.color = colour;
        }
    }
}