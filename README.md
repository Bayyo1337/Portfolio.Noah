# Noah Richter - Portfolio

A specialized Blazor Server portfolio application designed for self-hosting on Linux (specifically Proxmox LXC) with integrated 3D printing and HomeAssistant features.

## 🚀 Features

- **Personal Branding**: Customizable profile, experience, and projects.
- **Terminal UI**: Interactive terminal-style landing page.
- **Blog System**: Markdown-based blogging engine.
- **3D Viewer**: Integrated STL viewer for showcasing 3D models.
- **HomeAssistant**: Real-time sensor dashboard.
- **Self-Hosting Friendly**: Built for Linux systemd, includes auto-updater and Cloudflare Tunnel support.

## 📝 Content Management

The content is split between static configuration and dynamic markdown files.

### 1. General Configuration (`portfolio.json`)
Located in the root of the application folder. This file controls:
- **General Info**: Name, Job Title, Description, Social Links.
- **Feature Toggles**: Enable/Disable sections.
- **Experiences / Projects**: Arrays of data to display on the main page.
- **HomeAssistant**: Base URL and Token configuration.

**Example `SectionSettings` to disable features:**
```json
"SectionSettings": {
  "ShowTerminal": true,
  "ShowExperiences": true,
  "ShowProjects": true,
  "ShowContact": true,
  "ShowBlogs": false,      // Set to false to hide the Blog section
  "ShowHomeAssistant": true
}
```

### 2. Blog Posts
Blog posts are stored in `wwwroot/blogs-pages/`. Each post must be in its own subdirectory.

**Folder Structure:**
```
/wwwroot/blogs-pages/
  └── /my-awesome-post/
      ├── meta.json      (Metadata)
      ├── content.md     (The blog post content in Markdown)
      └── image.webp     (Optional assets)
```

**`meta.json` Format:**
```json
{
  "Title": "My Awesome Post",
  "Description": "Short summary...",
  "Date": "2023-10-27T00:00:00",
  "RelativeUrl": "my-awesome-post", 
  "Image": "image.webp",
  "ModelUrl": "",         // Optional: URL to .stl file to display
  "Tags": ["3D Printing", "Linux"],
  "SkillIds": ["Linux", "3DPrint"] // Must match IDs in portfolio.json Skills
}
```

## 🛠 Deployment

This project is designed to run in a Debian 13 LXC container on Proxmox, exposed via Cloudflare Tunnel.

### Prerequisites
- Debian 13 (or compatible)
- Root access
- GitHub Repository with Releases enabled

### Installation
1.  **Clone/Copy** this repository (or just the `deployment/` folder) to your LXC.
2.  **Run the Installer**:
    ```bash
    chmod +x deployment/install.sh
    sudo ./deployment/install.sh
    ```
    This script will:
    - Install dependencies (`curl`, `jq`, `unzip`, `libicu-dev`, `cloudflared`).
    - Create a `portfolio` user.
    - Setup systemd services (`portfolio.service` and `updater.timer`).
3.  **Configure Cloudflare Tunnel**:
    Follow the output instructions to login and link your tunnel.

### Auto-Update
The system includes an auto-updater (`deployment/update.sh`) that checks the GitHub Repository for the latest `release.zip`.
- **Automatic**: Runs daily via `updater.timer`.
- **Manual**: Run `/opt/portfolio/update.sh` as root.

**Note:** The updater expects a `release.zip` asset in the latest GitHub Release, containing the `Portfolio.Noah` executable and dependencies.

## 💻 Development

### Prerequisites
- .NET 10.0 SDK

### Build & Run
```bash
dotnet build
dotnet run --project Portfolio.Noah
```

### Publishing for Deployment
To create the `release.zip` expected by the updater, you simply need to create a git tag starting with `v` (e.g. `v1.0.0`) and push it. GitHub Actions will handle the rest.

1.  **Commit your changes** locally.
2.  **Tag the commit**:
    ```bash
    git tag v1.0.0
    ```
3.  **Push the tag**:
    ```bash
    git push origin v1.0.0
    ```

The GitHub Action will automatically:
- Build the project for Linux x64 (Self-Contained).
- Zip the output.
- Create a new Release on GitHub with `release.zip` attached.
- Your LXC container will verify and install this update automatically (within 24h) or manually via `update.sh`.
