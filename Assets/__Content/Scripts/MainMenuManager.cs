using __Content.Scripts.Network;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __Content.Scripts
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;

        public void OnNewGameClicked()
        {
            StartHost();
        }
        
        public void OnJoinGameClicked()
        {
            StartClient();
        }
        
        private async void StartHost()
        {
            if (RelayManager.Instance.isRelayEnabled)
                await RelayManager.Instance.SetupRelay();

            SceneManager.LoadScene("SampleGameplayScene");
        }
        
        private async void StartClient()
        {
            if (RelayManager.Instance.isRelayEnabled)
                await RelayManager.Instance.JoinRelay(inputField.text);
            
            SceneManager.LoadScene("SampleGameplayScene");
        }
    }
}
