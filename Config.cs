using ZModules.GameConfiguration.ConfigLoader;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using ZModules.Logger;

namespace _Main._Core.Scripts.Configs
{
    public class Config : MonoBehaviour
    {
        [SerializeField] private string m_configPath;
        [SerializeField] ConfigData m_configData = default;

        public Settings Settings => m_configData.settings[0];
        public List<Weapons> Weapons => m_configData.weapons.ToList();
        public List<Units> Units => m_configData.units.ToList();

        private static string LoadResourceFile(string path)
        {
            return Resources.Load<TextAsset>(path).text;
        }

    #if UNITY_EDITOR
        [Button]
        public void InitInEditor()
        {
            LoadConfig();
        }
    #endif

        private ConfigData ReadConfig(string json, bool suppressExceptions = true)
        {
            if (String.IsNullOrEmpty(json))
            {
                ZDebug.LogError("Config is empty");
                return null;
            }

            try
            {
                return JsonUtility.FromJson<ConfigData>(json);
            }
            catch (Exception e)
            {
                if (suppressExceptions)
                    ZDebug.LogError("Update config exception " + e.Message);
                else
                    throw e;
            }

            return null;
        }

        private void LoadConfig()
        {
            m_configData = ReadConfig(LoadResourceFile(m_configPath));
        }

        private void OnEnable()
        {
            LoadConfig();
        }
    }
}