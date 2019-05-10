using DataTypes;

namespace Question {
    public static class Question {
        public static Generate() {
            int operationChoice = Mathf.FloorToInt(Random.Range(0,4));
            int firstNumberChoice = Mathf.FloorToInt(Random.Range(-25,25));
            int secondNumberChoice = Mathf.FloorToInt(Random.Range(-25,25));
            string operation = '/';
            int m_Answer = 0;
            int[] options = new int[3];
            
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

            return new QuestionGroup(question, options);
        }
    }
}