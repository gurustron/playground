using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace ClearBufferTest {
    internal unsafe class Program {
        static byte[]? ByteFrame;
        static byte[]? ByteFrameClearer;
        static Int32[]? Int32Frame;
        static Int32[]? Int32FrameClearer;
        static float[]? FloatFrame;
        static float[]? FloatFrameClearer;
        static int[]? ResetCacheArray;
        static string ParseDouble(double d) {
            string doubleString = d.ToString();
            doubleString = doubleString.Replace(".", ",");
            if (doubleString.IndexOf(',') > -1 && doubleString.Length > doubleString.IndexOf(',')+3 ) {
                doubleString = doubleString.Substring(0, doubleString.IndexOf(',') + 3);
            }
            return doubleString;
        }
        static void Main(string[] args) {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            // size vars
            int Width = 4500;
            int Height = 4500;
            Console.WriteLine(Width+"*"+Height);
            //
            byte byteValue = (byte)5;
            int intValue = 5;
            int int64Value = 5;
            float floatValue = 5f;
            // Init frames
            ByteFrame = new byte[Width * Height * 4];
            ByteFrame.AsSpan().Fill(byteValue);
            ByteFrameClearer = new byte[Width * Height * 4];
            ByteFrameClearer.AsSpan().Fill(byteValue);
            Int32Frame = new Int32[Width * Height];
            Int32Frame.AsSpan().Fill(intValue);
            Int32FrameClearer = new Int32[Width * Height];
            Int32FrameClearer.AsSpan().Fill(intValue);
            FloatFrame = new float[Width * Height];
            FloatFrame.AsSpan().Fill(floatValue);
            FloatFrameClearer = new float[Width * Height];
            FloatFrameClearer.AsSpan().Fill(floatValue);
            ResetCacheArray = new int[20000 * 10000];
            ResetCacheArray.AsSpan().Fill(intValue);


            // warmup jitter
            for (int i = 0; i < 150; i++) {
                ClearByteFrameAsSpanFill(byteValue);
                ClearByteFrameArrayFill(byteValue);
                ClearByteFrameAsByteAVX(byteValue);
                ClearByteFrameAsByteAVXUnRolled(byteValue);
                ClearByteFrameAsByteAVXThreaded(byteValue, 12);
                ClearByteFrameMarshalCopy(byteValue);
                ClearByteFrameMarshalCopy4(byteValue);
                ClearByteFrameMarshalThreaded(byteValue, 12);
                ClearByteFrameNaive(byteValue);
                ClearByteFrameBytePointer(byteValue);
                ClearByteFrameInt32Pointer(intValue);
                ClearByteFrameInt32PointerThreads(intValue, 12);
                ClearByteFrameInt64Pointer(int64Value);
                ClearByteFrameInt64PointerThreads(int64Value, 12);

                ClearInt32FrameAsSpanFill(intValue);
                ClearInt32FrameArrayFill(intValue);

                ClearFloatFrameAsSpanFill(floatValue);
                ClearFloatFrameArrayFill(floatValue);

                ClearByteFrameAsIntSpanFill(intValue);
                ClearByteFrameAsLongSpanFill(int64Value);

                ClearCache();
            }


            Console.WriteLine(Environment.Is64BitProcess);

            int TestIterations;
            double nanoseconds;
            double MsDuration;
            double MB = 0;
            double MBSec;
            double GBSec;
            List<double> MSAr = new List<double>();
            List<double> MBSAr = new List<double>();
            TestIterations = 20;

            void Run(Func<double> f, string fName)
            {
                for (int i = 0; i < TestIterations; i++) {
                    nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                    MB = ClearByteFrameAsSpanFill(byteValue);
                    MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                    MSAr.Add(MsDuration);
                    MBSec = (MB / MsDuration) * 1000;
                    MBSAr.Add(MBSec);
                    ClearCache();
                }
                MsDuration = MSAr.Sum() / TestIterations;
                MBSec = MBSAr.Sum() / TestIterations;
                GBSec = MBSec / 1000;
                Console.WriteLine(
                    $"{fName}:              MS:{ParseDouble(MsDuration)} GB/s:{ParseDouble(GBSec)} MB/s:{ParseDouble(MBSec)}");
                MSAr.Clear();
                MBSAr.Clear();
            }

            Run(() => ClearByteFrameAsIntSpanFill(intValue), nameof(ClearByteFrameAsIntSpanFill));
            Run(() => ClearByteFrameAsLongSpanFill(int64Value), nameof(ClearByteFrameAsLongSpanFill));
            
            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsSpanFill(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsSpanFill:              MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameArrayFill(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameArrayFill:               MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVX(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVX:               MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXUnRolled(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXUnRolled:       MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXThreaded(byteValue, 2);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXThreaded(2):    MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXThreaded(byteValue, 4);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXThreaded(4):    MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXThreaded(byteValue, 6);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXThreaded(6):    MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXThreaded(byteValue, 8);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXThreaded(8):    MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameAsByteAVXThreaded(byteValue, 12);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameAsByteAVXThreaded(12):   MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalCopy(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalCopy:             MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalCopy4(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalCopy4:            MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalThreaded(byteValue, 2);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalThreaded(2):      MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalThreaded(byteValue, 4);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalThreaded(4):      MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalThreaded(byteValue, 6);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalThreaded(6):      MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalThreaded(byteValue, 8);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalThreaded(8):      MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameMarshalThreaded(byteValue, 12);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameMarshalThreaded(12):     MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameNaive(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameNaive:                   MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameBytePointer(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameBytePointer:             MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32Pointer(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32Pointer:            MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32PointerThreads(intValue, 2);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32PointerThreads(2):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32PointerThreads(intValue, 4);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32PointerThreads(4):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32PointerThreads(intValue, 6);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32PointerThreads(6):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32PointerThreads(intValue, 8);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32PointerThreads(8):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt32PointerThreads(intValue, 12);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt32PointerThreads(12): MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64Pointer(byteValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64Pointer:            MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64PointerThreads(int64Value, 2);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64PointerThreads(2):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64PointerThreads(int64Value, 4);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64PointerThreads(4):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64PointerThreads(int64Value, 6);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64PointerThreads(6):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64PointerThreads(int64Value, 8);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64PointerThreads(8):  MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearByteFrameInt64PointerThreads(int64Value, 12);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearByteFrameInt64PointerThreads(12): MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();


            Console.WriteLine();






            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearInt32FrameAsSpanFill(intValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearInt32FrameAsSpanFill:             MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearInt32FrameArrayFill(intValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearInt32FrameArrayFill:              MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();


            Console.WriteLine();








            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearFloatFrameAsSpanFill(floatValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearFloatFrameAsSpanFill:             MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();

            for (int i = 0; i < TestIterations; i++) {
                nanoseconds = 1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
                MB = ClearFloatFrameArrayFill(floatValue);
                MsDuration = (((1_000_000_000.0 * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - nanoseconds)) / 1000000;
                MSAr.Add(MsDuration);
                MBSec = (MB / MsDuration) * 1000;
                MBSAr.Add(MBSec);
                ClearCache();
            }
            MsDuration = MSAr.Sum() / TestIterations;
            MBSec = MBSAr.Sum() / TestIterations;
            GBSec = MBSec / 1000;
            Console.WriteLine("ClearFloatFrameArrayFill:              MS:" + ParseDouble(MsDuration) + " GB/s:" + ParseDouble(GBSec) + " MB/s:" + ParseDouble(MBSec));
            MSAr.Clear();
            MBSAr.Clear();



            Console.ReadLine();
        }
        static double ClearByteFrameAsSpanFill(byte clearValue) {
            ByteFrame.AsSpan().Fill(clearValue);
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameAsIntSpanFill(int clearValue) {
            var asSpan = ByteFrame.AsSpan();
            var cast = MemoryMarshal.Cast<byte, int>(asSpan);
            cast.Fill((int)clearValue);
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameAsLongSpanFill(long clearValue) {
            var asSpan = ByteFrame.AsSpan();
            var cast = MemoryMarshal.Cast<byte, long>(asSpan);
            cast.Fill(clearValue);
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameArrayFill(byte clearValue) {
            Array.Fill(ByteFrame, clearValue);
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameAsByteAVX(byte clearValue) {
            fixed (byte* ByteFramePTR = ByteFrame) {
                byte* ByteFramePTRIncrementor = (byte*)ByteFramePTR;
                byte[] FillAr = new byte[32];
                FillAr.AsSpan().Fill(clearValue);
                fixed (byte* FillArPTR = FillAr) {
                    Vector256<byte> FillVector = Avx2.LoadVector256(FillArPTR);
                    //int remainder = ByteFrame.Length % 32;
                    for (int i = 0; i <= ByteFrame.Length - 32; i += 32) {
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                    }
                }
            }
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameAsByteAVXUnRolled(byte clearValue) {
            fixed (byte* ByteFramePTR = ByteFrame) {
                byte* ByteFramePTRIncrementor = (byte*)ByteFramePTR;
                byte[] FillAr = new byte[32];
                FillAr.AsSpan().Fill(clearValue);
                fixed (byte* FillArPTR = FillAr) {
                    Vector256<byte> FillVector = Avx2.LoadVector256(FillArPTR);
                    //int remainder = ByteFrame.Length % 32;
                    for (int i = 0; i <= ByteFrame.Length - 96; i += 96) {
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                    }
                }
            }
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameAsByteAVXThreaded(byte clearValue, int numThreads) {
            int byteFrameLength = ByteFrame.Length;
            int amountPerThread = byteFrameLength / numThreads;

            CountdownEvent countdown = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClearByteFrameAsByteAvxThreadedDo), new object[] { i * amountPerThread, amountPerThread, countdown, clearValue });
            }
            countdown.Wait();
            return (float)ByteFrame.Length / 1000000;
        }
        static private void ClearByteFrameAsByteAvxThreadedDo(object state) {
            object[] array = state as object[];
            int from = Convert.ToInt32(array[0]);
            int amount = Convert.ToInt32(array[1]);
            CountdownEvent countdown = (CountdownEvent)array[2];
            byte clearValue = Convert.ToByte(array[3]);
            int test = Thread.CurrentThread.ManagedThreadId;

            fixed (byte* ByteFramePTR = ByteFrame) {
                byte* ByteFramePTRIncrementor = (byte*)ByteFramePTR;
                byte[] FillAr = new byte[32];
                FillAr.AsSpan().Fill(clearValue);
                fixed (byte* FillArPTR = FillAr) {
                    Vector256<byte> FillVector = Avx2.LoadVector256(FillArPTR);
                    ByteFramePTRIncrementor += from;
                    for (int i = from; i <= from+amount - 96; i += 96) {
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                        Avx2.Store(ByteFramePTRIncrementor, FillVector);
                        ByteFramePTRIncrementor += 32;
                    }
                }
            }
            countdown.Signal();
        }
        static double ClearByteFrameMarshalCopy(byte clearValue) {
            fixed (byte* f = ByteFrame) {
                Marshal.Copy(ByteFrameClearer, 0, (nint)f, ByteFrameClearer.Length);
            }
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameMarshalCopy4(byte clearValue) {
            fixed (byte* f = ByteFrame) {
                byte* p = (byte*)f;
                int iterations = 4;
                int iterationlength = ByteFrame.Length / iterations;
                for (int i = 0; i < iterations; i++) {
                    p = f + (i*iterationlength);
                    Marshal.Copy(ByteFrameClearer, i * iterationlength, (nint)p, iterationlength);
                }
            }
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameMarshalThreaded(byte clearValue, int numThreads) {
            int byteFrameLength = ByteFrame.Length;
            int amountPerThread = byteFrameLength / numThreads;

            CountdownEvent countdown = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClearBackBufferMarshalThreadedDo), new object[] { i * amountPerThread, amountPerThread, countdown });
            }
            countdown.Wait();
            return (float)ByteFrame.Length / 1000000;
        }
        static private void ClearBackBufferMarshalThreadedDo(object state) {
            object[] array = state as object[];
            int from = Convert.ToInt32(array[0]);
            int amount = Convert.ToInt32(array[1]);
            CountdownEvent countdown = (CountdownEvent)array[2];
            int test = Thread.CurrentThread.ManagedThreadId;

            bool debug = true;
            fixed (byte* f = ByteFrame) {
                byte* p = (byte*)f;
                p = f + from;
                Marshal.Copy(ByteFrameClearer, from, (nint)p, amount);
            }
            countdown.Signal();
        }
        static double ClearByteFrameNaive(byte clearValue) {
            for (int i = 0; i < ByteFrame.Length; i++) {
                ByteFrame[i] = clearValue;
            }
            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameBytePointer(byte clearValue) {

            fixed (byte* f = ByteFrame) {
                int x = 0;
                byte* p = (byte*)f;
                int maxloop = ByteFrame.Length - 32;
                for (; x < maxloop; x += 32) {
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                }
                maxloop = ByteFrame.Length - 16;
                for (; x < maxloop; x += 16) {
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                    *p++ = clearValue;
                }
                maxloop = ByteFrame.Length;
                for (; x < maxloop; x += 1) {
                    *p++ = clearValue;
                }
            }

            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameInt32Pointer(Int32 bgra) {

            //Int32 bgra = (clearValue << 24) + (clearValue << 16) + (clearValue << 8) + clearValue;
            fixed (byte* f = ByteFrame) {
                int x = 0;
                Int32* p = (Int32*)f;
                int maxloop = ByteFrame.Length - 128;
                for (; x < maxloop; x += 128) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = ByteFrame.Length - 64;
                for (; x < maxloop; x += 64) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = ByteFrame.Length - 4;
                for (; x < maxloop; x += 4) {
                    *p++ = bgra;
                }
            }

            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameInt32PointerThreads(Int32 bgra, int numThreads) {
            /*Int32[] bgraAr = new Int32[3];
            bgraAr[0] = (255 << 24) + (255 << 16) + (0 << 8) + 0;
            bgraAr[1] = (255 << 24) + (0 << 16) + (255 << 8) + 0;
            bgraAr[2] = (255 << 24) + (0 << 16) + (0 << 8) + 255;*/
            if (ByteFrame.Length < numThreads) {
                throw new Exception("buffer length < numthreads");
            }
            int backBufferLength = ByteFrame.Length / 4;
            int amountPerThread = backBufferLength / numThreads;

            CountdownEvent countdown = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClearBackBufferFromAmountInt32), new object[] { i * amountPerThread, amountPerThread, countdown, bgra });
            }
            countdown.Wait();

            return (float)ByteFrame.Length / 1000000;
        }
        static private void ClearBackBufferFromAmountInt32(object state) {
            object[] array = state as object[];
            int from = Convert.ToInt32(array[0]);
            int amount = Convert.ToInt32(array[1]);
            CountdownEvent countdown = (CountdownEvent)array[2];
            Int32 bgra = Convert.ToInt32(array[3]);
            int test = Thread.CurrentThread.ManagedThreadId;
            bool debug = true;
            fixed (byte* f = ByteFrame) {
                int x = 0;
                Int32* p = (Int32*)f;
                p += from;
                int maxloop = amount - 32;
                for (; x < maxloop; x += 32) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = amount - 16;
                for (; x < maxloop; x += 16) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = amount;
                for (; x < maxloop; x += 1) {
                    *p++ = bgra;
                }
            }
            countdown.Signal();
        }
        static double ClearByteFrameInt64Pointer(Int64 bgra) {

            //Int64 bgra = (clearValue << 24) + (clearValue << 16) + (clearValue << 8) + clearValue;
            fixed (byte* f = ByteFrame) {
                int x = 0;
                Int64* p = (Int64*)f;
                int maxloop = ByteFrame.Length - 256;
                for (; x < maxloop; x += 256) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = ByteFrame.Length - 128;
                for (; x < maxloop; x += 128) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = ByteFrame.Length - 8;
                for (; x < maxloop; x += 8) {
                    *p++ = bgra;
                }
            }

            return (float)ByteFrame.Length / 1000000;
        }
        static double ClearByteFrameInt64PointerThreads(Int64 bgra, int numThreads) {
            /*Int64[] bgraAr = new Int64[3];
            bgraAr[0] = (255 << 24) + (255 << 16) + (0 << 8) + 0;
            bgraAr[1] = (255 << 24) + (0 << 16) + (255 << 8) + 0;
            bgraAr[2] = (255 << 24) + (0 << 16) + (0 << 8) + 255;*/
            if (ByteFrame.Length < numThreads) {
                throw new Exception("buffer length < numthreads");
            }
            int backBufferLength = ByteFrame.Length / 8;
            int amountPerThread = backBufferLength / numThreads;

            CountdownEvent countdown = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClearBackBufferFromAmountInt64), new object[] { i * amountPerThread, amountPerThread, countdown, bgra });
            }
            countdown.Wait();

            return (float)ByteFrame.Length / 1000000;
        }
        static private void ClearBackBufferFromAmountInt64(object state) {
            object[] array = state as object[];
            int from = Convert.ToInt32(array[0]);
            int amount = Convert.ToInt32(array[1]);
            CountdownEvent countdown = (CountdownEvent)array[2];
            Int64 bgra = Convert.ToInt64(array[3]);
            int test = Thread.CurrentThread.ManagedThreadId;
            bool debug = true;
            fixed (byte* f = ByteFrame) {
                int x = 0;
                Int64* p = (Int64*)f;
                p += from;
                int maxloop = amount - 32;
                for (; x < maxloop; x += 32) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = amount - 16;
                for (; x < maxloop; x += 16) {
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                    *p++ = bgra;
                }
                maxloop = amount;
                for (; x < maxloop; x += 1) {
                    *p++ = bgra;
                }
            }
            countdown.Signal();
        }




        static double ClearInt32FrameAsSpanFill(Int32 clearValue) {
            Int32Frame.AsSpan().Fill(clearValue);
            return (float)(Int32Frame.Length * 4) / 1000000;
        }
        static double ClearInt32FrameArrayFill(Int32 clearValue) {
            Array.Fill(Int32Frame, clearValue);
            return (float)(Int32Frame.Length * 4) / 1000000;
        }






        static double ClearFloatFrameAsSpanFill(float clearValue) {
            FloatFrame.AsSpan().Fill(clearValue);
            return (float)(FloatFrame.Length * 4) / 1000000;
        }
        static double ClearFloatFrameArrayFill(float clearValue) {
            Array.Fill(FloatFrame, clearValue);
            return (float)(FloatFrame.Length * 4) / 1000000;
        }







        static void ClearCache() {
            ByteFrame.AsSpan().Fill((byte)255);
            Int32Frame.AsSpan().Fill((int)255);
            FloatFrame.AsSpan().Fill((float)255);
            int sum = 0;
            for (int i = 0; i < ResetCacheArray.Length; i++) {
                sum += ResetCacheArray[i];
            }
        }
    }
}