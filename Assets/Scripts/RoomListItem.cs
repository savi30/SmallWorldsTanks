using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour{
    [SerializeField] private Text roomNameText;
    private MatchInfoSnapshot _match;

    public delegate void JoinRoomDelegate(MatchInfoSnapshot matchInfoSnapshot);

    private JoinRoomDelegate _joinRoomCallback;

    public void Setup(MatchInfoSnapshot matchInfo, JoinRoomDelegate _delegate){
        _match = matchInfo;
        _joinRoomCallback = _delegate;
        roomNameText.text = _match.name + " (" + _match.currentSize + "/" + _match.maxSize + ")";
    }

    public void JoinRoom(){
        _joinRoomCallback.Invoke(_match);
    }
}