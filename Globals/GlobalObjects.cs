using Microsoft.ClearScript.V8;

namespace Globals
{
    public class GlobalObjects
    {
        public class EngineModule
        {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable CA1822 // Mark members as static
            public async void sendMessage(string message)
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                System.Console.WriteLine("EngineModule: sendMessage: " + message);
            }
        }
        /// <summary>
        /// Brings console.log functionality to JS contect
        /// </summary>
        public class Console
        {
            public void log(string text)
            {
                System.Console.WriteLine(text);
            }
            public void log(object text)
            {
                System.Console.WriteLine(text);
            }
        }

        private EngineModule _engineModuleInstance;
        /// <summary>
        /// Constructor handles adding global objects and methods to a JS engine instance
        /// </summary>
        /// <param name="engine"></param>
        public GlobalObjects(V8ScriptEngine engine)
        {
            _engineModuleInstance = new EngineModule();
            engine.Script.console = new GlobalObjects.Console();
            engine.Script.require = (Func<string, EngineModule>)require;
        }
        /// <summary>
        /// This method is used to bring EngineModule global instance to JS scene context.
        /// </summary>
        /// <param name="moduleName">is a key for gettings certain module</param>
        /// <returns>EngineModule global object instance </returns>
        /// <exception cref="Exception"></exception>
        private EngineModule require(string moduleName)
        {
            if (moduleName != "~engine")
            {
                throw new Exception("Unknown module");
            }

            return _engineModuleInstance;
        }

    }
}
