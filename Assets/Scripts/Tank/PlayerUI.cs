using UnityEngine;
using UnityEngine.Networking;

public class PlayerUI : NetworkBehaviour{
    [SerializeField] private RectTransform lifeFill;
    [SerializeField] private GameObject pauseMenu;

    private void Start(){
        PauseMenu.isOn = false;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu(){
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

    public void SetFill(float amount){
        lifeFill.localScale = new Vector3(Mathf.Max(amount, 0), 1f, 1f);
    }
}