#!/bin/bash

#!/bin/bash

# Path to the install.sh script
INSTALL_SCRIPT_PATH="./development/install.sh"

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo "dotnet is not installed. Running install.sh to install .NET Core SDK."
    if [ -f "$INSTALL_SCRIPT_PATH" ]; then
        bash "$INSTALL_SCRIPT_PATH"
        if [ $? -ne 0 ]; then
            echo "Failed to install .NET Core SDK."
            exit 1
        fi
    else
        echo "install.sh script not found at $INSTALL_SCRIPT_PATH."
        exit 1
    fi
fi

# Check if dotnet-ef tool is installed
if ! command -v dotnet ef --help &> /dev/null; then
    echo "dotnet ef is not installed. Installing now..."
    dotnet tool install --global dotnet-ef
    if [ $? -ne 0 ]; then
        echo "Failed to install dotnet ef."
        exit 1
    fi
fi

echo "dotnet ef is installed."
export DOTNET_ROOT=/usr/share/dotnet

# Check if the first argument is empty
if [ -z "$1" ]; then
    echo "Error: Connection string is required as the first argument."
    exit 1  # Exit with status 1 to indicate an error
fi

connectionString=$1

# Execute the dotnet command
dotnet ef database update --project ./src/Infrastructure/Infrastructure.csproj --startup-project ./src/Web/Web.csproj --connection "$connectionString"

echo ////////////////////////
echo     Migrations done
echo ////////////////////////
