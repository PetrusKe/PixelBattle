using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfig;

namespace GameUtils
{
    public class CameraController : MonoBehaviour
    {

        /*[HideInInspector]*/
        private Transform[] targetsPos;

        private CameraConfig cameraConf;

        private Camera useCamera;

        // tmp params?
        private Vector3 moveVelocity;
        private float zoomVeloctiy;


        // Use this for initialization
        void Start()
        {
            cameraConf = CameraConfig.getInstance();
            useCamera = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // TODO(Petrus): Need fix it later
            CheckCamera();
            AdjustCamera();
        }


        private void CheckCamera()
        {

        }

        private void AdjustCamera()
        {
            if (targetsPos != null)
            {
                // move camera
                Vector3 cameraWatchPos = FindAveragePos();
                transform.position =
                    Vector3.SmoothDamp(transform.position, cameraWatchPos, ref moveVelocity, cameraConf.dampTime);

                // resize camera
                float requiredSize = FindRequirdSize(cameraWatchPos);
                useCamera.orthographicSize =
                    Mathf.SmoothDamp(useCamera.orthographicSize, requiredSize, ref zoomVeloctiy, cameraConf.dampTime);
            }

        }

        private Vector3 FindAveragePos()
        {
            Vector3 avgPos = new Vector3();

            for (int i = 0; i < targetsPos.Length; i++)
            {
                if (!targetsPos[i].gameObject.activeSelf)
                    // if target not active, not handle it
                    continue;
                avgPos += targetsPos[i].position;
            }

            if (targetsPos.Length > 0)
                avgPos /= targetsPos.Length;

            avgPos.y = transform.position.y;
            return avgPos;
        }

        private float FindRequirdSize(Vector3 cameraWatchPos)
        {
            // change global pos to local pos
            Vector3 cameraLocalPos = transform.InverseTransformPoint(cameraWatchPos);
            float size = 0f;
            for (int i = 0; i < targetsPos.Length; i++)
            {
                if (!targetsPos[i].gameObject.activeSelf)
                    continue;

                Vector3 targetLocalPos = transform.InverseTransformPoint(targetsPos[i].position);
                Vector3 distance = targetLocalPos - cameraLocalPos;

                // in camera local axis, only x,y like 1920*1080 in the screen.
                // find max distance between cameraRig and targets.
                size = Mathf.Max(size, Mathf.Abs(distance.y));
                size = Mathf.Max(size, Mathf.Abs(distance.x) / useCamera.aspect);
            }

            size += cameraConf.screenEdgeBuffer;
            size = Mathf.Max(size, cameraConf.minSize);
            return size;
        }

        public void SetStartCamera()
        {
            // reset camera immediately
            transform.position = FindAveragePos();
            useCamera.orthographicSize = FindRequirdSize(transform.position);
        }

        public void CapturePlayers()
        {
            Debug.Log("------------");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length);
            targetsPos = new Transform[players.Length];
            for (int i = 0; i < players.Length; i++)
                targetsPos[i] = players[i].transform;
        }
    }
}
