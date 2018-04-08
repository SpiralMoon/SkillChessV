using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model;
using Assets.Model.Impl;
using Assets.Model.Bean;
using Assets.Model.SceneParameter;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

public class LobbyPage : MonoBehaviour, IPage, ISocketPage
{
    private NetworkManager _networkManager;

    private TextResource _textResource;

    private Setting _setting;

    private bool _isShowCreateRoom;

    private RoomForm _selectedRoom;

    private Timer _roomListLoader;

    private List<RoomForm> _roomList;

    public Button BtnShop;

    public Button BtnRank;

    public Button BtnInventory;

    public Button BtnChat;

    public Button BtnShowCreateRoom;

    public Button BtnJoin;

    public Button BtnShowFriends;

    public Button BtnCreateRoom;

    public Button BtnShowCreateRoomClose;

    public Button BtnBack;

    public GameObject ScrRoomList;

    public GameObject ScrChatList;

    public InputField InpChatting;

    public Text TxtNickname;

    public Text TxtScore;

    public InputField InpTitle;

    public Toggle TglClassic;

    public Toggle TglSkill;

    public Text TxtMessage;

    public GameObject PnlCreateRoom;

    public string NewChatMessage
    {
        get
        {
            var message = InpChatting.text;
                          InpChatting.text = null;

            return message;
        }
    }
    

    public string TotalChatMessage;

    public string Title
    {
        get { return InpTitle.text; }
    }

    public GameMode GameMode
    {
        get
        {
            if (TglClassic.isOn)
            {
                return GameMode.NORMAL;
            }

            if (TglSkill.isOn)
            {
                return GameMode.SKILL;
            }

            return GameMode.NONE;
        }
    }

    public string ErrorMessage
    {
        set { TxtMessage.text = value; }
    }

    private void Awake()
    {
        _networkManager = NetworkManager.GetInstance();
        _textResource = TextResource.GetInstance();
        _setting = Setting.GetInstance();
        _networkManager.SocketOpen();
        _roomListLoader = new Timer();

        _isShowCreateRoom = false;
        _selectedRoom = null;

        BtnShop.onClick.AddListener(OnClickBtnShop);
        BtnRank.onClick.AddListener(OnClickBtnRank);
        BtnInventory.onClick.AddListener(OnClickBtnInventory);
        BtnChat.onClick.AddListener(OnClickBtnChat);
        BtnShowCreateRoom.onClick.AddListener(OnClickBtnShowCreateRoom);
        BtnJoin.onClick.AddListener(OnClickBtnJoin);
        BtnShowFriends.onClick.AddListener(OnClickBtnShowFriends);
        BtnCreateRoom.onClick.AddListener(OnClickBtnCreateRoom);
        BtnShowCreateRoomClose.onClick.AddListener(OnClickBtnShowCreateRoomClose);
        BtnBack.onClick.AddListener(OnClickBtnBack);

        SetTextSize();
        SetTextValue();
        SetSocketEvents(_networkManager);
    }

    private async void Start()
    {
        var me = await _networkManager.Rank();

        if (me.Nickname != null)
        {
            TxtNickname.text = me.Nickname;
            TxtScore.text = me.Score + "";

            _setting.Nickname = me.Nickname;
            _setting.Score = me.Score;
            // TODO : 혹시 닉네임이 Setting에 필요해지면 넣도록 하자
        }
        
        StartCoroutine(LoadRoomList());
        //_networkManager.LoadRoomList();
        //_roomListLoader.Interval = Time.deltaTime * 1000 * 600;
        //_roomListLoader.Elapsed += (s, e) => {
        //    _networkManager.LoadRoomList();
        //};
        //_roomListLoader.Start();
    }

