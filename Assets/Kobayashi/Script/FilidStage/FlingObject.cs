using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingObject : MonoBehaviour
{
    private int MoveCount = 0;
    private  bool MoveFlag;

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveCount++;

        float MovePosNum = Random.Range(0.025f, 0.045f);

        if(MoveFlag)
        {
            Vector3 ObjectYPos = gameObject.transform.position;

            ObjectYPos.y += MovePosNum;

            gameObject.transform.position = ObjectYPos;

            if (MoveCount > 60)
            {
                MoveFlag = false;
                MoveCount = 0;
            }
        }
        else
        {
            Vector3 ObjectYPos = gameObject.transform.position;

            ObjectYPos.y -= MovePosNum;

            gameObject.transform.position = ObjectYPos;

            if (MoveCount > 60)
            {
                MoveFlag = true;
                MoveCount = 0;
            }
        }


    }
}
