using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tilia.Interactions.SpatialButtons;

namespace Mirror.Discovery
{
    public class NetWorkHud : MonoBehaviour
    {
        public NetworkDiscovery networkDiscovery;
        public NetworkRoomManager roomManager;
      

        public GameObject hostButton;
        public GameObject findServerButton;
        public GameObject serverButton;


        public GameObject serverList;

        public GameObject[] servers;
        
        readonly Dictionary<long,ServerResponse> discoveredServers=new Dictionary<long,ServerResponse>();

        //[SyncVar]
        //private bool readyToBegin;


        private void Start()
        {
            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active&&NetworkManager.singleton!=null)
            {
                //激活开始菜单
                hostButton.SetActive(true);
                findServerButton.SetActive(true);               
                serverList.SetActive(false);
            }
            
        }

        private void Update()
        {
            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
            {
                /*
                hostButton.SetActive(true);
                findServerButton.SetActive(true);
                */
                //控制UI显示
            }
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }
        public void OnFindServerButton()
        {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();          
            hostButton.SetActive(false);
            findServerButton.SetActive(false);
            Debug.Log(hostButton.activeSelf);
            Debug.Log(findServerButton.activeSelf);
            StartCoroutine(nameof(AddServer));

        }
        IEnumerator AddServer()
        {
           
            serverList.SetActive(true);
            Debug.Log(serverList.activeSelf);
            while (true)
            {
                
                int i = 0;
                foreach(ServerResponse info in discoveredServers.Values)
                {
                    SpatialButtonFacade _spatialButtonFacade = servers[i].GetComponent<SpatialButtonFacade>();
                    if (_spatialButtonFacade != null)
                    {
                        _spatialButtonFacade.EnabledInactive.ButtonText = info.EndPoint.ToString();
                        _spatialButtonFacade.EnabledActive.ButtonText = info.EndPoint.ToString();
                        _spatialButtonFacade.EnabledHover.ButtonText = info.EndPoint.ToString();
                        _spatialButtonFacade.Activated.AddListener(delegate { Connect(info); });
                        
                        i++;
                    }
                    
                }

                yield return null;
            }
            
        }

        public void OnStartHostBtn()
        {
            //停用UI
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
            if (NetworkServer.active||NetworkClient.active)
            {
                if(NetworkServer.active&&NetworkClient.active)
                {
                    //激活StopHostButton
                }
            }
        }

        public void  OnStartServerBtn()
        {
            
            //控制UI
            discoveredServers.Clear();
            NetworkManager.singleton.StartServer();
            networkDiscovery.AdvertiseServer();
            if (NetworkServer.active || NetworkClient.active)
            {
                if (NetworkServer.active && NetworkClient.active)
                {
                    //激活StopServerButton
                }
            }


        }

        public void OnStopHostBtn()
        {
            //控制UI

            NetworkManager.singleton.StopHost();
            networkDiscovery.StopDiscovery();
        }

        public void OnStopClient()
        {
            //控制UI
            NetworkManager.singleton.StopClient();
            networkDiscovery.StopDiscovery();
        }
        public void OnStopServerBtn()
        {
            //控制UI
            NetworkManager.singleton.StopServer();
            networkDiscovery.StopDiscovery();
        }

        void Connect(ServerResponse _info)
        {
            StopAllCoroutines();
            //停用UI
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(_info.uri);
        }


        public void ReadyToBegin()
        {

        }


        /*
        [Command]
        public void CmdChangeReadyState(bool readyState)
        {
            readyToBegin = readyState;
            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room != null)
            {
                room.ReadyStatusChanged();
            }
        }

        public virtual void OnGUI()
        {           

            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                /*
                if (!room.showRoomGUI)
                    return;
                

                if (!NetworkManager.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {            
            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer) || isServerOnly) && GUILayout.Button("REMOVE"))
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }
       */
    }
}