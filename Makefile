make:
	@echo 'Use make install command to install the scripts and make uninstall to remove it'

install:
	@echo 'Installing the scripts to /usr/bin'
	@sudo cp ./update-debian /usr/bin
	@sudo cp ./check-debian-backports /usr/bin
	@echo 'Done, run update-debian or check-debian-backports from any location.'

uninstall:
	@echo 'Removing the script from /usr/bin'
	@sudo rm /usr/bin/update-debian
	@sudo rm /usr/bin/check-debian-backports
	@echo 'Done, update-debian and check-debian-backports were removed from your system.'