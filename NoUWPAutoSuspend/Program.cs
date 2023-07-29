using System;
using System.Threading;
using Windows.System;
using System.Collections.Generic;
using System.Linq;

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

                if (appResourceGroupInfo.GetStateReport().ExecutionState == AppResourceGroupExecutionState.Suspended)
                    await appResourceGroupInfo.StartResumeAsync();

                appResourceGroupInfoWatcher.ExecutionStateChanged += async (sender2, e2) =>
                {
                    AppResourceGroupExecutionState appResourceGroupExecutionState = e2.AppResourceGroupInfo.GetStateReport().ExecutionState;
                    if (appResourceGroupExecutionState == AppResourceGroupExecutionState.Suspended ||
                    appResourceGroupExecutionState == AppResourceGroupExecutionState.Suspending)
                        await e2.AppResourceGroupInfo.StartResumeAsync();
                };
                appResourceGroupInfoWatcher.Start();
            }
        };
        appDiagnosticInfoWatcher.Start();
        Thread.Sleep(Timeout.Infinite);
    }
}
