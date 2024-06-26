﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace GitCodeSearch.Model
{
    public static class ExplorerHelper
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

        public static void OpenFolderAndSelectItem(string folderPath, string file)
        {
            SHParseDisplayName(folderPath, IntPtr.Zero, out nint nativeFolder, 0, out _);

            if (nativeFolder == IntPtr.Zero)
            {
                // Log error, can't find folder
                return;
            }

            SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nint nativeFile, 0, out _);

            IntPtr[] fileArray;
            if (nativeFile == IntPtr.Zero)
            {
                // Open the folder without the file selected if we can't find the file
                fileArray = [];
            }
            else
            {
                fileArray = [nativeFile];
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(nativeFile);
            }
        }

        public static void OpenFileInDefaultProgram(string filePath)
        {
            try
            {
                using Process explorerProcess = new();
                explorerProcess.StartInfo.FileName = "explorer";
                explorerProcess.StartInfo.Arguments = $"\"{filePath}\"";
                explorerProcess.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
