using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchcontro : MonoBehaviour
{
    public Text directionText;
    private Touch theTouch; 
    private Vector2 touchStartPosition, touchEndPosition; 
    private string direction;
    public bool m_isSwiped = true;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) 
        { theTouch = Input.GetTouch(0); 

            if (theTouch.phase == TouchPhase.Began) 

            { 
                touchStartPosition = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)

            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x; 
                float y = touchEndPosition.y - touchStartPosition.y; 
                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0) 
                { 
                    Debug.Log("No of Frame");
                } 
                else if (Mathf.Abs(x) > Mathf.Abs(y)) 
                { 
                    if(theTouch.phase == TouchPhase.Moved)
                    {
                        if( x > 0)
                        {
                            
                            if (i == 0)
                            {
                                Debug.Log("swipe Right");
                                i = 1;
                            }
                        }
                        else if( x < 0)
                        {
                           
                            if (i == 0)
                            {
                                Debug.Log("swipe Left");
                                i = 1;
                            }
                        }

                    }

                } 
                else  
                {
                    if(theTouch.phase == TouchPhase.Moved)
                    {
                        if(y > 0)
                        {
                            if (i == 0)
                            {
                                Debug.Log("swipe UP");
                                i = 1;
                            }

                        }
                        else if(y < 0)
                        {
                            if(i == 0)
                            {
                                Debug.Log("swipe Down");
                                i = 1;
                            }
                            
                        }


                    }
                } 

                if(theTouch.phase == TouchPhase.Ended)
                {
                    i = 0;
                }
            }
        }



    }

   
}
