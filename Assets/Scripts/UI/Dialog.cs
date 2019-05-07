using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Dialog : MonoBehaviour {
        [SerializeField] private Text m_DialogText;
        [SerializeField] private Text m_ButtonText;
        private Action m_OnCloseAction;
        
        public void CloseDialog() {
            print("closing");
            gameObject.SetActive(false);
            m_OnCloseAction?.Invoke();
            m_OnCloseAction = null;
        }

        public void ShowDialog(string text, string buttonText = "OK", Action onClose = null) {
            print("opening");
            gameObject.SetActive(true);
            m_DialogText.text = text;
            m_ButtonText.text = buttonText;
            if (onClose != null) {
                m_OnCloseAction = onClose;
            }
        }
    }
}
