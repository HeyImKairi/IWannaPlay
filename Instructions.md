# Installation

## Windows

* You need to download the [latest BepInEx 5 release here](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)
  * as the BepInEx 6 won't work with this mod.
  * (I didn't test it with BepInEx 6)
* Download the version corresponding to your machine
  * (I use Windows so this hasn't been tested on MacOS/Linux/Steamdeck)
* Extract the BepInEx folder in the Minishoot folder next to Minishoot.exe ("C:\Program Files (x86)\Steam\steamapps\common\Minishoot' Adventures\Windows")

- **LAUNCH THE GAME** so it allows BepInEx to create all the necessary files then you can close the game

- **THEN** Extract the folder "IWannaPlay" into the bepinex\plugins

## Steamdeck
* For Steamdeck users it's the same as Windows except you have to add this line in Minishoot's launch option in Steam: 
  * WINEDLLOVERRIDES="winhttp=n,b" %command% 
