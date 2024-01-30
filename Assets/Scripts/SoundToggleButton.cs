using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public enum ButtonType
    {
        BackGroundMusic,
        SoundFx
    };

    public ButtonType type;
    public Sprite Onsprite;
    public Sprite offSprite;

    public GameObject button;
    public Vector3 offButtonPosition;

    private Vector3 _onButtonPosition;
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = Onsprite;
        _onButtonPosition = button.GetComponent<RectTransform>().anchoredPosition;
        ToggleButton();
    }

    public void ToggleButton()
    {
        var muted = false;
        if (type == ButtonType.BackGroundMusic)
            muted = SoundManager.instance.IsBackGroundMusicMuted();
        else
            muted = SoundManager.instance.IsSoundFxMuted();

        if (muted)
        {
            _image.sprite = offSprite;
            button.GetComponent<RectTransform>().anchoredPosition = offButtonPosition;

        }
        else
        {
            _image.sprite = Onsprite;
            button.GetComponent<RectTransform>().anchoredPosition = _onButtonPosition;
        }
    }

}