using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collection.Generic;
using Room;

// Store the state to read client data on threads
public class StateStore {
  // The clients socket object
  public Socket clientSocket = null;
  public const int BufferSize = 1024;
  public byte[] buffer = new byte[BufferSize];
  // String builder to convert bytes to a readbale string
  public StringBuilder sb = new StringBuilder();
}

// class for the socket server
public class SocketServer {
  public static ArrayList<Socket> clientList = new ArrayList<Socket>();
  public static ManualResetEvent messageDone = new ManualResetEvent(false);

  public static void Startup() {
    // Get system network info for use with the socket
    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
    IPAddress ipAddress = ipHostInfo.AddressList[0];
    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

    Socket server = new Socket(ipAddress.AddressFamily, SocketType.Stream,
      ProtocolType.Tcp);

    // Attempt to bind the socket to the network
    try {
      server.Bind(localEndPoint);
      server.Listen(1874); // Listen on port 1874

      while(true) {
        messageDone.Reset();
        listener.BeginAccept(new RecieveMessage(AcceptMessage), server);
        messageDone.WaitOne(); // Wait for the connection before continuing
      }
    } catch (Exception e) {
      Console.WriteLine("Error whilst binding server to network: " +
        e.ToString());
    }
  }

  public static void AcceptMessage(IAsyncResult ar) {
    messageDone.Set();

    // Get the socket that is handling this message
    Socket listener = (Socket) ar.AsyncState;
    Socket handler = listener.EndAccept(ar);

    // Create the state object for handling the request
    StateStore state = new StateStore();
    state.clientSocket = handler;
    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
      new AsyncCallback(ReadMessage), state);
  }

  public static void ReadMessage(IAsyncResult ar) {
    String content = String.Empty;

    // Retrieve the state object and client socket
    StateStore state = (StateStore) ar.AsyncState;
    Socket handler = state.clientSocket;

    // Read data from the client
    int bytesRead = handler.EndReceive(ar);
    if (bytesRead > 0) {
      // Store data in case there is extra data to read
      state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

      // Look for the end of file tag
      content = state.sb.ToString();
      if(conetnt.IndexOf("<EOF>") > -1) {
        // All data from the client has been read
        foreach(Socket client in clientList) {
          // Let all clients now a new client has connected
          SendMessage(client, "A new client has connected");
        }
        clientList.Add(handler); // Add the new client to the list
        if(client.message.data.type == 'get-room') {
            JoinRoom(client, client.message.data.roomID);
        } else if(client.message.data.type == 'new-room') {
            int code = CreateRoom()
            JoinRoom(client, code);
        }
      } else {
        // Get more of the message data
        handler.BeginReceive(state.buffer, 0, StateStore.BufferSize,
          new AsyncCallback(ReadMessage, state));
      }
    }
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

  // Start the server
  public state int Main(String[] args) {
    Startup();
    return 0;
  }


  public static ArrayList<Room> roomList = new ArrayList<Room>();

  private static int CreateRoom() {
    bool roomCreated = false;
    while(!roomCreated) {
      Random random = new Random();
      int roomCode = random.Next(1000, 9999);
      if(CheckRoomExists(roomCode) == -1) {
        roomList.Add(new Room(roomCode));
        return roomCode;
      }
    }
  }

  private static int CheckRoomExists(int roomCode) {
    int count = 0;
    foreach(Room room in roomList) {
      if(room.roomCode == roomCode) {
        return count;
      }
      count++:
    }

    return -1;
  }

  private static void JoinRoom(Socket client, int roomCode) {
    int roomIndex = CheckRoomExists(roomCode);
    if(roomIndex > -1) {
        roomList[roomIndex].JoinRoom(client);
        SendMessage(client, "Joined Room");
    } else {
      SendMessage(client, "Room does not exist");
    }
  }
}