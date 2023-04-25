using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Services.Core.Internal
{
    /// <summary>
    /// Locally override <see cref="JsonConvert.DefaultSettings"/>.
    /// </summary>
    sealed class JsonConvertDefaultSettingsScope : IDisposable
    {
        bool m_HasRegisteredSettings;

        internal readonly Func<JsonSerializerSettings> Callback;

        public JsonConvertDefaultSettingsScope()
            : this((JsonSerializerSettings)null) {}

        public JsonConvertDefaultSettingsScope(JsonSerializerSettings invalidSettings, bool autoApply = true)
        {
            m_HasRegisteredSettings = false;
            Callback = () => invalidSettings;
            if (autoApply)
            {
                Apply();
            }
        }

        public JsonConvertDefaultSettingsScope(IEnumerable<JsonConverter> converters, bool autoApply = true)
            : this(
                new JsonSerializerSettings
                {
                    Converters = converters is null
                        ? new List<JsonConverter>()
                        : new List<JsonConverter>(converters),
                },
                autoApply) {}

        ~JsonConvertDefaultSettingsScope() => Revert();

        public void Apply()
        {
            if (m_HasRegisteredSettings)
            {
                JsonConvert.DefaultSettings -= Callback;
            }

            JsonConvert.DefaultSettings += Callback;
            m_HasRegisteredSettings = true;
        }

        public void Revert()
        {
            if (!m_HasRegisteredSettings)
                return;

            JsonConvert.DefaultSettings -= Callback;
            m_HasRegisteredSettings = false;
        }

        void IDisposable.Dispose() => Revert();
    }
}
