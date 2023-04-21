using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance;
    private Camera _Cam;
    [SerializeField] Line _LinePrefabs;
    public const float Resolusion = 0.2f;
    private Line _CurrentLine;
    private bool canDraw = true;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _Cam = Camera.main;
    }

    // Update is called once per frame

    private void OnDisable()
    {
        if (_CurrentLine != null)
        {
            ResetLine();
        }
    }


    void Update()
    {
        if (!GameController.Instance.PlayGame && !GameController.Instance.b_EndGame)
        {
            Vector2 mousePos = _Cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                canDraw = true;
                GameController.Instance.levelDesign.UnActiveHint();
                _CurrentLine = Instantiate(_LinePrefabs, mousePos, Quaternion.identity);
                
            }
            if (Input.GetMouseButton(0) && _CurrentLine != null && canDraw)
            {
                _CurrentLine.SetPos(mousePos);
            }
            if (Input.GetMouseButtonUp(0) && _CurrentLine._Render.positionCount > 1)
            {
                foreach (Line item in FindObjectsOfType<Line>())
                {
                    if (item != null && item._Render.positionCount <= 1)
                    {
                        Destroy(item.gameObject);
                    }
                }
                _CurrentLine.EndDraw();
                DropLine();
                GameController.Instance.PlayGameAfterDraw();
            }
        }
    }

    public void EndDraw()
    {
        canDraw = false;
        _CurrentLine.EndDraw();
        DropLine();
       // GameController.Instance.PlayGameAfterDraw();
    }

    public void DropLine()
    {
        if (_CurrentLine.gameObject)
        {
            _CurrentLine.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            _CurrentLine.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            _CurrentLine.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        }
    }
    public void ResetLine()
    {
        foreach (Line item in FindObjectsOfType<Line>())
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
        GameController.Instance.PlayGame = false;
    }
}