    private IEnumerator LoadRoomList()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            _networkManager.LoadRoomList();
            yield return new WaitForSeconds(8);
        }
    }

    public void OnClickBtnShop()
    {
        NextPage("ShopPage");
    }

    public void OnClickBtnRank()
    {
        NextPage("RankPage");
    }

    public void OnClickBtnInventory()
    {
        NextPage("InventoryPage");
    }

    public void OnClickBtnChat()
    {
        _networkManager.Chat(new ChatForm
        {
            Nickname = "test", // TODO : 닉네임 불러오기
            Message  = NewChatMessage
        });
    }

    public void OnClickBtnShowCreateRoom()
    {
        PnlCreateRoom.SetActive(!_isShowCreateRoom);
        _isShowCreateRoom = !_isShowCreateRoom;
    }

    public void OnClickBtnJoin()
    {
        _networkManager.JoinRoom(new RoomForm
        {
            Id = _selectedRoom.Id,
            OtherId =_setting.Email,
            GameMode = _selectedRoom.GameMode
        });
    }

    public void OnClickBtnShowFriends()
    {

    }

    public async void OnClickBtnCreateRoom()
    {
        if (Title.Trim() == null || Title.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.PLEASE_ENTER_A_GAME_TITLE);
            return;
        }
        if (!Title.IsGameTitle())
        {
            ErrorMessage = _textResource.GetText(TextCode.TITLE_LENGTH_CAN_NOT_EXCEED_25_CHARACTERS);
            return;
        }

        // 게임모드가 선택이 안된 경우
        if (GameMode == GameMode.NONE)
        {
            ErrorMessage = _textResource.GetText(TextCode.PLEASE_SELECT_GAME_MODE);
            return;
        }

        var room = new RoomForm
        {
            OwnerId = _setting.Email,
            Title = Title,
            GameMode = GameMode
        };
        
        _networkManager.CreateRoom(room);
    }

    public void OnClickBtnShowCreateRoomClose()
    {
        PnlCreateRoom.SetActive(!_isShowCreateRoom);
        _isShowCreateRoom = !_isShowCreateRoom;
    }

    public void OnClickBtnBack()
    {
        if(_setting.IsEnabledAutoLogin)
        {
            NextPage("TitlePage");
        }
        else
        {
            NextPage("LoginPage");
        }
    }

    public void SetTextSize()
    {

    }

    public void SetTextValue()
    {
        // 로비 기본 화면
        InpChatting.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.PLEASE_ENTER_A_MESSAGE);
        BtnChat.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.SEND);

        // 게임방 만들기 화면
        InpTitle.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.PLEASE_ENTER_A_GAME_TITLE);
        TglClassic.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.CLASSIC);
        TglSkill.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.SKILL);
        BtnCreateRoom.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.CREATE);
    }

    public void NextPage(string pageName)
    {
        SceneManager.LoadSceneAsync(pageName);
    }

    public void SetSocketEvents(NetworkManager networkManager)
    {
        // 해제
        networkManager.OnCreateRoom -= OnCreateRoom;
        networkManager.OnMatchBattle -= OnMatchBattle;
        networkManager.OnLoadRoomList -= OnLoadRoomList;
        networkManager.OnChat -= OnChat;

        // 연결
        networkManager.OnCreateRoom += OnCreateRoom;
        networkManager.OnMatchBattle += OnMatchBattle;
        networkManager.OnLoadRoomList += OnLoadRoomList;
        networkManager.OnChat += OnChat;
    }

    public void Invoke(Action action)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(action);
    }

    public void OnCreateRoom(object sender, RoomForm room)
    {
        if (room != null)
        {
            PageParameterDispatcher.Instance().SetPageParameter(new WaitingPageParameter
            {
                RoomForm = room
            });
            Invoke(() => NextPage("WaitingPage"));
        }
        else
        {
            ErrorMessage = "";
        }
    }

    public void OnMatchBattle(object sender, MatchForm match)
    {
        if (match != null)
        {
            Invoke(() =>
            {
                PageParameterDispatcher.Instance().SetPageParameter(new WaitingPageParameter
                {
                    MatchForm = match
                });
                NextPage("WaitingPage");
            });
        }
        else
        {
            ErrorMessage = "";
        }
    }

    public void OnLoadRoomList(object sender, List<RoomForm> roomList)
    {
        Invoke(() =>
        {
            // 지우면 안됨
            var scrollTrans = ScrRoomList.GetComponentsInChildren<Transform>(true)[0];

            // 기존 방 목록을 모두 지움
            foreach (var scroll in ScrRoomList.GetComponentsInChildren<Transform>(true))
            {
                if (scroll != scrollTrans)
                {
                    Destroy(scroll.gameObject);
                }
            }

            _roomList = roomList;

            // 방 목록을 화면에 추가함
            foreach (var room in roomList)
            {
                var roomPrefab = Instantiate(Resources.Load("UI/LobbyPage/PRF_RoomList") as GameObject);
                var gameModeIcon = (room.GameMode == GameMode.NORMAL)
                                    ? Resources.Load<Sprite>("UI/Icon/classic")
                                    : Resources.Load<Sprite>("UI/Icon/skill");

                roomPrefab.GetComponent<Button>().onClick.AddListener(() => OnClickRoom(room));
                roomPrefab.GetComponentInChildren<Text>().text = room.Title;
                roomPrefab.GetComponentsInChildren<Image>()[1].sprite = gameModeIcon;
                roomPrefab.name = room.Id;

                // 리스트에 연결
                roomPrefab.transform.parent = ScrRoomList.transform;
            }
        });
    }

    public void OnChat(object sender, ChatForm chat)
    {
        Debug.Log(chat.Nickname);
        Debug.Log(chat.Message);
    }

    /// <summary>
    /// 방을 터치(선택)했을 때
    /// </summary>
    /// <param name="roomId"></param>
    public void OnClickRoom(RoomForm roomForm)
    {
        var list = ScrRoomList.GetComponentsInChildren<Transform>(true);

        // 이전에 선택했던 방을 원래 색으로 되돌림
        if (_selectedRoom != null)
        {
            foreach(var room in list)
            {
                if(room.name.Contains(_selectedRoom.Id))
                {
                    room.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/LobbyPage/IMG_RoomList");
                }
            }
        }

        // 지금 선택한 방의 색을 바꿈
        foreach (var room in list)
        {
            if (room.name.Contains(roomForm.Id))
            {
                room.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/LobbyPage/IMG_RoomListSelect");
            }
        }

        _selectedRoom = roomForm;
    }
}
