# Config HOWTO
* Append following optional arguments after typing dotnet <assembly> to configure the application:
	* settings=<settings_file_path>
	* Or, use environment variables to set the settings path:
		- SETTINGS=<settings_file_path>
	* Or, use the default settings following this project. Selecting a default setting is given through environment variables:
		* Options are (POSTGRESQL is set by default):
			* DEFAULT_SETTINGS=POSTGRESQL