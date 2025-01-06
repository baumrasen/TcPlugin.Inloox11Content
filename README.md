# TcPlugin.Inloox11Content
This is a Total Commander Content .wdx plugin for show informations from the Inloox-API

So you can call it a Total Commander Inloox Plugin. It will show the project number, project name, project status and client name (for example) as a column in the folder, where the project data are stored.

## Installation 

Plugin required [**.NET Framework 4.7.2**](https://dotnet.microsoft.com/download/dotnet-framework) or newer installed in Windows.
By default is it pre-installed by system on Windows 10 (1803, April 2018) and newer.

To install the Plugin, using integrated plugin installer, do the following:
 * Download the latest Release [**Inloox11Content.zip**](https://github.com/baumrasen/TcPlugin.Inloox11Content/releases)
 * Use Total Commander to navigate to the zip-file and then hit `ENTER` on it.
 * Wait for the installer promt.
 * Follow the instructions of the installer
 * The settings dialog will be opend
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/inloox11content_settings.png)
	* Add the domain, that will open your inloox
	* Add a valid Access Token (from the Inloox user profile - you will find the url in a textbox in the dialog)
	* After the token was checked and is valid, the checkboxes for that will be checked
	* Change the projectnumber regular expression to match your needs
		* The plugin will use the projectnummer to match the data from the file system to the inloox-api. So you has to do the following things
		* Reset to default will fill a basic regular expression for that (the plugin will use the named field in the regular expression - so don't delete it!)
		* Rill the Test path content with an example from your system, so that you can check the regular expression
		* After the regular expression is valid and get back the named fields (projectnummer is essential), the valid checkbox will be checked.
	*  Remove the checkbox 'open this dialog on every start', if all is working as expected

 * Add some custom columns configuration in the TotalCommander settings
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/custom_columns_config.png)
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/custom_columns_result.png)
* Add some custom view configuration in the TotalCommander settings
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/custom_view_config.png)
	 
Optional:
 * Add some Auto Switch Mode in the TotalCommander settings
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/auto_switch_mode_config1.png)
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/auto_switch_mode_config2.png)
 * Change sorting directories mode to 'Like files (also by time)'
	* ![Image](https://raw.githubusercontent.com/baumrasen/TcPlugin.Inloox11Content/master/images/sorting_like_files.png)
	* with this option, you can sort by the shown inloox columns

More about the **[Total Commander integrated plugin installer](https://www.ghisler.ch/wiki/index.php/Plugin#Installation_using_Total_Commander.27s_integrated_plugin_installer).**

## Inloox

For informations about Inloox 

More infos: [inloox.com Pages](https://inloox.com)

## How to build

It uses the **[TcBuild](https://github.com/ficnar/TcBuild)** nuget package to build a Plugin 
that can be used with Total Commander. The installpackage will be in directory ContentInloox11\bin\Release\out\.

## Enable Trace logging

To enable trace logging copy this file **[Totalcmd.exe.config](https://github.com/r-Larch/TcBuild/blob/master/Totalcmd.exe.config)**
into the directory of **Totalcmd.exe**.
In case you use the 64-bit version of Total Comander then rename the file to **Totalcmd64.exe.config**.
