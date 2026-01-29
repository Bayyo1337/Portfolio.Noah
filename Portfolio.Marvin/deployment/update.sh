#!/bin/bash

# Configuration
REPO_USER="your-github-username"
REPO_NAME="Portfolio.Marvin"
INSTALL_DIR="/opt/portfolio"
SERVICE_NAME="portfolio"

# Ensure script is run as root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi

echo "Updating Portfolio on Debian..."

# Install dependencies if missing
apt-get update
apt-get install -y curl jq unzip libicu-dev acl

# Create install dir if not exists
mkdir -p $INSTALL_DIR

# Get latest release download URL
LATEST_URL=$(curl -s https://api.github.com/repos/$REPO_USER/$REPO_NAME/releases/latest | jq -r '.assets[] | select(.name=="release.zip") | .browser_download_url')

if [ -z "$LATEST_URL" ] || [ "$LATEST_URL" == "null" ]; then
  echo "Error: Could not find release.zip in latest release."
  exit 1
fi

echo "Downloading from $LATEST_URL..."
curl -L -o /tmp/release.zip "$LATEST_URL"

# Stop service if running
systemctl stop $SERVICE_NAME || true

# Backup config
cp $INSTALL_DIR/portfolio.json /tmp/portfolio.json.bak 2>/dev/null

# Unzip
unzip -o /tmp/release.zip -d $INSTALL_DIR

# Restore config
# mv /tmp/portfolio.json.bak $INSTALL_DIR/portfolio.json

# Fix permissions
# Ensure the user exists
if ! id "portfolio" &>/dev/null; then
    useradd -r -s /bin/false portfolio
fi

chown -R portfolio:portfolio $INSTALL_DIR
chmod +x $INSTALL_DIR/Portfolio.Marvin

# Setup Service if not exists
if [ ! -f /etc/systemd/system/$SERVICE_NAME.service ]; then
    echo "Installing systemd service..."
    cp $INSTALL_DIR/deployment/portfolio.service /etc/systemd/system/$SERVICE_NAME.service
    systemctl daemon-reload
    systemctl enable $SERVICE_NAME
fi

# Restart service
systemctl start $SERVICE_NAME

echo "Update complete! Status:"
systemctl status $SERVICE_NAME --no-pager
