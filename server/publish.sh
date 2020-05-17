sudo systemctl stop hacktheplanet.service
sudo cp ./hacktheplanet.service /etc/systemd/system/hacktheplanet.service
sudo systemctl daemon-reload
dotnet publish HackThePlanet.Host --configuration Release --output /usr/local/sbin/hacktheplanet
sudo systemctl start hacktheplanet.service
