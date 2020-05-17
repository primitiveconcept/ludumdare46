dotnet publish HackThePlanet.Host --configuration Release --output /usr/local/sbin/hacktheplanet
sudo cp ./hacktheplanet.service /etc/systemd/system/hacktheplanet.service
sudo systemctl daemon-reload
sudo systemctl restart hacktheplanet.service