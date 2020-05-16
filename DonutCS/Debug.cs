﻿using System;
using System.IO;
using System.Runtime.InteropServices;

using Donut.Structs;

namespace Donut
{
    public class D
    {
        [System.Diagnostics.Conditional("DEBUG")]
        static public void Print(string printme)
        {
            Console.WriteLine($"[DEBUG] {printme}");
        }
        [System.Diagnostics.Conditional("DEBUG")]
        static public void WriteInst(DSConfig config, IntPtr instptr)
        {
            byte[] instbuff = new byte[config.inst_len];
            for (int i = 0; i < Convert.ToInt32(config.inst_len); i++)
            {
                instbuff[i] = Marshal.ReadByte(instptr + i);
            }
            File.WriteAllBytes(@"rawinstance.bin", instbuff);
            D.Print("Wrote raw instance to disk");
        }
        public static void TestEncrypt()
        {
            byte[] mk = { 0x85, 0xc4, 0x7b, 0x5e, 0x39, 0x37, 0xa7, 0xf1, 0xf7, 0x10, 0xc0, 0x06, 0xf9, 0xfb, 0x65, 0x2d };
            byte[] ctr = { 0xab, 0x86, 0x6b, 0x5d, 0xa7, 0xa0, 0xc9, 0x4f, 0x49, 0xcf, 0x1b, 0x99, 0x12, 0x6c, 0xaf, 0x2f };
            byte[] plain = { 0x21, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x6f, 0x6c, 0x65, 0x33, 0x32, 0x2e, 0x64, 0x6c };
            byte[] crypt = { 0xc0, 0xdb, 0x0b, 0xb7, 0xd0, 0x3f, 0xe6, 0x0d, 0x08, 0x88, 0x34, 0x52, 0xb0, 0x9e, 0x95, 0xb7 };
            byte[] output = new byte[16];

            GCHandle pinned = GCHandle.Alloc(crypt, GCHandleType.Pinned);
            IntPtr address = pinned.AddrOfPinnedObject();

            Helper.Encrypt(mk, ctr, address, 16);
            for (int i = 0; i < 16; i++)
            {
                output[i] = Marshal.ReadByte(address + i);
            }
            pinned.Free();
        }
        public static void TestChasKey()
        {
            byte[] plain = { 0x56, 0x09, 0xe9, 0x68, 0x5f, 0x58, 0xe3, 0x29, 0x40, 0xec, 0xec, 0x98, 0xc5, 0x22, 0x98, 0x2f };
            byte[] key = { 0xb8, 0x23, 0x28, 0x26, 0xfd, 0x5e, 0x40, 0x5e, 0x69, 0xa3, 0x01, 0xa9, 0x78, 0xea, 0x7a, 0xd8 };
            byte[] cipher = { 0xd5, 0x60, 0x8d, 0x4d, 0xa2, 0xbf, 0x34, 0x7b, 0xab, 0xf8, 0x77, 0x2f, 0xdf, 0xed, 0xde, 0x07 };
            Helper.Chaskey(key, plain);
        }
    }
}
