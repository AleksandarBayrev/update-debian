make:
	@echo 'Compiling the project...'
	@dotnet publish -c Release -r linux-x64 --property:PublishDir=./out
	@echo 'Done, the compiled files are in the out/ directory.'
	@echo 'Run "make install" to install the scripts to /usr/bin'

install:
	@echo 'Installing the scripts to /usr/bin'
	@sudo cp ./out/update-debian /usr/bin
	@echo 'Done, run update-debian or check-debian-backports from any location.'

uninstall:
	@echo 'Removing the script from /usr/bin'
	@sudo rm /usr/bin/update-debian
	@sudo rm /usr/bin/check-debian-backports
	@echo 'Done, update-debian and check-debian-backports were removed from your system.'