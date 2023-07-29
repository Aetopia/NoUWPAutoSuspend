using System;
using System.Threading;
using Windows.System;

class Program
{
    public static void Main(string[] args)
    {
        AppDiagnosticInfoWatcher appDiagnosticInfoWatcher = AppDiagnosticInfo.CreateWatcher();
        appDiagnosticInfoWatcher.Added += async (sender1, e1) =>
        {
            if (Array.Exists(args, element => element == e1.AppDiagnosticInfo.AppInfo.PackageFamilyName))
            {
                AppResourceGroupInfo appResourceGroupInfo = e1.AppDiagnosticInfo.GetResourceGroups()[0];
                AppResourceGroupInfoWatcher appResourceGroupInfoWatcher = e1.AppDiagnosticInfo.CreateResourceGroupWatcher();
                appResourceGroupInfoWatcher.ExecutionStateChanged += async (sender2, e2) =>
                                {
                                    if (e2.AppResourceGroupInfo.GetStateReport().ExecutionState ==
                                    AppResourceGroupExecutionState.Suspending)
                                        await e2.AppResourceGroupInfo.StartResumeAsync();
                                };
                if (appResourceGroupInfo.GetStateReport().ExecutionState == AppResourceGroupExecutionState.Suspended)
                    await appResourceGroupInfo.StartResumeAsync();
                appResourceGroupInfoWatcher.Start();
            }
        };
        appDiagnosticInfoWatcher.Start();
        Thread.Sleep(Timeout.Infinite);
    }
}
