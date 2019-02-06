using DaanSleven.HexGrid;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Debugger : MonoBehaviour {

    PathingHexGrid<PathingHex> HexGrid = new PathingHexGrid<PathingHex>();
    Path hexPath = null;

    public Vector2 vtrBegin;
    public Vector2 vtrEnd;

    public Vector2 vtrSize;
    public float fltChanceForObstacle = 40f;
    public int intMaxHeight = 0;
    public bool blnShowPath = true;
    public float fltGizmosSize = 0.1f;
    public float fltGizmosSizePath = 0.1f;

    HexVector StartPoint = new HexVector();
    HexVector EndPoint = new HexVector();

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPoint = new HexVector((int)vtrBegin.x, (int)vtrBegin.y);
            EndPoint = new HexVector((int)vtrEnd.x, (int)vtrEnd.y);

            CreateRectangularField((int)vtrSize.x, (int)vtrSize.y);
            HexGrid.Get(EndPoint).IsObstacle = false;

            Stopwatch swTime = new Stopwatch();
            swTime.Start();
            hexPath = HexGrid.FindPath(StartPoint, EndPoint,0);
            swTime.Stop();
            UnityEngine.Debug.Log(swTime.ElapsedMilliseconds);
        }
    }

    #region Debugging
	public void CreateRectangularField(int pWidth,int pHeight) {
		for (int intX = 0; intX < pWidth; intX++) {
			for (int intY = 0; intY < pHeight; intY++) {
                if (Random.Range(0, 100) < fltChanceForObstacle)
                {
                    HexGrid.Add(new PathingHex(new HexVector(intX - intY / 2, intY), true, Random.Range(0, intMaxHeight)));
                }
                else
                {
                    HexGrid.Add(new PathingHex(new HexVector(intX - intY / 2, intY), false, Random.Range(0, intMaxHeight)));
                }
			}
		}
	}

	void OnDrawGizmos(){
        //Draw Start and end
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(StartPoint.ToWorldPosition(), fltGizmosSizePath);
        Gizmos.DrawSphere(EndPoint.ToWorldPosition(), fltGizmosSizePath);

        //Draw Grid
        foreach (HexVector thisKey in HexGrid.GetExistingPositions()) {
            PathingHex CurrentHex = HexGrid.Get(thisKey);
            Vector3 HexWorldPos = CurrentHex.Position.ToWorldPosition();
            HexWorldPos = new Vector3(HexWorldPos.x, CurrentHex.Height, HexWorldPos.z);

            if (CurrentHex.IsObstacle)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(HexWorldPos, fltGizmosSize);
            }
            else
            {
                Gizmos.color = Color.green;
                //Gizmos.DrawSphere(HexWorldPos, fltGizmosSize);
            }

            //Draw Outline
            Gizmos.color = Color.black;
            Vector3 Top = new Vector3(0, 0, 1);
            Vector3 TopRight = new Vector3(Mathf.Sqrt(3f) / 2f, 0, 1f / 2f);
            Vector3 BottomRight = new Vector3(Mathf.Sqrt(3f) / 2f, 0, -1f / 2f);
            Gizmos.DrawLine(HexWorldPos + Top, HexWorldPos + TopRight);
            Gizmos.DrawLine(HexWorldPos + TopRight, HexWorldPos + BottomRight);
            Gizmos.DrawLine(HexWorldPos + BottomRight, HexWorldPos - Top);
            Gizmos.DrawLine(HexWorldPos - Top, HexWorldPos - TopRight);
            Gizmos.DrawLine(HexWorldPos - TopRight, HexWorldPos - BottomRight);
            Gizmos.DrawLine(HexWorldPos - BottomRight, HexWorldPos + Top);
        }

        //Draw Path
        if (hexPath != null && blnShowPath)
        {
            Gizmos.color = Color.red;
            for (int intT = 0; intT < hexPath.Length-1; intT++)
            {
                Gizmos.DrawLine(hexPath.Get(intT).ToWorldPosition(), hexPath.Get(intT+1).ToWorldPosition());
            }
        }
	}
    #endregion
}
