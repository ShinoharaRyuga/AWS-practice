using System.Text;
using UnityEngine;

public class AWSImplement : MonoBehaviour
{
    const string URI = "https://gvebfpgkk8.execute-api.ap-northeast-1.amazonaws.com/default/TestFunction";

    class AWSLambdaAPIResult
    {
       // public int statusCode;
        public string body;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetDataRequest("test", (data) => Debug.Log(data.ToString())); 
        }
    }

    byte[] GetPacketBody(byte[] data)
    {
        string json = Encoding.UTF8.GetString(data);
        Debug.Log($"json{json}");
        AWSLambdaAPIResult result = JsonUtility.FromJson<AWSLambdaAPIResult>(json);
        return Encoding.UTF8.GetBytes(result.body);
    }

    public void GetDataRequest(string uuid, INetworkImplement.APICallback callback)
    {
        Network.WebRequest.GetRequest(URI, (byte[] data) => {
            callback?.Invoke(GetPacketBody(data));
        });
    }
}
