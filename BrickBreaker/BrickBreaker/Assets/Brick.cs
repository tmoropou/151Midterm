using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;

public class Brick : MonoBehaviour
{

    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    // Start is called before the first frame update
    void Start()
    {

        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/oscplayseq", 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;

        foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            if (item.Value.log.Count > 0)
            {
                int lastPacketIndex = item.Value.packets.Count - 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playbrick", 1);
        Destroy(gameObject);
        FindObjectOfType<BrickManager>().RemoveBrick(this);
    }

}
