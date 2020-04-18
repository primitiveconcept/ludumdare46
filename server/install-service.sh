cp ./hacktheplanet.service /etc/systemd/system/hacktheplanet.service
systemctl daemon-reload  
systemctl enable hacktheplanet.service  
systemctl start hacktheplanet.service