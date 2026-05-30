using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace pylorak.TinyWall
{
    [DataContract(Namespace = "TinyWall")]
    public class UpdateModule
    {
        [DataMember, AllowNull]
        public string Component;
        [DataMember]
        public string? ComponentVersion;
        [DataMember]
        public string? DownloadHash;
        [DataMember]
        public string? UpdateURL;
    }

    [DataContract(Namespace = "TinyWall")]
    public class UpdateDescriptor : ISerializable<UpdateDescriptor>
    {
        public const string MODULE_NAME_MAINBIN = "TinyWall";
        public const string MODULE_NAME_HOSTS = "HostsFile";
        public const string MODULE_NAME_DATABASE = "Database";

        [DataMember]
        public string MagicWord = "TinyWall Update Descriptor";
        [DataMember]
        public UpdateModule[] Modules = Array.Empty<UpdateModule>();

        public JsonTypeInfo<UpdateDescriptor> GetJsonTypeInfo()
        {
            return SourceGenerationContext.Default.UpdateDescriptor;
        }

        public UpdateModule? GetModule(string moduleName)
        {
            for (int i = 0; i < Modules.Length; ++i)
            {
                if (Modules[i].Component.Equals(moduleName, StringComparison.InvariantCultureIgnoreCase))
                    return Modules[i];
            }

            return null;
        }
    }

    public class ServerState : ISerializable<ServerState>
    {
        public bool HasPassword = false;
        public bool Locked = false;
        public UpdateDescriptor? Update = null;
        public FirewallMode Mode = FirewallMode.Unknown;
        public List<MessageType> ClientNotifs = new();

        public JsonTypeInfo<ServerState> GetJsonTypeInfo()
        {
            return SourceGenerationContext.Default.ServerState;
        }
    }
}
