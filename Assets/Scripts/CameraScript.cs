using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラの操縦
public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3;
    //[SerializeField]
    //private float maxSpeed = 50;
    //[SerializeField]
    //private float smoothTime = 3;
    [SerializeField]
    private float startMoveSpeed = 6;
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private Transform LeftLimit;
    [SerializeField]
    private Transform RightLimit;
    [SerializeField]
    private Transform UpLimit;
    [SerializeField]
    private Transform BottomLimit;

    private Camera cameraComponet;
    //private Vector2 nowVelocity;

    public static bool isStart;

    void Start()
    {
        if (aimTarget == null)
        {
            aimTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }

        isStart = false;
        cameraComponet = GetComponent<Camera>();

        //カメラの初期位置を設定
        transform.position = new Vector3(RightLimit.position.x - cameraComponet.orthographicSize * cameraComponet.aspect, BottomLimit.position.y + cameraComponet.orthographicSize, -10);
    }

    private void Update()
    {
        
    }

    void LateUpdate()
    {
        if (isStart)
        {
            //開始した場合、Playerを追跡する
            Vector2 newPosition = Vector2.Lerp(transform.position, aimTarget.position, moveSpeed * Time.deltaTime);

            //Vector2 newPosition = Vector2.SmoothDamp(transform.position, aimTarget.position, ref nowVelocity, smoothTime, maxSpeed, Time.deltaTime);

            if (UpLimit != null)
            {
                if (newPosition.y + cameraComponet.orthographicSize > UpLimit.position.y)
                {
                    newPosition.y = UpLimit.position.y - cameraComponet.orthographicSize;
                }
            }
            if (BottomLimit != null)
            {
                if (newPosition.y - cameraComponet.orthographicSize < BottomLimit.position.y)
                {
                    newPosition.y = BottomLimit.position.y + cameraComponet.orthographicSize;
                }
            }
            if (LeftLimit != null)
            {
                if (newPosition.x - cameraComponet.orthographicSize * cameraComponet.aspect < LeftLimit.position.x)
                {
                    newPosition.x = LeftLimit.position.x + cameraComponet.orthographicSize * cameraComponet.aspect;
                }
            }
            if (RightLimit != null)
            {
                if (newPosition.x + cameraComponet.orthographicSize * cameraComponet.aspect > RightLimit.position.x)
                {
                    newPosition.x = RightLimit.position.x - cameraComponet.orthographicSize * cameraComponet.aspect;
                }
            }
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        else
        {
            //開始しなかった場合、右から左まで移動する
            transform.Translate(-startMoveSpeed * Time.deltaTime, 0, 0);
            if(transform.position.x - cameraComponet.orthographicSize * cameraComponet.aspect <= LeftLimit.position.x)
            {
                isStart = true;
            }
        }       
    }
}
