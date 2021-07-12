# Unofficial
I want to make it very clear that this is not an official Marlin Firmware software. It is not in any way, shape or form supported nor endorsed by Marlin Firmware.

# Marlin LCD Screen Editor
This is a software to aid in the designing of custom bootscreens and perhaps in the future also statusscreens for [Marlin](https://github.com/MarlinFirmware/Marlin). It is a rather early proof of concept, so a bunch of features are still missing or not yet fully implemented.

# Current Features
Currently only supports the default Ender 3 LCD which is 88x58 pixels. It should be rather trivial to implement support for other screens, though.

• __WYSIWYG Editor__ - pixels are divided by an offset to make drawing a bit easier. You can also do a quick preview _(kinda bad atm)_.  
• __Code Generator__ - Generates the code for you, simply replace the default code.  
• __Share Designs__ - _Currently_ uses a rather basic binary-based string to share designs, you can load and share this string with other people. This will be optimized later.  

# Planned Features
• Projects - Save a bunch of various designs and easily load them for editing later.  
• Multiple Screens - I want to support more than just the default Ender 3 screen.  
• More...?

Again I want to re-iterate that this is a work in progress. It started as a personal project to help me design my own custom screen instead of fiddling with photoshop and converting it online - then I decided to clean things up and publish it in case other people may find it useful.

As such I can't really guarantee that I'll continue working on this in the future. But at the very least I want to get the planned features implemented and working.

As always, any and all feedback is welcome. Let me know if you have any suggestions or whatnot and of course feel free to contribute! :heart:

![Screenshot](https://i.imgur.com/N09KGce.png)

# Credits
• [MahApps](https://mahapps.com) - Using their Metro UI Framework as always 💙  
• [protobuf-net](https://github.com/protobuf-net/protobuf-net) - Makes serializing/deserializing .NET objects super ez 💚
