using UnityEngine;
using UnityEngine.Networking;

public class PlayerUI : NetworkBehaviour{
    [SerializeField] private RectTransform lifeFill;

    public void SetFill(float amount){
        lifeFill.localScale = new Vector3(Mathf.Max(amount, 0), 1f, 1f);
    }
}