// See https://aka.ms/new-console-template for more information
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xaml;
using wpfjs.HostClass;

namespace wpfjs {

    public static class Program
    {
        public static ExtendedHostFunctions Host;
        [STAThread]
        public static void Main(string[] args)
        {
            bool isDebug = false;
            bool isCommonJS = false;
            string scriptName = string.Empty;
            string searchFolder = string.Empty;
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    if (arg == "-d" || arg == "-debug")
                    {
                        isDebug = true;
                        continue;
                    }
                    if(arg.ToLower() == "commonjs")
                    {
                        isCommonJS = true;
                        continue;
                    }
                    else if (File.Exists(arg))
                    {
                        scriptName = new FileInfo(arg).FullName;
                        searchFolder = new FileInfo(arg).Directory.FullName;
                    }
                }
            }
            if (!File.Exists(scriptName))
            {
                Console.WriteLine("no such script");
            }
            V8ScriptEngineFlags flags = V8ScriptEngineFlags.EnableDateTimeConversion | V8ScriptEngineFlags.EnableTaskPromiseConversion | V8ScriptEngineFlags.EnableValueTaskPromiseConversion | V8ScriptEngineFlags.EnableStringifyEnhancements;
            if (isDebug)
            {
                flags = flags | V8ScriptEngineFlags.EnableDebugging | V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart;
            }
            V8ScriptEngine engine = null;
            if (isDebug)
            {
                engine = new V8ScriptEngine(flags, 9222);
            }
            else
            {
                engine = new V8ScriptEngine(flags);
            }
            engine.EnableAutoHostVariables = true;
            engine.DisableExtensionMethods = false;
            engine.AddHostObject("debug", new DebuggerHost());
            List<Type> types = new List<Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                types.AddRange(assembly.GetTypes().Where(x => x.IsPublic));
            }
            engine.DocumentSettings.SearchPath = searchFolder;
            List<string> conflict = new List<string>() { "Object", "Math" };
            types.RemoveAll(x => conflict.IndexOf(x.Name)>=0);
            engine.AddHostTypes(types.ToArray());
            Host = new ExtendedHostFunctions();
            engine.AddHostObject("host", Host);
            engine.AddHostObject("fetch", ResponseHost.fetch);
            engine.AddHostObject("wpf", new WpfHost());
           
            engine.AddHostTypes(typeof(Window),typeof(XamlReader),typeof(Binding),typeof(RelayCommand),typeof(ObservableObject));
            engine.EnforceAnonymousTypeAccess = true;
            engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableAllLoading;
            engine.EvaluateDocument(scriptName, isCommonJS? ModuleCategory.CommonJS:ModuleCategory.Standard);
            engine.Dispose();
        }
     
    }
}
