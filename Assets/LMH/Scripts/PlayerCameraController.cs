using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Speed Adjust")]
    [SerializeField] private Camera mainCamera;

    [Header("Camera Speed Adjust")]
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraMouseMoveSpeed;
    [SerializeField] private float cameraMouseZoomSpeed;

    [Header("ZoomLimit")]
    [SerializeField] private float cameraZoomInLimit;
    [SerializeField] private float cameraZoomOutLimit;
    private Vector3 inputKeyPos;
    private Vector2 inputMousePos;
    private Vector3 hitRayPos;
    private float mouseScroll;

    private int layerNum;
    private string hitTarget;

    private void Update()
    {
        KeyMove();
        MouseMove();
        MouseZoomInOut();
        DetectCamera();
    }

    private void KeyMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        inputKeyPos = new Vector3(x, 0, z).normalized;

        transform.Translate(inputKeyPos * cameraMoveSpeed * Time.deltaTime, Space.World);
    }

    private void MouseMove()
    {
        if(Input.GetMouseButtonDown(1))
        {
            inputMousePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(1))
        {
            Vector2 preMousePos = (Vector2)Input.mousePosition - inputMousePos;
            Vector3 isMovedPos = new Vector3(preMousePos.x, 0, preMousePos.y);
            transform.Translate(isMovedPos * cameraMouseMoveSpeed * Time.deltaTime, Space.World);

            preMousePos = Input.mousePosition;
        }
    }

    private void MouseZoomInOut()
    {
        mouseScroll = Input.mouseScrollDelta.y;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - mouseScroll, cameraZoomInLimit, cameraZoomOutLimit);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, hitRayPos);
    }

    private void DetectCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                hitRayPos = hitInfo.point;
                layerNum = hitInfo.collider.gameObject.layer;
                hitTarget = LayerMask.LayerToName(layerNum);
                Debug.Log($"���� ���� ��ġ {hitRayPos}"); // ��ġ Ȯ�ο� �����
                Debug.Log($"���� ���� ��� {hitTarget}"); // ��� Ȯ�ο� �����

            }
        }
    }
}
