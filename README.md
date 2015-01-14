# File Explorer NGUI v0.5.1
<br>

## What is it?
File Explorer NGUI is file explorer library for creating customized window to manage files in Unity3d at runtime. 
It is built using NGUI. Its [Unity3d new UI version] (https://github.com/f15gdsy/FileExplorer.git) is also available.


#### Features
1. Provides file exploration window in different styles (only [OS X list style] (https://dl.dropboxusercontent.com/u/27907965/images/Screen%20Shot%202015-01-08%20at%20%E4%B8%8B%E5%8D%883.18.56.png) currently, more styles are planning to come).
2. Customizable for UI interaction.
  
<br><br>

## How to use?
#### 1. Open a list style File Explorer window:

```csharp
using FileExplorerNGUI.Ex;
//...
public void OpenFileExplorer () {
  WindowControllerNGUI controller = new WindowControllerNGUI();     // Basic window controller provided in the library
  WindowControllerNGUIEx.Open(controller, WindowStyle.List);
}
```
<br>

#### 2. Customize UI interaction

Create a class inherits from WindowController.

```csharp
// WindowControllerNGUI.cs

public class WindowControllerNGUI {
  //...
  
  // Override these functions to customize.
		public virtual void OnStart () {}
		public virtual void OnFileHighlighted (string path) {}
		protected virtual void OnCancelButtonPressed () {
			FileExplorerNGUIEx.Close();
		}
		protected virtual void OnOtherButtonPressed () {}
}
```
