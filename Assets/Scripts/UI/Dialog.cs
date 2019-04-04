using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Dialog : MonoBehaviour {
        [SerializeField] private Text m_DialogText;
        
        public void CloseDialog() {
            gameObject.SetActive(false);
        }

        public void ShowDialog(string text) {
            m_DialogText.text = text;
            gameObject.SetActive(true);
        }
    }
}
