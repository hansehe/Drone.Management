After updating the schema, update the corresponding classes by running:
$ xsd SettingsSchema.xsd /classes /Namespace:Drone.Management.Settings.Interfaces

In general, to create classes from a given XSD schema:
	-> Open Developer Command Prompt for VS
	-> Step into folder containing the xsd file.
	-> Run following command to produce class: xsd <xsd_filename.xsd> /classes /Namespace:<namespace>
	-> example: xsd SettingsSchema.xsd /classes /Namespace:Drone.Management.Settings.Interfaces
