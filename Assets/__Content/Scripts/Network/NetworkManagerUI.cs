using Unity.Netcode;
using UnityEngine;

namespace __Content.Scripts.Network
{
    public class NetworkManagerUI : MonoBehaviour
    {
        public void OnHostButtonClicked()
        {
            StartHost();
        }

        public void OnClientButtonClicked()
        {
            StartClient();
        }

        private async void StartHost()
        {
            if (RelayManager.Instance.isRelayEnabled)
                await RelayManager.Instance.SetupRelay();

            NetworkManager.Singleton.StartHost();
        }
        
        private async void StartClient()
        {
            if (RelayManager.Instance.isRelayEnabled)
                await RelayManager.Instance.JoinRelay("RT8D87");
            
            NetworkManager.Singleton.StartClient();
        }
    }
}