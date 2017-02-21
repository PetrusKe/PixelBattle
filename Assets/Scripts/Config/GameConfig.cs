using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameConfig
{
    public class BaseConfig
    {

        private string[] paramsMap = { };
        protected virtual string[] ParamsMap
        {
            get { return paramsMap; }
        }

        public BaseConfig() { }

        public object GetParam(object cls, string paramName)
        {
            Type clsType = cls.GetType();
            System.Reflection.PropertyInfo propertyInfo = clsType.GetProperty(paramName);
            var paramValue = propertyInfo.GetValue(cls, null);
            return paramValue;
        }
    }

    public class GlobalConfig : BaseConfig
    {

        // Include all other config blocks.
        private CameraConfig cameraConf;

        // Singleton
        private static GlobalConfig _instance = new GlobalConfig();

        /// <summary>
        /// Init func.
        /// </summary>
        private GlobalConfig()
        {
            cameraConf = CameraConfig.getInstance();
        }

        public static GlobalConfig getInstance()
        {
            return _instance;
        }
    }
}
