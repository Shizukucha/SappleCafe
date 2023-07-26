using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallMover : MonoBehaviour
{

    public void ShakeWall()
    {
        transform.DOPunchPosition(new Vector3(0, 5f, 0), 0.3f, 1, 1f);
    }


}
