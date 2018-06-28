// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

// Based on
// Original generated by Fuzzlyn on 2018-06-20 00:58:58
// Seed: 11049252875418439527
// Reduced from 97.5 KiB to 0.5 KiB
// Debug: Outputs -1
// Release: Outputs -65536

// Similar to other variants but using a 3 byte struct instead of 6.

struct S0
{
    public byte F0;
    public byte F1;
    public byte F2;
}

struct S1
{
    public S0 F3;
    public sbyte F4;
    public short F0;
    public S1(sbyte f4): this()
    {
        F4 = f4;
    }
}

public class GitHub_18522_7
{
    static S1 s_6;
    static S1[] s_13 = new S1[]{new S1(-1)};
    public static int Main()
    {
        // When generating code for the x64 SysV ABI, the jit was
        // incorrectly typing the return type from M16, and so
        // inadvertently overwriting the F4 field of s_13[0] on return
        // from the call.
        // 
        // Here we make sure we properly handle the failed inline case.
        s_13[0].F3 = M16();
        return s_13[0].F4 == -1 ? 100 : 0;
    }

    static S0 M16()
    {
        // This bit of code is intended to allow M16 to be an
        // inline candidate that ultimately does not get inlined.
        short x = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                x++;
            }
        }
        s_6.F0 = x;

        return s_6.F3;
    }
}
