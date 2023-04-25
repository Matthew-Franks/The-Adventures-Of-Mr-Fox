using System;
using Newtonsoft.Json;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Editor
{
    static class JsonHelper
    {
        public static bool TryJsonDeserialize<T>(string json, ref T dest, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(json))
                return false;

            try
            {
                using (new JsonConvertDefaultSettingsScope(settings))
                {
                    dest = JsonConvert.DeserializeObject<T>(json);
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

            return false;
        }
    }
}
