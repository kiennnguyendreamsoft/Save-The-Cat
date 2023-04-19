using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer _Render;
    public LineRenderer _ErrorRender;
    [SerializeField] private EdgeCollider2D _Colider;

    public float _LineSize = 10;
    bool _ColCheck;

    int colCount;

    bool endDraw = false;
    private List<Vector2> _point = new List<Vector2>();

    private void Awake()
    {
        Setsize();
    }

    public void Setsize()
    {

        _Render.endWidth = _LineSize * 2;
        _Render.startWidth = _LineSize * 2;
        _ErrorRender.endWidth = _LineSize * 2;
        _ErrorRender.startWidth = _LineSize * 2;
        _Colider.edgeRadius = _LineSize*2;
    }

    public void SetPos(Vector2 pos)
    {
        if (_ColCheck == true)
        {
           
            if (_point.Count - 2 >= 0 && _point.Count >= 2)
            {
               
             _point[_point.Count - 2] = _point[_point.Count - 3];
                _Render.SetPosition(_Render.positionCount - 2, _Render.GetPosition(_Render.positionCount - 3));

                _ErrorRender.SetPosition(0, _Render.GetPosition(_Render.positionCount - 2));
                _ErrorRender.gameObject.SetActive(true);
                _ErrorRender.SetPosition(1, transform.InverseTransformPoint(pos));

                _point[_point.Count - 1] = (transform.InverseTransformPoint(pos));
                _Render.SetPosition(_Render.positionCount - 1, transform.InverseTransformPoint(pos));


            }
            _Colider.points = _point.ToArray();
        }
        else if (_ColCheck == false)
        {
            if (!CanAppend(pos))
            {
                return;
            }
            _point.Add(transform.InverseTransformPoint(pos));
            _Render.positionCount++;
            _Render.SetPosition(_Render.positionCount - 1, _point[_point.Count - 1]);
            _Colider.points = _point.ToArray();
            int maxPoint = GameController.Instance.levelDesign.maxPointLineCanDraw;
            float rate = (float)(maxPoint - _point.Count) / maxPoint;
            GameController.Instance.ChangeProcessDraw(rate);
        }

    }

    public void EndDraw()
    {
        if (_ColCheck == true)
        {
            _Render.positionCount--;
            _point.RemoveAt(_point.Count - 1);
            _Colider.points = _point.ToArray();

        }
        foreach (Vector2 item in _point)
        {
            CircleCollider2D col = this.gameObject.AddComponent<CircleCollider2D>();
            col.offset = item;
            col.radius = _LineSize;
        }
        _ErrorRender.gameObject.SetActive(false);
        endDraw = true;

    }

    private bool CanAppend(Vector2 pos)
    {
        if (_Render.positionCount == 0)
        {
            return true;
        }
        return Vector2.Distance(_Render.GetPosition(_Render.positionCount - 1), transform.InverseTransformPoint(pos)) > DrawManager.Resolusion;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (colCount <= 0 && endDraw == false)
        {
            _ColCheck = true;


        }
        colCount++;

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        colCount--;
        if (colCount <= 0 && endDraw == false)
        {
            _ErrorRender.gameObject.SetActive(false);
            _ColCheck = false;
        }
    }

}
