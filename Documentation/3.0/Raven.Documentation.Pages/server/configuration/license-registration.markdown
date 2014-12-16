# Configuration : License registration

In order to register an instance of RavenDB with a license, you can: 

- rename the license file to License.xml and put it in the bin folder where RavenDB executable ,exists
- use the following configuration options:

	*	 **Raven/License**
	The full license string for RavenDB. If Raven/License is specified, it overrides the Raven/LicensePath configuration.

	*	 **Raven/LicensePath**
	The path to the license file for RavenDB.   
	_Default:_ ~\license.xml

{NOTE Each instance of RavenDB outside of the development machines has to be registered with a license. /}

{DANGER To explicitly allow **unrestricted access to server** (`Raven/AnonymousAccess` set to `Admin`) when license **is** registered, set `Raven/Licensing/AllowAdminAnonymousAccessForCommercialUse` to **true**.  /}