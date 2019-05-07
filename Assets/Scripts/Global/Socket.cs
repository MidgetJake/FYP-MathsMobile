using System;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Global {
    [Serializable]
    public class DataPacket {
        public string type;
        public string[] values;
    }
    
    public static class Socket {
        private static WebSocket m_Client;
        private static Dictionary<string, Action<object[]>> m_Events = new Dictionary<string, Action<object[]>>();
        private static int m_RoomCode = 0;

        public static void On(string type, Action<object[]> callback) {
            if (m_Events.ContainsKey(type)) {
                m_Events.Remove(type);
            }
            m_Events.Add(type, callback);
        }
        
        public static void Connect() {
            try {
                m_Client = new WebSocket("ws://localhost:1874");
                m_Client.OnMessage += (sender, message) => { CheckMessage(message.Data); };
                m_Client.OnError += (sender, e) => { CheckMessage("{\"type\": \"error\", \"values\": []"); };
                m_Client.OnClose += (sender, e) => { CheckMessage("{\"type\": \"error\", \"values\": []"); };
                m_Client.OnOpen += (sender, message) => { };
                m_Client.Connect();
            } catch (Exception e) {
                Debug.LogError(e);
                CheckMessage("{\"type\": \"error\", \"values\": []");
            }
        }

        public static void CancelSearch() {
            m_Client.Send("{\"type\": \"cancel-room\", \"code\": " + m_RoomCode + "}");
        }

        public static int CreateRoom() {
            m_RoomCode = Random.Range(1000, 9999);
            m_Client.Send("{\"type\": \"create-room\", \"code\": " + m_RoomCode + "}");
            return m_RoomCode;
        }

        public static void SubmitAnswer(int answer) {
            m_Client.Send("{\"type\": \"submit-answer\", \"answer\": " + answer + "}");
        }

        public static void CheckRoom(int code) {
            m_Client.Send("{\"type\": \"join-room\", \"code\": " + code + "}");
        }

        private static void CheckMessage(string message) {
            DataPacket packet = JsonUtility.FromJson<DataPacket>(message);
            Debug.Log(packet.type);
            
            switch (packet.type) {
                case "start-room":
                    Debug.Log("Started Room");
                    if (m_Events.ContainsKey("start-room")) {
                        m_Events["start-room"](new object[] { m_RoomCode });
                    }
                    break;
                case "start-game":
                    Debug.Log("Started Game");
                    if (m_Events.ContainsKey("start-game")) {
                        m_Events["start-game"](new object[] { m_RoomCode });
                    }
                    break;
                case "error":
                    Debug.Log("Error");
                    if (m_Events.ContainsKey("error")) {
                        m_Events["error"](new object[] { "" });
                    }
                    break;
                case "get-question":
                    Debug.Log("Question");
                    if (m_Events.ContainsKey("get-question")) {
                        m_Events["get-question"](new object[] { packet.values[0], int.Parse(packet.values[1]), int.Parse(packet.values[2]), int.Parse(packet.values[3]) });
                    }
                    break;
                case "joined-room":
                    Debug.Log("Joined room");
                    if (m_Events.ContainsKey("joined-room")) {
                        m_Events["joined-room"](new object[] { "" });
                    }
                    break;
                case "failed-join":
                    Debug.Log("Failed to join");
                    if (m_Events.ContainsKey("failed-join")) {
                        m_Events["failed-join"](new object[] { "" });
                    }
                    break;
            }
        }
    }
}