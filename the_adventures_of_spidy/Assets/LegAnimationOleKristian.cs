using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimationOleKristian : MonoBehaviour {

    public float frequency = 2;
    public float amplitude = 1.5f;
    public float phase = 0;

    Vector3 deltaT = new Vector3();
    Vector3 curThead = new Vector3();
    Vector3 curTlegleft = new Vector3();
    Vector3 curTarmleft = new Vector3();
    Vector3 curTlegright = new Vector3();
    Vector3 curTarmright = new Vector3();

    float localTime = 0;

    public Transform targethead;
    public Transform targetlegleft;
    public Transform targetarmleft;
    public Transform targetlegright;
    public Transform targetarmright;

    public Vector3[] PositionArrayArmRight = new Vector3[10];
    public Vector3[] PositionArrayArmLeft = new Vector3[10];
    public Vector3[] PositionArrayLegRight = new Vector3[10];
    public Vector3[] PositionArrayLegLeft = new Vector3[10];
    public Vector3[] PositionArrayHead = new Vector3[10];

    public int CurPosition = 0;


    // Use this for initialization
    void Start()
    {
        phase = 0;
        deltaT = Vector3.zero;
        localTime = 0;
        //Right Arm animation location
        PositionArrayArmRight[0] = new Vector3(1.04f, 1.25f, 0.0f);
        PositionArrayArmRight[1] = new Vector3(2.55f, 3.54f, 0.0f);
        PositionArrayArmRight[2] = new Vector3(2.47f, 5.26f, 0.0f);
        PositionArrayArmRight[3] = new Vector3(1.37f, 6.36f, 0.0f);
        PositionArrayArmRight[4] = new Vector3(0.26f, 6.77f, 0.0f);
        PositionArrayArmRight[5] = new Vector3(0.845f, 7.105f, 0.0f);
        PositionArrayArmRight[6] = new Vector3(1.37f, 6.36f, 0.0f);
        PositionArrayArmRight[7] = new Vector3(2.47f, 5.26f, 0.0f);
        PositionArrayArmRight[8] = new Vector3(2.55f, 3.54f, 0.0f);
        PositionArrayArmRight[9] = new Vector3(1.4f, 1.25f, 0.0f);

        //Left Arm animation location
        PositionArrayArmLeft[0] = new Vector3(-1.0f, 1.62f, 0.0f);
        PositionArrayArmLeft[1] = new Vector3(-2.32f, 3.28f, 0.0f);
        PositionArrayArmLeft[2] = new Vector3(-2.52f, 4.42f, 0.0f);
        PositionArrayArmLeft[3] = new Vector3(-2.28f, 5.66f, 0.0f);
        PositionArrayArmLeft[4] = new Vector3(-0.68f, 6.4f, 0.0f);
        PositionArrayArmLeft[5] = new Vector3(-1.287f, 6.423f, 0.0f);
        PositionArrayArmLeft[6] = new Vector3(-2.28f, 5.66f, 0.0f);
        PositionArrayArmLeft[7] = new Vector3(-2.52f, 4.42f, 0.0f);
        PositionArrayArmLeft[8] = new Vector3(-2.32f, 3.28f, 0.0f);
        PositionArrayArmLeft[9] = new Vector3(-1.0f, 1.62f, 0.0f);

        //Right Leg animation location
        PositionArrayLegRight[0] = new Vector3(0.87f, -0.29f, 0.95f);
        PositionArrayLegRight[1] = new Vector3(0.87f, -0.05f, 1.32f);
        PositionArrayLegRight[2] = new Vector3(0.87f, 0.7f, 1.58f);
        PositionArrayLegRight[3] = new Vector3(0.87f, 1.12f, 2.06f);
        PositionArrayLegRight[4] = new Vector3(0.87f, 2.089f, 2.469f);
        PositionArrayLegRight[5] = new Vector3(0.87f, 2.089f, 2.469f);
        PositionArrayLegRight[6] = new Vector3(0.87f, 1.12f, 2.06f);
        PositionArrayLegRight[7] = new Vector3(0.87f, 0.7f, 1.58f);
        PositionArrayLegRight[8] = new Vector3(0.87f, -0.05f, 1.32f);
        PositionArrayLegRight[9] = new Vector3(0.87f, -0.29f, 0.95f);

        //left Leg animation location
        PositionArrayLegLeft[0] = new Vector3(-1.25f, -0.06f, 0.98f);
        PositionArrayLegLeft[1] = new Vector3(-1.24f, -0.03f, -0.08f);
        PositionArrayLegLeft[2] = new Vector3(-1.24f, 0.54f, -0.7f);
        PositionArrayLegLeft[3] = new Vector3(-1.24f, 1.153f, -0.669f);
        PositionArrayLegLeft[4] = new Vector3(-1.24f, 1.622f, -1.597f);
        PositionArrayLegLeft[5] = new Vector3(-1.24f, 1.622f, -1.597f);
        PositionArrayLegLeft[6] = new Vector3(-1.24f, 1.153f, -0.669f);
        PositionArrayLegLeft[7] = new Vector3(-1.24f, 0.54f, -0.7f);
        PositionArrayLegLeft[8] = new Vector3(-1.24f, -0.03f, -0.08f);
        PositionArrayLegLeft[9] = new Vector3(-1.25f, -0.6f, 0.98f);

        //Head animation location
        PositionArrayHead[0] = new Vector3(0.0f, 0.35f, 3.73f);
        PositionArrayHead[1] = new Vector3(0.44f, 0.35f, 2.93f);
        PositionArrayHead[2] = new Vector3(0.65f, 0.35f, 2.84f);
        PositionArrayHead[3] = new Vector3(0.85f, 0.35f, 2.73f);
        PositionArrayHead[4] = new Vector3(1.09f, 0.35f, 2.54f);
        PositionArrayHead[5] = new Vector3(1.09f, 0.35f, 2.54f);
        PositionArrayHead[6] = new Vector3(0.85f, 0.35f, 2.73f);
        PositionArrayHead[7] = new Vector3(0.65f, 0.35f, 2.84f);
        PositionArrayHead[8] = new Vector3(0.44f, 0.35f, 2.93f);
        PositionArrayHead[9] = new Vector3(0.0f, 0.35f, 3.73f);


    }

    // Update is called once per frame
    void Update()
    {

        //accumulate our own local time to this object
        localTime += Time.deltaTime;

        
        targetarmright.localPosition = PositionArrayArmRight[CurPosition];
        if (localTime > 0.3f)
        {
            CurPosition++;
            if (CurPosition >= PositionArrayArmRight.Length)
                CurPosition = 0;

            localTime = 0;
        }

        targetarmleft.localPosition = PositionArrayArmLeft[CurPosition];
        if (localTime > 0.3f)
        {
            CurPosition++;
            if (CurPosition >= PositionArrayArmLeft.Length)
                CurPosition = 0;

            localTime = 0;
        }

        targetlegleft.localPosition = PositionArrayLegLeft[CurPosition];
        if (localTime > 0.3f)
        {
            CurPosition++;
            if (CurPosition >= PositionArrayLegLeft.Length)
                CurPosition = 0;

            localTime = 0;
        }

        targetlegright.localPosition = PositionArrayLegRight[CurPosition];
        if (localTime > 0.3f)
        {
            CurPosition++;
            if (CurPosition >= PositionArrayLegRight.Length)
                CurPosition = 0;

            localTime = 0;
        }

        targethead.localPosition = PositionArrayHead[CurPosition];
        if (localTime > 0.3f)
        {
            CurPosition++;
            if (CurPosition >= PositionArrayHead.Length)
                CurPosition = 0;

            localTime = 0;
        }


        /* float z = Mathf.Sin((localTime + phase) * frequency) * amplitude;

         curThead.Set(targethead.localPosition.x, targethead.localPosition.y, deltaT.z + z);
         targethead.localPosition = curThead;

         curTlegleft.Set(targetlegleft.localPosition.x, targetlegleft.localPosition.y, deltaT.z + z);
         targetlegleft.localPosition = curTlegleft;

         curTarmleft.Set(targetarmleft.localPosition.x, targetarmleft.localPosition.y, deltaT.z + z);
         targetarmleft.localPosition = curTarmleft;

         curTlegright.Set(targetlegright.localPosition.x, targetlegright.localPosition.y, deltaT.z + z);
         targetlegright.localPosition = curTlegright;

         curTarmright.Set(targetarmright.localPosition.x, targetarmright.localPosition.y, deltaT.z + z);
         targetarmright.localPosition = curTarmright;*/


    }
}