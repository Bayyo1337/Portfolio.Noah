#!/bin/bash

# Configuration
REPO_USER="NoahRichter"
REPO_NAME="Portfolio.Noah"
INSTALL_DIR="/opt/portfolio"
SERVICE_NAME="portfolio"

# Ensure script is run as root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi

echo "Checking for updates..."

# Get latest release download URL for release.zip
LATEST_DATA=$(curl -s https://api.github.com/repos/$REPO_USER/$REPO_NAME/releases/latest)
LATEST_URL=$(echo "$LATEST_DATA" | jq -r '.assets[] | select(.name=="release.zip") | .browser_download_url')
TAG_NAME=$(echo "$LATEST_DATA" | jq -r '.tag_name')

if [ -z "$LATEST_URL" ] || [ "$LATEST_URL" == "null" ]; then
  echo "Error: Could not find release.zip in latest release."
  exit 1
fi

echo "Found release $TAG_NAME. Downloading..."
curl -L -o /tmp/release.zip "$LATEST_URL"

# Stop service
systemctl stop $SERVICE_NAME || true

# Backup config
cp $INSTALL_DIR/portfolio.json /tmp/portfolio.json.bak 2>/dev/null

# Unzip
unzip -o /tmp/release.zip -d $INSTALL_DIR

# Restore config
if [ -f /tmp/portfolio.json.bak ]; then
    mv /tmp/portfolio.json.bak $INSTALL_DIR/portfolio.json
fi

# Fix permissions
if id "portfolio" &>/dev/null; then
    chown -R portfolio:portfolio $INSTALL_DIR
fi

chmod +x $INSTALL_DIR/Portfolio.Noah
# Ensure deployment scripts are executable
chmod +x $INSTALL_DIR/deployment/*.sh 2>/dev/null || true

# Update the updater script itself if it changed (and we are running from a copy in root)
if [ -f "$INSTALL_DIR/deployment/update.sh" ] && [ -f "$INSTALL_DIR/update.sh" ]; then
    cp "$INSTALL_DIR/deployment/update.sh" "$INSTALL_DIR/update.sh"
    chmod +x "$INSTALL_DIR/update.sh"
fi

# Restart service
systemctl start $SERVICE_NAME

echo "Update complete! Status:"
systemctl status $SERVICE_NAME --no-pager
