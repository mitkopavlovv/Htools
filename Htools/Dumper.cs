using System.Diagnostics;
using System.Runtime.InteropServices;


namespace HtoolsPdump
{
    static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFile(string lpFileName, EFileAccess dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, ECreationDisposition dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        public enum EFileAccess : uint
        {
            GenericWrite = 0x40000000,
        }

        public enum ECreationDisposition : uint
        {
            CreateAlways = 2,
        }
    }

    static class Dumper
    {
        [DllImport("DbgHelp.dll")]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint ProcessId, IntPtr hFile, uint DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

        public static void ListProc()
        {
            var procs = Process.GetProcesses();
            foreach (Process proc in procs)
            {
                Console.WriteLine($"Name: {proc.ProcessName}, PID: {proc.Id}.");
            }
        }
        public static void DProcPid(uint pid)
        {
            Console.WriteLine($"Work on pid {pid}");
            IntPtr hFile = NativeMethods.CreateFile($"HtoolsOutput{pid}.dmp", NativeMethods.EFileAccess.GenericWrite, 0, IntPtr.Zero, NativeMethods.ECreationDisposition.CreateAlways, 0, IntPtr.Zero);

            const uint miniDumpNormal = 0x00000000;
            var procs = Process.GetProcesses();
            foreach (Process proc in procs)
            {
                if (proc.Id == pid)
                {
                    if (hFile != IntPtr.Zero)
                    {
                        try
                        {
                            MiniDumpWriteDump(proc.Handle, (uint)proc.Id, hFile, miniDumpNormal, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                            Console.WriteLine($"Output HtoolsOutput{pid}.dmp");
                        }
                        finally
                        {
                            NativeMethods.CloseHandle(hFile);
                        }
                    }
                    break;
                }
            }

        }

        public static void DProcName(string procName)
        {
            Console.WriteLine($"Work on process {procName}");
            IntPtr hFile = NativeMethods.CreateFile($"HtoolsOutput{procName}.dmp", NativeMethods.EFileAccess.GenericWrite, 0, IntPtr.Zero, NativeMethods.ECreationDisposition.CreateAlways, 0, IntPtr.Zero);

            const uint miniDumpNormal = 0x00000000;
            var procs = Process.GetProcesses();
            foreach (Process proc in procs)
            {
                if (proc.ProcessName == "Notepad")
                {
                    if (hFile != IntPtr.Zero)
                    {
                        try
                        {
                            MiniDumpWriteDump(proc.Handle, (uint)proc.Id, hFile, miniDumpNormal, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                        }
                        finally
                        {
                            NativeMethods.CloseHandle(hFile);
                        }
                    }
                    break;
                }

            }
        }
    }
}
