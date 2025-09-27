# update-debian
* update-debian is a script that automates the update process on Debian Linux systems

## How to install
* Run `make install` to install the scripts

## How to use the scripts
* `check-updates` - checks for available updates.
* `upgrade` - upgrades the current version system packages.
* `check-backports` - checks for updates in the backports repository, install them via `sudo apt install -t yourversion-backports your-package`

## Future plans - add a command to install packages from backports