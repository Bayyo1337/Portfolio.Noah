#!/bin/bash
set -e

# Configuration
INSTALL_DIR="/opt/portfolio"
USER="portfolio"

# Check root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi

echo "Installing dependencies..."
apt-get update
apt-get install -y curl jq unzip libicu-dev acl

# Create user
if ! id "$USER" &>/dev/null; then
    echo "Creating user $USER..."
    useradd -r -s /bin/false $USER
fi

# Create directory
mkdir -p $INSTALL_DIR
chown $USER:$USER $INSTALL_DIR

# Install Cloudflared
if ! command -v cloudflared &> /dev/null; then
    echo "Installing Cloudflared..."
    # Add Cloudflare repo key and source
    mkdir -p --mode=0755 /usr/share/keyrings
    curl -fsSL https://pkg.cloudflare.com/cloudflare-main.gpg | tee /usr/share/keyrings/cloudflare-main.gpg >/dev/null
    echo 'deb [signed-by=/usr/share/keyrings/cloudflare-main.gpg] https://pkg.cloudflare.com/cloudflared trixie main' | tee /etc/apt/sources.list.d/cloudflared.list
    
    apt-get update
    apt-get install -y cloudflared
fi

# Install Service
echo "Installing portfolio service..."
cp deployment/portfolio.service /etc/systemd/system/
systemctl daemon-reload
systemctl enable portfolio.service

# Install Updater
echo "Installing updater service..."
cp deployment/update.sh $INSTALL_DIR/update.sh
chmod +x $INSTALL_DIR/update.sh
cp deployment/updater.service /etc/systemd/system/
cp deployment/updater.timer /etc/systemd/system/
systemctl daemon-reload
systemctl enable updater.timer
systemctl start updater.timer

echo "Installation complete."
echo "--------------------------------------------------------"
echo "Next steps:"
echo "1. Configure Cloudflare Tunnel:"
echo "   cloudflared tunnel login"
echo "   cloudflared tunnel create portfolio"
echo "   cloudflared tunnel route dns portfolio <hostname>"
echo "   cloudflared service install <token>"
echo ""
echo "2. Initial Deployment:"
echo "   Run '/opt/portfolio/update.sh' to download the latest release."
echo "   Or copy your release.zip content to /opt/portfolio/ manually."
echo "--------------------------------------------------------"
