using HtoolsPdump;
using System.Diagnostics;


class Program
{
    public static void Main(string[] args)
    {

        while (true)
        {
            Console.Write("OS_CLI> ");
            string command = Console.ReadLine();
            switch (command)
            {
                case ("list_procs"):
                    Dumper.ListProc();
                    break;
                case ("dump_proc_by_name"):
                    Console.Write("Name: ");
                    string procName = Console.ReadLine();
                    Dumper.DProcName(procName);
                    break;
                case ("dump_proc_by_pid"):
                    Console.Write("PID: ");
                    uint pid = uint.Parse(Console.ReadLine());
                    Dumper.DProcPid(pid);
                    break;
                case ("parse_dmp"):
                    Console.Write("Dmp File Path: ");
                    string pathValue = Console.ReadLine();
                    Dumper.SendDumpFile(pathValue);
                    break;
                case ("help"):
                    Console.WriteLine("Help: \n" +
                        "1. list_procs - List processes.\n" +
                        "2. dump_proc_by_name - Dump process by name.\n" +
                        "3. dump_proc_by_pid - Dump process by pid.\n" +
                        "4. send_dmp - send memory dump to http endpoint");
                    break;
                case ("exit"):
                    Process currentProcess = Process.GetCurrentProcess();
                    currentProcess.Kill();
                    break;
                default:
                    continue;
            }
        }
        
    }
}



