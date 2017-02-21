using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameConfig
{

    /// <summary>
    /// A controller for game config.
    /// </summary>
    public class ConfigController : MonoBehaviour
    {
        private GlobalConfig globalConf;

        /// <summary>
        /// Init all config for game.
        /// </summary>
        void Awake()
        {
            globalConf = GlobalConfig.getInstance();
            this.LoadConfigFile(globalConf);
        }

        /// <summary>
        /// Load all game config from external config file.
        /// Now is json.
        /// </summary>
        private void LoadConfigFile(GlobalConfig conf)
        {

        }
    }
}

