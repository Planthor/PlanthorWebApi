#!/bin/bash

# Setting up commands from starting devcontainer
dotnet restore

# Ensure Copilot memory directory exists and has proper permissions
mkdir -p /home/vscode/.local/share/code/user
chmod 755 /home/vscode/.local/share/code/user

# Create symlink for VS Code to find settings in standard Linux location
if [ ! -d /home/vscode/.config/Code ]; then
  mkdir -p /home/vscode/.config
  ln -s /home/vscode/.local/share/code/user /home/vscode/.config/Code
fi

echo "✓ Copilot memory mounted and configured at /home/vscode/.local/share/code/user"
