using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay;
using UnityEngine;

namespace __Content.Scripts.Network
{
    public class RelayManager : MonoBehaviour
    {
        private UnityTransport transport;
        public bool isRelayEnabled;
        public string joinCode;
        public bool isHost;

        public static RelayManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
            isRelayEnabled = transport != null && transport.Protocol == UnityTransport.ProtocolType.RelayUnityTransport;
        }

        public async Task<RelayHostData> SetupRelay()
        {
            var options = new InitializationOptions()
                .SetEnvironmentName("development");

            await UnityServices.InitializeAsync(options);

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var allocation = await Relay.Instance.CreateAllocationAsync(2);

            var relayHostData = new RelayHostData
            {
                IPv4Address = allocation.RelayServer.IpV4,
                Port = (ushort) allocation.RelayServer.Port,
                AllocationID = allocation.AllocationId,
                AllocationIDBytes = allocation.AllocationIdBytes,
                ConnectionData = allocation.ConnectionData,
                Key = allocation.Key
            };

            relayHostData.JoinCode = await Relay.Instance.GetJoinCodeAsync(relayHostData.AllocationID);
            joinCode = relayHostData.JoinCode;
            isHost = true;

            transport.SetRelayServerData(relayHostData.IPv4Address, relayHostData.Port, relayHostData.AllocationIDBytes, relayHostData.Key, relayHostData.ConnectionData);

            Debug.Log("Relay server generated a join code " + relayHostData.JoinCode);
            return relayHostData;
        }
        
        public async Task<RelayJoinData> JoinRelay(string joinCode)
        {
            this.joinCode = joinCode;
            
            var options = new InitializationOptions()
                .SetEnvironmentName("development");

            await UnityServices.InitializeAsync(options);

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            var allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
            
            var relayJoinData = new RelayJoinData
            {
                JoinCode = joinCode,
                IPv4Address = allocation.RelayServer.IpV4,
                Port = (ushort) allocation.RelayServer.Port,
                AllocationID = allocation.AllocationId,
                AllocationIDBytes = allocation.AllocationIdBytes,
                ConnectionData = allocation.ConnectionData,
                HostConnectionData = allocation.HostConnectionData,
                Key = allocation.Key
            };
            
            transport.SetRelayServerData(relayJoinData.IPv4Address, relayJoinData.Port, relayJoinData.AllocationIDBytes, relayJoinData.Key, relayJoinData.ConnectionData, relayJoinData.HostConnectionData);
            
            Debug.Log("Client joined game with join code " + joinCode);
            return relayJoinData;
        }
    }
}