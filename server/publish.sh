dotnet publish HackThePlanet.Host --configuration Release --output /usr/local/sbin/hacktheplanet
systemctl stop hacktheplanet.service
cp ./hacktheplanet.service /etc/systemd/system/hacktheplanet.service
systemctl daemon-reload
systemctl start hacktheplanet.service