//Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;

// **************************************************************
// * Raw implementation of the MD5 hash algorithm
// * from RFC 1321.
// *
// * Written By: Reid Borsuk
// * Copyright (c) Microsoft Corporation.  All rights reserved.
// **************************************************************

[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
[assembly: CLSCompliant(true)]
class test
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("\tUsage:\n\t\tMD5Test.exe [<string to hash>|-v|-t]");
            Console.WriteLine("\t\t\t-v\tRun test vectors");
            Console.WriteLine("\t\t\t-t\tRun performance test");
            return;
        }
        if (args[0] == "-t")
        {
            Console.WriteLine("Running timing test...");
            Timing(0x100, 16000000);
            Timing(0x100000, 6000);
            Timing(0x10000000, 22);
            return;
        }
        else if (args[0] == "-v")
        {
            Console.WriteLine("Running test vectors...");
            int retvalT = 100;

            retvalT &= TestVector("Silverlight", "9A06BECFED4DFCC83F66406A1DB1BC6C");

            //Length is congruent to 448 mod 512. The easist way to handle this would be to skip the padding step, a violation of the standard
            retvalT &= TestVector("12345678901234567890123456789012345678901234567890123456789012", "A29FBA1F76305E4754853AFB94525918");

            //Length is 0 mod 512. Ensure we add a second block to contain padding and encoded length
            retvalT &= TestVector("1234567890123456789012345678901234567890123456789012345678901234", "EB6C4179C0A7C82CC2828C1E6338E165");

            //Length is 1600, and second & third blocks are different from the first. Ensure we are picking up blocks after the first one and before the last one (See bug ID# 1)
            retvalT &= TestVector("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", "8BE2CE74BF5FB83C9F391C8B2C3DF5BD");

            //Length is 1512, and second & third blocks are different from the first. In this case, the third block contains padding, but not the encoded length
            //Since this code path is different from the last block containing both padding & length, ensure we have a specific test case
            retvalT &= TestVector("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "A6B47F9BEAB51270E6BC9D5588032E28");

            if (100 == retvalT)
                Console.WriteLine("ALL TEST VECTORS PASS");
            else
                Console.WriteLine("***SOME OR ALL TEST VECTORS FAILED!***");
            return;
        }

        System.Text.UTF8Encoding decoder = new System.Text.UTF8Encoding();
        byte[] target = decoder.GetBytes(args[0]);

        Console.WriteLine("Hashing: {0}", args[0]);
        string retval = BitConverter.ToString(MD5Core.GetHash(target));
        retval = retval.Replace("-", "");
        Console.WriteLine("\t\tOutput: \t{0,30}", retval);

        return;
    }

    static void Timing(uint size, uint iterations)
    {
        Console.WriteLine("Hashing {0} (0x{0:X}) bytes of data for {1} iterations.", size, iterations);

        Stopwatch time = new Stopwatch();

        byte[] target = new byte[size];
        for (int j = 0; j < size; j++)
            target[j] = unchecked((byte)j); //Otherwise parts of the array are optimised out.

        MD5Core.GetHash(target);

        time.Start();
        for (int i = 1; i <= iterations; i++)
        {
            target[0] = unchecked((byte)i);
            MD5Core.GetHash(target);
        }
        time.Stop();

        TimeSpan ts = time.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format(CultureInfo.CurrentCulture, "Total time for core iterations: \t{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine(elapsedTime, "RunTime");

        return;

    }

    static int TestVector(string target, string expected)
    {
        Encoding encoding = new UTF8Encoding();
        bool success = false;

        Console.WriteLine("String: \"{0}\"", target);
        string retval = BitConverter.ToString(MD5Core.GetHash(target));
        retval = retval.Replace("-", "");
        Console.WriteLine("\t\tOutput (as byte): \t{0,30}", retval);

        success = (retval == expected.ToUpper(CultureInfo.InvariantCulture));

        retval = MD5Core.GetHashString(target);
        Console.WriteLine("\t\tOutput (as string): \t{0,30}", retval);
        success &= (retval == expected.ToUpper(CultureInfo.InvariantCulture));

        MD5Managed managed = new MD5Managed();
        byte[] targetB = encoding.GetBytes(target);
        retval = BitConverter.ToString(managed.ComputeHash(targetB));
        retval = retval.Replace("-", "");
        Console.WriteLine("\t\tOutput (MD5Managed): \t{0,30}", retval);
        success &= (retval == expected.ToUpper(CultureInfo.InvariantCulture));

        MD5Managed md5stream = new MD5Managed();
        CryptoStream cs = new CryptoStream(CryptoStream.Null, md5stream, CryptoStreamMode.Write);
        cs.Write(targetB, 0, targetB.Length/2);
        cs.Write(targetB, targetB.Length/2, (targetB.Length/2) + targetB.Length%2);
        cs.Close();
        retval = BitConverter.ToString(md5stream.Hash);
        retval = retval.Replace("-", "");
        Console.WriteLine("\t\tOutput (MD5Stream): \t{0,30}", retval);
        success &= (retval == expected.ToUpper(CultureInfo.InvariantCulture));


        if (success)
        {
            Console.WriteLine("PASSED.\n");
            return 100;
        }
        else
        {
            Console.WriteLine("FAILED!\t\tExpected:\t\t{0,30}\n", expected.ToUpper(CultureInfo.InvariantCulture));
            return 1;
        }
    }
}