# CovToJscript
Covenant C2 specific version of med0x2e GadgetToJScript

All the heavy lifting done by med0x2e, n1xbyte -> i simply glued some things together.

Whilst the resulting output file should not trigger AV, it is recomended that the compilation/generation is run on a dev system without AV running.

## Description
A tool for interfacing with Covenant API to obtain Grunt Launcher and subsiquently generate .NET serialized gadgets that can trigger .NET assembly load/execution when deserialized using BinaryFormatter from JS/VBS based scripts.
<br>The gadget being used triggers a call to Assembly.Load when deserialized via jscript/vbscript, this means it can be used in the same way to trigger in-memory load of your own shellcode loader at runtime.

## Usage:
  ``` 
  -c, --ConvantURL=VALUE     https://127.0.0.1:7443
  -u, --Username=VALUE       Covenant username
  -p, --Password=VALUE       Covenant Password
  -w, --scriptType=VALUE     js, vbs, vba or hta
  -e, --encodeType=VALUE     VBA gadgets encoding: b64 or hex (default set to
                               b64)
  -o, --output=VALUE         Generated payload output file, example:
                               C:\Users\userX\Desktop\output (Without extension)
  -r, --regfree              registration-free activation of .NET based COM
                              components
  -h, --help=VALUE           Show Help
  ```
  
