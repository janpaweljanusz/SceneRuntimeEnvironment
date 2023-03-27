using Microsoft.ClearScript.V8;
using Utils;

using (var engine = new V8ScriptEngine())
{
    engine.AddHostType("Console", typeof(Console));
    var globalObjects = new Globals.GlobalObjects(engine);
    var moduleInstance = new Globals.Module(engine);

    engine.Script.originalRequire = (Func<string, dynamic>)moduleInstance.Require;
    engine.Script.module = moduleInstance;

    var jsFileReader = new EmbeddedFileReader();
    var sandboxedRuntimeFile = jsFileReader.ReadFromEmbeddedFile("SceneRuntimeEnvironment.js.sandboxedRuntime.js");
    engine.Execute(sandboxedRuntimeFile);
    var sandboxedRuntimeWrapperFile = jsFileReader.ReadFromEmbeddedFile("SceneRuntimeEnvironment.js.sandboxedRuntimeWrapper.js");
    engine.Execute(sandboxedRuntimeWrapperFile);

    engine.Script.onStart();
    for (int i = 1; i <= 3; i++)
    {
        engine.Script.onUpdate(i);
    }
}