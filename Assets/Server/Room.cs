using System;
using System.Net;
using System.Net.Sockets;
using Question;

namespace DataTypes {
    public class QuestionGroup {
        public string questionString;
        public int[] answers = new int[3];

        public QuestionGroup(string qString, int[] posAnswers) {
            this.questionString = qString;
            this.answers = posAnswers;
        }
    }
}

public class Room {
  int roomCode;
  // Only 2 clients should be connected at once
  Socket[] clients = new Socket[2];
  string question = String.Empty;
  int[] answers = new int[3];

  public Room(int code) {
    this.roomCode = code;
  }

  // Send a message to all clients connected to the room
  public void BroadcastMessage(string message) {
    foreach(Socket client in clients) {
      SendMessage(client, message);
    }
  }

  public void JoinRoom(Socket client) {
    // Only broadcast if there is already a user in the room
    if(clients[0]) {
      BroadcastMessage("User has joined the room")
      clients[1] = client;
    } else {
      clients[0] = client;
    }
  }

  public void GenerateQuestion() {
    QuestionGroup newQuestion = Question.Generate();
    question = newQuestion.questionString;
    answers = newQuestion.answers;

    // Reset client answering state
    clientHasAnswered = {false, false};

    // Converting the question into a string so it can be broken down by the
    // client. Allowing it to be read easily.
    BroadcastMessage("New Question:Q" + question + ":A" + answers.Join(','))
  }

  // Set the starting values of the clients health points
  int[] clientHealth = {10, 10};
  // Which clients have submitted an answer
  bool[] clientHasAnswered = {false, false};
  bool questionAnswered = false;

  public void CheckAnswer(Socket client, int Answer) {
    // answers[0] will always be the correct answer. The order is shuffled on
    // the clients side.
    if(!clientHasAnswered[client.index] && Answer == answers[0]) {
      clientHealth[Math.abs(client.index - 1)]--;
      // broadcast who got the correct answers
      // C = Correct client
      // D = Damaged client
      BroadcastMessage("Question Answered:C" +
        client.index + ":D" +
        Math.abs(client.index - 1));
    } else {
      clientHasAnswered[client.index] = true;
      if(clientHasAnswered[0] && clientHasAnswered[1]) {
        // Both clients answered wrong so generate a new question
        BroadcastMessage("Both incorrect");
        GenerateQuestion();
      }
    }
  }

  public void MessageReceived(string message) {
    // Handle questions here
  }

  private static void SendMessage(Socket handler, String data) {
    // Convert string to byte data that can be sent via the socket
    byte[] byteData = Encoding.ASCII.GetBytes(data);

    // Send data to client
    handler.BeginSend(byteData, 0, byteData.Length, 0,
      new AsyncCallback(MessageSent), handler);
  }

  private static void MessageSent(IAsyncResult ar) {
    try {
      Socket handler = (Socket) ar.AsyncState;
      // Finish sending the data to client
      int bytesSent = handler.EndSend(ar);
    } catch (Exception e) {
      Console.WriteLine(e.ToString());
    }
  }
}