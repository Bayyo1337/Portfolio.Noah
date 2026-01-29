#!/bin/bash

# Cloudflare Tunnel Installation Helper
# Run this on your LXC container

if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi

# Install cloudflared
curl -L --output cloudflared.deb https://github.com/cloudflare/cloudflared/releases/latest/download/cloudflared-linux-amd64.deb
dpkg -i cloudflared.deb
rm cloudflared.deb

echo "Cloudflared installed."
echo "Now run: cloudflared tunnel login"
echo "Follow instructions to link your domain."
echo "Then create a tunnel: cloudflared tunnel create portfolio"
echo "Configure it to point to http://localhost:8080"
echo "Finally install as service: cloudflared service install <token>"
