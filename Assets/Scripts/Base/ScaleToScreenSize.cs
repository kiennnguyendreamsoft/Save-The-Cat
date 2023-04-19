using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    int _ScreenWith;
    int _ScreenHeight;
    void Start()
    {
        Setup();
        _ScreenWith = Screen.width;
        _ScreenHeight = Screen.height;
    }
    private void FixedUpdate()
    {
        if (_ScreenWith != Screen.width && _ScreenHeight != Screen.height)
        {
            Setup();
            _ScreenWith = Screen.width;
            _ScreenHeight = Screen.height;
        }
    }
    public void Setup()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1f, 1f, 1f);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)(worldScreenWidth / width), (float)(worldScreenHeight / height));

    }


}
