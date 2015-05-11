// ReSharper disable All

using Moonfish.Model;

using Moonfish.Tags;
using OpenTK;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Moonfish.Guerilla.Tags
{
    public partial class HsScriptsBlock : HsScriptsBlockBase
    {
        public HsScriptsBlock() : base()
        {
        }
    };

    [LayoutAttribute(Size = 40, Alignment = 4)]
    public class HsScriptsBlockBase : GuerillaBlock
    {
        internal Moonfish.Tags.String32 name;
        internal ScriptType scriptType;
        internal ReturnType returnType;
        internal int rootExpressionIndex;

        public override int SerializedSize
        {
            get { return 40; }
        }

        public override int Alignment
        {
            get { return 4; }
        }

        public HsScriptsBlockBase() : base()
        {
        }

        public override Queue<BlamPointer> ReadFields(BinaryReader binaryReader)
        {
            var blamPointers = new Queue<BlamPointer>(base.ReadFields(binaryReader));
            name = binaryReader.ReadString32();
            scriptType = (ScriptType) binaryReader.ReadInt16();
            returnType = (ReturnType) binaryReader.ReadInt16();
            rootExpressionIndex = binaryReader.ReadInt32();
            return blamPointers;
        }

        public override void ReadPointers(BinaryReader binaryReader, Queue<BlamPointer> blamPointers)
        {
            base.ReadPointers(binaryReader, blamPointers);
        }

        public override int Write(BinaryWriter binaryWriter, int nextAddress)
        {
            base.Write(binaryWriter, nextAddress);
            using (binaryWriter.BaseStream.Pin())
            {
                binaryWriter.Write(name);
                binaryWriter.Write((Int16) scriptType);
                binaryWriter.Write((Int16) returnType);
                binaryWriter.Write(rootExpressionIndex);
                return nextAddress;
            }
        }

        internal enum ScriptType : short
        {
            Startup = 0,
            Dormant = 1,
            Continuous = 2,
            Static = 3,
            Stub = 4,
            CommandScript = 5,
        };

        internal enum ReturnType : short
        {
            Unparsed = 0,
            SpecialForm = 1,
            FunctionName = 2,
            Passthrough = 3,
            Void = 4,
            Boolean = 5,
            Real = 6,
            Short = 7,
            Long = 8,
            String = 9,
            Script = 10,
            StringId = 11,
            UnitSeatMapping = 12,
            TriggerVolume = 13,
            CutsceneFlag = 14,
            CutsceneCameraPoint = 15,
            CutsceneTitle = 16,
            CutsceneRecording = 17,
            DeviceGroup = 18,
            Ai = 19,
            AiCommandList = 20,
            AiCommandScript = 21,
            AiBehavior = 22,
            AiOrders = 23,
            StartingProfile = 24,
            Conversation = 25,
            StructureBsp = 26,
            Navpoint = 27,
            PointReference = 28,
            Style = 29,
            HudMessage = 30,
            ObjectList = 31,
            Sound = 32,
            Effect = 33,
            Damage = 34,
            LoopingSound = 35,
            AnimationGraph = 36,
            DamageEffect = 37,
            ObjectDefinition = 38,
            Bitmap = 39,
            Shader = 40,
            RenderModel = 41,
            StructureDefinition = 42,
            LightmapDefinition = 43,
            GameDifficulty = 44,
            Team = 45,
            ActorType = 46,
            HudCorner = 47,
            ModelState = 48,
            NetworkEvent = 49,
            Object = 50,
            Unit = 51,
            Vehicle = 52,
            Weapon = 53,
            Device = 54,
            Scenery = 55,
            ObjectName = 56,
            UnitName = 57,
            VehicleName = 58,
            WeaponName = 59,
            DeviceName = 60,
            SceneryName = 61,
        };
    };
}