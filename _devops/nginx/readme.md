# Install 
```sh
sudo apt install nginx --yes     # Install
sudo systemctl stop nginx        # Stop
sudo systemctl disable nginx     # Disable on boo

sudo systemctl start nginx -c nginx.conf      # Start
sudo systemctl restart nginx     # Restart
sudo systemctl reload nginx      # Reload config

sudo nginx -s stop
sudo nginx -c "$(pwd)/nginx.conf"
```
