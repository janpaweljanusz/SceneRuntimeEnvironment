# Decentraland Scene Runtime Environment
C# .net 6 implementations of Decentraland scene runtime environment

The program embeds a JavaScript runtime basic functions to execute them in a sandboxed environment written in C# .net console application.
It provides access to functionality outside the sandbox with a bridge between JavaScript functions and C# environment. 
It is a simplified version of a minimal Decentraland scene runtime.


# Features

## Basic functionality

1. It loads a scene file written in JavaScript to a sandboxed environment.
2. Sandboxed environment provides a number of global objects:
* EngineModule sends data outside the environment in order to log it.
* Console immediately writes text to stdout.
3. On launch, the application calls and awaits the module.exports.onStart() method (simulating initialization).
4. On launch the application calls and awaits the module.exports.onUpdate(deltaTime) method three times, with the deltaTime sequence 1, 2, 3 (simulating 1 tick per second).
5. It handles messages sent from inside the sandbox during the simulation.

# Usage
run from the root of your project:
``` $ ./bin/run ```

## Requirements 
The project run in a Windows 10 shell environment with dotnet and .NET 6 installed.

# Technical description - decision process

The main challenge was to ensure asynchronous communication between a JavaScript engine and a .net 6 console application.
the following libraries were considered for the implementation of JavaScript in .net 6 environment:

* [Jering.Javascript.NodeJS](https://github.com/JeringTech/Javascript.NodeJS)
* [ChakraCore](https://github.com/chakra-core/ChakraCore)
* [Microsoft ClearScript.V8](https://github.com/Microsoft/ClearScript)
* [Jint](https://github.com/sebastienros/jint)
* [Jurassic](https://github.com/paulbartrum/jurassic)
* [MSIE JavaScript Engine for .NET](https://github.com/Taritsyn/MsieJavaScriptEngine)
* [NiL.JS](https://github.com/nilproject/NiL.JS)
* [VroomJs](https://github.com/pauldotknopf/vroomjs-core)

In addition, a following library was considered [JavaScriptEngineSwitcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher)

Finally, [Microsoft ClearScript.V8](https://github.com/Microsoft/ClearScript) was selected due to simplicity, support and rich range of functions.

At the same time, none of these libraries provided asynchronous calls of functions in JavaScript. Its implementation was the main task of the Challange.

# License
[Apache License Version 2.0](http://github.com/Taritsyn/JavaScriptEngineSwitcher/blob/master/LICENSE.txt)