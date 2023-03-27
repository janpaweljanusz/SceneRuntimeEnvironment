using Microsoft.ClearScript.V8;
using Utils;
using ExtensionMethods;

class Program
{
    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }
    /// <summary>
    /// Handling async functions require using Task and waiting for till it is completed 
    /// </summary>
    static async Task MainAsync(string[] args)
    {
        using (var engine = new V8ScriptEngine())
        {
            // passing classes to JS engine
            engine.AddHostType("Console", typeof(Console));
            engine.AddHostType(typeof(TaskToJSPromise));
            engine.AddHostType(typeof(Task));

            // instantiating global objects (c#/js bridge)
            var globalObjects = new Globals.GlobalObjects(engine);
            var moduleInstance = new Globals.Module(engine);

            // js file reader instantiation
            var jsFileReader = new EmbeddedFileReader();

            // reading JS files
            engine.Execute(jsFileReader.ReadFromEmbeddedFile("SceneRuntimeEnvironment.js.sandboxedRuntime.js"));
            engine.Execute(jsFileReader.ReadFromEmbeddedFile("SceneRuntimeEnvironment.js.sandboxedRuntimeWrapper.js"));

            // actuall challange task execution. App will wait till async will end.
            var jsAsyncToTask = new JSAsyncToTask();
            await jsAsyncToTask.ToTask(engine.Script.onStartWrapper);
            for (int i = 1; i <= 3; i++)
            {
                await jsAsyncToTask.ToTask(engine.Script.onUpdateWrapper, i);
            }
        }
    }
}
