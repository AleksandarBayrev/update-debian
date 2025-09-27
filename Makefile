make:
	@echo 'Compiling the project...'
	@git clean -fxd
	@dotnet clean
	@dotnet publish -c Release -r linux-x64 --property:PublishDir=./out
	@echo 'Done, the compiled files are in the out/ directory.'
	@echo 'Run "make install" to install the program to /usr/bin'

install:
	@echo 'Installing the program to /usr/bin'
	@sudo cp ./out/update-debian /usr/bin
	@echo 'Done, run update-debian from any location.'

uninstall:
	@echo 'Removing the program from /usr/bin'
	@sudo rm /usr/bin/update-debian
	@echo 'Done, update-debian was removed from your system.'