# HackThePlanet Server Host
This is a host service (daemon) for running the game server. When
building and running the game, this is the project you should use.

## Service/Daemon Installation
The install-service.sh script in the parent directory can be used
to install the service definition to the correct location on a Linux 
environment. The service definition is based on the assumption systemctl 
will be used to run the daemon. The service definition can be found in 
the parent folder (hacktheplanet.service).

## Publishing Server Changes
The publish.sh script in the parent directory can be used to rebuild 
and then push the updated binaries to appropriate location
(referenced in the service definition).  