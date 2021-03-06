﻿using System;
using CardGameServer;

namespace CardProtocolLibrary
{
    public enum GameDataType
    {
        String,
        Int,
        Float,
        Double
    }

    public class GameData
    {
        public GameDataType Type;
        public object Data;

        public GameData(GameDataType type, object data)
        {
            Type = type;
            Data = data;
        }

        public static implicit operator int(GameData i)
        {
            return i.Int();
        }

        public static implicit operator double (GameData i)
        {
            return i.Double();
        }

        public static implicit operator float (GameData i)
        {
            return i.Float();
        }

        public static implicit operator string (GameData i)
        {
            return i.String();
        }

        public static implicit operator ulong(GameData i)
        {
            return i.Ulong();
        }

        public static implicit operator SID(GameData i)
        {
            return new SID(i.Ulong());
        }

        public static implicit operator GameData(double d)
        {
            return new GameData(GameDataType.Double, d);
        }

        public static implicit operator GameData(float f)
        {
            return new GameData(GameDataType.Float, f);
        }

        public static implicit operator GameData(int i)
        {
            return new GameData(GameDataType.Int, i);
        }

        public static implicit operator GameData(string s)
        {
            return new GameData(GameDataType.String, s);
        }

        public static implicit operator GameData(ErrorCode e)
        {
            return new GameData(GameDataType.Int, e);
        }

        public static implicit operator ErrorCode(GameData d)
        {
            if (d.Type == GameDataType.String)
            {
                return (ErrorCode) Enum.Parse(typeof (ErrorCode), (string) d.Data);
            }

            // if it's an int we don't need to parse it
            return (ErrorCode) d.Data;
        }

        public static implicit operator GameData(ConnectionPhase e)
        {
            return new GameData(GameDataType.Int, (int)e);
        }

        public static implicit operator ConnectionPhase(GameData d)
        {
            if (d.Type == GameDataType.String)
            {
                return (ConnectionPhase)Enum.Parse(typeof(ConnectionPhase), (string)d.Data);
            }

            return (ConnectionPhase)d.Data;
        }

        public string String()
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (Type)
            {
                case GameDataType.String:
                    return (string) Data;
                case GameDataType.Float:
                    return Data + "f";
                default:
                    return Data.ToString();
            }
        }

        public ulong Ulong()
        {
            if (Type != GameDataType.String) return (ulong) Data;
            return ulong.Parse((string) Data);
        }

        public int Int()
        {
            if (Type != GameDataType.String) return (int) Data;
            return int.Parse((string)Data);
        }

        public float Float()
        {
            if (Type != GameDataType.String) return (float)Data;
            return float.Parse((string)Data);
        }

        public double Double()
        {
            if (Type != GameDataType.String) return (double)Data;
            return double.Parse((string)Data);
        }

        public override string ToString()
        {
            return String();
        }
    }
}
