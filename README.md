# ClientModSendCommand
Class that allows to send custom command to the clientmod

## Usage
1. Add reference to your project
2. Try to do something like:
```
if (ClientModSendCommand.ClientMod.SendCommandToWindow("say hello world") > 0)
{
    Console.WriteLine("Success");
}
```
