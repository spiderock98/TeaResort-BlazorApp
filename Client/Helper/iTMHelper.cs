using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRetail.Client.Helper
{
    public static class iTMHelper
    {

        // Acquires the connection status of air conditioners(D3 equipment)
        public static readonly uint ACConnectRequestCmd = 70000;
        public static readonly uint ACConnectResponesCmd = 70001;

        //Acquires the property of air conditioners (D3 equipment)
        public static readonly uint ACPropertyRequestCmd = 70002;
        public static readonly uint ACPropertyResponesCmd = 70003;

        //Acquires the current status of air conditioners (D3 equipment)
        public static readonly uint ACStatusRequestCmd = 70004;
        public static readonly uint ACStatusResponesCmd = 70005;

        //Makes settings on air conditioners(D3 equipment)
        public static readonly uint ACStatusSetCmd = 70006;
        public static readonly uint ACStatusSetResponeCmd = 70007;

        // Create Request Byte Array -------------------------------

        /// <summary>
        ///  Acquires the connection status of air conditioners(D3 equipment)
        /// </summary>
        /// <returns></returns>
        public static byte[] CreateACConnectionStatusCmd()
        {
            var data = new List<byte>() {   0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0};
            var outValue = CreateCommand(ACConnectRequestCmd, data);
            return outValue.ToArray();
        }
        /// <summary>
        /// Acquires the property of air conditioners (D3 equipment)
        /// </summary>
        /// <returns></returns>
        public static byte[] CreateACPropertyCmd()
        {
            var data = new List<byte>() {   0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0};
            var outValue = CreateCommand(ACPropertyRequestCmd, data);
            return outValue.ToArray();
        }
        /// <summary>
        /// Acquires the current status of air conditioners (D3 equipment)
        /// </summary>
        /// <param name="address"> List Of String Address</param>
        /// <returns></returns>
        public static byte[] CreateACStatusCmd(List<string> address)
        {
            var data = new List<byte>() {   0,0,0,0,
                                            0,0,0,0};

            var convertAddress = ConvertAddressToPoint(address);
            for (int i = 0; i < 64; i++)
            {
                byte cellByte = 0;
                foreach (var point in convertAddress)
                {
                    if (point.Value.X == i)
                    {
                        SetBitIndex(ref cellByte, point.Value.Y, true);
                    }
                }
                data.Add(cellByte);
            }
            var outValue = CreateCommand(ACStatusRequestCmd, data);
            return outValue.ToArray();
        }


        /// <summary>
        /// Makes settings on air conditioners(D3 equipment)
        /// </summary>
        /// <param name="items"> List Of ACStatusItem </param>
        /// <returns></returns>
        public static byte[] CreateACStatusSetCmd(List<ACStatusSetItemRaw> items)
        {
            uint count = (uint)items.Count;
            var noOfAddress = BitEndianConverter.GetBytes(count, true);
            var data2 = new List<byte>() {  0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0,
                                            0,0,0,0};
            var data = noOfAddress.ToList();
            data.AddRange(data2);

            for (int i = 0; i < items.Count; i++)
            {
                var itemBytes = convertACStatusItemToArray(items[i]);
                data.AddRange(itemBytes);
            }

            var outValue = CreateCommand(ACStatusSetCmd, data);
            return outValue.ToArray();
        }

        // End ----------------------------------------------------------------
        // Decoder Byte Array Respone.
        /// <summary>
        /// Acquires the connection status of air conditioners(D3 equipment)
        /// </summary>
        public static List<bool?> DecodeACConnectionStatusRespone(byte[] arrayData)
        {
            var lenght = BitConverter.ToInt32(arrayData, 0);
            if (lenght != arrayData.Length) return null;

            var cmd = BitConverter.ToInt32(arrayData, 4);
            if (cmd != ACConnectResponesCmd) return null;

            var outData = new List<bool?>();
            for (int i = 20; i < lenght; i++)
            {
                outData.Add(GetBitIndex(arrayData[i], 0));
                outData.Add(GetBitIndex(arrayData[i], 1));
                outData.Add(GetBitIndex(arrayData[i], 2));
                outData.Add(GetBitIndex(arrayData[i], 3));
                outData.Add(GetBitIndex(arrayData[i], 4));
                outData.Add(GetBitIndex(arrayData[i], 5));
                outData.Add(GetBitIndex(arrayData[i], 6));
                outData.Add(GetBitIndex(arrayData[i], 7));
            }

            return outData;
        }
        /// <summary>
        /// Acquires the property of air conditioners (D3 equipment)
        /// </summary>
        public static List<ACPropertyRespone> DecodeACPropertyCmdRespone(byte[] arrayData)
        {
            var lenght = BitConverter.ToInt32(arrayData, 0);
            if (lenght != arrayData.Length) return null;

            var cmd = BitConverter.ToInt32(arrayData, 4);
            if (cmd != ACPropertyResponesCmd) return null;

            List<ACPropertyRespone> outDatas = new List<ACPropertyRespone>();

            Encoding bytesUCS = Encoding.GetEncoding(1200);

            for (int i = 32; i < lenght; i += 84)
            {
                string shortName = bytesUCS.GetString(arrayData, i, 16).Replace("\0", "");
                var longName = bytesUCS.GetString(arrayData, i + 16, 64).Replace("\0", "");

                ACPropertyRespone _data = new ACPropertyRespone();

                _data.ShortName = shortName;
                _data.LongName = longName;

                var shortType = BitConverter.ToInt16(arrayData, i + 80);
                var shortInnerType = BitConverter.ToInt16(arrayData, i + 82);

                _data.Type = dicACType[shortType];
                _data.InnerType = dicACInnerType[shortInnerType];


                outDatas.Add(_data);
            }

            return outDatas;


        }
        /// <summary>
        /// Acquires the current status of air conditioners (D3 equipment)
        /// </summary>
        public static List<ACStatusResponeItem> DecodeACStatusRespone(byte[] arrayData)
        {
            var outData = new List<ACStatusResponeItem>();
            var lenght = BitConverter.ToInt32(arrayData, 0);
            if (lenght != arrayData.Length) return null;

            var cmd = BitConverter.ToInt32(arrayData, 4);
            if (cmd != ACStatusResponesCmd) return null;

            for (int i = 32; i < lenght; i += 32)
            {
                ACStatusResponeItem item = new ACStatusResponeItem()
                {
                    Address = ConvertIndexToAddress((int)BitConverter.ToUInt32(arrayData, i)),
                    Status = dicStatus.ContainsKey(BitConverter.ToInt16(arrayData, i + 4)) ?
                              dicStatus[BitConverter.ToInt16(arrayData, i + 4)] :
                              "",
                    ErrorCode = BitConverter.ToString(arrayData, i + 6, 2),
                    Power = dicPower.ContainsKey(BitConverter.ToUInt16(arrayData, i + 8)) ?
                            dicPower[BitConverter.ToUInt16(arrayData, i + 8)] :
                            null,
                    OperationMode = dicOperationMode[BitConverter.ToUInt16(arrayData, i + 10)],
                    VentilationMode = dicVentilationMode[BitConverter.ToUInt16(arrayData, i + 12)],
                    VentilationAmount = dicVentilationMode[BitConverter.ToUInt16(arrayData, i + 14)],
                    IsSetTemp = GetBitIndex(arrayData[i + 16], 0),
                    IsRoomTemp = GetBitIndex(arrayData[i + 16], 1),
                    RoomTemp = (float)Math.Round(BitConverter.ToSingle(arrayData, i + 20), 1),
                    SetTemp = BitConverter.ToSingle(arrayData, i + 24),
                    FanSpeed = dicFanSpeed[arrayData[i + 28]],
                    FanDirection = arrayData[i + 29],
                    FilterSign = (arrayData[i + 30] == 0) ? "Off" : "On",
                    DefrostHotStart = (arrayData[i + 31] == 0) ? "Off" : "On",
                };
                outData.Add(item);
            }
            return outData;

        }
        /// <summary>
        /// Makes settings on air conditioners(D3 equipment)
        /// </summary>
        public static List<bool?> DecodeACStatusSetRespone(byte[] arrayData)
        {
            var lenght = BitConverter.ToInt32(arrayData, 0);
            if (lenght != arrayData.Length) return null;

            var cmd = BitConverter.ToInt32(arrayData, 4);
            if (cmd != ACStatusSetResponeCmd) return null;

            var outData = new List<bool?>();
            for (int i = 20; i < lenght; i++)
            {
                outData.Add(GetBitIndex(arrayData[i], 0));
                outData.Add(GetBitIndex(arrayData[i], 1));
                outData.Add(GetBitIndex(arrayData[i], 2));
                outData.Add(GetBitIndex(arrayData[i], 3));
                outData.Add(GetBitIndex(arrayData[i], 4));
                outData.Add(GetBitIndex(arrayData[i], 5));
                outData.Add(GetBitIndex(arrayData[i], 6));
                outData.Add(GetBitIndex(arrayData[i], 7));
            }

            return outData;
        }

        // End ----------------------------------------------------------------
        public static List<byte> CreateCommand(uint CMD, List<byte> _data)
        {
            var sumData = new List<byte>();
            var data00 = BitEndianConverter.GetBytes(CMD, true).ToList();
            data00.AddRange(_data);

            var data = BitEndianConverter.GetBytes(data00.Count + 4, true);
            sumData.AddRange(data.ToList());
            sumData.AddRange(data00.ToList());

            return sumData;
        }

        // Struture -----------------------------------------------------------
        /// <summary>
        /// ACStatus Struture For Set
        /// </summary>
        public class ACStatusSetItemRaw
        {
            public ACStatusSetItemRaw()
            {

            }
            public ACStatusSetItemRaw(uint indexAdr, uint setBit, string power, string operationMode, string ventilationMode,
                                      string ventilationAmount, float setTemp, string fanSpeed, byte fanDirection, string filterResetSignal)
            {
                Address = indexAdr;
                SettingBit = setBit;
                Power = GetDictionary(dicPower, power);
                OperationMode = GetDictionary(dicOperationMode, operationMode);
                VentilationAmount = GetDictionary(dicVentilationAmount, ventilationAmount);
                VentilationMode = GetDictionary(dicVentilationMode, ventilationMode);
                SetTemp = setTemp;
                FanSpeed = GetDictionary(dicFanSpeed, fanSpeed);
                FanDirection = fanDirection;
                FilterSignReset = (filterResetSignal.ToUpper().Trim() == "ON") ? (byte)1 : (byte)0;
                Reserved6 = 0;
            }

            public uint Address;
            public uint SettingBit;

            public ushort Power;
            public ushort OperationMode;

            public ushort VentilationMode;
            public ushort VentilationAmount;

            public float SetTemp;
            public byte FanSpeed;
            public byte FanDirection;
            public byte FilterSignReset;
            public byte Reserved6;
        }

        // Struture -----------------------------------------------------------
        /// <summary>
        /// ACStatus Struture For Respone
        /// </summary>
        public struct ACStatusResponeItem
        {
            public string Address;
            public string Status;
            public string ErrorCode;

            public string Power;
            public string OperationMode;

            public string VentilationMode;
            public string VentilationAmount;

            public bool? IsSetTemp;
            public bool? IsRoomTemp;

            public float RoomTemp;
            public float SetTemp;
            public string FanSpeed;
            public int FanDirection;
            public string FilterSign;
            public string DefrostHotStart;
        }

        public static readonly Dictionary<int, string> dicACType = new Dictionary<int, string>()
        { [0] = "UnKnown", [6] = "Di", [7] = "Dio", [9] = "D3" };
        public static readonly Dictionary<int, string> dicACInnerType = new Dictionary<int, string>()
        { [0] = "VRV", [3] = "HRV", [4] = "D3Chiller", [5] = "Altherma", [6] = "InvChiller", [-1] = "Unknown" };

        public static readonly Dictionary<Int16, string> dicStatus = new Dictionary<Int16, string>()
        { [1] = "Normal ", [0] = "Error", [-1] = "Unconnected" };
        public static readonly Dictionary<UInt16, string> dicPower = new Dictionary<UInt16, string>()
        { [0] = "Off", [1] = "On", [2] = "Unknown" };

        public static readonly Dictionary<UInt16, string> dicOperationMode = new Dictionary<UInt16, string>()
        { [0x1000] = "Unknown", [0x0001] = "Fan", [0x0002] = "Heat", [0x0004] = "Cool", [0x0020] = "Ventilation", [0x0040] = "Dry", [0x0100] = "AutoHeat", [0x020] = "AutoCool" };

        public static readonly Dictionary<UInt16, string> dicVentilationMode = new Dictionary<UInt16, string>()
        { [0x1000] = "Unknown", [0x0001] = "Automatic", [0x0002] = "HeatExchange", [0x0004] = "Bypass" };

        public static readonly Dictionary<UInt16, string> dicVentilationAmount = new Dictionary<UInt16, string>()
        { [0x1000] = "Unknown", [0x0001] = " AutomaticNormal", [0x0002] = "WeakNormal", [0x0004] = " StrongNormal", [0x0008] = "AutomaticFreshUp", [0x0010] = "WeakFreshUp", [0x0020] = "StrongFreshUp" };

        public static readonly Dictionary<byte, string> dicFanSpeed = new Dictionary<byte, string>()
        { [0] = "Low", [1] = "Medium", [2] = "High", [100] = "Auto", [255] = "Unknown" };

        public static Int16 GetDictionary(Dictionary<Int16, string> dic, string _value)
        {
            foreach (var value in dic)
            {
                if (value.Value.Trim() == _value.Trim())
                    return value.Key;
            }
            throw new Exception("Wrong Parameter");
        }
        public static UInt16 GetDictionary(Dictionary<UInt16, string> dic, string _value)
        {
            foreach (var value in dic)
            {
                if (value.Value.Trim() == _value.Trim())
                    return value.Key;
            }
            throw new Exception("Wrong Parameter");
        }
        public static byte GetDictionary(Dictionary<byte, string> dic, string _value)
        {
            foreach (var value in dic)
            {
                if (value.Value.Trim() == _value.Trim())
                    return value.Key;
            }
            throw new Exception("Wrong Parameter");
        }

        public struct ACPropertyRespone
        {
            public string? ShortName;
            public string? LongName;
            public string? Type;
            public string? InnerType;
        }
        // Function -----------------------------------------------------------

        static List<byte> convertACStatusItemToArray(ACStatusSetItemRaw item)
        {
            var itemArray = new List<byte>();
            itemArray.AddRange(BitEndianConverter.GetBytes(item.Address, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.SettingBit, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.Power, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.OperationMode, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.VentilationMode, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.VentilationAmount, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.SetTemp, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.FanSpeed, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.FanDirection, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.FilterSignReset, true).ToList());
            itemArray.AddRange(BitEndianConverter.GetBytes(item.Reserved6, true).ToList());

            return itemArray;
        }
        static bool? GetBitIndex(byte data, int index)
        {
            if ((index < 0) || (index > 7)) return null;
            byte mask = (byte)(1 << index);
            bool? isSet = (data & mask) != 0;
            return isSet;
        }
        static void SetBitIndex(ref byte data, int index, bool value)
        {
            if ((index < 0) || (index > 7)) return;
            byte mask = (byte)(1 << index);
            if (value)
                data |= mask;
            else
            {
                var NegMask = (byte)~mask;
                data &= NegMask;
            }
        }

        public static Point? ConvertIndexToPoint(int index)
        {
            var outPoint = new Point();
            try
            {
                outPoint.X = index / 8;
                outPoint.Y = index % 8;

                return outPoint;
            }
            catch
            {
                return null;
            }
        }

        public static List<string?> ConvertIndexToAddress(List<int> indexs)
        {
            var points = new List<string?>();
            foreach (var adr in indexs)
            {
                var pointAdrr = ConvertIndexToAddress(adr);
                points.Add(pointAdrr);
            }
            return points;
        }

        public static string? ConvertIndexToAddress(int index)
        {
            try
            {
                return ((index / 16) / 4 + 1).ToString() + ":" + ((index / 16) % 4 + 1).ToString() + "-" + (index % 16).ToString("00");
            }
            catch
            {
                return null;
            }
        }
        static List<Point?> ConvertAddressToPoint(List<string> address)
        {
            var points = new List<Point?>();
            foreach (var adr in address)
            {
                var pointAdrr = ConvertAddressToPoint(adr);
                points.Add(pointAdrr);
            }
            return points;
        }
        static Point? ConvertAddressToPoint(string address)
        {
            var outPoint = new Point();
            try
            {
                var buffers = address.Split("-".ToCharArray());
                var index = int.Parse(buffers[1].Trim());

                var firstBuffs = buffers[0].Trim().Split(":".ToCharArray());
                var firstAddress = (int.Parse(firstBuffs[0]) - 1) * 4 + int.Parse(firstBuffs[1]) - 1;

                outPoint.X = firstAddress * 2 + index / 8;
                outPoint.Y = index % 8;

                return outPoint;
            }
            catch
            {
                return null;
            }
        }
        public static int? ConvertAddressToIndex(string address)
        {
            var outIndex = 0;
            try
            {
                var buffers = address.Split("-".ToCharArray());
                var index = int.Parse(buffers[1].Trim());

                var firstBuffs = buffers[0].Trim().Split(":".ToCharArray());
                var firstAddress = (int.Parse(firstBuffs[0]) - 1) * 4 + int.Parse(firstBuffs[1]) - 1;

                outIndex = firstAddress * 16 + index;


                return outIndex;
            }
            catch
            {
                return null;
            }
        }
        static List<byte> convertStringListToBytes(string[] values, int lenght)
        {
            List<byte> outs = new List<byte>();
            if ((values.Length == 1) && (values[0] == ""))
            {
                if (lenght > 0)
                {

                    for (int i = 0; i < lenght; i++)
                    {
                        outs.Add(0);
                    }
                }

                return outs;
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        byte val = byte.Parse(values[i]);
                        outs.Add(val);
                    }
                    catch { outs.Add(0); }
                }
                return outs;
            }
        }

    }

    public static class BitEndianConverter
    {
        /// <summary>
        /// Convert Bool => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(bool value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert Char => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(char value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert Double => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(double value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert float => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(float value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert int => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert Long => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(long value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert Short => Bytes with Little Endian
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(short value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert Uint => Bytes
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(uint value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert ulong => Bytes
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ulong value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        /// <summary>
        /// Convert ushort => Bytes
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ushort value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }
        private static byte[] ReverseAsNeeded(byte[] bytes, bool wantsLittleEndian)
        {
            if (wantsLittleEndian == BitConverter.IsLittleEndian)
                return bytes;
            else
                return (byte[])bytes.Reverse().ToArray();
        }
    }
}
