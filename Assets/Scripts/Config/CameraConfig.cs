using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameConfig
{
    public class CameraConfig : BaseConfig
    {
        private string[] cameraParamsMap = {
            "dampTime",
            "screenEdgeBuffer",
            "minSize",

        };

        protected override string[] ParamsMap
        {
            get { return cameraParamsMap; }
        }

        public float dampTime { get; private set; }
        public float screenEdgeBuffer { get; private set; }
        public float minSize { get; private set; }



        private static CameraConfig _instance = new CameraConfig();

        private CameraConfig()
        {
            // Just for test.
            dampTime = 0.2f;
            screenEdgeBuffer = 5f;
            minSize = 7f;
        }

        public static CameraConfig getInstance()
        {
            return _instance;
        }
    }
}


