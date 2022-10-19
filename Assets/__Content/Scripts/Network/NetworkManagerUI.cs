using Unity.Netcode;
using UnityEngine;

namespace __Content.Scripts.Network
{
    public class NetworkManagerUI : MonoBehaviour
    {
        public void OnHostButtonClicked()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        public void OnClientButtonClicked()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
