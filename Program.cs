using System;
using System.IO;
using System.Security.Principal;

public class Program
{
    private static string[] _iobitServices = new string[]
    {
        "IUService",
        "AUpdate",
        "DSPut",
        "PPUninstaller",
        "CareScan",
        "Scheduler",
        "ScanWinUpd",
        "ProductStat3"
    };

    public static void Main()
    {
        Console.Title = "IOBitDebloater | Made by https://github.com/ZygoteCode/";

        if (!(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
        {
            Logger.LogError("The program is not run with Administrator privileges so it can not work correctly.");
            Logger.LogWarning("Press ENTER to exit from the program.");
            Console.ReadLine();
            return;
        }

        Logger.LogInfo("Welcome to IOBitDebloater, the first program to remove crap, useless, bloatwares, resource-consuming services from your installed IOBit programs.");
        Logger.LogWarning("If you are ready to proceed, press ENTER.");
        Console.ReadLine();

        string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1) + ":";
        Logger.LogSuccess($"Succesfully retrieved the main root directory: {rootDir}\\");
        Logger.LogWarning("Checking for the main IOBit folder.");

        string mainIOBitDir = $"{rootDir}\\Program Files\\IOBit";
        Logger.LogWarning($"Checking the folder '{mainIOBitDir}'.");

        if (!Directory.Exists(mainIOBitDir))
        {
            Logger.LogError($"The folder '{mainIOBitDir}' does not exist. Trying with another directory.");
            mainIOBitDir = $"{rootDir}\\Program Files (x86)\\IOBit";
            Logger.LogWarning($"Checking the folder '{mainIOBitDir}'.");

            if (!Directory.Exists(mainIOBitDir))
            {
                Logger.LogError($"The folder '{mainIOBitDir}' does not exist. Maybe, no IOBit program has been installed in this computer.");
                Logger.LogWarning("Press ENTER to exit from the program.");
                Console.ReadLine();
                return;
            }
        }

        Logger.LogSuccess($"Succesfully retrieved the main IOBit folder: '{mainIOBitDir}'.");
        Logger.LogWarning("If you are ready to proceed for the de-bloating process, press ENTER.");
        Console.ReadLine();

        string[] installedIOBitPrograms = Directory.GetDirectories(mainIOBitDir);

        if (installedIOBitPrograms.Length == 0)
        {
            Logger.LogError($"No folders for installed programs are created in the main IOBit folder ('{mainIOBitDir}').");
            Logger.LogWarning("Press ENTER to exit from the program.");
            Console.ReadLine();
            return;
        }

        string installedProgramsList = "Succesfully detected all the IOBit installed programs:\r\n";

        foreach (string installedProgram in installedIOBitPrograms)
        {
            if (Path.GetFileName(installedProgram).Equals("IOBit Unlocker"))
            {
                Logger.LogWarning("Skipping IOBit Unlocker since it's a free service offered by IOBit and has no services to have de-bloated.");
                continue;
            }

            installedProgramsList += $"\r\n- {Path.GetFileNameWithoutExtension(installedProgram)} => [{installedProgram}]";
        }

        Logger.LogSuccess(installedProgramsList);
        string servicesToDebloat = "Here is a full list of the known services to remove:\r\n";

        foreach (string service in _iobitServices)
        {
            servicesToDebloat += $"\r\n- {service}.exe";
        }

        Logger.LogSuccess(servicesToDebloat);
        Logger.LogWarning("Starting de-bloating process, please wait a while.");

        foreach (string installedProgram in installedIOBitPrograms)
        {
            Logger.LogWarning($"De-bloating {Path.GetFileNameWithoutExtension(installedProgram)} ({installedProgram}), please wait a while.");
            string removedServices = "";
            int removedServicesCount = 0;

            foreach (string service in _iobitServices)
            {
                if (File.Exists($"{installedProgram}\\{service}.exe"))
                {
                    Logger.LogSuccess($"Found the file '{installedProgram}\\{service}.exe', deleting it.");

                    try
                    {
                        File.Delete($"{installedProgram}\\{service}.exe");
                        Logger.LogSuccess($"Succesfully deleted the file '{installedProgram}\\{service}.exe'.");
                        removedServicesCount++;

                        if (removedServices == "")
                        {
                            removedServices = $"{service}.exe"; 
                        }
                        else
                        {
                            removedServices += $", {service}.exe";
                        }
                    }
                    catch
                    {
                        Logger.LogError($"Failed to delete the file '{installedProgram}\\{service}.exe'.");
                    }
                }
                else
                {
                    Logger.LogWarning($"The file '{installedProgram}\\{service}.exe' is not found.");
                }
            }

            if (removedServicesCount == 0)
            {
                Logger.LogError($"Failed to de-bloat {Path.GetFileNameWithoutExtension(installedProgram)} ({installedProgram}), no bad services have been found.");
            }
            else
            {
                Logger.LogSuccess($"Succesfully de-bloated {Path.GetFileNameWithoutExtension(installedProgram)} ({installedProgram}). Services removed: {removedServices} ({removedServicesCount}).");
            }
        }

        Logger.LogWarning("Applying special method for IOBit Driver Booster, please wait a while.");

        if (Directory.Exists($"{mainIOBitDir}\\Driver Booster"))
        {
            string[] dirs = Directory.GetDirectories(mainIOBitDir + "\\Driver Booster");

            if (dirs.Length == 0)
            {
                Logger.LogError("Can not apply special method for IOBit Driver Booster, no installed versions have been found in the folder.");
            }
            else
            {
                string removedServices = "";
                int removedServicesCount = 0;

                foreach (string dir in dirs)
                {
                    foreach (string service in _iobitServices)
                    {
                        if (File.Exists($"{dir}\\{service}.exe"))
                        {
                            try
                            {
                                File.Delete($"{dir}\\{service}.exe");
                                removedServicesCount++;

                                if (removedServices == "")
                                {
                                    removedServices = service;
                                }
                                else
                                {
                                    removedServices += $", {service}";
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }

                if (removedServicesCount == 0)
                {
                    Logger.LogError("Failed to de-bloat IOBit Driver Booster with the special method. No services have been found.");
                }
                else
                {
                    Logger.LogSuccess($"Succesfully de-bloated IOBit Driver Booster with the special method. Removed services: {removedServices} ({removedServicesCount}).");
                }
            }
        }
        else
        {
            Logger.LogError("Can not apply special method for IOBit Driver Booster since it seems not installed.");
        }

        Logger.LogSuccess("De-bloating process succesfully FINISHED. All is OK.");
        Logger.LogWarning("Press ENTER to exit from the program.");
        Console.ReadLine();
    }
}
