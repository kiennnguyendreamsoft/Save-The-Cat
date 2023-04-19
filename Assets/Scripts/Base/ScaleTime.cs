using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTime : MonoBehaviour
{


    public float _Speed = 1;
    public float _MinRange;
    public float _MaxRange;

  
    bool _IsBigg = true;
    // Start is called before the first frame update
    float _Count;

    // Update is called once per frame
    private void FixedUpdate() {
         if (this.gameObject.transform.localScale.x < _MinRange)
        {
            _IsBigg = true;
            _Count=0;
        }
        else if (this.gameObject.transform.localScale.x > _MaxRange)
        {
            _IsBigg = false;
             _Count=0;
        }
        if (_IsBigg == true)
        {
            _Count += Time.deltaTime * (_Speed * 0.1f);
            SetScale(_Count);
        }
        else
        {
            _Count -= Time.deltaTime * (_Speed * 0.1f);
            SetScale(_Count);

        }
    }
    public void SetScale(float value)
    {
       // Debug.Log(this.gameObject.transform.localScale.x);
       
            this.gameObject.transform.localScale += new Vector3(value, 0, 0);
        
            this.gameObject.transform.localScale += new Vector3(0, value, 0);
        
    }
}
